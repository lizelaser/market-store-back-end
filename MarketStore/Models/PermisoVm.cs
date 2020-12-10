using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Models
{
    public class PermisoVm
    {
        public PermisoVm(Permiso Permiso)
        {
            Id = Permiso.Id;
            MenuId = Permiso.MenuId;
            Nombre = Permiso.Nombre;
            Ruta = Permiso.Ruta;
            Children = new List<PermisoVm>();
        }

        public int Id { get; set; }
        public int? MenuId { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public string Ruta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Protegido { get; set; }
        public bool Visible { get; set; }

        public List<PermisoVm> Children { get; set; }

    }
}
