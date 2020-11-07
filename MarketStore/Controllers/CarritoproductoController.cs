using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    [ApiController]
    public class CarritoproductoController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public CarritoproductoController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Carritoproducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carritoproducto>>> GetCarritoproducto()
        {
            return await _context.Carritoproducto.ToListAsync();
        }

        // GET: api/Carritoproducto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carritoproducto>> GetCarritoproducto(int id)
        {
            var carritoproducto = await _context.Carritoproducto.FindAsync(id);

            if (carritoproducto == null)
            {
                return NotFound();
            }

            return carritoproducto;
        }

        // PUT: api/Carritoproducto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarritoproducto(int id, Carritoproducto carritoproducto)
        {
            if (id != carritoproducto.Id)
            {
                return BadRequest();
            }

            _context.Entry(carritoproducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoproductoExists(id))
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

        // POST: api/Carritoproducto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Carritoproducto>> PostCarritoproducto(Carritoproducto carritoproducto)
        {
            _context.Carritoproducto.Add(carritoproducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarritoproducto", new { id = carritoproducto.Id }, carritoproducto);
        }

        // DELETE: api/Carritoproducto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Carritoproducto>> DeleteCarritoproducto(int id)
        {
            var carritoproducto = await _context.Carritoproducto.FindAsync(id);
            if (carritoproducto == null)
            {
                return NotFound();
            }

            _context.Carritoproducto.Remove(carritoproducto);
            await _context.SaveChangesAsync();

            return carritoproducto;
        }

        private bool CarritoproductoExists(int id)
        {
            return _context.Carritoproducto.Any(e => e.Id == id);
        }
    }
}
