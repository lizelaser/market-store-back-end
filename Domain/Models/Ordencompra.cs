using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Ordencompra
    {
        public Ordencompra()
        {
            Ordencompradetalle = new HashSet<Ordencompradetalle>();
        }

        public int Id { get; set; }
        public int CarritoId { get; set; }
        public int DireccionId { get; set; }
        public string NroOrdenCompra { get; set; }
        public string Moneda { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal PrecioEnvio { get; set; }
        public decimal Total { get; set; }

        public virtual Carrito Carrito { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual ICollection<Ordencompradetalle> Ordencompradetalle { get; set; }
    }
}
