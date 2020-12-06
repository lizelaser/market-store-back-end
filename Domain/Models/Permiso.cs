using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Permiso
    {
        public int Id { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public string Descripcion { get; set; }
        public bool Protegido { get; set; }

        [JsonIgnore]
        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
    }
}
