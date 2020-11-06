using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Menurol = new HashSet<Menurol>();
        }

        public int Id { get; set; }
        public string Denominacion { get; set; }

        public virtual ICollection<Menurol> Menurol { get; set; }
    }
}
