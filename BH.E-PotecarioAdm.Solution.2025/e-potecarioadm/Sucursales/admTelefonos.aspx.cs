using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace BH.EPotecario.Adm.Sucursales
{
	/// <summary>
	/// Summary description for admTelefonos.
	/// </summary>
	public partial class admTelefonos : System.Web.UI.Page
	{
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
			this.dgTelefonos.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgTelefonos_ItemCommand);
			this.dgTelefonos.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgTelefonos_PageIndexChanged);
			this.dgTelefonos.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgTelefonos_SortCommand);
			this.dgTelefonos.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTelefonos_ItemDataBound);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			HelperWeb.CheckSecurity("SUCURSALES_Y_TELEFONOS,ADMINISTRACION");

			this.MenuTab1.ItemsMenu = HelperWeb.GetItemsMenuPrincipal();
			this.MenuTab1.CurrentMenuItem = "admTelefonos";
			
			if(!IsPostBack)
			{
				lblError.Text = "";
				CargarGrilla();
			}
		}
		private void BindearGrilla(DataView dv)
		{
			dgTelefonos.DataSource = dv;
			Session["dvTelefonos"] = dv;
			try
			{
				dgTelefonos.DataBind();
			}
			catch
			{
				dgTelefonos.CurrentPageIndex = 0;
				dgTelefonos.DataBind();
			}
		}

		private void CargarGrilla()
		{
			Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
			DataSet dsTelefonos = oSucursales.GetTelefonos();

			Session["dsTelefonos"] = dsTelefonos;

			DataView dvTelefonos = new DataView(dsTelefonos.Tables["Telefonos"]);
			dvTelefonos.Sort = "desSucursal ASC";
			Session["Orden"] = "ASC";
			Session["ColumnaUltimoOrden"] = "OrdenSucursal";
			BindearGrilla(dvTelefonos);
		}

		private void dgTelefonos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
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
			pnlAdmTelefonos.Visible = true;
			pnlListaTelefonos.Visible = false;
			Session["Operacion"] = TipoOperacion.Alta;
			CargarCombos();
		}

		private void BorrarIngresos()
		{
			txtNombreContacto.Text = "";
			txtNumeroTelefono.Text = "";
			txtEmailContacto.Text = "";
			cboSucursal.SelectedValue = "-1";
			cboTipoTelefono.SelectedValue = "-1";
		}

		protected void btnCancelar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			pnlAdmTelefonos.Visible = false;
			pnlListaTelefonos.Visible = true;
			BorrarIngresos();
		}

		private void CargarCombos()
		{
			cboTipoTelefono.Items.Clear();
			cboSucursal.Items.Clear();

			Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
			DataSet dsTiposTelefonos = oSucursales.GetTiposTelefonos();
			DataSet dsProvincias = oSucursales.GetSucursales();

			cboTipoTelefono.Items.Add(new ListItem("Seleccionar...","-1"));
			cboSucursal.Items.Add(new ListItem("Seleccionar...","-1"));

			foreach(DataRow oRow in dsTiposTelefonos.Tables["TiposTelefonos"].Rows)
			{
				cboTipoTelefono.Items.Add(new ListItem(oRow["Tipo"].ToString(),oRow["codTipoTelefono"].ToString()));
			}
			foreach(DataRow oRow in dsProvincias.Tables["Sucursales"].Rows)
			{
				cboSucursal.Items.Add(new ListItem(oRow["desSucursal"].ToString(),oRow["codSucursal"].ToString()));
			}
		}

		private bool ValidarIngreso()
		{
			if (cboSucursal.SelectedValue  == "-1")
			{
				lblError.Text = "Debe Seleccionar una Sucursal.";
				return false;
			}
			if (cboTipoTelefono.SelectedValue == "-1")
			{
				lblError.Text = "Debe seleccionar el Tipo de Telefono.";
				return false;
			}
			if (txtNumeroTelefono.Text == "")
			{
				lblError.Text = "Debe ingresar un Número de Teléfono.";
				return false;
			}
			if (cboTipoTelefono.SelectedValue != "1")
			{
				if (txtNombreContacto.Text == "")
				{
					lblError.Text = "Debe ingresar un Nombre de Contacto.";
					return false;
				}
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
							DataSet dsTelefonos = (DataSet)Session["dsTelefonos"];
							DataRow drNuevoTelefono = dsTelefonos.Tables["Telefonos"].NewRow();
							drNuevoTelefono["codTelefono"] = IDs.Nuevo;
							if (cboTipoTelefono.SelectedValue == "1")
							{
								drNuevoTelefono["Nombre"] = "Teléfonos";
								drNuevoTelefono["NombreContacto"] = DBNull.Value;
								drNuevoTelefono["EMailContacto"] = DBNull.Value;
							}
							else if (cboTipoTelefono.SelectedValue == "2")
							{
								drNuevoTelefono["Nombre"] = "Contacto";
								drNuevoTelefono["NombreContacto"] = txtNombreContacto.Text;
								drNuevoTelefono["EMailContacto"] = txtEmailContacto.Text;
							}
							drNuevoTelefono["Numero"] = txtNumeroTelefono.Text;
							drNuevoTelefono["codTipoTelefono"] = Int32.Parse(cboTipoTelefono.SelectedValue);
							drNuevoTelefono["codSucursal"] = Int32.Parse(cboSucursal.SelectedValue);
							
							dsTelefonos.Tables["Telefonos"].Rows.Add(drNuevoTelefono);
												
							Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
							oSucursales.UpdateTelefonos(dsTelefonos);
						
							break;
						}

						case TipoOperacion.Modificacion:
						{
							DataSet dsTelefonos = (DataSet)Session["dsTelefonos"];
							DataRow[] drNuevoTelefono = dsTelefonos.Tables["Telefonos"].Select("codTelefono = "+Session["codTelefono"].ToString());
							drNuevoTelefono[0]["Numero"] = txtNumeroTelefono.Text;
							drNuevoTelefono[0]["codTipoTelefono"] = Int32.Parse(cboTipoTelefono.SelectedValue);
							if (cboTipoTelefono.SelectedValue == "1")
							{
								drNuevoTelefono[0]["Nombre"] = "Teléfonos";
								drNuevoTelefono[0]["NombreContacto"] = DBNull.Value;
								drNuevoTelefono[0]["EMailContacto"] = DBNull.Value;
							}
							else if (cboTipoTelefono.SelectedValue == "2")
							{
								drNuevoTelefono[0]["Nombre"] = "Contacto";
								drNuevoTelefono[0]["NombreContacto"] = txtNombreContacto.Text;
								drNuevoTelefono[0]["EMailContacto"] = txtEmailContacto.Text;
							}
							drNuevoTelefono[0]["codSucursal"] = Int32.Parse(cboSucursal.SelectedValue);


							Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
							oSucursales.UpdateTelefonos(dsTelefonos);
							
							break;
						}
					}

					HelperWeb.RemoveAllCache();

					CargarGrilla();

					BorrarIngresos();
					pnlAdmTelefonos.Visible = false;
					pnlListaTelefonos.Visible = true;
				}
				catch (Exception ex)
				{
					lblError.Text = "Ha ocurrido un Error. "+ex.Message;
				}
			}
		}

		private void dgTelefonos_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			switch (e.CommandName)
			{
				case TipoOperacion.Modificacion:
				{
					pnlAdmTelefonos.Visible = true;
					pnlListaTelefonos.Visible = false;
					CargarCombos();

					DataSet dsTelefonos = (DataSet)Session["dsTelefonos"];
					DataRow[] drTelefono = dsTelefonos.Tables["Telefonos"].Select("codTelefono = "+e.Item.Cells[0].Text);
					cboTipoTelefono.SelectedValue = drTelefono[0]["codTipoTelefono"].ToString();
					cboSucursal.SelectedValue = drTelefono[0]["codSucursal"].ToString();
					txtNumeroTelefono.Text = drTelefono[0]["Numero"].ToString();
					txtNombreContacto.Text = drTelefono[0]["NombreContacto"].ToString();
					txtEmailContacto.Text = drTelefono[0]["EMailContacto"].ToString();
					
					Session["Operacion"] = TipoOperacion.Modificacion;
					Session["codTelefono"] = e.Item.Cells[0].Text; 
					break;
				}
				case TipoOperacion.Baja:
				{
					DataSet dsTelefonos = (DataSet)Session["dsTelefonos"];
					DataRow[] drTelefono = dsTelefonos.Tables["Telefonos"].Select("codTelefono = "+e.Item.Cells[0].Text);
					for(int i = 0;i<=dsTelefonos.Tables["Telefonos"].Rows.Count-1;i++)
					{
						if (dsTelefonos.Tables["Telefonos"].Rows[i]["codTelefono"].ToString() == drTelefono[0]["codTelefono"].ToString())
						{
							dsTelefonos.Tables["Telefonos"].Rows[i].Delete();
							break;
						}
					}

					Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
					oSucursales.UpdateTelefonos(dsTelefonos);
							
					HelperWeb.RemoveAllCache();

					CargarGrilla();
					break;
				}
			}
		}

		private void dgTelefonos_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgTelefonos.CurrentPageIndex = e.NewPageIndex;
			BindearGrilla((DataView)Session["dvTelefonos"]);
		}

		private void dgTelefonos_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			DataView dvTelefonos = (DataView)Session["dvTelefonos"];
			
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
				case "OrdenSucursal":
				{
					dvTelefonos.Sort = "desSucursal "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenSucursal";
					break;
				}
				case "OrdenTipoTelefono":
				{
					dvTelefonos.Sort = "Tipo "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenTipoTelefono";
					break;
				}
			}
			BindearGrilla(dvTelefonos);
		}
	
		private struct TipoOperacion
		{
			public const string Alta = "Alta";
			public const string Modificacion = "Modificar";
			public const string Baja = "Eliminar";
		}
	}
}
