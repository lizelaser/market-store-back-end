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
using MarketStore.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly MARKETSTOREContext _context;
        private readonly IWebHostEnvironment _env;
        private const int RegistrosPorPagina = 12;

        public ProductoController(MARKETSTOREContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {
            var items = await _context.Producto.Include(x => x.Categoria).ToListAsync();
            ImagenUtilidad.CrearImagenUrls(items, Request);
            return Ok(items);
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult Tabla(int pagina = 1, int categoria = 0, decimal precioMin = 0, decimal precioMax = 9999)
        {
            int totalRegistros;
            int totalPaginas;

            List<Producto> productos;

            try
            {
                // Total number of records in the student table
                //TotalRegistros = _context.Producto.Count();
                // We get the 'records page' from the student table
                var productosQuery = _context.Producto.OrderBy(x => x.Id)
                                           .Include(x => x.Categoria)
                                           .Where(x => categoria == 0 || x.CategoriaId == categoria)
                                           .Skip((pagina - 1) * RegistrosPorPagina)
                                           .Take(RegistrosPorPagina)
                                           .Where(x => x.Precio >= precioMin && x.Precio <= precioMax);


                totalRegistros = productosQuery.Count();
                productos = productosQuery.ToList();
                // Total number of pages in the student table
                totalPaginas = (int)Math.Ceiling((double)totalRegistros / RegistrosPorPagina);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            ImagenUtilidad.CrearImagenUrls(productos, Request);
            // We instantiate the 'Paging class' and assign the new values
            Paginador<Producto> listadoProductos = new Paginador<Producto>()
            {
                RegistrosPorPagina = RegistrosPorPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = totalPaginas,
                PaginaActual = pagina,
                Listado = productos
            };

            //we send the pagination class to the view
            return Ok(listadoProductos);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            Producto producto = await _context.Producto.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            ImagenUtilidad.CrearImagenUrl(producto, Request);
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
                producto.Imagen = ImagenUtilidad.GuardarImagen(_env.ContentRootPath, producto.Imagen);

                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (ImagenUtilidadException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
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
            try
            {
                producto.Imagen = ImagenUtilidad.GuardarImagen(_env.ContentRootPath, producto.Imagen);
            }
            catch (ImagenUtilidadException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            try
            {
                await _context.Producto.AddAsync(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
