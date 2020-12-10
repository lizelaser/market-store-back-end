using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Ordencompradetalle
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int OrdenCompraId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GastoEnvio { get; set; }

        [JsonIgnore]
        public virtual Ordencompra OrdenCompra { get; set; }
        [JsonIgnore]
        public virtual Producto Producto { get; set; }
    }
}
