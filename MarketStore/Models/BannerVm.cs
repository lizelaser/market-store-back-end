using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Models
{
    public class BannerVm
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Link { get; set; }
        public DateTime FechaReg { get; set; }
        public int ProductoId { get; set; }
    }
}
