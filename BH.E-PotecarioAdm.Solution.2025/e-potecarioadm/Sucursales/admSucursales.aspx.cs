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

namespace BH.EPotecario.Adm.Sucursales
{
	/// <summary>
	/// Summary description for admSucursales.
	/// </summary>
	public partial class admSucursales : System.Web.UI.Page
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
			this.dgSucursales.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgSucursales_ItemCommand);
			this.dgSucursales.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgSucursales_PageIndexChanged);
			this.dgSucursales.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgSucursales_SortCommand);
			this.dgSucursales.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgSucursales_ItemDataBound);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			HelperWeb.CheckSecurity("SUCURSALES_Y_TELEFONOS,ADMINISTRACION");

			((Master)this.Master).TituloHTML = "Sucursales";
			((Master)this.Master).CurrentMenuItem = "admSucursales";

			if (!IsPostBack)
			{
				lblError.Text = "";
				CargarGrilla();
			}
		}

		private void BindearGrilla(DataView dv)
		{
			dgSucursales.DataSource = dv;
			Session["dvSucursales"] = dv;
			try
			{
				dgSucursales.DataBind();
			}
			catch
			{
				dgSucursales.CurrentPageIndex = 0;
				dgSucursales.DataBind();
			}
		}

		private void CargarGrilla()
		{
			Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
			DataSet dsSucursales = oSucursales.GetSucursales();

			Session["dsSucursales"] = dsSucursales;

			DataView dvSucursales = new DataView(dsSucursales.Tables["Sucursales"]);
			dvSucursales.Sort = "codSucursal ASC";
			Session["Orden"] = "ASC";
			Session["ColumnaUltimoOrden"] = "OrdenSucursal";
			BindearGrilla(dvSucursales);
		}

		private void dgSucursales_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item)||(e.Item.ItemType == ListItemType.AlternatingItem))
			{
				LinkButton lnkEliminar = (LinkButton)e.Item.Cells[e.Item.Cells.Count-1].Controls[3];
				lnkEliminar.Attributes.Add("onclick","javascript:return window.confirm('¿Confirma la Baja del Item?');");

				Literal litAudioNoVidentes = e.Item.FindControl("litAudioNoVidentes") as Literal;
				if (litAudioNoVidentes != null)
				{
					DataRowView drv = e.Item.DataItem as DataRowView;
					DataRow dr = drv.Row;
					litAudioNoVidentes.Text = (bool.Parse(dr["AudioNoVidentes"].ToString()) == true) ? "SI" : "NO";
				}
                
				Literal Vigente = e.Item.FindControl("Vigente") as Literal;
                if (Vigente != null)
                {
                    DataRowView drv = e.Item.DataItem as DataRowView;
                    DataRow dr = drv.Row;
                    Vigente.Text = (bool.Parse(dr["Vigente"].ToString()) == true) ? "SI" : "NO";
                }
            }
		}

		protected void btnAgregar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			txtCodSucursal.Enabled = true;
			pnlAdmSucursales.Visible = true;
			pnlListaSucursales.Visible = false;
			Session["Operacion"] = TipoOperacion.Alta;
			CargarCombos();
		}

		private void BorrarIngresos()
		{
			txtCodSucursal.Text = "";
			txtNombre.Text = "";
			txtDomicilio.Text = "";
            txtCodigoPostal.Text = " ";
            txtHorarioAtencion.Text = "";
			cboProvincia.SelectedValue = "-1";
			cboTipoSucursal.SelectedValue = "-1";
			txtLatitud.Text = string.Empty;
			txtLongitud.Text = string.Empty;
            txtEmailOfEmpresa.Text = string.Empty;
            txtEmailNyp.Text = string.Empty;
        }

		protected void btnCancelar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			pnlAdmSucursales.Visible = false;
			pnlListaSucursales.Visible = true;
			BorrarIngresos();
		}

		private void CargarCombos()
		{
			cboTipoSucursal.Items.Clear();
			cboProvincia.Items.Clear();

			Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
			DataSet dsTiposSucursales = oSucursales.GetTiposSucursales();
			DataSet dsProvincias = oSucursales.GetProvincias();

			cboTipoSucursal.Items.Add(new ListItem("Seleccionar...","-1"));
			cboProvincia.Items.Add(new ListItem("Seleccionar...","-1"));

			foreach(DataRow oRow in dsTiposSucursales.Tables["TiposSucursales"].Rows)
			{
				cboTipoSucursal.Items.Add(new ListItem(oRow["desTipoSucursal"].ToString(),oRow["codTipoSucursal"].ToString()));
			}
			foreach(DataRow oRow in dsProvincias.Tables["Provincias"].Rows)
			{
				cboProvincia.Items.Add(new ListItem(oRow["nomProvincia"].ToString(),oRow["codProvincia"].ToString()));
			}
		}

		private bool ValidarIngreso()
		{
			decimal decAux;
			if (cboProvincia.SelectedValue  == "-1")
			{
				lblError.Text = "Debe Seleccionar una Provincia.";
				return false;
			}
			if (cboTipoSucursal.SelectedValue == "-1")
			{
				lblError.Text = "Debe seleccionar el Tipo de Sucursal.";
				return false;
			}
			if (txtCodSucursal.Text == "")
			{
				lblError.Text = "Debe ingresar un Código de Sucursal.";
				return false;
			}
			if (txtDomicilio.Text == "")
			{
				lblError.Text = "Debe ingresar un Domicilio.";
				return false;
			}
			if (txtHorarioAtencion.Text == "")
			{
				lblError.Text = "Debe ingresar el Horario de Atención.";
				return false;
			}
			if (txtNombre.Text == "")
			{
				lblError.Text = "Debe ingresar el Nombre de la Sucursal.";
				return false;
			}

            if (!Page.IsValid)
            {
                lblError.Text = "Debe ingresar correos validos separados por punto y coma (;)" ;
                return false;
            }

            if (!decimal.TryParse(txtLatitud.Text, out decAux))
			{
				lblError.Text = "El valor de la latitud ingresado es incorrecto.";
				return false;
			}
			if (!decimal.TryParse(txtLongitud.Text, out decAux))
			{
				lblError.Text = "El valor de la longitud ingresado es incorrecto.";
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
							DataSet dsSucursales = (DataSet)Session["dsSucursales"];
							DataRow drNuevaSucursal = dsSucursales.Tables["Sucursales"].NewRow();
							drNuevaSucursal["codSucursal"] = txtCodSucursal.Text;
							drNuevaSucursal["desSucursal"] = txtNombre.Text;
							drNuevaSucursal["codTipoSucursal"] = cboTipoSucursal.SelectedValue;
							drNuevaSucursal["codZonaCotizacion"] = DBNull.Value;
							drNuevaSucursal["codProvincia"] =cboProvincia.SelectedValue;
							drNuevaSucursal["Domicilio"] = txtDomicilio.Text;
                            drNuevaSucursal["CodigoPostal"] = txtCodigoPostal.Text;
                            drNuevaSucursal["HorarioAtencion"] = txtHorarioAtencion.Text;
							drNuevaSucursal["AudioNoVidentes"] = chkAudioNoVidentes.Checked;
							drNuevaSucursal["Latitud"] = decimal.Parse(txtLatitud.Text);
							drNuevaSucursal["Longitud"] = decimal.Parse(txtLongitud.Text);
                            drNuevaSucursal["EMailOficialEmpresa"] = txtEmailOfEmpresa.Text;
							drNuevaSucursal["EmailOficialNYP"] = txtEmailNyp.Text;
                            drNuevaSucursal["Vigente"] = checkBoxV.Checked;
                            dsSucursales.Tables["Sucursales"].Rows.Add(drNuevaSucursal);
												
							Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
							oSucursales.UpdateSucursales(dsSucursales);
						
							break;
						}
						case TipoOperacion.Modificacion:
						{

							DataSet dsSucursales = (DataSet)Session["dsSucursales"];
							DataRow[] drNuevaSucursal = dsSucursales.Tables["Sucursales"].Select("codSucursal = "+txtCodSucursal.Text);
							drNuevaSucursal[0]["desSucursal"] = txtNombre.Text;
							drNuevaSucursal[0]["codTipoSucursal"] = cboTipoSucursal.SelectedValue;
							drNuevaSucursal[0]["codZonaCotizacion"] = DBNull.Value;
							drNuevaSucursal[0]["codProvincia"] =cboProvincia.SelectedValue;
                            drNuevaSucursal[0]["Domicilio"] = txtDomicilio.Text;
                            drNuevaSucursal[0]["CodigoPostal"] = txtCodigoPostal.Text;
							drNuevaSucursal[0]["HorarioAtencion"] = txtHorarioAtencion.Text;
							drNuevaSucursal[0]["AudioNoVidentes"] = chkAudioNoVidentes.Checked;
							drNuevaSucursal[0]["Latitud"] = decimal.Parse(txtLatitud.Text);
							drNuevaSucursal[0]["Longitud"] = decimal.Parse(txtLongitud.Text);
							drNuevaSucursal[0]["EMailOficialEmpresa"] = txtEmailOfEmpresa.Text;
							drNuevaSucursal[0]["EmailOficialNYP"] = txtEmailNyp.Text;
                            drNuevaSucursal[0]["Vigente"] = checkBoxV.Checked;
                            Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
							oSucursales.UpdateSucursales(dsSucursales);
							
							break;
						}
					}

					HelperWeb.RemoveAllCache();

					CargarGrilla();

					BorrarIngresos();
					pnlAdmSucursales.Visible = false;
					pnlListaSucursales.Visible = true;
				}
				catch (Exception ex)
				{
					lblError.Text = "Ha ocurrido un Error. "+ex.Message;
				}
			}
		}

		private void dgSucursales_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			switch (e.CommandName)
			{
				case TipoOperacion.Modificacion:
				{
					pnlAdmSucursales.Visible = true;
					pnlListaSucursales.Visible = false;
					CargarCombos();

					DataSet dsSucursales = (DataSet)Session["dsSucursales"];
					DataRow[] drSucursal = dsSucursales.Tables["Sucursales"].Select("codSucursal = "+e.Item.Cells[0].Text);
					cboProvincia.SelectedValue = drSucursal[0]["codProvincia"].ToString();
					cboTipoSucursal.SelectedValue = drSucursal[0]["codTipoSucursal"].ToString();
					txtCodSucursal.Text = drSucursal[0]["codSucursal"].ToString();
					txtCodSucursal.Enabled = false;
					txtNombre.Text= drSucursal[0]["DesSucursal"].ToString();
					txtDomicilio.Text = drSucursal[0]["Domicilio"].ToString();
                    txtCodigoPostal.Text = drSucursal[0]["CodigoPostal"].ToString();
                    txtHorarioAtencion.Text = drSucursal[0]["HorarioAtencion"].ToString();
					chkAudioNoVidentes.Checked = bool.Parse(drSucursal[0]["AudioNoVidentes"].ToString());
					txtLatitud.Text = drSucursal[0]["Latitud"].ToString();
					txtLongitud.Text = drSucursal[0]["Longitud"].ToString();
                    txtEmailNyp.Text = drSucursal[0]["EmailOficialNYP"].ToString();
                    txtEmailOfEmpresa.Text= drSucursal[0]["EMailOficialEmpresa"].ToString();
					txtEmailNyp.Text = drSucursal[0]["EmailOficialNYP"].ToString();
                    checkBoxV.Checked = bool.Parse(drSucursal[0]["Vigente"].ToString());
                    Session["Operacion"] = TipoOperacion.Modificacion;
					break;
				}
				case TipoOperacion.Baja:
				{
					Componentes.Sucursales oSucursales = new BH.EPotecario.Adm.Componentes.Sucursales();
					DataSet dsSucursales = (DataSet)Session["dsSucursales"];
					DataSet dsTelefonos = oSucursales.GetTelefonos();
					DataRow[] drSucursal = dsSucursales.Tables["Sucursales"].Select("codSucursal = "+e.Item.Cells[0].Text);
					DataRow[] drTelefonos = dsTelefonos.Tables["Telefonos"].Select("codSucursal = "+e.Item.Cells[0].Text);
					
					//PRIMERO ELIMINO LOS TELEFONOS DE LA SUCURSAL
					for(int i = 0;i<=dsTelefonos.Tables["Telefonos"].Rows.Count-1;i++)
					{
						for (int j = 0;j<=drTelefonos.Length-1;j++)
						{
							if ((dsTelefonos.Tables["Telefonos"].Rows[i].RowState != DataRowState.Deleted)&&(drTelefonos[j].RowState != DataRowState.Deleted))
							{
								if (dsTelefonos.Tables["Telefonos"].Rows[i]["codTelefono"].ToString() == drTelefonos[j]["codTelefono"].ToString())
								{
									dsTelefonos.Tables["Telefonos"].Rows[i].Delete();
								}
							}
						}
					}

					//ELIMINO LA SUCURSAL
					for(int i = 0;i<=dsSucursales.Tables["Sucursales"].Rows.Count-1;i++)
					{
						if (dsSucursales.Tables["Sucursales"].Rows[i]["codSucursal"].ToString() == drSucursal[0]["codSucursal"].ToString())
						{
							dsSucursales.Tables["Sucursales"].Rows[i].Delete();
							break;
						}
					}

					oSucursales.UpdateTelefonos(dsTelefonos);
					oSucursales.UpdateSucursales(dsSucursales);
	
					HelperWeb.RemoveAllCache();

					CargarGrilla();
					break;
				}
			}
		}

		private void dgSucursales_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgSucursales.CurrentPageIndex = e.NewPageIndex;
			BindearGrilla((DataView)Session["dvSucursales"]);
		}

		private void dgSucursales_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			DataView dvSucursales = (DataView)Session["dvSucursales"];
			
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
					dvSucursales.Sort = "codSucursal "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenSucursal";
					break;
				}
				case "OrdenNombre":
				{
					dvSucursales.Sort = "desSucursal "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenNombre";
					break;
				}
				case "OrdenProvincia":
				{
					dvSucursales.Sort = "nomProvincia "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenProvincia";
					break;
				}
				case "OrdenTipoSucursal":
				{
					dvSucursales.Sort = "desTipoSucursal "+Session["Orden"].ToString();
					Session["ColumnaUltimoOrden"] = "OrdenTipoSucursal";
					break;
				}
			}
			BindearGrilla(dvSucursales);
		}

		private struct TipoOperacion
		{
			public const string Alta = "Alta";
			public const string Modificacion = "Modificar";
			public const string Baja = "Eliminar";
		}

	}

	
}
