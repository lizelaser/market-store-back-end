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
    public class BannerdetalleController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public BannerdetalleController(MARKETSTOREContext context)
        {
            _context = context;
        }

        // GET: api/Bannerdetalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bannerdetalle>>> GetBannerdetalle()
        {
            return await _context.Bannerdetalle.ToListAsync();
        }

        // GET: api/Bannerdetalle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bannerdetalle>> GetBannerdetalle(int id)
        {
            var bannerdetalle = await _context.Bannerdetalle.FindAsync(id);

            if (bannerdetalle == null)
            {
                return NotFound();
            }

            return bannerdetalle;
        }

        // PUT: api/Bannerdetalle/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBannerdetalle(int id, Bannerdetalle bannerdetalle)
        {
            if (id != bannerdetalle.Id)
            {
                return BadRequest();
            }

            _context.Entry(bannerdetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerdetalleExists(id))
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

        // POST: api/Bannerdetalle
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bannerdetalle>> PostBannerdetalle(Bannerdetalle bannerdetalle)
        {
            _context.Bannerdetalle.Add(bannerdetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBannerdetalle", new { id = bannerdetalle.Id }, bannerdetalle);
        }

        // DELETE: api/Bannerdetalle/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bannerdetalle>> DeleteBannerdetalle(int id)
        {
            var bannerdetalle = await _context.Bannerdetalle.FindAsync(id);
            if (bannerdetalle == null)
            {
                return NotFound();
            }

            _context.Bannerdetalle.Remove(bannerdetalle);
            await _context.SaveChangesAsync();

            return bannerdetalle;
        }

        private bool BannerdetalleExists(int id)
        {
            return _context.Bannerdetalle.Any(e => e.Id == id);
        }
    }
}
