using BH.EPotecario.Adm.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Servicios
{
    public interface IExcelServicio
    {
        int RegistrarImportacion(string nombreArchivo, string usuario, string tipoImportacion);
        void ActualizarImportacion(int idImportacion, int totalRegistros, int errores, string estado);
        IList<RegistroExcel> ObtenerFilasExcel(Stream excelStream);
        IList<RegistroExcel> ValidarFilasExcel(IList<RegistroExcel> registros);
        ResultadoImportacion ImportarRegistros(IList<RegistroExcel> registros, string nombreArchivo, string usuario, string tipoImportacion);
        DataTable CrearDataTable();
    }

    public abstract class ExcelServicio : IExcelServicio
    {
        public abstract DataTable CrearDataTable();

        public abstract ResultadoImportacion ImportarRegistros(IList<RegistroExcel> registros, string nombreArchivo, string usuario, string tipoImportacion);

        public abstract IList<RegistroExcel> ObtenerFilasExcel(Stream excelStream);

        public int RegistrarImportacion(string nombreArchivo, string usuario, string tipoImportacion)
        {
            using (SqlConnection connection = Conexion.GetSysNet().DB.Connection.GetConnection() as SqlConnection)
            {
                using (SqlCommand command = new SqlCommand(
                    "INSERT INTO ImportacionesExcel (NombreArchivo, UsuarioImportacion, TipoImportacion, EstadoImportacion) " +
                    "VALUES (@NombreArchivo, @UsuarioImportacion, @TipoImportacion, 'En Proceso'); " +
                    "SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@NombreArchivo", nombreArchivo);
                    command.Parameters.AddWithValue("@UsuarioImportacion", usuario);
                    command.Parameters.AddWithValue("@TipoImportacion", tipoImportacion);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void ActualizarImportacion(int idImportacion, int totalRegistros, int errores, string estado)
        {
            using (SqlConnection connection = Conexion.GetSysNet().DB.Connection.GetConnection() as SqlConnection)
            {
                using (SqlCommand command = new SqlCommand(
                    "UPDATE ImportacionesExcel SET " +
                    "CantidadRegistros = @CantidadRegistros, " +
                    "CantidadErrores = @CantidadErrores, " +
                    "EstadoImportacion = @EstadoImportacion " +
                    "WHERE ImportacionID = @ImportacionID", connection))
                {
                    command.Parameters.AddWithValue("@ImportacionID", idImportacion);
                    command.Parameters.AddWithValue("@CantidadRegistros", totalRegistros);
                    command.Parameters.AddWithValue("@CantidadErrores", errores);
                    command.Parameters.AddWithValue("@EstadoImportacion", estado);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public abstract IList<RegistroExcel> ValidarFilasExcel(IList<RegistroExcel> registros);
    }
}
