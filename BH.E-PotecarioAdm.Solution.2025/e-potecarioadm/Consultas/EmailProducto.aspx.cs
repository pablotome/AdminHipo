using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BH.EPotecario.Adm.Componentes;
using BH.WS.Common;
using System.Collections.Generic;

namespace BH.EPotecario.Adm.Consultas
{
    public partial class EmailProducto : WebFormBase
    {
        ConsultaMail cm;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMCONSULTASMAIL,ADMINISTRACION");

            this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admEmailProducto";

            cm = new ConsultaMail();
            if (!IsPostBack)
            {
                CargarCombos();
            }
        }

        private void CargarCombos()
        {
            this.ddlProductos.DataSource = cm.ObtenerProductos();
            this.ddlProductos.DataTextField = "DesProductoConsulta";
            this.ddlProductos.DataValueField = "CodProductoConsulta";
            this.ddlProductos.DataBind();
            this.ddlProductos.Items.Insert(0, new ListItem("Seleccioná...", "-1"));
            if (ddlProductos.Items.Count == 1)
                ddlProductos.Visible = false;
            else
                ddlProductos.Visible = true;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dtRegistrados = cm.ObtenerMails(this.txtFechaDesde.Text, this.ddlProductos.SelectedValue);

            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename=Emails del Producto {0}.txt", this.ddlProductos.SelectedItem.Text));
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.text";

            int cantColumnas = dtRegistrados.Columns.Count;
            int i;
            foreach (DataColumn dc in dtRegistrados.Columns)
            {
                Response.Write(dc.ColumnName.ToLower());
                Response.Write("\t");
            }

            Response.Write(Environment.NewLine);

            foreach (DataRow dr in dtRegistrados.Rows)
            {
                for (i = 0; i < cantColumnas; i++)
                {
                    Response.Write(dr[i].ToString());
                    Response.Write("\t");
                }
                Response.Write(Environment.NewLine);
            }

            Response.End();
        }
    }
}
