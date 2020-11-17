using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Categoria
    {
        public int Id { get; set; }
        public string Denominacion { get; set; }
        public string Imagen { get; set; }

        [JsonIgnore]
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
