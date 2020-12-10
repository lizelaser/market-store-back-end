using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


#nullable disable

namespace Domain.Models
{
    public partial class Bannerdetalle
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public int ProductoId { get; set; }

        [JsonIgnore]
        public virtual Banner Banner { get; set; }

        [JsonIgnore]
        public virtual Producto Producto { get; set; }
    }
}
