using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdefinitivo.Models
{
    public class Cuenta
    {
        public string Numero { get; set; }
        public string SaldoTotal { get; set; }
        public string CodigoSocio { get; set; }
        public int Estado { get; set; }

        public virtual Socio CodigoSocioNavigation { get; set; }
    }
}
