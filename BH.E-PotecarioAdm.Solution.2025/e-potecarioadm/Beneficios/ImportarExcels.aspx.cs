using BH.EPotecario.Adm.Dominio;
using BH.EPotecario.Adm.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BH.EPotecario.Adm
{
	public partial class ImportarExcels : System.Web.UI.Page
	{
        private readonly IExcelBeneficiosServicio _excelBeneficiosServicio;

        public ImportarExcels()
        {
            _excelBeneficiosServicio = (IExcelBeneficiosServicio)Global.ServiceProvider.GetService(typeof(IExcelBeneficiosServicio));
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            ((Master)this.Master).CurrentMenuItem = "admImportarExcels";
            ((Master)this.Master).TituloPagina = "Importar Datos desde Excel";

        }

        private void MostrarResultado(bool exito, string titulo, string detalle)
        {
            PanelResultado.Visible = true;
            PanelResultado.CssClass = exito ? "result-container success" : "result-container error";
            LitResultadoTitulo.Text = titulo;
            LitResultadoDetalle.Text = detalle;
        }

        protected void BtnImportar_Click(object sender, EventArgs e)
        {
            if (FileUploadExcel.HasFile)
            {
                try
                {
                    string fileExtension = Path.GetExtension(FileUploadExcel.FileName).ToLower();

                    if (fileExtension != ".xlsx")
                    {
                        MostrarResultado(false, "Error", "Por favor, seleccione un archivo Excel (.xlsx)");
                        return;
                    }

                    string tipoImportacion = DropDownTipoImportacion.SelectedValue;
                    ResultadoImportacion resultado = null;
                    IList<RegistroExcel> registros = null;

                    // Guardar el archivo temporalmente para evitar problemas con el stream
                    string tempFilePath = Path.GetTempFileName();
                    FileUploadExcel.SaveAs(tempFilePath);

                    try
                    {
                        // Abrir el archivo como un stream
                        using (FileStream stream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read))
                        {
                           // int tipoArchivoExcel = (int)HelperExcel.ObtenerTipoArchivoExcel(stream);

                            switch (tipoImportacion)
                            {
                                case "Beneficios":
                                    registros = _excelBeneficiosServicio.ObtenerFilasExcel(stream);
                                    registros = _excelBeneficiosServicio.ValidarFilasExcel(registros);
                                    resultado = _excelBeneficiosServicio.ImportarRegistros(registros, FileUploadExcel.FileName, User.Identity.Name ?? "Sistema", "Beneficios");
                                    /*resultado = _excelBeneficiosServicio.ObtenerFilasExcel(
                                        stream, FileUploadExcel.FileName, User.Identity.Name ?? "Sistema");*/
                                    break;

                                /*case "Alianzas":
                                    resultado = _importacionService.ImportarAlianzasDesdeExcel(
                                        stream, FileUploadExcel.FileName, User.Identity.Name ?? "Sistema");
                                    break;

                                case "Sucursales":
                                    resultado = _importacionService.ImportarSucursalesDesdeExcel(
                                        stream, FileUploadExcel.FileName, User.Identity.Name ?? "Sistema");
                                    break;*/

                                default:
                                    MostrarResultado(false, "Error", "Tipo de importación no válido");
                                    return;
                            }
                        }
                    }
                    finally
                    {
                        // Eliminar el archivo temporal
                        if (File.Exists(tempFilePath))
                        {
                            File.Delete(tempFilePath);
                        }
                    }

                    // Mostrar resultado
                    bool exito = resultado.RegistrosConError == 0;
                    string titulo = exito ? "Importación Exitosa" : "Importación Completada con Errores";
                    string detalle = $@"
                    <ul>
                        <li>Tipo de importación: {resultado.TipoImportacion}</li>
                        <li>ImportacionID: {resultado.ImportacionID}</li>
                        <li>Total de registros procesados: {resultado.TotalRegistros}</li>
                        <li>Registros insertados: {resultado.RegistrosInsertados}</li>
                        <li>Registros actualizados: {resultado.RegistrosActualizados}</li>
                        <li>Registros con error: {resultado.RegistrosConError}</li>
                    </ul>";

                    MostrarResultado(exito, titulo, detalle);

                    // Recargar datos
                    //ActualizarDatos();
                }
                catch (Exception ex)
                {
                    MostrarResultado(false, "Error en la Importación", $"Se produjo un error: {ex.Message}");
                }
            }
            else
            {
                MostrarResultado(false, "Error", "Por favor, seleccione un archivo Excel");
            }
        }

        protected void lbImportacionesBeneficios_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void lbImportacionesAlianzas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}