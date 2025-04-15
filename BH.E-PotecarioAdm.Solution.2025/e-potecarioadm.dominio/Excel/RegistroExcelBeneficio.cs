using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Dominio
{
    public class RegistroExcelBeneficio : RegistroExcel
    {
        public class EstructuraExcelBeneficios
        {
            public const int COL_TITULO = 0;
            public const int COL_CLIENTES = 1;
            public const int COL_AHORROS = 2;
            public const int COL_CUOTAS = 3;
            public const int COL_MEDIOSPAGO = 4;
            public const int COL_DIAS = 5;
            public const int COL_CFT = 6;
            public const int COL_BASESYCONDICIONES = 7;
            public const int COL_TOPEREINTEGRO = 8;
            public const int COL_BREVERESUMEN = 9;
            public const int COL_TIPODESCUENTO = 10;
            public const int COL_PRIORIDAD = 11;
            public const int COL_ID_BEN = 12;
            public const int COL_IDIMPORTACION = 13;
        }

        public int CodBeneficio { get; set; }
        public string Titulo { get; set; }
        public string Clientes { get; set; }
        public string Ahorros { get; set; }
        public string Cuotas { get; set; }
        public string MediosPago { get; set; }
        public string Dias { get; set; }
        public string CFT { get; set; }
        public string BasesYCondiciones { get; set; }
        public string TopeReintegro { get; set; }
        public string BreveResumen { get; set; }
        public string TipoDescuento { get; set; }
        public string Prioridad { get; set; }
        public string ID_BEN { get; set; }
        //public int IdImportacion { get; set; }

        public bool TituloValido { get; set; }
        public bool ClientesValido { get; set; }
        public bool AhorroValido { get; set; }
        public bool CuotasValido { get; set; }
        public bool MediosDePagoValido { get; set; }
        public bool DiasValido { get; set; }
        public bool CFTValido { get; set; }
        public bool TopeReintegroValido { get; set; }
        public bool PrioridadValido { get; set; }

        public string BasesYCondicionesGrilla
        {
            get
            {
                if (string.IsNullOrEmpty(BasesYCondiciones))
                    return string.Empty;
                else if (BasesYCondiciones.Length <= 10)
                    return BasesYCondiciones;
                else
                    return $"{BasesYCondiciones.Substring(0, 10)}...";
            }
        }

        // Para validación y manejo de errores
        public override bool RegistroValido
        {
            get
            {
                TituloValido = !string.IsNullOrEmpty(Titulo);
                ClientesValido = !string.IsNullOrEmpty(Clientes) && ClientesValido;
                AhorroValido = string.IsNullOrEmpty(Ahorros) || (!string.IsNullOrEmpty(Ahorros) && Regex.IsMatch(Ahorros, @"^\d+(\.\d+)?\s?%$") && AhorroValido);
                CuotasValido = string.IsNullOrEmpty(Cuotas) || (!string.IsNullOrEmpty(Cuotas) && Regex.IsMatch(Cuotas, @"^\d+$") && CuotasValido);
                MediosDePagoValido = !string.IsNullOrEmpty(MediosPago) && MediosDePagoValido;
                DiasValido = !string.IsNullOrEmpty(Dias) && DiasValido;
                CFTValido = string.IsNullOrEmpty(CFT) || (!string.IsNullOrEmpty(CFT) && Regex.IsMatch(CFT, @"^\d+(\.\d+)?\s?%$"));
                TopeReintegroValido = string.IsNullOrEmpty(TopeReintegro) || (!string.IsNullOrEmpty(TopeReintegro) && Regex.IsMatch(TopeReintegro, @"^\d+$"));
                PrioridadValido = string.IsNullOrEmpty(Prioridad) || (!string.IsNullOrEmpty(Prioridad) && Regex.IsMatch(Prioridad, @"^\d+$"));

                return TituloValido && ClientesValido && MediosDePagoValido && DiasValido && CFTValido && TopeReintegroValido && PrioridadValido;
            }
        }

        public override string DetalleError
        {
            get
            {
                string errores = string.Empty;

                if (!TituloValido)
                    errores += "El título del beneficio es obligatorio.";

                if (!ClientesValido)
                    errores += "El cliente debe ser un dato válido.";

                if (!AhorroValido)
                    errores += "El ahorro debe ser un dato válido, dentro de los parametrizados.";

                if (!CuotasValido)
                    errores += "La cantidad de cuotas ser un dato válido, dentro de los parametrizados.";

                if (!MediosDePagoValido)
                    errores += "El medio de pago debe ser un dato válido.";

                if (!DiasValido)
                    errores += "El día debe ser un dato válido.";

                if (!CFTValido)
                    errores += "El cft debe ser vacío o un número seguido de \"%\".";

                if (!TopeReintegroValido)
                    errores += "El tope de reintegro debe ser vacío o un número entero.";

                if (!PrioridadValido)
                    errores += "La prioridad debe ser vacío o un dato válido.";

                return errores;
            }
        }

        /*
        private bool EsTipoClienteValido
        {
            get
            {
                string[] clientes = Clientes.Split(",".ToCharArray());
                List<TipoCliente> clientesParam = CacheHelper.GetOrAddToCache("TiposClientes", () => CacheHelper.TiposClientes(), TimeSpan.FromMinutes(30));
                bool clienteAusente = false;

                foreach (string cliente in clientes)
                {
                    if (clientesParam.FirstOrDefault(c => c.Slug == cliente.Trim().ToLower()) == null)
                        clienteAusente = true;
                }
                return !clienteAusente;

            }
        }

        private bool EsMedioPagoValido
        {
            get
            {
                string[] mediosPago = MediosPago.Split(",".ToCharArray());
                List<MedioDePago> medioDePagoParam = CacheHelper.GetOrAddToCache("MediosDePago", () => CacheHelper.MediosDePago(), TimeSpan.FromMinutes(30));
                bool medioDePagoAusente = false;

                foreach (string medioPago in mediosPago)
                {
                    if (medioDePagoParam.FirstOrDefault(m => m.Slug == medioPago.Trim().ToLower()) == null)
                        medioDePagoAusente = true;
                }
                return !medioDePagoAusente;

            }
        }

        private bool EsDiaSemanaValido
        {
            get
            {
                string[] dias = Dias.Split(",".ToCharArray());
                List<DiaSemana> diasParam = CacheHelper.GetOrAddToCache("DiasSemana", () => CacheHelper.DiasSemana(), TimeSpan.FromMinutes(30));
                bool diaAusente = false;

                foreach (string dia in dias)
                {
                    if (diasParam.FirstOrDefault(m => m.Slug == dia.Trim().ToLower()) == null)
                        diaAusente = true;
                }
                return !diaAusente;
            }
        }

        public bool EsAhorroValido
        {
            get
            {
                List<Ahorro> ahorrosParam = CacheHelper.GetOrAddToCache("Ahorros", () => CacheHelper.Ahorros(), TimeSpan.FromMinutes(30));
                return ahorrosParam.FirstOrDefault(m => m.Nombre == Ahorros.Trim().ToLower()) != null;
            }
        }

        public bool EsCuotaValida
        {
            get
            {
                List<Cuota> cuotasParam = CacheHelper.GetOrAddToCache("Cuotas", () => CacheHelper.Cuotas(), TimeSpan.FromMinutes(30));
                return cuotasParam.FirstOrDefault(m => m.Nombre == Cuotas.Trim().ToLower()) != null;
            }
        }
        */
    }
}
