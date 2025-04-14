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
    public partial class Tema : WebFormBase
    {
        #region Propiedades Privadas
        private int CodTema
        {
            get { return (ViewState["codTema"] != null) ? int.Parse(ViewState["codTema"].ToString()) : -1; }
            set { ViewState["codTema"] = value; }
        }
        ConsultaMail cm;
        #endregion

        #region Evento de Página y Métodos Privados
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMCONSULTASMAIL,ADMINISTRACION");

            this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admTemas";

            cm = new ConsultaMail();
            if (!IsPostBack)
            {
                CargarCombos();

                this.pnlDatos.Visible = false;
                this.chkHabilitado.Checked = true;
                this.btnEliminar.Attributes.Add("onclick",
                    "javascript:return window.confirm('¿Está seguro de eliminar el Tema seleccionado?');");
            }
        }

        private void CargarCombos()
        {
            this.ddlTemas.DataSource = cm.ObtenerTemas();
            this.ddlTemas.DataTextField = "DesTemaConsulta";
            this.ddlTemas.DataValueField = "CodTemaConsulta";
            this.ddlTemas.DataBind();
            this.ddlTemas.Items.Insert(0, new ListItem("Seleccioná...", "-1"));

            if (ddlTemas.Items.Count == 1)
                ddlTemas.Visible = false;
            else
                ddlTemas.Visible = true;
        }

        private void CargarDatos(TemaConsulta tc)
        {
            this.txtDescripcion.Text = tc.DesTemaConsulta;
            this.txtOrden.Text = Convert.ToString(tc.Orden);
            this.chkHabilitado.Checked = tc.Habilitado;
            this.lblError.Text = String.Empty;
        }

        private void LimpiarCarga()
        {
            CodTema = -1;
            this.ddlTemas.SelectedIndex = 0;
            this.txtDescripcion.Text = String.Empty;
            this.txtOrden.Text = String.Empty;
            this.chkHabilitado.Checked = true;
        }

        private bool ValidarOrden()
        {
            bool res = false;

            IList<TemaConsulta> tcs = cm.ObtenerTemas();

            foreach (TemaConsulta tc in tcs)
            {
                if (tc.Orden == int.Parse(this.txtOrden.Text))
                {
                    if (tc.CodTemaConsulta != CodTema)
                        res = true;
                }
            }
            return res;
        }

        private bool ValidarExistencia()
        {
            bool auxExiste = cm.HayAsociacionesConTema(CodTema);
            if (auxExiste)
                this.lblError.Text = "El Tema que querés eliminar tiene una asociación a un Producto. <br/> Primero eliminá la relación e intentá nuevamente.";
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

        protected void ddlTemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTemas.SelectedValue != "-1")
            {
                this.pnlDatos.Visible = true;
                this.btnEliminar.Visible = true;
                TemaConsulta tc = new TemaConsulta();
                CodTema = int.Parse(this.ddlTemas.SelectedValue);
                tc = cm.ObtenerTema(int.Parse(this.ddlTemas.SelectedValue));
                CargarDatos(tc);
                tc = null;
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
                TemaConsulta t = new TemaConsulta();
                if (CodTema != -1)
                    t.CodTemaConsulta = CodTema;

                t.DesTemaConsulta = this.txtDescripcion.Text.Trim();
                t.Orden = int.Parse(this.txtOrden.Text.Trim());
                t.Habilitado = this.chkHabilitado.Checked;

                this.lblError.Text = cm.AgregarTema(t);
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
                this.lblError.Text = cm.EliminarTema(CodTema);
                CargarCombos();
                LimpiarCarga();
                this.pnlDatos.Visible = false;
            }
        }
        #endregion
    }
}
