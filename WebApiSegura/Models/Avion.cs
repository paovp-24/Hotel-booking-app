using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Avion
    {
        public int AV_ID { get; set; }
        public int AERO_ID { get; set; }
        public int AV_CAPACIDAD_TOTAL { get; set; }
        public string AV_MARCA { get; set; }
        public string AV_TIPO_AVION { get; set; }
        public string AV_MODELO { get; set; }
    }

}