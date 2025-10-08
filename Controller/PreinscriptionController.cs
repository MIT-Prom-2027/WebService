using Microsoft.AspNetCore.Mvc;
using UnivManager.Dtos;
using UnivManager.Context;
namespace UnivManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreinscriptionController : ControllerBase
    {
        public readonly AppDbContext _context;
        public PreinscriptionController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("getDataByNumBacc")]
        public ActionResult<PreinscriptionResponse> Post([FromBody] PreinscriptionRequest data)
        {
            var bachelier = _context.Bacheliers.Where(b => b.NumeroCandidat == data.Num_bacc.ToString() && b.Annee.Year.ToString() == data.Annee_bacc).ToList();
            if (bachelier == null || bachelier.Count == 0)
            {
                return NotFound("Bachelier not found");
            }
            var personnes = _context.Personnes.Where(p => p.IdPersonne == bachelier[0].IdPersonne).ToList();
            if (personnes == null || personnes.Count == 0)
            {
                return NotFound("Person not found");
            }
            return Ok(new PreinscriptionResponse
            {
                Nom_prenom = personnes[0].NomPrenom,
                Date_naissance = personnes[0].DateNaissance.ToDateTime(new TimeOnly(0, 0)),
                Lieu_naissance = personnes[0].LieuNaissance,
                Sexe = personnes[0].Sexe,
                // Sexe = "M",
                Mention = _context.Mentions.Where(m => m.IdMention == bachelier[0].IdMention).Select(m => m.NomMention).FirstOrDefault(),
                Option = _context.Options.Where(o => o.IdOption == bachelier[0].IdOption).Select(o => o.Serie).FirstOrDefault(),
                Num_bacc = data.Num_bacc.ToString()
            });
        }
    }
}
