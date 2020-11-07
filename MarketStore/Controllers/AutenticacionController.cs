using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using MarketStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public String Login(LoginVm json)
        {
            var usuario = _context.Usuario.Where(x => x.Nombre.Equals(json.Usuario) && x.Contrasena.Equals(json.Clave)).FirstOrDefault();

            if (usuario!=null)
            {
                // Leemos el secret_key desde nuestro appseting
                var secretKey = _configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                // Creamos los claims (pertenencias, características) del usuario
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.RolId.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo)
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    // Nuestro token va a durar un día
                    Expires = DateTime.UtcNow.AddDays(28),
                    // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(createdToken);
            }
            else
            {
                return "Error en login";
            }

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> Registrar(RegistroUsuarioVm json)
        {
            var dbuser = (from u in _context.Usuario where u.Nombre.Equals(json.UsuarioNombre) && u.Correo.Equals(json.Correo) select u).SingleOrDefault();

            if (dbuser == null)
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

                    _context.Usuario.Add(nuevoUsuario);
                    await _context.SaveChangesAsync();

                    var usuarioid = nuevoUsuario.Id;

                    Cliente nuevoCliente = new Cliente();
                    nuevoCliente.UsuarioId = usuarioid;
                    nuevoCliente.Nombres = json.Nombres;
                    nuevoCliente.Apellidos = json.Apellidos;
                    nuevoCliente.Telefono = json.Telefono;
                    nuevoCliente.Estado = true;

                    _context.Cliente.Add(nuevoCliente);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetUsuario","Usuario", new { id = nuevoUsuario.Id }, nuevoUsuario);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }

            return BadRequest(new { error = "El usuario ya existe" });
        }

    }
}