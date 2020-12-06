using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolMenu = new HashSet<RolMenu>();
            RolPermiso = new HashSet<RolPermiso>();
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Denominacion { get; set; }

        public virtual ICollection<RolMenu> RolMenu { get; set; }
        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
