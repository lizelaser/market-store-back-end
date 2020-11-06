using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Carrito
    {
        public Carrito()
        {
            Carritoproducto = new HashSet<Carritoproducto>();
            Ordencompra = new HashSet<Ordencompra>();
        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaMod { get; set; }
        public bool Estado { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Carritoproducto> Carritoproducto { get; set; }
        public virtual ICollection<Ordencompra> Ordencompra { get; set; }
    }
}
