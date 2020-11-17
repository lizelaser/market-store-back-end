using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Carritoproducto
    {
        public int Id { get; set; }
        public int CarritoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }

        [JsonIgnore]
        public virtual Carrito Carrito { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
