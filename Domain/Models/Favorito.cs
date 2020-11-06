using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Favorito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaReg { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
