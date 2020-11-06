using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Denominacion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
