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

/*using BH.Intranet.Web.IntraDOWebService;
using BH.Intranet.Common;
using BH.Intranet.Web.Clases;*/

namespace BH.EPotecario.Adm
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class WebFormTareasDeAdmin : System.Web.UI.Page
	{
		
		private DataSet ds
		{
			get{return (DataSet)Session["dsInSession"];}
			set{Session["dsInSession"] = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			HelperWeb.CheckSecurity("ADMGALERIADECOMPRAS,ADMINISTRACION");
			
			if (!this.IsPostBack)
			{
				Componentes.TareasAdmin oTareasAdmin = new BH.EPotecario.Adm.Componentes.TareasAdmin();

				DataSet dsSysTables = oTareasAdmin.GetSystemTables();

				foreach (DataRow dr in dsSysTables.Tables[0].Rows)
				{
					cboTablas.Items.Add(new ListItem(dr["name"].ToString(),dr["name"].ToString()));
				}
			}

			//Vuelvo a crear la tabla genérica porque sino no encuentra los datos para guardar
			if (ViewState["index"] != null)
			{
				crearTablaGenerica((int)ViewState["index"]);
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
			this.dgResultado.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgResultado_ItemCommand);
			this.dgResultado.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgResultado_ItemDataBound);

		}
		#endregion

		protected void btnExportarDatos_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			Componentes.TareasAdmin oTareasAdmin = new BH.EPotecario.Adm.Componentes.TareasAdmin();
			DataSet ds = oTareasAdmin.GetSelectFrom(cboTablas.SelectedItem.Text);
			
			dgExportacion.DataSource = ds;

			Response.Buffer = true;
			Response.ContentType = "application/vnd.ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename="+cboTablas.SelectedItem.Text+".xls");
			Response.Charset = "UTF-8";
			Response.ContentEncoding = System.Text.Encoding.Default;

			dgExportacion.EnableViewState = false;

			dgExportacion.AllowPaging = false;
			dgExportacion.AllowSorting = false;

			dgExportacion.DataBind();

			System.IO.StringWriter tw = new System.IO.StringWriter();
			HtmlTextWriter hw = new HtmlTextWriter(tw);

			dgExportacion.RenderControl(hw);
			Response.Write(tw.ToString());
			
			// Enviamos los datos al cliente
			Response.End();
		}
		private void BuscarDatos(string tableName)
		{
			lblError.Text = "";
			Componentes.TareasAdmin oTareasAdmin = new BH.EPotecario.Adm.Componentes.TareasAdmin();
			ds = oTareasAdmin.GetSelectFrom(tableName);

			dgResultado.DataSource = ds;
			ViewState.Remove("Operacion");
			dgResultado.DataBind();
		}

		protected void cmdBuscar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			btnExportarDatos.Visible = true;
			btnAgregar.Visible = true;
			BuscarDatos(cboTablas.SelectedItem.Text);
		}

		private void dgResultado_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			lblError.Text = "";
			switch (e.CommandName)
			{
				case "Eliminar":
				{
					try
					{
						ds.Tables[0].Rows[e.Item.ItemIndex].Delete();

						Componentes.TareasAdmin oTareasAdmin = new BH.EPotecario.Adm.Componentes.TareasAdmin();
						DataSet dsResultado = oTareasAdmin.UpdateSystemTables(ds,ds.Tables[0].TableName);
					
						BuscarDatos(ds.Tables[0].TableName);
					}
					catch (Exception ex)
					{
						lblError.Text = ex.Message;
					}
					break;
				}
				case "Modificar":
				{
					pnlBusqueda.Visible = false;
					pnlModificar.Visible = true;
					
					crearTablaGenerica(e.Item.ItemIndex);
					ViewState["Operacion"] = "M";
					
					break;
				}
			}
		}
		private void crearTablaGenerica(int index)
		{
			lblError.Text = "";
			ViewState["index"] = index;
			TableRow row;
			TableCell cell;
			for (int i = 0; i <= ds.Tables[0].Columns.Count-1; i++)
			{
				row = new TableRow();
				cell = new TableCell();
				Label oLbl = new Label();
				oLbl.Text = ds.Tables[0].Columns[i].ColumnName;
				cell.Controls.Add(oLbl);
				row.Cells.Add(cell);

				cell = new TableCell();
				TextBox oTxt = new TextBox();
				oTxt.ID = "txt_"+ds.Tables[0].Columns[i].ColumnName;
				oTxt.Width = Unit.Pixel(350);
				if (index != -1)
					oTxt.Text = ds.Tables[0].Rows[index][i].ToString();
				cell.Controls.Add(oTxt);
				row.Cells.Add(cell);

				tblGenerica.Rows.Add(row);
			}
		}
		protected void btnCancelar_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			ViewState.Remove("index");
			pnlBusqueda.Visible = true;
			pnlModificar.Visible = false;
		}

		private void dgResultado_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//HelperWeb.SetClasesCssEnGrilla((DataGrid)sender, e.Item, "tabla2", true);

			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				LinkButton oLnk = (LinkButton)e.Item.Cells[0].Controls[1];
				oLnk.Attributes.Add("onclick","javascript:return window.confirm('Está Seguro que desea eliminar este registro?');");
			}
		}

		protected void btnGuardarCambios_Click(object sender, System.EventArgs e)
		{
			lblError.Text = "";
			Componentes.TareasAdmin oTareasAdmin = new BH.EPotecario.Adm.Componentes.TareasAdmin();
			try
			{
				if (ViewState["Operacion"].ToString() == "A")
				{
					DataTable table = Componentes.DBSchemaSingleton.GetTable(ds.Tables[0].TableName);
					table.Rows.Clear();
					DataRow row = table.NewRow();
					
					foreach (TableRow oRow in tblGenerica.Rows)
					{
						TextBox oTxt = (TextBox)oRow.Cells[1].Controls[0];
						if (!table.Columns[oTxt.ID.Split('_')[1].ToString()].AutoIncrement)
						{
							if (oTxt.Text != "")
							{
								switch(table.Columns[oTxt.ID.Split('_')[1].ToString()].DataType.Name)
								{
									case "String":row[oTxt.ID.Split('_')[1].ToString()] = oTxt.Text; break;
									case "Int32":row[oTxt.ID.Split('_')[1].ToString()] = Int32.Parse(oTxt.Text); break;
									case "DateTime":row[oTxt.ID.Split('_')[1].ToString()] = Convert.ToDateTime(oTxt.Text); break;
									case "Decimal": row[oTxt.ID.Split('_')[1].ToString()] = Convert.ToDecimal(oTxt.Text); break;
								}
							}
							else
							{
								row[oTxt.ID.Split('_')[1].ToString()] = DBNull.Value;
							}
						}
						else
						{
							row[oTxt.ID.Split('_')[1].ToString()] = IDs.Nuevo;
						}
					}

					table.Rows.Add(row.ItemArray);
					DataSet oDs = new DataSet();
					oDs.Tables.Add(table.Copy());
					
					oTareasAdmin.UpdateSystemTables(oDs,ds.Tables[0].TableName);

					ViewState.Remove("index");
					pnlBusqueda.Visible = true;
					pnlModificar.Visible = false;

					BuscarDatos(ds.Tables[0].TableName);
				}
				else
				{
					int index = (int)ViewState["index"];
					int i = 0;
					foreach (TableRow oRow in tblGenerica.Rows)
					{
						TextBox oTxt = (TextBox)oRow.Cells[1].Controls[0];
						if (oTxt.Text != "")
						{
							switch(ds.Tables[0].Columns[oTxt.ID.Split('_')[1].ToString()].DataType.Name)
							{
								case "String":ds.Tables[0].Rows[index][i] = oTxt.Text; break;
								case "Int32":ds.Tables[0].Rows[index][i] = Int32.Parse(oTxt.Text); break;
								case "DateTime":ds.Tables[0].Rows[index][i] = Convert.ToDateTime(oTxt.Text); break;
								case "Decimal": ds.Tables[0].Rows[index][i] = Convert.ToDecimal(oTxt.Text); break;
							}
						}
						else
						{
							ds.Tables[0].Rows[index][i] = DBNull.Value;
						}
						i++;
					}
					
					oTareasAdmin.UpdateSystemTables(ds,ds.Tables[0].TableName);

					ViewState.Remove("index");
					pnlBusqueda.Visible = true;
					pnlModificar.Visible = false;

					BuscarDatos(ds.Tables[0].TableName);
				}
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
			}
			
		}

		protected void btnAgregar_Click(object sender, System.EventArgs e)
		{
			pnlBusqueda.Visible = false;
			pnlModificar.Visible = true;

			ViewState["Operacion"] = "A";
			crearTablaGenerica(-1);
		}

		protected void btnEjecutarQuery_Click(object sender, System.EventArgs e)
		{
			Componentes.TareasAdmin oTareasAdmin = new BH.EPotecario.Adm.Componentes.TareasAdmin();
			oTareasAdmin.ExecuteQuery(txtQuery.Text);

			txtQuery.Text = string.Empty;
			ClientScript.RegisterClientScriptBlock(Page.GetType(), "EjecutadoOK", "<script type=\"text/javascript\">alert('Query Ejecutado Correctamente');</script>");
		}
	}
}
