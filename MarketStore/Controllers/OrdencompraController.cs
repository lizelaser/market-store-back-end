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
    public class OrdencompraController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public OrdencompraController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Ordencompra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ordencompra>>> GetOrdencompra()
        {
            return await _context.Ordencompra.Include(oc => oc.Direccion).ToListAsync();
        }

        // GET: api/Ordencompra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ordencompra>> GetOrdencompra(int id)
        {
            var ordencompra = await _context.Ordencompra.FindAsync(id);

            if (ordencompra == null)
            {
                return NotFound();
            }

            return ordencompra;
        }

        // PUT: api/Ordencompra/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdencompra(int id, Ordencompra ordencompra)
        {
            if (id != ordencompra.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordencompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdencompraExists(id))
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

        // POST: api/Ordencompra
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ordencompra>> PostOrdencompra(Ordencompra ordencompra)
        {
            _context.Ordencompra.Add(ordencompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdencompra", new { id = ordencompra.Id }, ordencompra);
        }

        // DELETE: api/Ordencompra/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ordencompra>> DeleteOrdencompra(int id)
        {
            var ordencompra = await _context.Ordencompra.FindAsync(id);
            if (ordencompra == null)
            {
                return NotFound();
            }

            _context.Ordencompra.Remove(ordencompra);
            await _context.SaveChangesAsync();

            return ordencompra;
        }

        private bool OrdencompraExists(int id)
        {
            return _context.Ordencompra.Any(e => e.Id == id);
        }
    }
}
