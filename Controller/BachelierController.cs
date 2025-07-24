using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnivManager.Context;
using UnivManager.Models;


namespace UnivManager.Controller;

[ApiController]
[Route("api/[controller]")]
class BachelierController : ControllerBase
{
    private readonly AppDbContext _context;

    public BachelierController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Bachelier>> PostBachelier(Bachelier bachelier)
    {
        // Exemple : vérifier s'il existe déjà un bachelier avec même NumeroCandidat et Annee
        bool exists = await _context.Bacheliers.AnyAsync(b =>
            b.NumeroCandidat == bachelier.NumeroCandidat &&
            b.Annee == bachelier.Annee);

        if (exists)
        {
            return Conflict("Un bachelier avec ce numéro de candidat et cette année existe déjà.");
            // ou return BadRequest("...") selon ta politique
        }

        _context.Bacheliers.Add(bachelier);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBachelier), new { id = bachelier.IdBachelier }, bachelier);
    }

    // GET: api/Bacheliers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Bachelier>> GetBachelier(int id)
    {
        var bachelier = await _context.Bacheliers.FindAsync(id);
        if (bachelier == null)
        {
            return NotFound();
        }
        return bachelier;
    }

}