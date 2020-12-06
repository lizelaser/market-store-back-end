using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Ordencompra
    {
        public int Id { get; set; }
        public int CarritoId { get; set; }
        public int DireccionId { get; set; }
        public string NroOrdenCompra { get; set; }
        public string Moneda { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal PrecioEnvio { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual Carrito Carrito { get; set; }
        [JsonIgnore]
        public virtual Direccion Direccion { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ordencompradetalle> Ordencompradetalle { get; set; }
    }
}
