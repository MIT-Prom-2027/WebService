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

            var bachelier = _context.bacheliers.Where(b => b.numero_candidat == data.Num_bacc.ToString()).ToList();
            if (bachelier == null || bachelier.Count == 0)
            {
                return NotFound("Bachelier not found");
            }
            var personnes = _context.personnes.Where(p => p.id_personne == bachelier[0].id_personne).ToList();
            if (personnes == null || personnes.Count == 0)
            {
                return NotFound("Person not found");
            }

            // Récupération des notes
            var notes = _context.notes.Where(n => n.id_bachelier == bachelier[0].id_bachelier).ToList();
            var noteDetails = new List<NoteDetail>();
            double totalNotes = 0;
            double totalCoefficients = 0;

            foreach (var note in notes)
            {
                var matiere = _context.matieres.Where(m => m.id_matiere == note.id_matiere).FirstOrDefault();
                double coefficient = 1.0; // Coefficient par défaut
                
                var noteDetail = new NoteDetail
                {
                    Matiere = matiere?.nom_matiere,
                    Note = note.valeur_note,
                    Coefficient = coefficient,
                    Est_optionnel = note.est_optionnel ?? false,
                    Note_ponderee = note.valeur_note * coefficient
                };

                noteDetails.Add(noteDetail);
                totalNotes += noteDetail.Note_ponderee;
                totalCoefficients += coefficient;
            }

            // Récupération des informations supplémentaires
            var centre = _context.centres.Where(c => c.id_centre == bachelier[0].id_centre).FirstOrDefault();
            var option = _context.options.Where(o => o.id_option == bachelier[0].id_option).FirstOrDefault();
            var mention = _context.mentions.Where(m => m.id_mention == bachelier[0].id_mention).FirstOrDefault();

            double moyenne = totalCoefficients > 0 ? totalNotes / totalCoefficients : 0;

            return Ok(new ReleveResponse
            {
                Nom_prenom = personnes[0].nom_prenom,
                Date_naissance = personnes[0].date_naissance.ToDateTime(new TimeOnly(0, 0)),
                Lieu_naissance = personnes[0].lieu_naissance,
                Annee = bachelier[0].annee.Year,
                Num_bacc = data.Num_bacc.ToString(),
                Serie_bacc = option?.serie,
                Centre_bacc = centre?.nom_centre,
                Notes = noteDetails,
                Total_notes = totalNotes,
                Total_coefficients = totalCoefficients,
                Moyenne_bacc = moyenne,
                Est_admis = moyenne >= 10.0,
                Mention = mention?.nom_mention
            });
        }
    }
}
