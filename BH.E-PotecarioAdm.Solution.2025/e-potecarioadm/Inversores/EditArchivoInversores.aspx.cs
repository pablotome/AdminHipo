using MicroSitios.Componentes;
using MicroSitios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace BH.EPotecario.Adm.Inversores
{
    public partial class EditArchivoInversores :WebFormBase
    {

        public int CodArchivo
        {
            get
            {
                return (Request.QueryString["CodArchivo"] != null) ? int.Parse(Request.QueryString["CodArchivo"]) : 0;
            }
        }

        public bool Eliminar
        {
            get
            {
                return (Request.QueryString["Eliminar"] != null) ? Convert.ToBoolean(Request.QueryString["Eliminar"]) : false;
            }
        }

        private SeccionInversores Seccion
        {
           
            get {

                SeccionInversores seccion=null;

                int seccionId=0;//se podria poner en otro lado 

                var Archivo = ServicioSistema<ArchivosInversores>.GetById(x => x.CodArchivo == CodArchivo);

                if (Archivo != null)
                {
                    if (Archivo.Seccion.Idioma == 1)
                    {
                        seccionId = IdSeccion;
                    }
                    else
                    {
                        seccionId = IdSeccionEN;
                    }
                     seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == seccionId);
                  
                }else
                {
                    if (Idioma == 1)
                    { 
                         seccionId = IdSeccion;
                    }
                    else
                    {
                        seccionId = IdSeccionEN;
                    }
                    seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == seccionId);
                }
                

                                
                return seccion;
            }
        }

        private int IdSeccion
        {
            get
            {
                return (Request.QueryString["IdSeccion"] != null) ? int.Parse(Request.QueryString["IdSeccion"]) : 0;
            }
        }

        private int IdSeccionEN
        {
            get
            {
                var idSeccionEN = (Request.QueryString["IdSeccionEN"] != null) ? int.Parse(Request.QueryString["IdSeccionEN"]) : 0;

                return idSeccionEN;
            }
        }

        private int Idioma
        {
            get {

                var idioma = (Request.QueryString["Idioma"] != null) ? int.Parse(Request.QueryString["Idioma"]) : 0;

                return idioma;
            }
        
        }

        #region Manejadores

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");
            this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admInversores";
             lblTitulo.Text ="Adm. "+Seccion.DesSeccion.ToString();
            if (!Page.IsPostBack)
            {
                CargarControles();

            }

        }

        public void CargarArchivo()
        {
            if (CodArchivo != 0)
            {
                var Archivo = ServicioSistema<ArchivosInversores>.GetById(x => x.CodArchivo == CodArchivo);
                //no editable
                //txtNombre.Text = Archivo.NombreArchivo;
                //no editable
                ddAño.SelectedValue = Archivo.Anio.ToString();
                //editable
                txtTitulo.Text = Archivo.Titulo;
                //editable
                txtCopete.Text = Archivo.Copete;
                //no editable
                txtRuta.Text = Archivo.Ruta;
                //no editable
                //txtTamaño.Text = Archivo.Tamaño;
                ////no editable
                //FuArchivo.Enabled = false;
            }
        
        }

        public void CargamosDatos()
        {
            try
            {
                if (CodArchivo != 0)
                {
                    #region Edicion
                    var Archivo = ServicioSistema<ArchivosInversores>.GetById(x => x.CodArchivo == CodArchivo);

                    if (Eliminar)
                    {
                        ServicioSistema<ArchivosInversores>.Delete(Archivo);

                        MostrarAlertConfirm("Esta Seguro de Eliminar El Item?");

                        Response.Redirect(string.Format("AdmArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}", IdSeccion.ToString(), IdSeccionEN.ToString()));

                    }
                    else
                    {
                        HttpFileCollection fileCollection = Request.Files;

                        if (!string.IsNullOrEmpty(fileCollection[0].FileName))
                        {

                            for (int i = 0; i < fileCollection.Count; i++)
                            {
                                HttpPostedFile uploadfile = fileCollection[i];

                                var nombreArchivo = uploadfile.FileName;

                                var ruta = Archivo.Seccion.Directorio.ToString();

                                var rutaCompleta = ruta.TrimEnd() + nombreArchivo.TrimStart();
                                GuardarArchivo(uploadfile, ruta, rutaCompleta, i);
                                Archivo.Anio = Convert.ToInt32(ddAño.SelectedValue.ToString());
                                Archivo.Titulo = txtTitulo.Text.ToString();
                                Archivo.Copete = txtCopete.Text.ToString();
                                Archivo.Ruta = rutaCompleta;
                                Archivo.NombreArchivo = nombreArchivo;
                                GuardarDatosArchivo(Archivo);
                            }


                            Response.Redirect(string.Format("AdmArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}", Seccion.CodSeccion.ToString(), IdSeccionEN.ToString()));
                        }
                        else {
                            //solamente modifico parametros
                            var ruta = txtRuta.Text;

                            //Archivo.NombreArchivo = txtNombre.Text.ToString();
                            Archivo.Anio = Convert.ToInt32(ddAño.SelectedValue.ToString());
                            Archivo.Titulo = txtTitulo.Text.ToString();
                            Archivo.Copete = txtCopete.Text.ToString();
                            Archivo.Ruta = txtRuta.Text.ToString();
                            //Archivo.Tamaño = txtTamaño.Text.ToString();
                            Archivo.Seccion = Seccion;

                            GuardarDatosArchivo(Archivo);

                            Response.Redirect(string.Format("AdmArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}", Seccion.CodSeccion.ToString(), IdSeccionEN.ToString()));
                        }

                        

                       

                    }
                    #endregion
                }
                else
                {
                    #region ArchivoNuevo

                        HttpFileCollection fileCollection = Request.Files;

                        ArchivosInversores arch;

                        if(fileCollection.Count > 1)
                        {//es carga multiple de archivos

                            for (int i = 0; i < fileCollection.Count; i++)
                            {
                                HttpPostedFile uploadfile = fileCollection[i];

                                arch = new ArchivosInversores();

                                var nombreArchivo = uploadfile.FileName;

                                var ruta = txtRuta.Text;

                                var rutaCompleta = ruta.TrimEnd() + nombreArchivo.TrimStart();


                                GuardarArchivo(uploadfile, ruta, rutaCompleta, i);

                            }


                            Response.Redirect(string.Format("AdmArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}", Seccion.CodSeccion.ToString(), IdSeccionEN.ToString()));
                        }
                        else
                        {//solamente se cargo un archivo
                            HttpPostedFile postedFile = fileCollection[0];

                            if (!string.IsNullOrEmpty(postedFile.FileName))
                            {

                                arch = new ArchivosInversores();

                                var nombreArchivo = postedFile.FileName;

                                var ruta = txtRuta.Text;

                                var rutaCompleta = ruta.TrimEnd() + nombreArchivo.TrimStart();

                                GuardarArchivo(postedFile, ruta, rutaCompleta, 1);

                                Response.Redirect(string.Format("AdmArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}", Seccion.CodSeccion.ToString(), IdSeccionEN.ToString()));

                            }//es nuevo y no cargo archivo entonces valida 
                            else {
                                lblErrores.Text = "Debe cargar un archivo.";
                                lblErrores.Visible = true;
                            }

                    }
                    #endregion 

                }

            }
            catch //(Exception ex)
            { 
            
            }
        
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                CargamosDatos();
            }
            else //page is valid
            {
                lblErrores.Text = "Debe Completar los datos ";
                lblErrores.Visible = true;
            }
            CargarControles();            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

            Response.Redirect(string.Format("AdmArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}", IdSeccion.ToString(), IdSeccionEN.ToString()));
        }
        #endregion

        #region Metodos

        

        /// <summary>
        /// Cargamos los controles en la pagina 
        /// </summary>
        public void CargarControles()
        {
            CargarComboAño();

            txtIdioma.Text = Seccion.Idioma == 1 ? "Español" : "Ingles";

            txtSeccion.Text = Seccion.DesSeccion;

            txtRuta.Text = Seccion.Directorio;

            //txtNombre.Text = "";
            txtTitulo.Text = "";
            //txtTamaño.Text = "";
            txtCopete.Text = "";
            CargarArchivo();
        }

        //cargamos el combo con los años empezando por el año actual y 10 para abajo
        public void CargarComboAño()
        {
            var añoActual = DateTime.Now.Year;

            IList<int> Años=new List<int>();
            //cargamos 20 años en el combo
            for (int i = añoActual-20; i < añoActual; i++)
            {
                Años.Add(i);
            }

            Años.Add(añoActual);
            ddAño.DataSource = Años.Reverse();
            ddAño.DataBind();

        }

        public ArchivosInversores ObtenerArchivoDeControles(HttpPostedFile archivo)
        { 
            ArchivosInversores arch =new ArchivosInversores();

            //1-obtengo Datos comunes todos los archivos
            var año = ddAño.SelectedValue.ToString();

            try
            {         
                //obtengo la extension del archivo
                var ext = Path.GetExtension(archivo.FileName);
                //obtengo la extension sin el puto
                var tipo = ext.Substring(1, 3);
                arch.Anio = Convert.ToInt16(año);
                //guardo el nombre del archivoo
                arch.NombreArchivo = archivo.FileName;
                var tamaño = archivo.ContentLength;
                var tamañoKB =Convert.ToDouble(tamaño / 1024);

                if (tamañoKB >= 1024)
                {
                    arch.Tamanio = Math.Round(Convert.ToDouble(tamañoKB / 1024), 2)+" MB";
                }
                else {
                    arch.Tamanio = Math.Round(tamañoKB,2) + " KB";
                }
                if (String.IsNullOrEmpty(txtTitulo.Text))
                {
                    var cantCaracteres = archivo.FileName.Length - ext.Length;
                    arch.Titulo = archivo.FileName.Substring(0, cantCaracteres)+" (Archivo pdf "+arch.Tamanio.Replace(',','.')+")";
                }
                else
                {
                    arch.Titulo = txtTitulo.Text + " (Archivo pdf " + arch.Tamanio.Replace(',', '.') + ")";
                }
                arch.Copete = txtCopete.Text;
                //tambien verifico si el archivo existe y lo creo si no existe
                arch.Ruta = txtRuta.Text.TrimEnd() + archivo.FileName; ;
                arch.Seccion = new SeccionInversores();
                arch.Seccion = Seccion;
                   
            }
            catch(Exception ex)
            {
                throw ex;
            }
             return arch;
        
        }

        /// <summary>
        /// Metodo que guarda el o los archivos en el directorio especificado en la tabla secciones
        /// </summary>
        /// <param name="ruta">recibe como parametro la ruta donde se guardaran lo arachivos</param>
        public void GuardarArchivo(HttpPostedFile  postedFile, string ruta,string rutaCompleta,int orden)
        { 
           try
           {               
                   //verifico que el directorio exista
                   if (Directory.Exists(ruta))
                   {
                       //si el archivo no existe lo creo 
                       if (!File.Exists(rutaCompleta))
                       {
                           postedFile.SaveAs(NormalizoNombreArchivos(rutaCompleta.TrimStart()));

                       }//elimino y creo nuevamente
                       else
                       {

                           File.Delete(rutaCompleta);
                           postedFile.SaveAs(NormalizoNombreArchivos(rutaCompleta.TrimStart()));
                       }
                   }
                   else
                   {
                       //creo el directorio y el archivo
                       Directory.CreateDirectory(ruta);
                       postedFile.SaveAs(NormalizoNombreArchivos(rutaCompleta.TrimStart()));
                   }

                   if (CodArchivo == 0)
                   {
                       GuardarDatosArchivo(ObtenerArchivoDeControles(postedFile));
                   }
                  
              
           }
            catch(Exception ex)
           {
               throw ex;
            }

        }

        public string NormalizoNombreArchivos(string nombre)
        {   
            //saco los espacios adelante o atras 
            
            var sinÑ = nombre.Replace("Ñ", "N");
            var sinñ = nombre.Replace("ñ", "n");
            var sinEspacio = nombre.Replace(" ", "_") ;
            return sinEspacio;
        }

        /// <summary>
        /// Metodo que guarda uno o varios archivos en la base de datos epotecario
        /// </summary>
        public void GuardarDatosArchivo(ArchivosInversores archivo)
        {
            //Recibo un archivo con la Ruta para la aplicacion e-potecarioAdm y tengo que cambiar a la ruta para la aplicacion e-potecario

            try
            {

                //asi archivo.Ruta = CambioUrlArchivo(archivo.Ruta);

                var nuevaUrl = CambioUrlArchivo(archivo.Ruta.TrimEnd());

                archivo.NombreArchivo = NormalizoNombreArchivos(archivo.NombreArchivo);

                archivo.Ruta = NormalizoNombreArchivos(nuevaUrl);

                ServicioSistema<ArchivosInversores>.SaveOrUpdate(archivo);
           
            }
            catch (Exception ex)
            {
                
                MostrarAlert(ex.Message);
            
            }
        
        }

        /// <summary>
        /// se cambia la url por el servicio que copia los archivos de un servidor a otro
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <returns></returns>
        public string CambioUrlArchivo(string rutaArchivo)
        {
            //se saca esta cadena para guardar la ruta al servidor correcto 
			var urlImg = ConfigurationManager.AppSettings["urlFolderImage"].ToString();
            //se saca esta cadena para guardar la ruta al servidor correcto 
            //string nuevaRuta = rutaArchivo.Replace("D:\\Web\\e-potecario\\", "");
            string nuevaRuta = rutaArchivo.Replace(urlImg, "");


            return nuevaRuta;
        }


        //motramos un mensaje 
        public void MostrarAlert(string msg)
        {

            string script = @"<script type='text/javascript'>
                            alert('{0}');
                        </script>";

            script = string.Format(script, msg);

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

        }

        public void MostrarAlertConfirm(string msg)
        {

            string script = @"<script type='text/javascript'>
                            Confirm('{0}');
                        </script>";

            script = string.Format(script, msg);

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

        }

        #endregion

    }
}
