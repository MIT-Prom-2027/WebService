using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnivManager.Models;
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
        public ActionResult<OutputModel> Post([FromBody] InputModel data)
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
            return Ok(new OutputModel
            {
                Nom_prenom = personnes[0].nom_prenom,
                Date_naissance = personnes[0].date_naissance.ToDateTime(new TimeOnly(0, 0)),
                Lieu_naissance = personnes[0].lieu_naissance,
                Sexe = "M",
                Mention = _context.mentions.Where(m => m.id_mention == bachelier[0].id_mention).Select(m => m.nom_mention).FirstOrDefault(),
                Option = "test",
                Num_bacc = data.Num_bacc.ToString()
            });
            // return Ok(new OutputModel
            // {
            //     Nom_prenom = "aona",
            //     Date_naissance = DateTime.Now,
            //     Lieu_naissance = "Itaosy",
            //     Sexe = "M",
            //     Mention = "Bien",
            //     Option = "S",
            //     Num_bacc = data.Num_bacc.ToString()

            // });

        }
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}