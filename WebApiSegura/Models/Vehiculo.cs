using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Vehiculo
    {
        public int VEH_ID { get; set; }
        public int SUC_ID { get; set; }
        public string VEH_PLACA { get; set; }
        public string VEH_MARCA { get; set; }
        public string VEH_MODELO { get; set; }
        public string VEH_ESTADO { get; set; }
        public string VEH_TIPO { get; set; }
        public string VEH_TRACCION { get; set; }
        public int VEH_CANT_PASAJEROS { get; set; }
        public string VEH_TRANSMISION { get; set; }
    }
}