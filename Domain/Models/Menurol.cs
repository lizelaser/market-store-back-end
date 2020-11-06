using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Menurol
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int RolId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
