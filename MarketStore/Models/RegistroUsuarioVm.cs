using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Models
{
    public class RegistroUsuarioVm
    {
        public string UsuarioNombre { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
    }
}
