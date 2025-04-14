using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MicroSitios.Componentes;
using MicroSitios.Dominio.Entidades;
using BH.EPotecario.Adm.Componentes;

namespace BH.EPotecario.Adm.Inversores
{
    public partial class AdmSeccionInversores : WebFormBase
    {
        protected BH.EPotecario.Adm.MenuTab MenuTab2;
        protected DataGrid dgrSeccion;
        public int Idioma;

        #region Manejadores

        public void CargarGrilla(int idioma)
        {
            try
            {
                if (idioma == 0)
                {
                    var seccion = ServicioSistema<SeccionInversores>.GetAll().ToList();

                    dgrSeccion.DataSource = seccion;

                    dgrSeccion.DataBind();
                }
                else
                {

                    if (dgrSeccion.CurrentPageIndex >= 1)
                    {
                        dgrSeccion.CurrentPageIndex = 0;

                        var seccion = ServicioSistema<SeccionInversores>.GetAll().Where(x => x.Idioma == idioma).ToList();

                        dgrSeccion.DataSource = null;

                        dgrSeccion.DataSource = seccion;

                        dgrSeccion.DataBind();

                    }
                    else
                    {

                        var seccion = ServicioSistema<SeccionInversores>.GetAll().Where(x => x.Idioma == idioma).ToList();

                        dgrSeccion.DataSource = null;

                        dgrSeccion.DataSource = seccion;

                        dgrSeccion.DataBind();
                    }


                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void CambiarIdioma()
        {
            Idioma = Convert.ToInt16(ddIdioma.SelectedValue);

            CargarGrilla(Idioma);

        }

        #endregion

        #region Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");

            this.MenuTab2.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab2.CurrentMenuItem = "AdmSecciones";

            if (!Page.IsPostBack)
            {
                var idioma=Session["idioma"]==null?0:Convert.ToInt32(Session["idioma"]);
                if (idioma == 0)
                {
                    CargarGrilla(Convert.ToInt16(ddIdioma.SelectedValue));
                }
                else {
                    ddIdioma.SelectedValue = idioma.ToString();
                    CargarGrilla(Convert.ToInt16(idioma));
                }
                
            }
        }

        protected void btnNuevaSeccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditSeccionInversores.Aspx");
        }


        protected void PaginarBusqueda(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgrSeccion.CurrentPageIndex = e.NewPageIndex;

            Idioma = Convert.ToInt16(ddIdioma.SelectedValue);

            CargarGrilla(Idioma);
        }

        protected void dgrSeccion_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkEditar = (HyperLink)e.Item.FindControl("lnkEditar");
                HyperLink lnkEliminar = (HyperLink)e.Item.FindControl("lnkEliminar");

                SeccionInversores seccion = (SeccionInversores)e.Item.DataItem;

                if (lnkEditar != null)
                {
                    lnkEditar.NavigateUrl = string.Format("~/Inversores/EditSeccionInversores.aspx?CodSeccion={0}", seccion.CodSeccion);
                    lnkEditar.Visible = true;
                }
                if (lnkEliminar != null)
                {
                    lnkEliminar.NavigateUrl = string.Format("~/Inversores/EditSeccionInversores.aspx?CodSeccion={0}&Eliminar=true", seccion.CodSeccion);
                    lnkEliminar.Visible = true;
                }

            }
        }
        #endregion

        protected void ddIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambiarIdioma();
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {

            EspacioDuenios ed = new EspacioDuenios();
            var url = ed.ObtenerParametro("UrlPaginaInversores");
            Response.Write("<script>window.open('" + url + "','_blank');</script>");

        }

        

        

      
    }
}
