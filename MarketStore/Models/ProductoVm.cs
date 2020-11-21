using System.Collections.Generic;

namespace MarketStore.Models
{
    public class ProductoVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Medida { get; set; }
        public string Imagen { get; set; }
    }

    public class ProductoVm2
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }

    public class ProductoVm3
    {
        public List<ProductoVm2> Productos { get; set; }
        public int DireccionId { get; set; }
    }
}
