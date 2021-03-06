using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Sucursal
    {
        public int SUC_ID { get; set; }

        public string SUC_NOMBRE { get; set; }
        public string SUB_UBICACION { get; set; }

        public string SUC_CORREO { get; set; }

        public int SUC_TELEFONO { get; set; }
    }
}