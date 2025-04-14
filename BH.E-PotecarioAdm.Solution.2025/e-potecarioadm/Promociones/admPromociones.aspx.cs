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

namespace BH.EPotecario.Adm.Promociones
{
	/// <summary>
	/// Summary description for admPromociones.
	/// </summary>
	public partial class admPromociones : WebFormBase
	{
		protected System.Web.UI.WebControls.Button btnCancelar;
		protected System.Web.UI.WebControls.Button btnAceptar;
		/*protected System.Web.UI.WebControls.Label lblApellido;
		protected System.Web.UI.WebControls.Label lblUsuarioNT;
		protected System.Web.UI.WebControls.Label lblCodSucursal;
		protected System.Web.UI.WebControls.TextBox txtCodSucursal;
		protected System.Web.UI.WebControls.Panel pnlAdmSucursales;
		protected System.Web.UI.WebControls.TextBox txtHorarioAtencion;
		protected System.Web.UI.WebControls.TextBox txtDomicilio;
		protected System.Web.UI.WebControls.Label lblDomicilio;
		protected System.Web.UI.WebControls.TextBox txtNombre;*/
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		/*protected System.Web.UI.WebControls.DropDownList cboProvincia;
		protected System.Web.UI.WebControls.DropDownList cboTipoSucursal;*/
		//protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.Panel pnlAdmPromociones;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden codPromocion;

		protected BH.EPotecario.Adm.MenuTab MenuTab1;

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
			this.dgPromociones.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPromociones_ItemCommand);
			this.dgPromociones.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgPromociones_PageIndexChanged);
			this.dgPromociones.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPromociones_CancelCommand);
			this.dgPromociones.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPromociones_EditCommand);
			this.dgPromociones.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgPromociones_SortCommand);
			this.dgPromociones.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgPromociones_UpdateCommand);
			this.dgPromociones.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPromociones_ItemDataBound);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.CheckSecurity("PROMOCIONES,ADMINISTRACION");

			this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
			this.MenuTab1.CurrentMenuItem = "admPromociones";
			if(!IsPostBack)
			{
				lblError.Text = "";
				CargarGrilla();
			}
		}

		private void BindearGrilla(DataView dv)
		{
			dgPromociones.DataSource = dv;
			Session["dvPromociones"] = dv;
			try
			{
				dgPromociones.DataBind();
			}
			catch//(Exception ex)
			{
				dgPromociones.CurrentPageIndex = 0;
				dgPromociones.DataBind();
			}
		}

		private void CargarGrilla()
		{
			Componentes.Promociones oPromociones = new BH.EPotecario.Adm.Componentes.Promociones();
			DataSet dsPromociones = oPromociones.GetPromociones();

			Session["dsPromociones"] = dsPromociones;
		
			BindearGrilla(new DataView(dsPromociones.Tables["Promociones_Tipos"]));
		}

		private void dgPromociones_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			DataRowView rowPromocion = (DataRowView)e.Item.DataItem;

			if ((e.Item.ItemType == ListItemType.Item)||(e.Item.ItemType == ListItemType.AlternatingItem))
			{
				LinkButton lnkEliminar = (LinkButton)e.Item.FindControl("lnkEliminar");
				LinkButton lnkObtenerRegistrados = (LinkButton)e.Item.FindControl("lnkObtenerRegistrados");
				Label lblCantidad = (Label)e.Item.Cells[4].FindControl("lblCantidad");
				if (lblCantidad != null && lblCantidad.Text != "0")
				{
					lnkEliminar.Enabled = false;
					lnkObtenerRegistrados.Enabled = true;
				}
				else
				{
					lnkEliminar.Attributes.Add("onclick","javascript:return window.confirm('¿Confirma la Baja del Item?');");
					lnkObtenerRegistrados.Enabled = false;
				}
			}
			else if (e.Item.ItemType == ListItemType.EditItem)
			{
				HtmlInputControl txtCodPromocion = (HtmlInputControl)e.Item.FindControl("txtCodPromocion");
				TextBox txtPromocion = (TextBox)e.Item.FindControl("txtPromocion");
				TextBox txtEmailFrom = (TextBox)e.Item.FindControl("txtEmailFrom");
				TextBox txtEmailTo = (TextBox)e.Item.FindControl("txtEmailsTO");
				
				if (txtCodPromocion != null)
				{
					txtCodPromocion.Value = rowPromocion["codPromocion"].ToString();
					txtCodPromocion.Disabled = true;
				}

				if (txtPromocion != null)
					txtPromocion.Text = rowPromocion["desPromocion"].ToString();

				if (txtEmailFrom != null)
					txtEmailFrom.Text = rowPromocion["emailFrom"].ToString();
				
				if (txtEmailTo != null)
					txtEmailTo.Text = rowPromocion["EmailsTo"].ToString();

				//Fecha Desde
				if(rowPromocion["FechaInicio"] != System.DBNull.Value)
				{
					DatePicker dpFechaInicio = (DatePicker) e.Item.FindControl("dpFechaInicio");
					dpFechaInicio.Date = Convert.ToDateTime(rowPromocion["FechaInicio"]);
				}
				
				//Fecha Hasta
				if(rowPromocion["FechaFin"] != System.DBNull.Value)
				{
					DatePicker dpFechaFin = (DatePicker) e.Item.FindControl("dpFechaFin");
					dpFechaFin.Date = Convert.ToDateTime(rowPromocion["FechaFin"]);
				}
			}
		}

		protected void btnAgregar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";

			DataSet dsPromociones = (DataSet)Session["dsPromociones"];
			
			int indiceNuevo = ExisteNuevoEnDS(dsPromociones);
			if (indiceNuevo == -1)
			{
				DataRow drNuevaPromocion = dsPromociones.Tables["Promociones_Tipos"].NewRow();

				Componentes.Promociones promocion = new BH.EPotecario.Adm.Componentes.Promociones();

				drNuevaPromocion["codPromocion"] = IDs.Nuevo;
				drNuevaPromocion["desPromocion"] = string.Empty;
				drNuevaPromocion["EmailFrom"] = string.Empty;
				drNuevaPromocion["EmailsTo"] = string.Empty;
				drNuevaPromocion["fechaInicio"] = DBNull.Value;
				drNuevaPromocion["fechaFin"] = DBNull.Value;
			
				dsPromociones.Tables["Promociones_Tipos"].Rows.InsertAt(drNuevaPromocion, dsPromociones.Tables["Promociones_Tipos"].Rows.Count - 1);

				Session["dsPromociones"] = dsPromociones;
				Session["Orden"] = "desc";

				dgPromociones.EditItemIndex = dgPromociones.Items.Count;
				CargarGrilla();
			}
			else
			{
				dgPromociones.EditItemIndex = indiceNuevo+1;
				CargarGrilla();
			}

			LinkButton lnkEliminar = (LinkButton) dgPromociones.Items[dgPromociones.EditItemIndex].FindControl("lnkEliminar");
			LinkButton lnkObtenerRegistrados = (LinkButton) dgPromociones.Items[dgPromociones.EditItemIndex].FindControl("lnkObtenerRegistrados");

			lnkEliminar.Enabled = false;
			lnkObtenerRegistrados.Enabled = false;
		}


		private int ExisteNuevoEnDS(DataSet dsPromociones)
		{
			int i;
			for(i=0; i<dsPromociones.Tables["Promociones_Tipos"].Rows.Count; i++)
				if (dsPromociones.Tables["Promociones_Tipos"].Rows[i]["codPromocion"].ToString() == "-1")
					return i;
			return -1;
		}

		private bool ValidarIngreso(DataGridItem item)
		{
			TextBox txtPromocion = (TextBox) item.FindControl("txtPromocion");

			//Promoción
			if (txtPromocion.Text.Trim() == "")
			{
				lblError.Text = "Debe ingresar un nombre de Promoción.";
				return false;
			}

			//Fecha Desde
			DatePicker dpFechaInicio = (DatePicker) item.FindControl("dpFechaInicio");
			if(dpFechaInicio.IsEmpty)
			{
				lblError.Text = "Debe seleccionar una Fecha de Inicio de promoción.";
				return false;
			}
			//dr["FechaDesde"] = dpFechaInicio.Date;

			return true;
		}

		private void dgPromociones_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			dgPromociones.EditItemIndex = e.Item.ItemIndex;			
			CargarGrilla();
		}

		private void dgPromociones_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			switch (e.CommandName)
			{
				case TipoOperacion.Modificacion:
				{
					dgPromociones.EditItemIndex = e.Item.ItemIndex;
					
					Session["Operacion"] = TipoOperacion.Modificacion;
					break;
				}
				case TipoOperacion.Baja:
				{
					Componentes.Promociones oPromociones = new BH.EPotecario.Adm.Componentes.Promociones();
					DataSet dsPromociones = (DataSet)Session["dsPromociones"];
					Label lblCodPromocion = (Label)e.Item.FindControl("lblCodPromocion");
					DataRow[] drPromocion = dsPromociones.Tables["Promociones_Tipos"].Select("codPromocion = "+lblCodPromocion.Text);
					
					//Elimino la promoción
					for(int i = 0;i<=dsPromociones.Tables["Promociones_Tipos"].Rows.Count-1;i++)
					{
						if (dsPromociones.Tables["Promociones_Tipos"].Rows[i]["codPromocion"].ToString() == drPromocion[0]["codPromocion"].ToString())
						{
							dsPromociones.Tables["Promociones_Tipos"].Rows[i].Delete();
							break;
						}
					}

					oPromociones.UpdatePromociones(dsPromociones, "Promociones_Tipos");
	
					HelperWeb.RemoveAllCache();

					CargarGrilla();
					break;
				}
				case TipoOperacion.Registrados:
				{
					Label lblCodPromocion = (Label)e.Item.FindControl("lblCodPromocion");
					Componentes.Promociones pPromociones = new BH.EPotecario.Adm.Componentes.Promociones();
					DataTable dtRegistrados = pPromociones.ObtenerUsuariosRegistrados(int.Parse(lblCodPromocion.Text));

					Response.Clear(); 
					Response.AddHeader("content-disposition", string.Format("attachment;filename=Registrados{0}.txt", lblCodPromocion.Text)); 
					Response.Charset = ""; 
					Response.Cache.SetCacheability(HttpCacheability.NoCache); 
					Response.ContentType = "application/vnd.text"; 
					
					int cantColumnas = dtRegistrados.Columns.Count;
					int i;
					foreach(DataColumn dc in dtRegistrados.Columns)
					{
						Response.Write(dc.ColumnName.ToLower());
						Response.Write("\t");
					}
					
					Response.Write(Environment.NewLine);

					foreach(DataRow dr in dtRegistrados.Rows)
					{
						for(i=0; i<cantColumnas; i++)
						{
							Response.Write(dr[i].ToString());
							Response.Write("\t");
						}
						Response.Write(Environment.NewLine);
					}
					
					Response.End();
					
					break;
				}
			}
		}

		private void dgPromociones_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgPromociones.CurrentPageIndex = e.NewPageIndex;
			BindearGrilla((DataView)Session["dvPromociones"]);
		}

		private void dgPromociones_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			DataView dvPromociones = (DataView)Session["dvPromociones"];
			
			if (e.SortExpression == Session["ColumnaUltimoOrden"].ToString())
			{
				if (Session["Orden"].ToString() == "ASC")
				{
					Session["Orden"] = "DESC";
				}
				else
				{
					Session["Orden"] = "ASC";
				}
			}
			else
			{
				Session["Orden"] = "ASC";
			}
			
			switch (e.SortExpression)
			{
				case "OrdenCodPromocion":
				{
					dvPromociones.Sort = "codPromocion "+Session["Orden"].ToString();
					break;
				}
				case "OrdenDesPromocion":
				{
					dvPromociones.Sort = "desPromocion "+Session["Orden"].ToString();
					break;
				}
				case "OrdenFechaInicio":
				{
					dvPromociones.Sort = "fechaInicio "+Session["Orden"].ToString();
					break;
				}
				case "OrdenFechaFin":
				{
					dvPromociones.Sort = "fechaFin "+Session["Orden"].ToString();
					break;
				}
				case "OrdenCantidad":
				{
					dvPromociones.Sort = "cantidad "+Session["Orden"].ToString();
					break;
				}
			}
			Session["ColumnaUltimoOrden"] = e.SortExpression;
			BindearGrilla(dvPromociones);
		}

		private void dgPromociones_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			if (ValidarIngreso(e.Item))
			{
				HtmlInputControl txtCodPromocion = (HtmlInputControl)e.Item.FindControl("txtCodPromocion");
				TextBox txtPromocion = (TextBox)e.Item.FindControl("txtPromocion");
				TextBox txtEmailFrom = (TextBox)e.Item.FindControl("txtEmailFrom");
				TextBox txtEmailsTO = (TextBox)e.Item.FindControl("txtEmailsTO");
			
				DataSet dsPromociones = (DataSet)Session["dsPromociones"];
				DataRow[] drNuevaPromocion = dsPromociones.Tables["Promociones_Tipos"].Select("codPromocion = "+txtCodPromocion.Value);
				drNuevaPromocion[0]["codPromocion"] = txtCodPromocion.Value;
				drNuevaPromocion[0]["desPromocion"] = txtPromocion.Text;
				drNuevaPromocion[0]["emailFrom"] = txtEmailFrom.Text;
				drNuevaPromocion[0]["emailsTO"] = txtEmailsTO.Text;

				DatePicker dpFechaInicio = (DatePicker) e.Item.FindControl("dpFechaInicio");
				drNuevaPromocion[0]["fechaInicio"] = dpFechaInicio.Date;

				DatePicker dpFechaFin = (DatePicker) e.Item.FindControl("dpFechaFin");
				if(dpFechaFin.IsEmpty)
					drNuevaPromocion[0]["fechaFin"] = DBNull.Value;
				else
					drNuevaPromocion[0]["fechaFin"] = dpFechaFin.Date;

				Componentes.Promociones oPromociones = new BH.EPotecario.Adm.Componentes.Promociones();
				oPromociones.UpdatePromociones(dsPromociones, "Promociones_Tipos");


				Session["dsPromociones"] = null;
				dgPromociones.EditItemIndex = -1;
				CargarGrilla();
			}

		}

		private void dgPromociones_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			DataSet dsPromociones = (DataSet)Session["dsPromociones"];

			for(int i=0; i<dsPromociones.Tables["Promociones_Tipos"].Rows.Count; i++)
			{
				if (dsPromociones.Tables["Promociones_Tipos"].Rows[i]["codPromocion"].ToString() == "-1")
					dsPromociones.Tables["Promociones_Tipos"].Rows[i].Delete();
			}
			
			Session["dsPromociones"] = dsPromociones;
			
			dgPromociones.EditItemIndex = -1;
			CargarGrilla();
		}

		private struct TipoOperacion
		{
			public const string Alta = "Alta";
			public const string Modificacion = "Modificar";
			public const string Baja = "Eliminar";
			public const string Registrados = "Registrados";
		}

	}

	
}
