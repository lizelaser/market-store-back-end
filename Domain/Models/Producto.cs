using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Producto
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string PalabrasClave { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public decimal? Descuento { get; set; }
        public DateTime? InicioDescuento { get; set; }
        public DateTime? FinDescuento { get; set; }
        public string Medida { get; set; }
        public string Imagen { get; set; }
        public string Resumen { get; set; }
        public string Descripcion { get; set; }
        public bool? Estado { get; set; }

        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bannerdetalle> Bannerdetalle { get; set; }
        [JsonIgnore]
        public virtual ICollection<Canastadetalle> Canastadetalle { get; set; }
        [JsonIgnore]
        public virtual ICollection<Carritoproducto> Carritoproducto { get; set; }
        [JsonIgnore]
        public virtual ICollection<Especificacion> Especificacion { get; set; }
        [JsonIgnore]
        public virtual ICollection<Favorito> Favorito { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ordencompradetalle> Ordencompradetalle { get; set; }
    }
}
