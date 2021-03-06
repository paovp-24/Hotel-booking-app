using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Aeropuerto
    {
        public int AERO_ID { get; set; }
        public string AERO_NOMBRE { get; set; }
        public string AERO_PAIS { get; set; }
        public string AERO_CIUDAD { get; set; }
        public string AERO_TIPO { get; set; }
    }
}