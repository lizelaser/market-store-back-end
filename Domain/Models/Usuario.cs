using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Carrito = new HashSet<Carrito>();
            Cliente = new HashSet<Cliente>();
            Favorito = new HashSet<Favorito>();
        }

        public int Id { get; set; }
        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime? FechaMod { get; set; }
        public bool? Estado { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Favorito> Favorito { get; set; }
    }
}
