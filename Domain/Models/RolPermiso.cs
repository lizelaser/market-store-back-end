using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Rolpermiso
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int PermisoId { get; set; }

        [JsonIgnore]
        public virtual Permiso Permiso { get; set; }
        [JsonIgnore]
        public virtual Rol Rol { get; set; }
    }
}
