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
    public class OrdencompradetalleController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public OrdencompradetalleController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Ordencompradetalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ordencompradetalle>>> GetOrdencompradetalle()
        {
            return await _context.Ordencompradetalle.ToListAsync();
        }

        // GET: api/Ordencompradetalle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ordencompradetalle>> GetOrdencompradetalle(int id)
        {
            var ordencompradetalle = await _context.Ordencompradetalle.FindAsync(id);

            if (ordencompradetalle == null)
            {
                return NotFound();
            }

            return ordencompradetalle;
        }

        // PUT: api/Ordencompradetalle/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdencompradetalle(int id, Ordencompradetalle ordencompradetalle)
        {
            if (id != ordencompradetalle.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordencompradetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdencompradetalleExists(id))
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

        // POST: api/Ordencompradetalle
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ordencompradetalle>> PostOrdencompradetalle(Ordencompradetalle ordencompradetalle)
        {
            _context.Ordencompradetalle.Add(ordencompradetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdencompradetalle", new { id = ordencompradetalle.Id }, ordencompradetalle);
        }

        // DELETE: api/Ordencompradetalle/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ordencompradetalle>> DeleteOrdencompradetalle(int id)
        {
            var ordencompradetalle = await _context.Ordencompradetalle.FindAsync(id);
            if (ordencompradetalle == null)
            {
                return NotFound();
            }

            _context.Ordencompradetalle.Remove(ordencompradetalle);
            await _context.SaveChangesAsync();

            return ordencompradetalle;
        }

        private bool OrdencompradetalleExists(int id)
        {
            return _context.Ordencompradetalle.Any(e => e.Id == id);
        }
    }
}
