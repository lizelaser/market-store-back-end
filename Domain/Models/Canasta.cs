using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Canasta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime? FechaMod { get; set; }
        public DateTime? FechaFin { get; set; }

        [JsonIgnore]
        public virtual ICollection<Canastadetalle> Canastadetalle { get; set; }
    }
}
