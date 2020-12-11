using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Permiso
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public string Ruta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Protegido { get; set; }
        public bool? Visible { get; set; }

        [JsonIgnore]
        public virtual Menugrupo Menu { get; set; }
        [JsonIgnore]
        public virtual ICollection<Rolpermiso> Rolpermiso { get; set; }
    }
}
