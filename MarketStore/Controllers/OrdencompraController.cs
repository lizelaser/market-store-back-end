using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using MarketStore.Models;
using System.Security.Claims;

namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "CustomerOnly")]
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
            int clienteId;
            try
            {
                clienteId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
            catch (Exception error)
            {
                return Unauthorized(error.Message);
            }

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
        public async Task<ActionResult<Ordencompra>> PostOrdencompra(ProductoVm3 input)
        {
            Carrito carrito = new Carrito();
            carrito.UsuarioId = int.Parse(User.Identity.Name);
            carrito.FechaReg = DateTime.Now;
            carrito.FechaMod = DateTime.Now;
            carrito.Estado = true;

            _context.Carrito.Add(carrito);
            await _context.SaveChangesAsync();

            decimal total = 0.0m;

            input.Productos.ForEach((p) =>
            {
                decimal total0;

                Carritoproducto cp = new Carritoproducto();
                cp.CarritoId = carrito.Id;
                cp.ProductoId = p.Id;
                cp.Cantidad = p.Cantidad;

                total0 = p.Cantidad * p.Precio;
                cp.Subtotal = total0;

                total += total0;

                _context.Carritoproducto.Add(cp);
            });


            Ordencompra oc = new Ordencompra();
            oc.CarritoId = carrito.Id;
            oc.DireccionId = input.DireccionId;
            oc.NroOrdenCompra = (new Guid()).ToString();
            oc.Moneda = "PEN";

            oc.Total = total;
            oc.Subtotal = oc.Total / 1.18m;
            oc.Impuesto = oc.Total - oc.Subtotal;

            oc.PrecioEnvio = 5.0m;

            _context.Ordencompra.Add(oc);
            await _context.SaveChangesAsync();

            input.Productos.ForEach((p) =>
            {
                Ordencompradetalle ocd = new Ordencompradetalle();
                ocd.ProductoId = p.Id;
                ocd.OrdenCompraId = oc.Id;
                ocd.Subtotal = p.Cantidad * p.Precio;
                ocd.GastoEnvio = 5.0m;

                _context.Ordencompradetalle.Add(ocd);
            });

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdencompra", new { id = oc.Id }, input);
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
