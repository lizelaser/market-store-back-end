using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using MarketStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace MarketStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly MARKETSTOREContext _context;

        public AutenticacionController(IConfiguration configuration, MARKETSTOREContext _db)
        {
            _configuration = configuration;
            _context = _db;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<String>> Login(LoginVm json)
        {
            Usuario usuario = await _context.Usuario
                .Where(x => x.Nombre.Equals(json.Usuario) && x.Contrasena.Equals(json.Clave))
                .Include(u => u.Rol)
                .FirstOrDefaultAsync();

            if (usuario != null)
            {
                // Leemos el secret_key desde nuestro appseting
                var secretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                // Creamos los claims (pertenencias, características) del usuario
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, usuario.RolId.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, usuario.Correo));
                claims.Add(new Claim(ClaimTypes.Name, usuario.Id.ToString()));

                Cliente cliente = await _context.Cliente
                    .Where(c => c.UsuarioId == usuario.Id)
                    .FirstOrDefaultAsync();

                if (cliente != null)
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    // Nuestro token va a durar un día
                    Expires = DateTime.UtcNow.AddDays(28),
                    // Credenciales para generar el token usando nuestro secret key y el algoritmo hash 256
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                usuario.Estado = true;
                await new UsuarioController(_context).PutUsuario(usuario.Id, usuario);

                return Ok(tokenHandler.WriteToken(createdToken));
            }
            else
            {
                return BadRequest("El usuario no existe, no es un cliente o se encuentra desactivado");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> Logout()
        {
            int id;
            try
            {
                id = int.Parse(User.Identity.Name);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            Usuario usuario = await _context.Usuario.Include(u => u.Rol).Where(u => u.Id == id).FirstOrDefaultAsync();
            if (usuario == null) return BadRequest("El usuario no existe");

            usuario.Estado = false;
            await new UsuarioController(_context).PutUsuario(usuario.Id, usuario);

            return Ok(usuario);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> Registrar(RegistroUsuarioVm json)
        {
            var dbUser = _context.Usuario
                .Where(u => u.Nombre.Equals(json.UsuarioNombre) && u.Correo.Equals(json.Correo))
                .Include(u => u.Rol)
                .SingleOrDefault();

            if (dbUser == null)
            {
                try
                {
                    Usuario nuevoUsuario = new Usuario();
                    nuevoUsuario.Nombre = json.UsuarioNombre;
                    nuevoUsuario.Contrasena = json.Contrasena;
                    nuevoUsuario.Correo = json.Correo;
                    nuevoUsuario.RolId = 2;
                    nuevoUsuario.FechaReg = DateTime.Now;
                    nuevoUsuario.Estado = true;
                    nuevoUsuario.Rol = await _context.Rol.FindAsync(nuevoUsuario.RolId);

                    _context.Usuario.Add(nuevoUsuario);
                    await _context.SaveChangesAsync();

                    Cliente nuevoCliente = new Cliente();
                    nuevoCliente.UsuarioId = nuevoUsuario.Id;
                    nuevoCliente.Nombres = json.Nombres;
                    nuevoCliente.Apellidos = json.Apellidos;
                    nuevoCliente.Telefono = json.Telefono;
                    nuevoCliente.Estado = true;

                    _context.Cliente.Add(nuevoCliente);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetUsuario", "Usuario", new { id = nuevoCliente.Id }, nuevoUsuario);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }

            return BadRequest("El usuario ya existe");
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> Usuario()
        {
            int id;
            try
            {
                id = int.Parse(User.Identity.Name);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            Usuario usuario = await _context.Usuario.Include(u => u.Rol).Where(u => u.Id == id).FirstOrDefaultAsync();
            if (usuario == null) return BadRequest("El usuario no existe");

            return Ok(usuario);
        }

    }
}