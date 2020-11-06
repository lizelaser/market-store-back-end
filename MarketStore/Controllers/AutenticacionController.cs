using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
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
        private readonly MARKETSTOREContext db;

        public AutenticacionController(IConfiguration configuration, Domain.Models.MARKETSTOREContext _db)
        {
            _configuration = configuration;
            db = _db;
        }

        [HttpGet]
        [Route("[action]")]
        public String Login()
        {
            //var usuarios = db.Usuario.Select(x => x.Nombre , x.Contrasena).ToList();

            // Tu código para validar que el usuario ingresado es válido

            // Asumamos que tenemos un usuario válido
            var user = new Domain.Models.Usuario
            {
                Nombre = "Eduardo",
                Correo = "admin@kodoti.com",
                Id = 1
            };

            var usuario = db.Usuario.ToList();

            // Leemos el secret_key desde nuestro appseting
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Creamos los claims (pertenencias, características) del usuario
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RolId.ToString()),
                new Claim(ClaimTypes.Email, user.Correo)
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

    }
}