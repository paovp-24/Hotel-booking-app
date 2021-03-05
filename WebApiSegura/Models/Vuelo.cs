using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Vuelo
    {
        public int VUE_ID { get; set; }
        public int AV_ID { get; set; }
        public string VUE_ORIGEN { get; set; }
        public string VUE_DESTINO { get; set; }
        public int VUE_CANT_PASAJEROS { get; set; }
    }
}