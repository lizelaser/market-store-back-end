using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Ordencompradetalle
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int OrdenCompraId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GastoEnvio { get; set; }

        public virtual Ordencompra OrdenCompra { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
