using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Direccion
    {
        public Direccion()
        {
            Ordencompra = new HashSet<Ordencompra>();
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Direccion1 { get; set; }
        public string Numero { get; set; }
        public string Telefono { get; set; }
        public string CodigoPostal { get; set; }
        public string Referencia { get; set; }
        public bool Defecto { get; set; }

        public virtual ICollection<Ordencompra> Ordencompra { get; set; }
    }
}
