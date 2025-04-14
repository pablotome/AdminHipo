using BH.EPotecario.Adm.Dominio;
using BH.EPotecario.Adm.Servicios;
using BH.Sysnet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Servicios
{
    public class ImportacionService
    {
        //private readonly IExcelBeneficiosServicio _procesadorBeneficios;
        //private readonly ExcelAlianzaData _alianzaData;
        //private readonly ExcelSucursalData _sucursalData;

        public ImportacionService()
        {
            //_procesadorBeneficios = new ExcelBeneficiosServicio();
            //_alianzaData = new ExcelAlianzaData();
            //_sucursalData = new ExcelSucursalData();
        }

        public int RegistrarImportacion(string nombreArchivo, string usuario, string tipoImportacion)
        {
            using (SysNet sysnet = Conexion.GetSysNet())
            {
                string sql = @"INSERT INTO ImportacionesExcel (NombreArchivo, UsuarioImportacion, TipoImportacion, EstadoImportacion) VALUES (@NombreArchivo, @UsuarioImportacion, @TipoImportacion, 'En Proceso')";

                sysnet.DB.Parameters.Add("@NombreArchivo", nombreArchivo);
                sysnet.DB.Parameters.Add("@UsuarioImportacion", usuario);
                sysnet.DB.Parameters.Add("@TipoImportacion", tipoImportacion);

                return Convert.ToInt32(sysnet.DB.Execute(sql, true));
            }
        }

        // Método para actualizar el estado de una importación
        public void ActualizarImportacion(int importacionId, int totalRegistros, int errores, string estado)
        {
            using (SysNet sysnet = Conexion.GetSysNet())
            {
                string sql = @"UPDATE ImportacionesExcel SET CantidadRegistros = @CantidadRegistros, CantidadErrores = @CantidadErrores, EstadoImportacion = @EstadoImportacion WHERE ImportacionID = @ImportacionID";

                sysnet.DB.Parameters.Add("@ImportacionID", importacionId);
                sysnet.DB.Parameters.Add("@CantidadRegistros", totalRegistros);
                sysnet.DB.Parameters.Add("@CantidadErrores", errores);
                sysnet.DB.Parameters.Add("@EstadoImportacion", estado);

                sysnet.DB.Execute(sql);
            }
        }

        // Método para registrar un error de importación
        public void RegistrarError(int idImportacion, string error)
        {
            using (SysNet sysnet = Conexion.GetSysNet())
            {
                string sql = @"UPDATE ImportacionesExcel SET ErrorImportacion = @ErrorImportacion, EstadoImportacion = @EstadoImportacion WHERE ImportacionID = @ImportacionID";

                sysnet.DB.Parameters.Add("@ErrorImportacion", error);
                sysnet.DB.Parameters.Add("@EstadoImportacion", error);
                sysnet.DB.Parameters.Add("@ImportacionID", idImportacion);

                sysnet.DB.Execute(sql);
            }
        }

        // Método para importar beneficios desde Excel
        public ResultadoImportacion ImportarBeneficiosDesdeExcel(Stream excelStream, string nombreArchivo, string usuario)
        {
            // Registrar la importación
            //IList<RegistroExcelBeneficio> beneficios = _procesadorBeneficios.ValidarTipoCliente .ObtenerFilasExcel(excelStream) as IList<RegistroExcelBeneficio>;
            //
            //_procesadorBeneficios.ValidarFilasExcel((IList<RegistroExcel>)beneficios);

            return new ResultadoImportacion();

            /*

            ResultadoImportacion resultado = new ResultadoImportacion
            {
                ImportacionID = idImportacion,
                TipoImportacion = "Beneficios",
                TotalRegistros = 0,
                RegistrosInsertados = 0,
                RegistrosActualizados = 0,
                RegistrosConError = 0
            };

            try
            {
                // Leer beneficios desde Excel
                List<ExcelBeneficio> beneficios = HelperExcel.LeerBeneficiosDesdeExcel(excelStream, idImportacion);
                resultado.TotalRegistros = beneficios.Count;
                CacheHelper.RefrescarCache();

                // Procesar cada beneficio
                for (int i = 0; i < beneficios.Count; i++)
                {
                    ExcelBeneficio beneficio = beneficios[i];
                    try
                    {
                        // Insertar o actualizar beneficio
                        bool esNuevo = _beneficioData.InsertarBeneficio(beneficio);

                        if (esNuevo)
                            resultado.RegistrosInsertados++;
                        else
                            resultado.RegistrosActualizados++;
                    }
                    catch (Exception ex)
                    {
                        // Registrar error de procesamiento
                        resultado.RegistrosConError++;
                        _beneficioData.RegistrarError(
                            idImportacion,
                            i + 2,
                            ex.Message,
                            $"{beneficio.Titulo},{beneficio.BreveResumen},{beneficio.Ahorros}");
                    }
                }

                // Actualizar estado de importación
                _beneficioData.ActualizarImportacion(
                    idImportacion,
                    resultado.TotalRegistros,
                    resultado.RegistrosConError,
                    resultado.RegistrosConError == 0 ? "Completado" : "Completado con errores");
            }
            catch (Exception ex)
            {
                // Registrar error general
                _beneficioData.RegistrarError(idImportacion, 0, ex.Message, "Error general de procesamiento");
                _beneficioData.ActualizarImportacion(idImportacion, 0, 1, "Error");

                throw; // Re-lanzar para manejo en la capa de presentación
            }

            return resultado;*/
        }
    }
}
