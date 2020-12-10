using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


#nullable disable

namespace Domain.Models
{
    public partial class Cliente
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Telefono { get; set; }
        public bool? Estado { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
