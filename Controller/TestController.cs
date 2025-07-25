using Microsoft.AspNetCore.Mvc;

namespace UnivManager.Controller
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTest()
        {
            var resultat = new { result = "ok" };
            return StatusCode(201, resultat);
        }

        [HttpPost]
        public IActionResult PostTest()
        {
            [FromBody] dynamic data
            {
                return Ok(new { message = "JSON reçu", contenu = data });
            }
        }
    }
}
