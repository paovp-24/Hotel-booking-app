using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Boleto
    {
        public int BOL_ID { get; set; }
        public int USU_CODIGO { get; set; }
        public int VUE_ID { get; set; }
        public int PAGO_ID { get; set; }
        public DateTime BOL_FEC_IDA { get; set; }
        public DateTime BOL_FEC_VUELTA { get; set; }
        public decimal BOL_PRECIO { get; set; }
        public string BOL_ASIENTO { get; set; }
        public string BOL_TERMINAL { get; set; }
    }
}