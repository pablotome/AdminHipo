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

namespace BH.EPotecario.Adm.LugaresDePagoVisa
{
	/// <summary>
	/// Summary description for admLugaresDePagoVisa.
	/// </summary>
	public partial class admLugaresDePagoVisa : WebFormBase
	{
		protected System.Web.UI.WebControls.Label lblLugarPagoVisa;

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
			this.dgLugaresDePagoVisa.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgLugaresDePagoVisa_ItemCommand);
			this.dgLugaresDePagoVisa.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgLugaresDePagoVisa_PageIndexChanged);
			this.dgLugaresDePagoVisa.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgLugaresDePagoVisa_SortCommand);
			this.dgLugaresDePagoVisa.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgLugaresDePagoVisa_ItemDataBound);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.CheckSecurity("LUGARESPAGOVISA,ADMINISTRACION");

			this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
			//this.MenuTab1.CurrentMenuItem = "admLugaresDePagoVisa";
            this.MenuTab1.CurrentMenuItem = "Personas";
			if(!IsPostBack)
			{
				lblError.Text = "";
				CargarGrilla();
			}
		}

		private void BindearGrilla(DataView dv)
		{
			dgLugaresDePagoVisa.DataSource = dv;
			Session["dvLugaresDePagoVisa"] = dv;
			try
			{
				dgLugaresDePagoVisa.DataBind();
			}
			catch
			{
				dgLugaresDePagoVisa.CurrentPageIndex = 0;
				dgLugaresDePagoVisa.DataBind();
			}
		}

		private void CargarGrilla()
		{
			Componentes.LugaresDePagoVisa oLugaresDePagoVisa = new BH.EPotecario.Adm.Componentes.LugaresDePagoVisa();
			DataSet dsLugaresDePagoVisa = oLugaresDePagoVisa.GetLugaresDePagoVisa();

			Session["dsLugaresDePagoVisa"] = dsLugaresDePagoVisa;

			DataView dvLugaresDePagoVisa = new DataView(dsLugaresDePagoVisa.Tables["LugarPagoVisa"]);
			dvLugaresDePagoVisa.Sort = "Denominacion ASC";
			Session["Orden"] = "ASC";
			Session["ColumnaUltimoOrden"] = "OrdenDenominacion";
			BindearGrilla(dvLugaresDePagoVisa);
		}

		private void dgLugaresDePagoVisa_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item)||(e.Item.ItemType == ListItemType.AlternatingItem))
			{
				LinkButton lnkEliminar = (LinkButton)e.Item.Cells[e.Item.Cells.Count-1].Controls[3];
				lnkEliminar.Attributes.Add("onclick","javascript:return window.confirm('¿Confirma la Baja del Item?');");
			}
		}

		protected void btnAgregar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			txtDenominacion.Enabled = true;
			txtDireccion.Enabled = true;
			pnlAdmLugaresDePagoVisa.Visible = true;
			pnlListaLugaresDePagoVisa.Visible = false;
			Session["Operacion"] = TipoOperacion.Alta;
			CargarCombos();
		}

		private void BorrarIngresos()
		{
			txtDenominacion.Text = "";
			txtDireccion.Text = "";
			cboProvincia.SelectedValue = "-1";
			cboLocalidad.SelectedValue = "-1";
			cboEntidadPago.SelectedValue = "-1";
		}

		protected void btnCancelar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			pnlAdmLugaresDePagoVisa.Visible = false;
			pnlListaLugaresDePagoVisa.Visible = true;
			BorrarIngresos();
		}
		private void CargarComboGenerico(DropDownList combo, DataView dv, string valueField, string textField)
		{
			combo.Items.Clear();
			combo.Items.Add(new ListItem("Seleccionar...","-1"));
			for(int i = 0;i<=dv.Count-1;i++)
			{
				combo.Items.Add(new ListItem(dv[i][textField].ToString(),dv[i][valueField].ToString()));
			}
			combo.DataBind();
		}

		private void CargarCombos()
		{
			Componentes.LugaresDePagoVisa oLugaresDePagoVisa = new BH.EPotecario.Adm.Componentes.LugaresDePagoVisa();
			DataSet dsLocalidad = oLugaresDePagoVisa.GetLocalidades();
			DataSet dsProvincias = oLugaresDePagoVisa.GetProvincias();
			DataSet dsEntidadPago = oLugaresDePagoVisa.GetEntidadPagoVisa();

			CargarComboGenerico(cboLocalidad,new DataView(dsLocalidad.Tables["Localidades"]),"CodLocalidad","DesLocalidad");
			CargarComboGenerico(cboProvincia,new DataView(dsProvincias.Tables["Provincias"]),"CodProvincia","NomProvincia");
			CargarComboGenerico(cboEntidadPago,new DataView(dsEntidadPago.Tables["EntidadPagoVisa"]),"CodEntidadPagoVisa","DesEntidadPagoVisa");
		}

		private bool ValidarIngreso()
		{
			if (cboLocalidad.SelectedValue == "-1")
			{
				lblError.Text = "Debe seleccionar Una Localidad.";
				return false;
			}
			if (txtDenominacion.Text == "")
			{
				lblError.Text = "Debe ingresar la Denominación.";
				return false;
			}
			if (txtDireccion.Text == "")
			{
				lblError.Text = "Debe ingresar la Dirección.";
				return false;
			}
			if (cboEntidadPago.SelectedValue == "-1")
			{
				lblError.Text = "Debe Seleccionar la Entidad de Pago.";
				return false;
			}

			return true;
		}

		protected void btnAceptar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			if (ValidarIngreso())
			{
				try
				{
					switch (Session["Operacion"].ToString())
					{
						case TipoOperacion.Alta:
						{
							DataSet dsLugaresDePagoVisa = (DataSet)Session["dsLugaresDePagoVisa"];
							DataRow drNuevoLugarDePago = dsLugaresDePagoVisa.Tables["LugarPagoVisa"].NewRow();
							drNuevoLugarDePago["codLugarPagoVisa"] = IDs.Nuevo;
							drNuevoLugarDePago["Denominacion"] = txtDenominacion.Text;
							drNuevoLugarDePago["Direccion"] = txtDireccion.Text;
							drNuevoLugarDePago["CodEntidadPagoVisa"] = cboEntidadPago.SelectedValue;
							drNuevoLugarDePago["CodLocalidad"] = cboLocalidad.SelectedValue;

							dsLugaresDePagoVisa.Tables["LugarPagoVisa"].Rows.Add(drNuevoLugarDePago);
												
							Componentes.LugaresDePagoVisa oLugaresDePagoVisa = new BH.EPotecario.Adm.Componentes.LugaresDePagoVisa();
							oLugaresDePagoVisa.UpdateLugaresDePagoVisa(dsLugaresDePagoVisa);
						
							break;
						}
						case TipoOperacion.Modificacion:
						{

							DataSet dsLugaresDePagoVisa = (DataSet)Session["dsLugaresDePagoVisa"];
							DataRow[] drNuevoLugarDePago = dsLugaresDePagoVisa.Tables["LugarPagoVisa"].Select("codLugarPagoVisa = "+txtCodLugarPagoVisa.Text);
							drNuevoLugarDePago[0]["Denominacion"] = txtDenominacion.Text;
							drNuevoLugarDePago[0]["Direccion"] = txtDireccion.Text;
							drNuevoLugarDePago[0]["CodEntidadPagoVisa"] = cboEntidadPago.SelectedValue;
							drNuevoLugarDePago[0]["CodLocalidad"] = cboLocalidad.SelectedValue;

							Componentes.LugaresDePagoVisa oLugaresDePagoVisa = new BH.EPotecario.Adm.Componentes.LugaresDePagoVisa();
							oLugaresDePagoVisa.UpdateLugaresDePagoVisa(dsLugaresDePagoVisa);
							
							break;
						}
					}

					HelperWeb.RemoveAllCache();

					CargarGrilla();

					BorrarIngresos();
					pnlAdmLugaresDePagoVisa.Visible = false;
					pnlListaLugaresDePagoVisa.Visible = true;
				}
				catch (Exception ex)
				{
					lblError.Text = "Ha ocurrido un Error. "+ex.Message;
				}
			}
		}

		private void dgLugaresDePagoVisa_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			switch (e.CommandName)
			{
				case TipoOperacion.Modificacion:
				{
					pnlAdmLugaresDePagoVisa.Visible = true;
					pnlListaLugaresDePagoVisa.Visible = false;
					CargarCombos();

					DataSet dsLugaresDePagoVisa = (DataSet)Session["dsLugaresDePagoVisa"];
					DataRow[] drLugarDePago = dsLugaresDePagoVisa.Tables["LugarPagoVisa"].Select("codLugarPagoVisa = "+e.Item.Cells[0].Text);
					cboProvincia.SelectedValue = drLugarDePago[0]["codProvincia"].ToString();
					cboEntidadPago.SelectedValue = drLugarDePago[0]["codEntidadPagoVisa"].ToString();
					cboLocalidad.SelectedValue = drLugarDePago[0]["codLocalidad"].ToString();
					txtDenominacion.Text= drLugarDePago[0]["Denominacion"].ToString();
					txtDireccion.Text = drLugarDePago[0]["Direccion"].ToString();
					txtCodLugarPagoVisa.Text = drLugarDePago[0]["codLugarPagoVisa"].ToString();
					
					Session["Operacion"] = TipoOperacion.Modificacion;
					break;
				}
				case TipoOperacion.Baja:
				{
					Componentes.LugaresDePagoVisa oLugaresDePagoVisa = new BH.EPotecario.Adm.Componentes.LugaresDePagoVisa();
					DataSet dsLugaresDePagoVisa = (DataSet)Session["dsLugaresDePagoVisa"];

					DataRow[] drLugarDePagoVisa = dsLugaresDePagoVisa.Tables["LugarPagoVisa"].Select("codLugarPagoVisa = "+e.Item.Cells[0].Text);
					
					//Elimino el Lugar De Pago Visa
					for(int i = 0;i<=dsLugaresDePagoVisa.Tables["LugarPagoVisa"].Rows.Count-1;i++)
					{
						if (dsLugaresDePagoVisa.Tables["LugarPagoVisa"].Rows[i]["codLugarPagoVisa"].ToString() == drLugarDePagoVisa[0]["codLugarPagoVisa"].ToString())
						{
							dsLugaresDePagoVisa.Tables["LugarPagoVisa"].Rows[i].Delete();
							break;
						}
					}

					oLugaresDePagoVisa.UpdateLugaresDePagoVisa(dsLugaresDePagoVisa);

					HelperWeb.RemoveAllCache();

					CargarGrilla();

					break;
				}
			}
		}

		private void dgLugaresDePagoVisa_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgLugaresDePagoVisa.CurrentPageIndex = e.NewPageIndex;
			BindearGrilla((DataView)Session["dvLugaresDePagoVisa"]);
		}

		private void dgLugaresDePagoVisa_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			DataView dvLugaresDePagoVisa = (DataView)Session["dvLugaresDePagoVisa"];
			
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
				case "OrdenDenominacion":
				{
					dvLugaresDePagoVisa.Sort = "Denominacion "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenDenominacion";
					break;
				}
				case "OrdenEntidadDePago":
				{
					dvLugaresDePagoVisa.Sort = "desEntidadPagoVisa "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenEntidadDePago";
					break;
				}
				case "OrdenProvincia":
				{
					dvLugaresDePagoVisa.Sort = "nomProvincia "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenProvincia";
					break;
				}
				case "OrdenLocalidad":
				{
					dvLugaresDePagoVisa.Sort = "desLocalidad "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenLocalidad";
					break;
				}
			}
			BindearGrilla(dvLugaresDePagoVisa);
		}

		protected void cboProvincia_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Componentes.LugaresDePagoVisa oLugaresDePagoVisa = new BH.EPotecario.Adm.Componentes.LugaresDePagoVisa();
			DataSet dsLocalidad = oLugaresDePagoVisa.GetLocalidades();
			
			DataView dvLocalidades = new DataView(dsLocalidad.Tables["Localidades"]);
			
			if (cboProvincia.SelectedValue != "-1")
				dvLocalidades.RowFilter = "CodProvincia = "+cboProvincia.SelectedValue;

			CargarComboGenerico(cboLocalidad,dvLocalidades,"CodLocalidad","DesLocalidad");
		}

		protected void cboLocalidad_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Componentes.LugaresDePagoVisa oLugaresDePagoVisa = new BH.EPotecario.Adm.Componentes.LugaresDePagoVisa();
			DataSet dsLocalidad = oLugaresDePagoVisa.GetLocalidades();
			
			DataRow[] dr = dsLocalidad.Tables["Localidades"].Select("CodLocalidad = "+cboLocalidad.SelectedValue);
			cboProvincia.SelectedValue = dr[0]["CodProvincia"].ToString();
		}


	}

	public class TipoOperacion
	{
		public const string Alta = "Alta";
		public const string Modificacion = "Modificar";
		public const string Baja = "Eliminar";
	}
}
