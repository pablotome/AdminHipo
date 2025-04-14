using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Dominio
{
    public abstract class RegistroExcel
    {
        public virtual int IdImportacion { get; set; }
        public virtual bool RegistroValido { get; set; }
        public virtual string DetalleError { get; set; }
    }
}
