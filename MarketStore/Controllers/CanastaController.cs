using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using MarketStore.Utilities;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanastaController : ControllerBase
    {
        
        private readonly MARKETSTOREContext _context;
        private IWebHostEnvironment _env;

        public CanastaController(MARKETSTOREContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Canasta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canasta>>> GetCanasta()
        {
            return await _context.Canasta.ToListAsync();
        }

        // GET: api/Canasta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canasta>> GetCanasta(int id)
        {
            var canasta = await _context.Canasta.FindAsync(id);

            if (canasta == null)
            {
                return NotFound();
            }

            return canasta;
        }

        // PUT: api/Canasta/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutCanasta(int id, Canasta canasta)
        {
            if (id != canasta.Id)
            {
                return BadRequest();
            }

            _context.Entry(canasta).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanastaExists(id))
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

        // POST: api/Canasta
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Canasta>> PostCanasta(Canasta canasta)
        {
            (bool success, string path) t = Conversor.SaveImage(_env.ContentRootPath, canasta.Imagen);
            if (t.success && t.path != null)
            {
                canasta.Imagen = t.path;

                _context.Canasta.Add(canasta);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCanasta", new { id = canasta.Id }, canasta);
            }
            return StatusCode(StatusCodes.Status400BadRequest, t.path);
        }

        // DELETE: api/Canasta/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Canasta>> DeleteCanasta(int id)
        {
            var canasta = await _context.Canasta.FindAsync(id);
            if (canasta == null)
            {
                return NotFound();
            }

            _context.Canasta.Remove(canasta);
            await _context.SaveChangesAsync();

            return canasta;
        }

        private bool CanastaExists(int id)
        {
            return _context.Canasta.Any(e => e.Id == id);
        }
    }
}
