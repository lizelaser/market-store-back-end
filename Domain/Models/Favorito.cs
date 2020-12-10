using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


#nullable disable

namespace Domain.Models
{
    public partial class Favorito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public DateTime FechaReg { get; set; }

        [JsonIgnore]
        public virtual Producto Producto { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
