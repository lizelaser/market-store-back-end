using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Rol
    {
        public int Id { get; set; }
        public string Denominacion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Rolpermiso> Rolpermiso { get; set; }
        [JsonIgnore]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
