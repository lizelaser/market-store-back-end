using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "CustomerOnly")]
    public class DireccionController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public DireccionController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Direccion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Direccion>>> GetDireccion()
        {
            int clienteId;
            try
            {
                clienteId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch (Exception error)
            {
                return Unauthorized(error.Message);
            }

            return await _context.Direccion.Where(d => d.ClienteId == clienteId).OrderByDescending(d => d.Defecto).ToListAsync();
        }

        // GET: api/Direccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Direccion>> GetDireccion(int id)
        {
            int clienteId;
            try
            {
                clienteId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch (Exception error)
            {
                return Unauthorized(error.Message);
            }

            var direccion = await _context.Direccion
                .Where(d => d.ClienteId == clienteId && d.Id == id)
                .SingleOrDefaultAsync();

            if (direccion == null)
            {
                return NotFound();
            }

            return direccion;
        }

        // PUT: api/Direccion/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccion(int id, Direccion direccion)
        {
            int clienteId;
            try
            {
                clienteId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch (Exception error)
            {
                return Unauthorized(error.Message);
            }

            Direccion exists = await _context.Direccion
                .Where(d => d.ClienteId == clienteId && d.Id == id)
                .SingleOrDefaultAsync();

            if (exists == null)
            {
                return Unauthorized();
            }

            if (id != direccion.Id)
            {
                return BadRequest();
            }

            _context.Entry(direccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionExists(id))
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

        // POST: api/Direccion
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Direccion>> PostDireccion(Direccion direccion)
        {
            int clienteId;
            try
            {
                clienteId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch (Exception error)
            {
                return Unauthorized(error.Message);
            }

            direccion.ClienteId = clienteId;

            if (direccion.Defecto)
            {
                var olds = await _context.Direccion.Where(d => d.ClienteId == clienteId).ToListAsync();
                olds.ForEach(d => d.Defecto = false);
            }

            _context.Direccion.Add(direccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDireccion", new { id = direccion.Id }, direccion);
        }

        // DELETE: api/Direccion/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Direccion>> DeleteDireccion(int id)
        {
            int clienteId;
            try
            {
                clienteId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch (Exception error)
            {
                return Unauthorized(error.Message);
            }

            var direccion = await _context.Direccion
                .Where(d => d.ClienteId == clienteId && d.Id == id)
                .SingleOrDefaultAsync();

            if (direccion == null)
            {
                return NotFound();
            }

            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();

            return direccion;
        }

        private bool DireccionExists(int id)
        {
            return _context.Direccion.Any(e => e.Id == id);
        }
    }
}
