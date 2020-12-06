using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Domain.Models
{
    public partial class Canastadetalle
    {
        public int Id { get; set; }
        public int CanastaId { get; set; }
        public int ProductoId { get; set; }

        [JsonIgnore]
        public virtual Canasta Canasta { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
