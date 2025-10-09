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

        [HttpPost]
        public ActionResult<AttestationResponse> Post([FromBody] AttestationRequest request)
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

            var bachelier = _context.Bacheliers
                .Include(b => b.IdPersonneNavigation)
                .Include(b => b.IdMentionNavigation)
                .FirstOrDefault(b =>
                    b.NumeroCandidat == request.NumeroBacc &&
                    b.Annee.Year == anneeInt);

            if (bachelier == null)
            {
                return NotFound("Bachelier non trouvé.");
            }

            var response = new AttestationResponse
            {
                NomPrenom = bachelier.IdPersonneNavigation?.NomPrenom,
                DateNaissance = bachelier.IdPersonneNavigation?.DateNaissance.ToString("yyyy-MM-dd"),
                Mention = bachelier.IdMentionNavigation?.NomMention,
                AnneeBacc = bachelier.Annee.Year.ToString()
            };

            return Ok(response);
        }
    }
}

