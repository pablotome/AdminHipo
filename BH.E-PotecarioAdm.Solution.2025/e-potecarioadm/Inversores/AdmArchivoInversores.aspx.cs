using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BH.WebControls;

using BH.EPotecario.Adm.Componentes;
using BH.EPotecario.Adm;
using MicroSitios.Dominio.Entidades;
using MicroSitios.Componentes;
using System.Collections.Generic;
using System.Linq;


namespace BH.EPotecario.Inversores
{
	/// <summary>
	/// Summary description for admPromociones.
	/// </summary>
    public partial class AdmArchivoInversores : WebFormBase
	{
		protected BH.EPotecario.Adm.MenuTab MenuTab1;

		protected DataGrid dgParametrosSeccion;

        public List<ArchivosInversores> ListArchivos;

        private SeccionInversores Seccion
        {
            get
            {
                var idSeccion = (Request.QueryString["IdSeccion"] != null) ? int.Parse(Request.QueryString["IdSeccion"]) : 1;
                SeccionInversores seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == idSeccion);
                return seccion;
            }
        }

        private int IdSeccion
        {
            get
            {
                return (Request.QueryString["IdSeccion"] != null) ? int.Parse(Request.QueryString["IdSeccion"]) : 1;
            }
        }

        private int IdSeccionEN
        {
            get
            {
                return (Request.QueryString["IdSeccionEN"] != null) ? int.Parse(Request.QueryString["IdSeccionEN"]) : 9;
            }
        }

        public int IdIdioma;		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion		

        #region Metodos

        /// <summary>
        /// Cargamos la grilla con los datos dependiendo el idseccion ingresado
        /// </summary>
        /// <param name="idSeccion"></param>
        protected void CargarGrilla(int idSeccion)
        {
            // cargo los archivos

            ListArchivos = new List<ArchivosInversores>();

            try
            {
                if (idSeccion != 0)
                {   //traemos por defaul en idioma españo. 
                    ListArchivos = ServicioSistema<ArchivosInversores>.GetAll()
                   .Where(x => x.Seccion.CodSeccion == idSeccion && x.Seccion.Idioma == IdIdioma).OrderByDescending(x => x.Anio).ToList();
                   
                    btnNuevo.Visible = true;
                    lblTxtPatch.Text = "Patch :";
                    txtPatchArchivos.Text = CambioUrlArchivo(Seccion.Directorio);
                    txtPatchArchivos.ReadOnly = true;
                }
                else
                {
                    ListArchivos = ServicioSistema<ArchivosInversores>.GetAll().OrderByDescending(x => x.Anio).ToList();
                }


                dgParametrosSeccion.DataSource = ListArchivos;
                dgParametrosSeccion.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }

        public string CambioUrlArchivo(string rutaArchivo)
        {
            //se saca esta cadena para guardar la ruta al servidor correcto 

            string nuevaRuta = rutaArchivo.Replace("D:\\Web\\e-potecarioAdm\\SincroPortal\\", "");


            return nuevaRuta;
        }

        /// <summary>
        /// Cargamos el combo año para la seccion 
        /// </summary>
        public void CargarComboAños()
        {
            try
            {
                var años = (from a in ListArchivos
                            select a.Anio).Distinct().ToList();

                ddAño.DataSource = años;
                ddAño.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void PaginarBusqueda(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgParametrosSeccion.CurrentPageIndex = e.NewPageIndex;

            IdIdioma = Convert.ToInt16(ddIdioma.SelectedValue);
            //////////////funcion para validar que este seleccionado en ingles
            if (IdIdioma == 1)
            {//cargamos la seccion en castellano
                CargarGrilla(IdSeccion);
            }
            else
            { //cargamos la seccion en Ingles
                if (IdSeccionEN != 0)
                {
                    CargarGrilla(IdSeccionEN);
                }
                else
                {
                    //mensaje de que no tiene seccion en ingles
                }

            }


        }


        #endregion

        #region Manejadores

        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");


                IdIdioma = Convert.ToInt16(ddIdioma.SelectedValue);

                dgParametrosSeccion.CurrentPageIndex = 0;

                CargarGrilla(IdSeccion);

                CargarComboAños();


            }
            this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admInversores";

            if (IdSeccion != 0)
            {
                lblTitulo.Text = "Adm. " + Seccion.DesSeccion.ToString();
            }

        }

        protected void dgParametros_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkEditar = (HyperLink)e.Item.FindControl("lnkEditar");
                HyperLink lnkEliminar = (HyperLink)e.Item.FindControl("lnkEliminar");

                ArchivosInversores archivo = (ArchivosInversores)e.Item.DataItem;

                if (lnkEditar != null)
                {
                    lnkEditar.NavigateUrl = string.Format("~/Inversores/EditArchivoInversores.aspx?CodArchivo={0}&IdSeccion={1}&IdSeccionEN={2}", archivo.CodArchivo, IdSeccion, IdSeccionEN);
                    lnkEditar.Visible = true;
                }
                if (lnkEliminar != null)
                {
                    lnkEliminar.NavigateUrl = string.Format("~/Inversores/EditArchivoInversores.aspx?CodArchivo={0}&IdSeccion={1}&IdSeccionEN={2}&Eliminar=True", archivo.CodArchivo, IdSeccion, IdSeccionEN);
                    lnkEliminar.Visible = true;
                }

            }
        }


        protected void btnNuevo_Click(object sender, EventArgs e)
        {

            IdIdioma = Convert.ToInt16(ddIdioma.SelectedValue);

            if (IdIdioma == 1)
            {//cargamos la seccion en castellano

                Response.Redirect(string.Format("EditArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}&Idioma=1", Seccion.CodSeccion.ToString(), IdSeccionEN.ToString()));
            }
            else
            { //cargamos la seccion en Ingles
                if (IdSeccionEN != 0)
                {
                    Response.Redirect(string.Format("EditArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}&Idioma=2", Seccion.CodSeccion.ToString(), IdSeccionEN.ToString()));
                }
                else
                {
                    //mensaje de que no tiene seccion en ingles
                    Response.Redirect(string.Format("EditArchivoInversores.aspx?IdSeccion={0}&IdSeccionEN={1}&Idioma=1", Seccion.CodSeccion.ToString(), IdSeccionEN.ToString()));
                }

            }


        }

        protected void ddAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdIdioma = Convert.ToInt16(ddIdioma.SelectedValue);
            dgParametrosSeccion.CurrentPageIndex = 0;
            if (IdIdioma == 1)
            {

                var año = Convert.ToInt16(ddAño.SelectedValue.ToString());

                ListArchivos = ServicioSistema<ArchivosInversores>.GetAll().Where(x => x.Anio == año && x.Seccion.CodSeccion == IdSeccion).ToList();
                dgParametrosSeccion.DataSource = ListArchivos;
                dgParametrosSeccion.DataBind();
            }
            else
            {

                var año = Convert.ToInt16(ddAño.SelectedValue.ToString());

                ListArchivos = ServicioSistema<ArchivosInversores>.GetAll().Where(x => x.Anio == año && x.Seccion.CodSeccion == IdSeccionEN).ToList();
                dgParametrosSeccion.DataSource = ListArchivos;
                dgParametrosSeccion.DataBind();
            }



        }
        #endregion

        protected void ddIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdIdioma = Convert.ToInt16(ddIdioma.SelectedValue);
            //////////////funcion para validar que este seleccionado en ingles
            if (IdIdioma == 1)
            {//cargamos la seccion en castellano
                //IdSeccion = Convert.ToInt32(Session["IdSeccion"]);
                CargarGrilla(IdSeccion);
            }
            else
            { //cargamos la seccion en Ingles
                //IdSeccionEN = Convert.ToInt32(Session["IdSeccionEN"]);
                if (IdSeccionEN != 0)
                {
                    CargarGrilla(IdSeccionEN);
                }

            }

        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            var seccion = IdSeccion;

            EspacioDuenios ed = new EspacioDuenios();
            var url = ed.ObtenerParametro("UrlPaginaInversores");
            Response.Write("<script>window.open('" + url + "','_blank');</script>");

        }

       
		

        
	}

	
}
