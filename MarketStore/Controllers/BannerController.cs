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
    public class BannerController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;
        private IWebHostEnvironment _env;

        public BannerController(MARKETSTOREContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Banner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banner>>> GetBanner()
        {
            return await _context.Banner.ToListAsync();
        }

        // GET: api/Banner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Banner>> GetBanner(int id)
        {
            var banner = await _context.Banner.FindAsync(id);

            if (banner == null)
            {
                return NotFound();
            }

            return banner;
        }

        // PUT: api/Banner/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutBanner(int id, Banner banner)
        {
            if (id != banner.Id)
            {
                return BadRequest();
            }

            _context.Entry(banner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(id))
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

        // POST: api/Banner
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Banner>> PostBanner(Banner banner)
        {
            (bool success, string path) t = Conversor.SaveImage(_env.ContentRootPath, banner.Imagen);
            if (t.success && t.path != null)
            {
                banner.Imagen = t.path;

                _context.Banner.Add(banner);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBanner", new { id = banner.Id }, banner);
            }
            return StatusCode(StatusCodes.Status400BadRequest, t.path);

        }

        // DELETE: api/Banner/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Banner>> DeleteBanner(int id)
        {
            var banner = await _context.Banner.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }

            _context.Banner.Remove(banner);
            await _context.SaveChangesAsync();

            return banner;
        }

        private bool BannerExists(int id)
        {
            return _context.Banner.Any(e => e.Id == id);
        }
    }
}
