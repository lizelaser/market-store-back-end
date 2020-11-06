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
    public class EspecificacionController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public EspecificacionController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Especificacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especificacion>>> GetEspecificacion()
        {
            return await _context.Especificacion.ToListAsync();
        }

        // GET: api/Especificacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Especificacion>> GetEspecificacion(int id)
        {
            var especificacion = await _context.Especificacion.FindAsync(id);

            if (especificacion == null)
            {
                return NotFound();
            }

            return especificacion;
        }

        // PUT: api/Especificacion/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecificacion(int id, Especificacion especificacion)
        {
            if (id != especificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(especificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecificacionExists(id))
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

        // POST: api/Especificacion
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Especificacion>> PostEspecificacion(Especificacion especificacion)
        {
            _context.Especificacion.Add(especificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspecificacion", new { id = especificacion.Id }, especificacion);
        }

        // DELETE: api/Especificacion/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Especificacion>> DeleteEspecificacion(int id)
        {
            var especificacion = await _context.Especificacion.FindAsync(id);
            if (especificacion == null)
            {
                return NotFound();
            }

            _context.Especificacion.Remove(especificacion);
            await _context.SaveChangesAsync();

            return especificacion;
        }

        private bool EspecificacionExists(int id)
        {
            return _context.Especificacion.Any(e => e.Id == id);
        }
    }
}
