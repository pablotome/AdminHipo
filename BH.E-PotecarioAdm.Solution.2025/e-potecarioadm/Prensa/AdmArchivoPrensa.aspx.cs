using System;
using System.Configuration;
using MicroSitios.Dominio;
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


namespace BH.EPotecario.Prensa
{
	/// <summary>
	/// Summary description for admPromociones.
	/// </summary>
    public partial class AdmArchivoPrensa : WebFormBase
	{
		protected BH.EPotecario.Adm.MenuTab MenuTab1;

		private int CodSeccion 
        {
            get {
				return (Session["CodSeccion"] != null) ? int.Parse(Session["CodSeccion"].ToString()) : 0;
            }

			set {
				Session["CodSeccion"] = value;
            }
        }

		private string MesAnio
		{
			get
			{
				return (Session["MesAnio"] != null) ? Session["MesAnio"].ToString() : string.Empty;
			}

			set
			{
				Session["MesAnio"] = value;
			}
		}

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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			CheckSecurity("ADMPRENSA,ADMINISTRACION");
			
			MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
			
			MenuTab1.CurrentMenuItem = "AdmArchivosPrensa";

			if (!Page.IsPostBack)
			{
				CargarComboSecciones();
				CargarMesAnio();
				CargarArchivos();
			}
		}

		/// <summary>
		/// Cargamos el combo con las secciones para el filtrado 
		/// </summary>
		public void CargarComboSecciones()
		{
			// cargo los archivos
			try
			{
				//obtenemos las secciones 
				var secciones = ServicioSistema<SeccionPrensa>.GetAll().Where(x => x.Vigente).ToList();

				secciones.Insert(0, new SeccionPrensa { CodSeccion = 0, DesSeccion = "Seleccione", Url = "", Vigente = true });

				//removemos estados contables y Informes para la edicion y filtro
				var seccionesNuevas = secciones.Where(x => x.CodSeccion != 6 && x.CodSeccion != 10).ToList();

				ddlSeccion.DataSource = seccionesNuevas;
				ddlSeccion.DataTextField = "DesSeccion";
				ddlSeccion.DataValueField = "CodSeccion";
				ddlSeccion.DataBind();

				ddlSeccion.SelectedValue = CodSeccion.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
		}

		/// <summary>
		/// cargamos el combo años para el filtado
		/// </summary>
		public void CargarMesAnio()
		{
			try
			{
				List<ArchivoPrensa> archivosSeccion = ServicioSistema<ArchivoPrensa>.Get(x => x.Seccion.CodSeccion == CodSeccion).ToList<ArchivoPrensa>();

				var mesesAnios = archivosSeccion
								.Select(a => new {a.Fecha.Year, a.Fecha.Month})
								.Distinct()
								.OrderByDescending(a => a.Year)
								.ThenByDescending(a => a.Month)
								.ToArray();
				
				ddlMesAnio.Items.Clear();
				
				foreach (var mesAnio in mesesAnios) {
					ddlMesAnio.Items.Add(new ListItem(new DateTime(int.Parse(mesAnio.Year.ToString()), int.Parse(mesAnio.Month.ToString()), 1).ToString("MMMM 'de' yyyy"), string.Format("{0}-{1}", mesAnio.Year, mesAnio.Month)));
				}

				ddlMesAnio.Items.Insert(0, new ListItem("(Todos)", string.Empty));

				ddlMesAnio.SelectedValue = string.Empty;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
		}

		protected void ddSeccion_SelectedIndexChanged(object sender, EventArgs e)
		{
			CodSeccion = int.Parse(ddlSeccion.SelectedValue);
			MesAnio = string.Empty;
			CargarMesAnio();
			CargarArchivos();
		}

		protected void ddlMesAnio_SelectedIndexChanged(object sender, EventArgs e)
		{
			CodSeccion = int.Parse(ddlSeccion.SelectedValue);
			MesAnio = ddlMesAnio.SelectedValue;
			CargarArchivos();
		}

		protected void CargarArchivos()
		{
			CargarArchivos(0);
		}

		protected void CargarArchivos(int paginaActual)
		{ 
			int mes, anio;

			anio = (MesAnio != string.Empty) ? int.Parse(MesAnio.Split("-".ToCharArray())[0]) : 0;
			mes = (MesAnio != string.Empty) ? int.Parse(MesAnio.Split("-".ToCharArray())[1]) : 0;

			List<ArchivoPrensa> archivos = ServicioSistema<ArchivoPrensa>.Get(ap => ap.Seccion.CodSeccion == CodSeccion && (ap.Fecha.Year == anio || anio == 0) && (ap.Fecha.Month == mes || mes == 0) && (ap.Vigente == chkSoloVigentes.Checked || !chkSoloVigentes.Checked)).OrderByDescending(a => a.Destacado).ThenByDescending(a => a.Fecha).ThenBy(a => a.Orden).ToList();

			if (archivos.Count == 0)
			{
				dgArchivosSeccion.Visible = false;
				litSinArchivos.Text = string.Format("Sin archivos en la sección");
			}
			else
			{
				dgArchivosSeccion.CurrentPageIndex = paginaActual;
				dgArchivosSeccion.DataSource = archivos;
				dgArchivosSeccion.DataBind();
				dgArchivosSeccion.Visible = true;

				litSinArchivos.Text = (anio != 0 && mes != 0) ? string.Format("{0} archivos en la sección en {1}", archivos.Count, ddlMesAnio.SelectedItem.Text) : string.Format("{0} archivos en la sección", archivos.Count);
			}
		}
		
		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("EditArchivoPrensa.aspx"));
		}

		protected void dgArchivosSeccion_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem
				|| e.Item.ItemType == ListItemType.Item)
			{
				HyperLink lnkEditar = (HyperLink)e.Item.FindControl("lnkEditar");
				HyperLink lnkEliminar = (HyperLink)e.Item.FindControl("lnkEliminar");
				HyperLink hplURL = (HyperLink)e.Item.FindControl("hplURL");
				Literal litFecha = (Literal)e.Item.FindControl("litFecha");
				Literal litVigente = (Literal)e.Item.FindControl("litVigente");
				Literal litDestacado = (Literal)e.Item.FindControl("litDestacado");

				ArchivoPrensa archivo = (ArchivoPrensa)e.Item.DataItem;

				if (hplURL != null)
				{
					//hplURL.NavigateUrl = (archivo.Seccion.CodTipoArchivo == TipoArchivo.Link) ? archivo.Url : hplURL.NavigateUrl = HelperWeb.LinkSincroPortal(archivo.Url);
					hplURL.NavigateUrl = (archivo.Seccion.CodTipoArchivo == TipoArchivo.Link) ? archivo.Url : ConfigurationManager.AppSettings["linkMedia"] + archivo.Url;
					hplURL.Text = (archivo.Url.Length > 30) ? archivo.Url.Substring(0, 30) + "..." : archivo.Url;
					hplURL.Target = "_blank";
				}
				
				if (litFecha != null)
				{
					litFecha.Text = archivo.Fecha.ToString("dd/MM/yyyy");
				}

				if (litVigente != null)
				{
					litVigente.Text = (archivo.Vigente) ? "SI" : "NO";
				}

				if (litDestacado != null)
				{
					litDestacado.Text = (archivo.Destacado) ? "SI" : "NO";
				}

				if (lnkEditar != null)
				{
					lnkEditar.NavigateUrl = string.Format("~/Prensa/EditArchivoPrensa.aspx?CodArchivo={0}", archivo.CodArchivo);
					lnkEditar.Visible = true;
				}
				if (lnkEliminar != null)
				{
					//lnkEliminar.NavigateUrl = string.Format("~/Prensa/EditArchivoPrensa.aspx?CodArchivo={0}&Eliminar=True", archivo.CodArchivo);
					lnkEliminar.NavigateUrl = string.Format("javascript:EliminarArchivo({0}, '{1}')", archivo.CodArchivo, archivo.Nombre);
					lnkEliminar.Visible = true;
				}

			}
		}

		protected void PaginarBusqueda(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			CargarArchivos(e.NewPageIndex);
		}

		protected void chkSoloVigentes_CheckedChanged(object sender, EventArgs e)
		{
			CargarArchivos();
		}

		protected void btnEliminar_Click(object sender, EventArgs e)
		{
			int codArchivo = int.Parse(hdnCodArchivoEliminar.Value);
			ServicioSistema<ArchivoPrensa>.Delete(ap => ap.CodArchivo == codArchivo);
			CargarArchivos();
		}

        #endregion


 
		

        
	}

	
}
