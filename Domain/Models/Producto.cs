using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Bannerdetalle = new HashSet<Bannerdetalle>();
            Canastadetalle = new HashSet<Canastadetalle>();
            Carritoproducto = new HashSet<Carritoproducto>();
            Especificacion = new HashSet<Especificacion>();
            Favorito = new HashSet<Favorito>();
            Ordencompradetalle = new HashSet<Ordencompradetalle>();
        }

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

        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Bannerdetalle> Bannerdetalle { get; set; }
        public virtual ICollection<Canastadetalle> Canastadetalle { get; set; }
        public virtual ICollection<Carritoproducto> Carritoproducto { get; set; }
        public virtual ICollection<Especificacion> Especificacion { get; set; }
        public virtual ICollection<Favorito> Favorito { get; set; }
        public virtual ICollection<Ordencompradetalle> Ordencompradetalle { get; set; }
    }
}
