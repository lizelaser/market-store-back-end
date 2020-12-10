using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


#nullable disable

namespace Domain.Models
{
    public partial class Menu
    {

        public int Id { get; set; }
        public int PermisoId { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Icono { get; set; }
        public int? Nivel { get; set; }
        public bool Visible { get; set; }

        [JsonIgnore]
        public virtual Menu NivelNavigation { get; set; }
        [JsonIgnore]
        public virtual Permiso Permiso { get; set; }
        [JsonIgnore]
        public virtual ICollection<Menu> InverseNivelNavigation { get; set; }
    }
}
