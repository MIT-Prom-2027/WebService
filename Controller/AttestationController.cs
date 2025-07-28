using UnivManager.Dtos;
using Microsoft.AspNetCore.Mvc;
using UnivManager.Context;
using Microsoft.EntityFrameworkCore;

namespace UnivManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttestationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttestationController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IActionResult Post([FromBody] AttestationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.NumeroBacc) || string.IsNullOrEmpty(request.Annee))
            {
                return BadRequest("Données de requête invalides.");
            }

            // Convertir l'année en DateOnly en supposant que l'année est au format "2023"
            if (!int.TryParse(request.Annee, out int anneeInt))
            {
                return BadRequest("Format de l'année invalide.");
            }

            var bachelier = _context.bacheliers
                .Include(b => b.id_personneNavigation)
                .Include(b => b.id_mentionNavigation)
                .FirstOrDefault(b =>
                    b.numero_candidat == request.NumeroBacc &&
                    b.annee.Year == anneeInt);

            if (bachelier == null)
            {
                return NotFound("Bachelier non trouvé.");
            }

            var response = new AttestationResponse
            {
                NomPrenom = bachelier.id_personneNavigation?.nom_prenom,
                DateNaissance = bachelier.id_personneNavigation?.date_naissance.ToString("yyyy-MM-dd"),
                Mention = bachelier.id_mentionNavigation?.nom_mention,
                AnneeBacc = bachelier.annee.Year.ToString()
            };

            return Ok(response);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}