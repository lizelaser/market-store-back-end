using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public FavoritoController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Favorito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorito>>> GetFavorito()
        {
            return await _context.Favorito.ToListAsync();
        }

        // GET: api/Favorito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorito>> GetFavorito(int id)
        {
            var favorito = await _context.Favorito.FindAsync(id);

            if (favorito == null)
            {
                return NotFound();
            }

            return favorito;
        }

        // PUT: api/Favorito/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorito(int id, Favorito favorito)
        {
            if (id != favorito.Id)
            {
                return BadRequest();
            }

            _context.Entry(favorito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoritoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Favorito
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Favorito>> PostFavorito(Favorito favorito)
        {
            _context.Favorito.Add(favorito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorito", new { id = favorito.Id }, favorito);
        }

        // DELETE: api/Favorito/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Favorito>> DeleteFavorito(int id)
        {
            var favorito = await _context.Favorito.FindAsync(id);
            if (favorito == null)
            {
                return NotFound();
            }

            _context.Favorito.Remove(favorito);
            await _context.SaveChangesAsync();

            return favorito;
        }

        private bool FavoritoExists(int id)
        {
            return _context.Favorito.Any(e => e.Id == id);
        }
    }
}
