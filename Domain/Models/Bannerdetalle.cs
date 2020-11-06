using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Bannerdetalle
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public int ProductoId { get; set; }

        public virtual Banner Banner { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
