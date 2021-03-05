using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Alquiler
    {

        public int ALQ_ID { get; set; }
        public int USU_CODIGO { get; set; }
        public int VEH_ID { get; set; }
        public int PAGO_ID { get; set; }
        public DateTime ALQ_FECHA_ENTREGA { get; set; }
        public DateTime ALQ_FECHA_ALQUILER { get; set; }
        public decimal ALQ_PRECIOXHORA { get; set; }

    }
}