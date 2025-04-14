#region Using
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
using System.Web.SessionState;
using System.Collections.Generic;

using BH.EPotecario.Adm.Componentes;
using BH.WS.Common;
#endregion

namespace BH.EPotecario.Adm.Consultas
{
    public partial class Producto : WebFormBase
    {
        #region Propiedades Privadas
        private int CodProducto
        {
            get { return (ViewState["codProducto"] != null) ? int.Parse(ViewState["codProducto"].ToString()) : -1; }
            set { ViewState["codProducto"] = value; }
        }
        ConsultaMail cm;
        #endregion

        #region Evento de Página y Métodos Privados
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMCONSULTASMAIL,ADMINISTRACION");

            this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admProductos";
            cm = new ConsultaMail();
            if (!IsPostBack)
            {
                CargarCombos();

                this.txtEmail.Text = cm.ObtenerParametroMail().Replace(';', ' ').Trim();
                this.chkHabilitado.Checked = true;
                this.pnlDatos.Visible = false;
                this.btnEliminar.Attributes.Add("onclick",
                    "javascript:return window.confirm('¿Está seguro de eliminar el Producto seleccionado?');");
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

        private void CargarDatos(ProductoConsulta pc)
        {
            this.txtDescripcion.Text = pc.DesProductoConsulta;
            this.txtEmail.Text = pc.Email;
            this.txtOrden.Text = Convert.ToString(pc.Orden);
            this.chkHabilitado.Checked = pc.Habilitado;
            this.lblError.Text = String.Empty;
        }

        private void LimpiarCarga()
        {
            CodProducto = -1;
            this.ddlProductos.SelectedIndex = 0;
            this.txtDescripcion.Text = String.Empty;
            this.txtEmail.Text = cm.ObtenerParametroMail().Replace(';', ' ').Trim();
            this.txtOrden.Text = String.Empty;
            this.chkHabilitado.Checked = true;
        }

        private bool ValidarOrden()
        {
            bool res = false;

            IList<ProductoConsulta> pcs = cm.ObtenerProductos();

            foreach (ProductoConsulta pc in pcs)
            {
                if (pc.Orden == int.Parse(this.txtOrden.Text))
                {
                    if (pc.CodProductoConsulta != CodProducto)
                        res = true;
                }
            }
            return res;
        }

        private bool ValidarExistencia()
        {
            bool auxExiste = cm.HayAsociacionesConProducto(CodProducto);
            if (auxExiste)
                this.lblError.Text = "El Producto que querés eliminar tiene una asociación a un Tema. <br/> Primero eliminá la relación e intentá nuevamente.";
            return auxExiste;
        }
        #endregion

        #region Eventos de los Controles
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = true;
            this.btnEliminar.Visible = false;
            LimpiarCarga();
            this.lblError.Text = String.Empty;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.pnlDatos.Visible = false;
            LimpiarCarga();
            this.lblError.Text = String.Empty;
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlProductos.SelectedValue != "-1")
            {
                this.pnlDatos.Visible = true;
                this.btnEliminar.Visible = true;
                ProductoConsulta pc = new ProductoConsulta();
                CodProducto = int.Parse(this.ddlProductos.SelectedValue);
                pc = cm.ObtenerProducto(int.Parse(this.ddlProductos.SelectedValue));
                CargarDatos(pc);
                pc = null;
            }
            else
            {
                LimpiarCarga();
                this.pnlDatos.Visible = false;
                this.lblError.Text = String.Empty;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarOrden())
            {
                ProductoConsulta p = new ProductoConsulta();
                if (CodProducto != -1)
                    p.CodProductoConsulta = CodProducto;

                p.DesProductoConsulta = this.txtDescripcion.Text.Trim();
                p.Email = this.txtEmail.Text.Trim();
                p.Orden = int.Parse(this.txtOrden.Text.Trim());
                p.Habilitado = this.chkHabilitado.Checked;

                this.lblError.Text = cm.AgregarProducto(p);
                CargarCombos();
                LimpiarCarga();
                this.pnlDatos.Visible = false; 
            }
            else 
            {
                this.lblError.Text = "El Orden ingresado ya existe. Ingresá otro.";
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarExistencia())
            {
                this.lblError.Text = cm.EliminarProducto(CodProducto);
                CargarCombos();
                LimpiarCarga();
                this.pnlDatos.Visible = false;
            }
        }
        #endregion
    }
}
