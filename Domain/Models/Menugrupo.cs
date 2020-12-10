using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class Menugrupo
    {
        public Menugrupo()
        {
            Permiso = new HashSet<Permiso>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }

        public virtual ICollection<Permiso> Permiso { get; set; }
    }
}
