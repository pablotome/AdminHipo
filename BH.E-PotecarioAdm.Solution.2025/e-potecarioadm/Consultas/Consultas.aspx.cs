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
    public partial class Consultas : WebFormBase
    {
        ConsultaMail cm;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMCONSULTASMAIL,ADMINISTRACION");

			this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admConsultas";
            cm = new ConsultaMail();
            if (!IsPostBack)
            {
                CargarGrilla();
                CargarCombos();
            }
        }

        private void CargarGrilla()
        {
            dgProductoTema.DataSource = cm.ObtenerTemasProductos();
            dgProductoTema.DataBind();

            if (dgProductoTema.Items.Count == 0)
            {
                dgProductoTema.Visible = false;
                lblNoItems.Text = "No existen relaciones ''Producto - Tema'' en éste momento.";
            }
            else
            {
                dgProductoTema.Visible = true;
                lblNoItems.Text = string.Empty;
            }
        }

        protected void dgProductoTema_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label litProducto = (Label)e.Item.FindControl("litProducto");
                Label litTema = (Label)e.Item.FindControl("litTema");
                Label litHabilitado = (Label)e.Item.FindControl("litHabilitado");
               
                System.Web.UI.WebControls.Image imgEspera = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgEspera");

                HyperLink hplHabilitar = (HyperLink)e.Item.FindControl("hplHabilitar");
                HyperLink hplEliminar = (HyperLink)e.Item.FindControl("hplEliminar");

                TemaProducto tp = (TemaProducto)e.Item.DataItem;
                ProductoConsulta auxProducto = cm.ObtenerProducto(tp.CodProductoConsulta);
                TemaConsulta auxTema = cm.ObtenerTema(tp.CodTemaConsulta);

                litProducto.Text = auxProducto.DesProductoConsulta.ToString();
                litTema.Text = auxTema.DesTemaConsulta.ToString();

                auxProducto = null;
                auxTema = null;

                litHabilitado.Text = (tp.Habilitado) ? "SI" : "NO";

                imgEspera.Style.Add("display", "none");

                hplHabilitar.Attributes.Add("onclick", string.Format("javascript:HabilitarTemaProducto({0},'{1}','{2}','{3}'); return false;", tp.CodTemaProducto, hplHabilitar.ClientID.ToString(), litHabilitado.ClientID.ToString(), imgEspera.ClientID.ToString()));
                hplHabilitar.NavigateUrl = "#";
                hplHabilitar.Text = (tp.Habilitado) ? "Deshabilitar" : "Habilitar";

                hplEliminar.Attributes.Add("onclick", string.Format("javascript:EliminarTemaProducto({0},'{1}','{2}','{3}'); return false;", tp.CodTemaProducto, hplEliminar.ClientID.ToString(), tp.CodProductoConsulta.ToString(), imgEspera.ClientID.ToString()));
                hplEliminar.NavigateUrl = "#";
                hplEliminar.Text = "Eliminar";

                e.Item.Attributes.Add("Id", "trId" + tp.CodTemaProducto.ToString());

                tp = null;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.pnlDatosCargados.Visible = false;
            LimpiarControles();

            if (ddlTemas.Items.Count == 1 || ddlProductos.Items.Count == 1)
            {
                this.pnlDatos.Visible = false;
                lblNoItems.Text = "No se puede cargar ninguna relación, debido a que no hay Productos ó Temas.";
            }
            else
            {
                this.pnlDatos.Visible = true;
                this.lblNoItems.Text = string.Empty;
            }
        }

        private void CargarCombos()
        {
            this.ddlProductos.DataSource = cm.ObtenerProductos();
            this.ddlProductos.DataTextField = "DesProductoConsulta";
            this.ddlProductos.DataValueField = "CodProductoConsulta";
            this.ddlProductos.DataBind();
            this.ddlProductos.Items.Insert(0, new ListItem("Seleccioná...", "-1"));
           
            this.ddlTemas.DataSource = cm.ObtenerTemas();
            this.ddlTemas.DataTextField = "DesTemaConsulta";
            this.ddlTemas.DataValueField = "CodTemaConsulta";
            this.ddlTemas.DataBind();
            this.ddlTemas.Items.Insert(0, new ListItem("Seleccioná...", "-1"));
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
            {
                TemaProducto tp = new TemaProducto();
                tp.CodTemaConsulta = int.Parse(this.ddlTemas.SelectedItem.Value);
                tp.CodProductoConsulta = int.Parse(this.ddlProductos.SelectedValue);
                tp.Habilitado = this.chkHabilitado.Checked;
                cm.AgregarTemaProducto(tp);
                tp = null;
                CargarGrilla();
                this.pnlDatos.Visible = false;
                this.pnlDatosCargados.Visible = true;
                this.lblNoItems.Text = "La relación ''Producto - Tema'' se agregó correctamente.";
            }
        }

        private bool ValidarDatos()
        {
            IList<TemaProducto> temasProductos = cm.ObtenerTemasProductosPorCodigos(int.Parse(this.ddlProductos.SelectedValue), int.Parse(this.ddlTemas.SelectedItem.Value));
            if (temasProductos.Count > 0)
            {
                this.lblError.Text = "La relación ''Producto - Tema'' ya existe. Elija otra combinación.";
                return true;
            }
            else
            {
                this.lblError.Text = string.Empty;
                return false;
            }
        }

        private void LimpiarControles()
        {
            this.lblNoItems.Text = string.Empty;
            this.lblError.Text = string.Empty;
            this.chkHabilitado.Checked = true;
            this.ddlProductos.SelectedIndex = 0;
            this.ddlTemas.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.pnlDatosCargados.Visible = true;
            this.pnlDatos.Visible = false;
            CargarGrilla();
            LimpiarControles();
        }
    }
}
