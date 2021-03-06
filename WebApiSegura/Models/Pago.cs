using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Pago
    {
        public int PAGO_ID { get; set; }

        public DateTime PAGO_FECHA { get; set; }
        public decimal PAGO_TOTAL { get; set; }

        public string PAGO_ESTADO { get; set; }

        public string PAGO_DESCRIPCION { get; set; }


    }
}