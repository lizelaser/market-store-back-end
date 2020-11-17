using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public partial class Direccion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Direccion1 { get; set; }
        public string Numero { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }
        public bool Defecto { get; set; }

        [JsonIgnore]
        public virtual ICollection<Ordencompra> Ordencompra { get; set; }
    }
}
