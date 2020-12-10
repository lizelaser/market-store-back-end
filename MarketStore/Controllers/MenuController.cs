using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System.Reflection;
using MarketStore.Models;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;

        public MenuController(MARKETSTOREContext context)
        {
            _context = context;
        }

        private static List<MenuVm> ListarMenus(List<Menu> menus)
        {
            List<MenuVm> result = new List<MenuVm>();
            Dictionary<int, MenuVm> resultDict = new Dictionary<int, MenuVm>();

            foreach (var item in menus)
            {
                MenuVm child = new MenuVm(item);

                if (child.Nivel != null)
                {
                    resultDict[child.Nivel ?? 0].Children.Add(child);
                    continue;
                }

                resultDict.Add(child.Id, child);
                result.Add(child);
            }

            return result;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu(bool todos=false)
        {
            try
            {
                if (todos)
                {
                    var menus = await (from m in _context.Menu
                                       join p in _context.Permiso
                                       on m.PermisoId equals p.Id
                                       where p.Protegido
                                       select m).ToListAsync();

                    List<MenuVm> result = ListarMenus(menus);

                    return Ok(result);
                }

                if (User.Identity?.Name != null)
                {
                    int usuarioId = int.Parse(User.Identity.Name);
                    Usuario usuario = await _context.Usuario.FindAsync(usuarioId);

                    var menus = await (from m in _context.Menu
                                            join p in _context.Permiso
                                            on m.PermisoId equals p.Id
                                            join rp in _context.RolPermiso
                                            on p.Id equals rp.PermisoId
                                            join r in _context.Rol
                                            on rp.RolId equals r.Id
                                            join u in _context.Usuario
                                            on r.Id equals u.RolId
                                            where u.Id == usuario.Id
                                            orderby m.Nivel
                                            select m).ToListAsync();

                    List<MenuVm> result = ListarMenus(menus);
                   
                    return Ok(result);
                }

                return Unauthorized();
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // GET: api/Menu/5
        [HttpGet("validar/")]
        public async Task<ActionResult> ValidateMenu(string ruta)
        {
            try
            {
                Permiso permiso = await (from m in _context.Menu
                                         join p in _context.Permiso
                                         on m.PermisoId equals p.Id
                                         where m.Ruta == ruta
                                         select p).FirstOrDefaultAsync();

                if(permiso!=null && !permiso.Protegido)
                {
                    return NoContent();
                }

                if (User.Identity?.Name != null)
                {
                    int usuarioId = int.Parse(User.Identity.Name);
                    Usuario usuario = await _context.Usuario.FindAsync(usuarioId);

                    var menu = await (from m in _context.Menu
                                      join p in _context.Permiso
                                      on m.PermisoId equals p.Id
                                      join rp in _context.RolPermiso
                                      on p.Id equals rp.PermisoId
                                      join r in _context.Rol
                                      on rp.RolId equals r.Id
                                      join u in _context.Usuario
                                      on r.Id equals u.RolId
                                      where u.Id == usuario.Id && p.Protegido
                                      select m).ToListAsync();

                    foreach (Menu x in menu)
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
                            } else
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
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // PUT: api/Menu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }
    }
}
