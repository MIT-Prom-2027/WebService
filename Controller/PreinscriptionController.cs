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
            return Ok(new PreinscriptionResponse
            {
                Nom_prenom = personnes[0].nom_prenom,
                Date_naissance = personnes[0].date_naissance.ToDateTime(new TimeOnly(0, 0)),
                Lieu_naissance = personnes[0].lieu_naissance,
                // Sexe = "M",
                Mention = _context.mentions.Where(m => m.id_mention == bachelier[0].id_mention).Select(m => m.nom_mention).FirstOrDefault(),
                Option = _context.options.Where(o => o.id_option == bachelier[0].id_option).Select(o => o.serie).FirstOrDefault(),
                Num_bacc = data.Num_bacc.ToString()
            });
        }
    }
}