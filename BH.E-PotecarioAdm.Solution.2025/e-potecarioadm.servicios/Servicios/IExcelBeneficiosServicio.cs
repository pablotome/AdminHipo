using BH.EPotecario.Adm.Dominio;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using static BH.EPotecario.Adm.Dominio.RegistroExcelBeneficio;

namespace BH.EPotecario.Adm.Servicios
{
    public interface IExcelBeneficiosServicio : IExcelServicio
    {
        bool ValidarTipoCliente(RegistroExcelBeneficio registroExcelBeneficio);
    }

    public class ExcelBeneficiosServicio : ExcelServicio, IExcelBeneficiosServicio
    {
        private readonly IRepositorioParametria _repository;
        private readonly IList<TipoCliente> _tiposCliente;
        private readonly IList<MedioDePago> _mediosPago;
        private readonly IList<DiaSemana> _diasSemana;
        private readonly IList<Ahorro> _ahorros;
        private readonly IList<Cuota> _cuotas;

        public ExcelBeneficiosServicio(IRepositorioParametria repository)
        {
            _repository = repository;
            _tiposCliente = _repository.TiposClientes();
            _mediosPago = _repository.MediosDePago();
            _diasSemana = _repository.DiasSemana();
            _ahorros = _repository.Ahorros();
            _cuotas = _repository.Cuotas();
        }

        public override IList<RegistroExcel> ObtenerFilasExcel(Stream excelStream)
        {
            //List<RegistroExcelBeneficio> beneficios = new List<RegistroExcelBeneficio>();
            List<RegistroExcel> beneficios = new List<RegistroExcel>();

            // Crear el libro de trabajo desde el stream
            IWorkbook workbook = new XSSFWorkbook(excelStream);

            // Obtener la primera hoja
            ISheet sheet = workbook.GetSheetAt(0);

            // Determinar el rango de datos
            int startRow = 1; // Asumiendo que la fila 1 contiene encabezados
            int endRow = sheet.LastRowNum;

            // Leer cada fila
            for (int i = startRow; i <= endRow; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null)
                    continue;

                beneficios.Add(new RegistroExcelBeneficio
                {
                    Titulo = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_TITULO)),
                    Clientes = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_CLIENTES)),
                    Ahorros = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_AHORROS)),
                    Cuotas = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_CUOTAS)),
                    MediosPago = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_MEDIOSPAGO)),
                    Dias = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_DIAS)),
                    CFT = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_CFT)),
                    BasesYCondiciones = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_BASESYCONDICIONES)),
                    TopeReintegro = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_TOPEREINTEGRO)),
                    BreveResumen = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_BREVERESUMEN)),
                    TipoDescuento = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_TIPODESCUENTO)),
                    Prioridad = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_PRIORIDAD)),
                    ID_BEN = HelperExcel.GetCellValueAsString(row.GetCell(EstructuraExcelBeneficios.COL_ID_BEN)),
                    //IdImportacion = idImportacion
                });
            }

            return beneficios;
        }

        public override IList<RegistroExcel> ValidarFilasExcel(IList<RegistroExcel> registros)
        {
            foreach (RegistroExcelBeneficio registro in registros)
            {
                registro.ClientesValido = ValidarTipoCliente(registro);
                registro.MediosDePagoValido = ValidarMedioPago(registro);
                registro.DiasValido = ValidarDiaSemana(registro);
                registro.AhorroValido = ValidarAhorro(registro);
                registro.CuotasValido = ValidarCuota(registro);
            }

            return registros;

        }

        public override ResultadoImportacion ImportarRegistros(IList<RegistroExcel> registros, string nombreArchivo, string usuario, string tipoImportacion)
        {
            int idImportacion, cantidadErrores;
            ResultadoImportacion resultado = new ResultadoImportacion();
            idImportacion = cantidadErrores = 0;

            try
            {
                idImportacion = RegistrarImportacion(nombreArchivo, usuario, tipoImportacion);

                using (SqlConnection connection = Conexion.GetSysNet().DB.Connection.GetConnection() as SqlConnection)
                {
                    connection.Open();

                    DataTable dtBeneficiosExcel = CrearDataTable();

                    foreach (RegistroExcelBeneficio beneficio in registros)
                    {
                        DataRow dr = dtBeneficiosExcel.NewRow();

                        dr["Titulo"] = beneficio.Titulo;
                        dr["Clientes"] = beneficio.Clientes;
                        dr["Ahorros"] = beneficio.Ahorros;
                        dr["Cuotas"] = beneficio.Cuotas;
                        dr["MediosPago"] = beneficio.MediosPago;
                        dr["Dias"] = beneficio.Dias;
                        dr["CFT"] = beneficio.CFT;
                        dr["BasesYCondiciones"] = beneficio.BasesYCondiciones;
                        dr["TopeReintegro"] = beneficio.TopeReintegro;
                        dr["BreveResumen"] = beneficio.BreveResumen;
                        dr["TipoDescuento"] = beneficio.TipoDescuento;
                        dr["Prioridad"] = beneficio.Prioridad;
                        dr["ID_BEN"] = beneficio.ID_BEN;
                        dr["IdImportacion"] = beneficio.IdImportacion = idImportacion;
                        dr["RegistroValido"] = beneficio.RegistroValido;
                        dr["DetalleError"] = beneficio.DetalleError;

                        dtBeneficiosExcel.Rows.Add(dr);
                    }

                    dtBeneficiosExcel.AcceptChanges();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "dbo.ExcelBeneficio";

                        try
                        {
                            // Write unchanged rows from the source to the destination.
                            bulkCopy.WriteToServer(dtBeneficiosExcel, DataRowState.Unchanged);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                cantidadErrores = registros.Where(r => r.RegistroValido.Equals(false)).Count();

                resultado.ImportacionID = idImportacion;
                resultado.TipoImportacion = tipoImportacion;
                resultado.TotalRegistros = registros.Count;
                resultado.RegistrosInsertados = registros.Count;
                resultado.RegistrosActualizados = 0;
                resultado.RegistrosConError = cantidadErrores;
        
                ActualizarImportacion(idImportacion, registros.Count, cantidadErrores, cantidadErrores == 0 ? "Completado" : "Completado con errores");
            }
            catch (Exception ex)
            {
                ActualizarImportacion(idImportacion, registros.Count, cantidadErrores, ex.Message);
            }

            return resultado;
        }

        public override DataTable CrearDataTable()
        {
            DataTable dtBeneficiosExcel = new DataTable("AlianzasExcel");

            dtBeneficiosExcel.Columns.Add(new DataColumn("CodBeneficio", typeof(int)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("Titulo", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("Clientes", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("Ahorros", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("Cuotas", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("MediosPago", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("Dias", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("CFT", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("BasesYCondiciones", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("TopeReintegro", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("BreveResumen", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("TipoDescuento", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("Prioridad", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("ID_BEN", typeof(string)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("IdImportacion", typeof(int)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("RegistroValido", typeof(bool)));
            dtBeneficiosExcel.Columns.Add(new DataColumn("DetalleError", typeof(string)));

            return dtBeneficiosExcel;
        }

        public bool ValidarTipoCliente(RegistroExcelBeneficio registroExcelBeneficio)
        {
            string[] clientes = registroExcelBeneficio.Clientes.Split(",".ToCharArray());
            //List<TipoCliente> clientesParam = CacheHelper.GetOrAddToCache("TiposClientes", () => CacheHelper.TiposClientes(), TimeSpan.FromMinutes(30));
            bool clienteAusente = false;

            foreach (string cliente in clientes)
            {
                if (_tiposCliente.FirstOrDefault(c => c.Slug == cliente.Trim().ToLower()) == null)
                    clienteAusente = true;
            }
            return !clienteAusente;
        }

        public bool ValidarMedioPago(RegistroExcelBeneficio registroExcelBeneficio)
        {
            string[] mediosPago = registroExcelBeneficio.MediosPago.Split(",".ToCharArray());
            //List<MedioDePago> medioDePagoParam = CacheHelper.GetOrAddToCache("MediosDePago", () => CacheHelper.MediosDePago(), TimeSpan.FromMinutes(30));
            bool medioDePagoAusente = false;

            foreach (string medioPago in mediosPago)
            {
                if (_mediosPago.FirstOrDefault(m => m.Slug == medioPago.Trim().ToLower()) == null)
                    medioDePagoAusente = true;
            }
            return !medioDePagoAusente;
        }

        public bool ValidarDiaSemana(RegistroExcelBeneficio registroExcelBeneficio)
        {
            string[] dias = registroExcelBeneficio.Dias.Split(",".ToCharArray());
            //List<DiaSemana> diasParam = CacheHelper.GetOrAddToCache("DiasSemana", () => CacheHelper.DiasSemana(), TimeSpan.FromMinutes(30));
            bool diaAusente = false;

            foreach (string dia in dias)
            {
                if (_diasSemana.FirstOrDefault(m => m.Slug == dia.Trim().ToLower()) == null)
                    diaAusente = true;
            }
            return !diaAusente;
        }

        public bool ValidarAhorro(RegistroExcelBeneficio registroExcelBeneficio)
        {
            //List<Ahorro> ahorrosParam = CacheHelper.GetOrAddToCache("Ahorros", () => CacheHelper.Ahorros(), TimeSpan.FromMinutes(30));
			return _ahorros.FirstOrDefault(m => m.Nombre == registroExcelBeneficio.Ahorros.Trim().ToLower()) != null;
        }

        public bool ValidarCuota(RegistroExcelBeneficio registroExcelBeneficio)
        {
            return _cuotas.FirstOrDefault(m => m.Nombre == registroExcelBeneficio.Cuotas.Trim().ToLower()) != null;
        }
    }

}
