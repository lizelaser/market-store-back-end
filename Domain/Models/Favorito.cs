using System;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Favorito
    {
        Favorito()
        {

        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaReg { get; set; }

        public Producto Producto { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
