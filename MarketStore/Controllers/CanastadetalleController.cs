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
    public class CanastadetalleController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public CanastadetalleController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Canastadetalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canastadetalle>>> GetCanastadetalle()
        {
            return await _context.Canastadetalle.ToListAsync();
        }

        // GET: api/Canastadetalle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canastadetalle>> GetCanastadetalle(int id)
        {
            var canastadetalle = await _context.Canastadetalle.FindAsync(id);

            if (canastadetalle == null)
            {
                return NotFound();
            }

            return canastadetalle;
        }

        // PUT: api/Canastadetalle/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanastadetalle(int id, Canastadetalle canastadetalle)
        {
            if (id != canastadetalle.Id)
            {
                return BadRequest();
            }

            _context.Entry(canastadetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanastadetalleExists(id))
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

        // POST: api/Canastadetalle
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Canastadetalle>> PostCanastadetalle(Canastadetalle canastadetalle)
        {
            _context.Canastadetalle.Add(canastadetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanastadetalle", new { id = canastadetalle.Id }, canastadetalle);
        }

        // DELETE: api/Canastadetalle/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Canastadetalle>> DeleteCanastadetalle(int id)
        {
            var canastadetalle = await _context.Canastadetalle.FindAsync(id);
            if (canastadetalle == null)
            {
                return NotFound();
            }

            _context.Canastadetalle.Remove(canastadetalle);
            await _context.SaveChangesAsync();

            return canastadetalle;
        }

        private bool CanastadetalleExists(int id)
        {
            return _context.Canastadetalle.Any(e => e.Id == id);
        }
    }
}
