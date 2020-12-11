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
    public class PermisoController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public PermisoController(MARKETSTOREContext context)
        {
            _context = context;
        }

        //private static List<PermisoVm> ListarMenus(List<Menu> menus)
        //{
        //    List<PermisoVm> result = new List<PermisoVm>();
        //    Dictionary<int, PermisoVm> resultDict = new Dictionary<int, PermisoVm>();

        //    foreach (var item in menus)
        //    {
        //        PermisoVm child = new PermisoVm(item);

        //        if (child.GrupoId != null)
        //        {
        //            resultDict[child.GrupoId ?? 0].Children.Add(child);
        //            continue;
        //        }

        //        resultDict.Add(child.Id, child);
        //        result.Add(child);
        //    }

        //    return result;
        //}

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permiso>>> GetPermiso(bool todos = false)
        {
            try
            {
                if (todos)
                {
                    var menuGrupos = await _context.Menugrupo.Include(x => x.Permiso).ToListAsync();

                    return Ok(menuGrupos);
                }

                if (User.Identity?.Name != null)
                {
                    int usuarioId = int.Parse(User.Identity.Name);
                    Usuario usuario = await _context.Usuario.FindAsync(usuarioId);

                    var menuGrupos = await (from mg in _context.Menugrupo

                                            join p in _context.Permiso
                                            on mg.Id equals p.MenuId

                                            join rp in _context.Rolpermiso
                                            on p.Id equals rp.PermisoId

                                            where rp.RolId == usuario.RolId

                                            select mg)
                        .Distinct()
                        .ToListAsync();

                    foreach (var menugrupo in menuGrupos)
                    {
                        menugrupo.Permiso = await (from p in _context.Permiso

                                                   join mg in _context.Menugrupo
                                                   on p.MenuId equals mg.Id

                                                   join rp in _context.Rolpermiso
                                                   on p.Id equals rp.PermisoId

                                                   where menugrupo.Id == p.MenuId
                                                   where rp.RolId == usuario.RolId

                                                   select p).ToListAsync();
                    }

                    return Ok(menuGrupos);
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: api/Menu/5
        [HttpGet("validar/")]
        public async Task<ActionResult> ValidatePermiso(string ruta)
        {
            try
            {
                Permiso permiso = await (from p in _context.Permiso
                                         where p.Ruta == ruta
                                         select p).FirstOrDefaultAsync();

                if (permiso != null && !permiso.Protegido)
                {
                    return NoContent();
                }

                if (User.Identity?.Name != null)
                {
                    int usuarioId = int.Parse(User.Identity.Name);
                    Usuario usuario = await _context.Usuario.FindAsync(usuarioId);

                    var menu = await (from m in _context.Menugrupo
                                      join p in _context.Permiso
                                      on m.Id equals p.MenuId
                                      join rp in _context.Rolpermiso
                                      on p.Id equals rp.PermisoId
                                      join r in _context.Rol
                                      on rp.RolId equals r.Id
                                      join u in _context.Usuario
                                      on r.Id equals u.RolId
                                      where u.Id == usuario.Id && p.Protegido
                                      select p).ToListAsync();

                    foreach (Permiso x in menu)
                    {
                        string[] bdUri = new Uri("http://localhost" + x.Ruta).Segments;
                        string[] inputUri = new Uri("http://localhost" + ruta).Segments;

                        if (inputUri.Length != bdUri.Length) continue;

                        for (int i = 0; i < inputUri.Length; i++)
                        {
                            string bd = bdUri[i];
                            string input = inputUri[i];

                            if (bd == "*")
                            {
                                if (i == inputUri.Length - 1) return NoContent();
                                continue;
                            }
                            else
                            {
                                if (bdUri[i] != inputUri[i]) break;
                                else
                                {
                                    if (i == inputUri.Length - 1) return NoContent();
                                    continue;
                                }
                            }
                        }
                    }

                    return Forbid();
                }

                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permiso>> GetPermiso(int id)
        {
            var menu = await _context.Permiso.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // PUT: api/Menu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermiso(int id, Permiso permiso)
        {
            if (id != permiso.Id)
            {
                return BadRequest();
            }

            _context.Entry(permiso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermisoExists(id))
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

        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Permiso>> PostPermiso(Permiso permiso)
        {
            _context.Permiso.Add(permiso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermiso", new { id = permiso.Id }, permiso);
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermiso(int id)
        {
            var permiso = await _context.Permiso.FindAsync(id);
            if (permiso == null)
            {
                return NotFound();
            }

            _context.Permiso.Remove(permiso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermisoExists(int id)
        {
            return _context.Permiso.Any(e => e.Id == id);
        }
    }
}
