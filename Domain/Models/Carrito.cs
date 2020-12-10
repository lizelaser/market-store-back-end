using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


#nullable disable

namespace Domain.Models
{
    public partial class Carrito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaMod { get; set; }
        public bool Estado { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        [JsonIgnore]
        public virtual ICollection<Carritoproducto> Carritoproducto { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ordencompra> Ordencompra { get; set; }
    }
}
