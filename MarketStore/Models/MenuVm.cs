using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Models
{
    public class MenuVm
    {
        public MenuVm(Menu menu)
        {
            Id = menu.Id;
            PermisoId = menu.PermisoId;
            Nombre = menu.Nombre;
            Ruta = menu.Ruta;
            Icono = menu.Icono;
            Nivel = menu.Nivel;
            Visible = menu.Visible;
            Children = new List<MenuVm>();
        }

        public int Id { get; set; }
        public int PermisoId { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Icono { get; set; }
        public int? Nivel { get; set; }
        public bool Visible { get; set; }

        public List<MenuVm> Children { get; set; }

    }
}
