using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Dominio
{
    public class RegistroExcelSucursal : RegistroExcel
    {
        public class EstructuraExcelSucursales
        {
            public const int COL_TITULO = 0;
            public const int COL_CALLE = 1;
            public const int COL_NUMERO = 2;
            public const int COL_LOCALIDAD = 3;
            public const int COL_CODIGOPOSTAL = 4;
            public const int COL_PROVINCIA = 5;
            public const int COL_LATITUD = 6;
            public const int COL_LONGITUD = 7;
            public const int COL_INFOADICIONAL = 8;
            public const int COL_IDALIANZAS = 9;
            public const int COL_ID = 10;
            public const int COL_IDIMPORTACION = 11;
        }

        public string Titulo { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Provincia { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string InfoAdicional { get; set; }
        public string IDAlianzas { get; set; }
        public string ID { get; set; }
        //public int IdImportacion { get; set; }

        private bool tituloValido;
        private bool calleValida;
        private bool numeroValido;
        private bool localidadValido;
        private bool codigoPostalValido;
        private bool provinciaValido;
        private bool latitudValida;
        private bool longitudValida;
        private bool idAlianzasValido;
        private bool direccion1Valida;  // si completó calle, número código postal, localidad y provincia, la DIRECCION1 es válida
        private bool direccion2Valida;  // si latitud y longitud, la DIRECCION2 es válida
        private bool direccionValida;   // la dirección es válida si completó DIRECCION1 ó DIRECCION2

        private string alianzaInexistente;

        // Para validación y manejo de errores
        public override bool RegistroValido
        {
            get
            {
                tituloValido = !string.IsNullOrEmpty(Titulo);
                calleValida = !string.IsNullOrEmpty(Calle);
                numeroValido = !string.IsNullOrEmpty(Numero) && Regex.IsMatch(Latitud, @"^\d+$");
                codigoPostalValido = !string.IsNullOrEmpty(CodigoPostal);
                localidadValido = !string.IsNullOrEmpty(Localidad);
                provinciaValido = !string.IsNullOrEmpty(Provincia);
                direccion1Valida = calleValida && numeroValido && codigoPostalValido && localidadValido && provinciaValido;
                latitudValida = string.IsNullOrEmpty(Latitud) || (!string.IsNullOrEmpty(Latitud) && Regex.IsMatch(Latitud, @"^-?\d+\.\d+$"));
                longitudValida = string.IsNullOrEmpty(Longitud) || (!string.IsNullOrEmpty(Longitud) && Regex.IsMatch(Longitud, @"^-?\d+\.\d+$"));
                direccion2Valida = latitudValida && longitudValida;
                direccionValida = direccion1Valida || direccion2Valida;
                idAlianzasValido = !string.IsNullOrEmpty(IDAlianzas) && EsAlianzaValido;

                return
                    tituloValido && direccionValida && idAlianzasValido;
            }
        }

        public override string DetalleError
        {
            get
            {
                string errores = string.Empty;

                if (!tituloValido)
                    errores += "El nombre de la sucursal es obligatorio.";

                if (!direccionValida)
                {
                    if (!direccion1Valida && !direccion2Valida)
                        errores += "Es necesario completar el domicilio correctamente. O se completa calle, número, código postal, localidad y provincia; o se completa latitud y longitud";
                }

                if (!idAlianzasValido)
                {
                    if (string.IsNullOrEmpty(alianzaInexistente))
                        errores += "Se debe ingresar una lista de códigos de alianza válidos.";
                    else
                        errores += $"Estas alianzas no existen: {alianzaInexistente}.";
                }

                return errores;
            }
        }

        public bool EsAlianzaValido
        {
            get
            {
                string[] alianzas = IDAlianzas.Split(",".ToCharArray()).Select(s => s.Trim()).ToArray();
                alianzaInexistente = string.Empty;

                foreach (string alianza in alianzas)
                {
                    if (!Regex.IsMatch(alianza, @"^\d+$"))
                        return false;
                }

                List<Alianza> alianzasExistentes = CacheHelper.GetOrAddToCache("AlianzasExistentes", () => CacheHelper.Alianzas(), TimeSpan.FromMinutes(30));
                foreach (string alianza in alianzas)
                {
                    if (alianzasExistentes.FirstOrDefault(a => a.CodAlianza == int.Parse(alianza)) == null)
                    {
                        alianzaInexistente += $"{alianza}, ";
                    }
                }
                return string.IsNullOrEmpty(alianzaInexistente);
            }
        }
    }
}
