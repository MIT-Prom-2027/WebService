using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnivManager.Context;
using UnivManager.Dtos;
using UnivManager.Models;

namespace UnivManager.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReleveController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReleveController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère le relevé de notes d'un bachelier par son numéro de candidat
        /// </summary>
        /// <param name="numeroCandidat">Numéro de candidat du bachelier</param>
        /// <returns>Relevé de notes complet</returns>
        [HttpGet("par-numero/{numeroCandidat}")]
        public async Task<ActionResult<ReleveResponse>> GetReleveParNumero(string numeroCandidat)
        {
            var bachelier = await _context.bacheliers
                .Include(b => b.id_personneNavigation)
                .Include(b => b.id_centreNavigation)
                .Include(b => b.id_optionNavigation)
                .Include(b => b.id_mentionNavigation)
                .Include(b => b.notes)
                    .ThenInclude(n => n.id_matiereNavigation)
                .FirstOrDefaultAsync(b => b.numero_candidat == numeroCandidat);

            if (bachelier == null)
            {
                return NotFound($"Aucun bachelier trouvé avec le numéro de candidat : {numeroCandidat}");
            }

            var releve = await ConstruireReleve(bachelier);
            return Ok(releve);
        }

        /// <summary>
        /// Récupère le relevé de notes d'un bachelier par son ID
        /// </summary>
        /// <param name="id">ID du bachelier</param>
        /// <returns>Relevé de notes complet</returns>
        [HttpGet("par-id/{id}")]
        public async Task<ActionResult<ReleveResponse>> GetReleveParId(int id)
        {
            var bachelier = await _context.bacheliers
                .Include(b => b.id_personneNavigation)
                .Include(b => b.id_centreNavigation)
                .Include(b => b.id_optionNavigation)
                .Include(b => b.id_mentionNavigation)
                .Include(b => b.notes)
                    .ThenInclude(n => n.id_matiereNavigation)
                .FirstOrDefaultAsync(b => b.id_bachelier == id);

            if (bachelier == null)
            {
                return NotFound($"Aucun bachelier trouvé avec l'ID : {id}");
            }

            var releve = await ConstruireReleve(bachelier);
            return Ok(releve);
        }

        /// <summary>
        /// Récupère tous les relevés de notes pour une année donnée
        /// </summary>
        /// <param name="annee">Année du baccalauréat</param>
        /// <returns>Liste des relevés de notes</returns>
        [HttpGet("par-annee/{annee}")]
        public async Task<ActionResult<List<ReleveResponse>>> GetRelevesParAnnee(DateOnly annee)
        {
            var bacheliers = await _context.bacheliers
                .Include(b => b.id_personneNavigation)
                .Include(b => b.id_centreNavigation)
                .Include(b => b.id_optionNavigation)
                .Include(b => b.id_mentionNavigation)
                .Include(b => b.notes)
                    .ThenInclude(n => n.id_matiereNavigation)
                .Where(b => b.annee == annee)
                .ToListAsync();

            if (!bacheliers.Any())
            {
                return NotFound($"Aucun bachelier trouvé pour l'année : {annee}");
            }

            var releves = new List<ReleveResponse>();
            foreach (var bachelier in bacheliers)
            {
                var releve = await ConstruireReleve(bachelier);
                releves.Add(releve);
            }

            return Ok(releves);
        }

        /// <summary>
        /// Construit un objet ReleveResponse à partir d'un bachelier
        /// </summary>
        private async Task<ReleveResponse> ConstruireReleve(bachelier bachelier)
        {
            var releve = new ReleveResponse
            {
                NomPrenom = bachelier.id_personneNavigation?.nom_prenom ?? "Non renseigné",
                DateNaissance = bachelier.id_personneNavigation?.date_naissance ?? DateOnly.MinValue,
                LieuNaissance = bachelier.id_personneNavigation?.lieu_naissance ?? "Non renseigné",
                AnneeBacc = bachelier.annee,
                NumeroBacc = bachelier.numero_candidat,
                SerieBacc = bachelier.id_optionNavigation?.serie ?? "Non renseigné",
                CentreBacc = bachelier.id_centreNavigation?.nom_centre ?? "Non renseigné",
                MoyenneBacc = bachelier.moyenne,
                Mention = bachelier.id_mentionNavigation?.nom_mention ?? "Non renseigné"
            };

            // Calcul des notes et coefficients
            double totalNotes = 0;
            double totalCoefficients = 0;

            foreach (var note in bachelier.notes)
            {
                // Coefficient par défaut de 1 si non spécifié
                double coefficient = 1.0; // Vous pouvez ajouter un champ coefficient dans le modèle note si nécessaire
                
                var noteDetail = new NoteDetail
                {
                    Matiere = note.id_matiereNavigation?.nom_matiere ?? "Matière inconnue",
                    Note = note.valeur_note,
                    Coefficient = coefficient,
                    EstOptionnel = note.est_optionnel ?? false,
                    NotePondere = note.valeur_note * coefficient
                };

                releve.Notes.Add(noteDetail);
                totalNotes += noteDetail.NotePondere;
                totalCoefficients += coefficient;
            }

            releve.TotalNotes = totalNotes;
            releve.TotalCoefficients = totalCoefficients;
            
            // Recalcul de la moyenne si nécessaire
            if (totalCoefficients > 0)
            {
                releve.MoyenneBacc = totalNotes / totalCoefficients;
            }

            // Détermination de l'admission (moyenne >= 10)
            releve.EstAdmis = releve.MoyenneBacc >= 10.0;

            return releve;
        }
    }
}
