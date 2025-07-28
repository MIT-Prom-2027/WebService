using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnivManager.Models;
namespace UnivManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreinscriptionController : ControllerBase
    {
        [HttpPost("getDataByNumBacc")]
        public ActionResult<OutputModel> Post([FromBody] InputModel data)
        {
            return Ok(new OutputModel
            {
                Nom_prenom = "John Doe",
                Date_naissance = new DateTime(2000, 1, 1),
                Lieu_naissance = "City, Country",
                Sexe = "M",
                Mention = "Excellent",
                Option = "Science",
                Num_bacc = data.Num_bacc.ToString()
            });
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