using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Dominio
{
    public class ResultadoImportacion
    {
        public int ImportacionID { get; set; }
        public string TipoImportacion { get; set; }
        public int TotalRegistros { get; set; }
        public int RegistrosInsertados { get; set; }
        public int RegistrosActualizados { get; set; }
        public int RegistrosConError { get; set; }
    }
}
