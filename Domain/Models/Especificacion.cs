using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Especificacion
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Detalle { get; set; }
        public string Valor { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
