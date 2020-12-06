using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string Denominacion { get; set; }
        public string Icono { get; set; }
        public string Ruta { get; set; }

        [JsonIgnore]
        public virtual ICollection<RolMenu> RolMenu { get; set; }
    }
}
