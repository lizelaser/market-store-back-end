using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Canastadetalle
    {
        public int Id { get; set; }
        public int CanastaId { get; set; }
        public int ProductoId { get; set; }

        public virtual Canasta Canasta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
