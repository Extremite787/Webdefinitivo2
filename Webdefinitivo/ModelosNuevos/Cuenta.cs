using System;
using System.Collections.Generic;
using Webdefinitivo.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webdefinitivo.ModelosNuevos
{
    public partial class Cuenta
    {
        public string Numero { get; set; }
        public string SaldoTotal { get; set; }
        public string CodigoSocio { get; set; }
        public int Estado { get; set; }

        public virtual Socio CodigoSocioNavigation { get; set; }
    }
}
