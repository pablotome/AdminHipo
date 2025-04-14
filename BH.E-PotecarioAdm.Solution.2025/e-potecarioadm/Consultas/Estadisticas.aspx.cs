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
    public partial class Estadisticas : WebFormBase
    {
        ConsultaMail cm;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMCONSULTASMAIL,ADMINISTRACION");

            this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admEstadisticas";
            cm = new ConsultaMail();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            dgProductoTema.DataSource = cm.ObtenerEstadisticas(Convert.ToDateTime(this.txtFechaDesde.Text), Convert.ToDateTime(this.txtFechaHasta.Text));
            dgProductoTema.DataBind();
            if (dgProductoTema.Items.Count == 0)
            {
                this.dgProductoTema.Visible = false;
                this.lblError.Text = "No existen Consultas para ese período de tiempo.";
            }
            else
            {
                this.dgProductoTema.Visible = true;
                this.lblError.Text = string.Empty;
            }
        }
    }
}
