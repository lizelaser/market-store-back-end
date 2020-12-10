using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


#nullable disable

namespace Domain.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime? FechaMod { get; set; }
        public bool? Estado { get; set; }

        [JsonIgnore]
        public virtual Rol Rol { get; set; }
        [JsonIgnore]
        public virtual ICollection<Carrito> Carrito { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cliente> Cliente { get; set; }
        [JsonIgnore]
        public virtual ICollection<Favorito> Favorito { get; set; }
    }
}
