using Microsoft.AspNetCore.Mvc;
using UnivManager.Dtos;
using UnivManager.Context;

namespace UnivManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReleveController : ControllerBase
    {
        public readonly AppDbContext _context;
        public ReleveController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("getDataByNumBacc")]
        public ActionResult<ReleveResponse> Post([FromBody] ReleveRequest data)
        {
            if (data == null || data.Num_bacc == 0 || data.Annee == 0)
            {
                return BadRequest("Données de requête invalides.");
            }

            var bachelier = _context.Bacheliers.Where(b => b.NumeroCandidat == data.Num_bacc.ToString() &&  b.Annee.Year.ToString() == data.Annee_bacc).ToList();
            if (bachelier == null || bachelier.Count == 0)
            {
                return NotFound("Bachelier not found");
            }
            var personnes = _context.Personnes.Where(p => p.IdPersonne == bachelier[0].IdPersonne).ToList();
            if (personnes == null || personnes.Count == 0)
            {
                return NotFound("Person not found");
            }

            // Récupération des notes
            var notes = _context.Notes.Where(n => n.IdBachelier == bachelier[0].IdBachelier).ToList();
            var noteDetails = new List<NoteDetail>();
            double totalNotes = 0;
            double totalCoefficients = 0;

            foreach (var note in notes)
            {
                var matiere = _context.Matieres.Where(m => m.IdMatiere == note.IdMatiere).FirstOrDefault();
                double coefficient = 1.0; // Coefficient par défaut
                
                var noteDetail = new NoteDetail
                {
                    Matiere = matiere?.NomMatiere,
                    Note = note.ValeurNote,
                    Coefficient = coefficient,
                    Est_optionnel = note.EstOptionnel ?? false,
                    Note_ponderee = note.ValeurNote * coefficient
                };

                noteDetails.Add(noteDetail);
                totalNotes += noteDetail.Note_ponderee;
                totalCoefficients += coefficient;
            }

            // Récupération des informations supplémentaires
            var centre = _context.Centres.Where(c => c.IdCentre == bachelier[0].IdCentre).FirstOrDefault();
            var option = _context.Options.Where(o => o.IdOption == bachelier[0].IdOption).FirstOrDefault();
            var mention = _context.Mentions.Where(m => m.IdMention == bachelier[0].IdMention).FirstOrDefault();

            double moyenne = totalCoefficients > 0 ? totalNotes / totalCoefficients : 0;

            return Ok(new ReleveResponse
            {
                Nom_prenom = personnes[0].NomPrenom,
                Date_naissance = personnes[0].DateNaissance.ToDateTime(new TimeOnly(0, 0)),
                Lieu_naissance = personnes[0].LieuNaissance,
                Annee = bachelier[0].Annee.Year,
                Num_bacc = data.Num_bacc.ToString(),
                Serie_bacc = option?.Serie,
                Centre_bacc = centre?.NomCentre,
                Notes = noteDetails,
                Total_notes = totalNotes,
                Total_coefficients = totalCoefficients,
                Moyenne_bacc = moyenne,
                Est_admis = moyenne >= 10.0,
                Mention = mention?.NomMention
            });
        }
    }
}
