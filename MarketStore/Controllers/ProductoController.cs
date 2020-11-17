using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using javax.jws;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MarketStore.Utilities;
using MarketStore.Models;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;
        private IWebHostEnvironment _env;
        private readonly int RegistrosPorPagina = 12;

        public ProductoController(MARKETSTOREContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {
            return await _context.Producto.Include(x=>x.Categoria).ToListAsync();
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult Tabla(int pagina=1, int categoria=0, decimal preciomin=0, decimal preciomax=9999)
        {
            int TotalRegistros;
            int TotalPaginas;
            
            List<Producto> Productos;
            Paginador<ProductoVm> ListadoProductos;

            try
            {
                // Total number of records in the student table
                //TotalRegistros = _context.Producto.Count();
                // We get the 'records page' from the student table
                 var pinga = _context.Producto.OrderBy(x => x.Id)
                                            .Include(x => x.Categoria)
                                            .Where(x => categoria != 0 ? x.CategoriaId == categoria : true)
                                            .Skip((pagina - 1) * RegistrosPorPagina)
                                            .Take(RegistrosPorPagina)
                                            .Where(x=>x.Precio>=preciomin && x.Precio<=preciomax);
               
                
                TotalRegistros = pinga.Count();
                Productos = pinga.ToList();
                // Total number of pages in the student table
                TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / RegistrosPorPagina);
            } catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            

            //We list "Especialidad" only with the required fields to avoid serialization problems
            var SubProductos = Productos.Select(S => new ProductoVm
            {
                Id = S.Id,
                Nombre = S.Nombre,
                Precio = S.Precio,
                Stock = S.Stock,
                Medida = S.Medida,
                Imagen = S.Imagen

            }).ToList();

            // We instantiate the 'Paging class' and assign the new values
            ListadoProductos = new Paginador<ProductoVm>()
            {
                RegistrosPorPagina = RegistrosPorPagina,
                TotalRegistros = TotalRegistros,
                TotalPaginas = TotalPaginas,
                PaginaActual = pagina,
                Listado = SubProductos
            };

            //we send the pagination class to the view
            return Ok(ListadoProductos);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            try
            {
                (bool success, string path) t = Conversor.SaveImage(_env.ContentRootPath, producto.Imagen);

                if (t.success && t.path != null)
                {
                    producto.Imagen = t.path;
                }
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Producto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            (bool success, string path) t = Conversor.SaveImage(_env.ContentRootPath, producto.Imagen);

            if (t.success && t.path != null)
            {
                producto.Imagen = t.path;

                _context.Producto.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
            }

            return StatusCode(StatusCodes.Status400BadRequest, t.path);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Producto>> DeleteProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }

        

    }
}
