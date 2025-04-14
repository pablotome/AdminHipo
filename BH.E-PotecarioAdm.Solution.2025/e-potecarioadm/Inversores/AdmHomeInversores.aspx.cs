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
    public partial class AdmHomeInversores : WebFormBase
    {
        protected BH.EPotecario.Adm.MenuTab MenuTab;
        protected DataGrid dgrHome;
        public int Idioma;
        public string CodSeccionHome
        {
            get
            {
                return Request.QueryString["CodSeccion"] == null ? "" : Request.QueryString["CodSeccion"].ToString();
            }
        }
        #region Manejadores

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");

            this.MenuTab.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab.CurrentMenuItem = "AdmHome";

            if (!Page.IsPostBack)
            {
                var idioma = Session["idioma"] == null ? Convert.ToInt16(ddIdioma.SelectedValue) : Convert.ToInt32(Session["idioma"]);

                CargoSecciones();
                CargarGrilla(idioma);

            }

        }

        public void CargoSecciones()
        {
            //obtengo todas las secciones

            List<SeccionInversores> Secciones = ServicioSistema<SeccionInversores>.GetAll().Where(x => x.Idioma == 1).ToList();

            var seccionesFiltradas = Secciones.Where(x => x.Directorio.StartsWith("SeccionHome") || x.Directorio.StartsWith("MenuInversores"));
            //saco hechos relevantes 
            ddSecciones.DataSource = seccionesFiltradas.Where(x => x.DesSeccion != "Hechos Relevantes");

            ddSecciones.DataTextField = "DesSeccion";

            ddSecciones.DataValueField = "CodSeccion";

            ddSecciones.DataBind();

        }
        protected void dgrHome_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkEditar = (HyperLink)e.Item.FindControl("lnkEditar");
                HyperLink lnkEliminar = (HyperLink)e.Item.FindControl("lnkEliminar");

                HomeInversores Home = (HomeInversores)e.Item.DataItem;

                if (lnkEditar != null)
                {
                    lnkEditar.NavigateUrl = string.Format("~/Inversores/EditHomeInversores.aspx?CodHome={0}", Home.CodSeccionHome);
                    lnkEditar.Visible = true;
                }
                if (lnkEliminar != null)
                {
                    lnkEliminar.NavigateUrl = string.Format("~/Inversores/EditHomeInversores.aspx?CodHome={0}&Eliminar=true", Home.CodSeccionHome);
                    lnkEliminar.Visible = true;
                }

            }
        }

        protected void ddSecciones_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddSecciones.SelectedIndex > -1)
            {
                dgrHome.CurrentPageIndex = 0;

                Idioma = Convert.ToInt16(ddIdioma.SelectedValue);

                var seleccionDd = ddSecciones.SelectedItem.Text;

                var Home = ServicioSistema<HomeInversores>.GetAll()
                    .Where(x => x.Seccion.Idioma == Idioma && x.Seccion.DesSeccion == seleccionDd)
                    .ToList();


                dgrHome.DataSource = Home;

                dgrHome.DataBind();
            }
            else
            {
                Idioma = Convert.ToInt16(ddIdioma.SelectedValue);

                var seleccionDd = ddSecciones.SelectedItem.Text;

                var Home = ServicioSistema<HomeInversores>.GetAll()
                    .Where(x => x.Seccion.Idioma == Idioma).OrderByDescending(x => x.Seccion.DesSeccion).ToList();


                dgrHome.DataSource = Home;

                dgrHome.DataBind();

            }
        }

        protected void dgrHome_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        protected void btnNuevaHome_Click(object sender, EventArgs e)
        {
            var seccion = ddSecciones.SelectedValue.ToString();
            Response.Redirect("~/Inversores/EditHomeInversores.aspx?Seccion=" + seccion);
        }


        #endregion

        #region Metodos

        public void CargarGrilla(int idioma)
        {
            try
            {
                if (!string.IsNullOrEmpty(CodSeccionHome))
                {
                    var index = dgrHome.CurrentPageIndex;
                    ddSecciones.SelectedValue = CodSeccionHome;
                    ddIdioma.SelectedValue = idioma.ToString();
                    var Home = ServicioSistema<HomeInversores>.GetAll()
                   .Where(x => x.Seccion.Idioma == idioma && x.Seccion.CodSeccion == Convert.ToInt16(CodSeccionHome))
                   .OrderByDescending(x => x.Orden).ToList();
                    dgrHome.DataSource = Home;
                    dgrHome.DataBind();


                }
                else
                {

                    var index = dgrHome.CurrentPageIndex;
                    var seleccionDd = ddSecciones.SelectedItem.Text;
                    ddIdioma.SelectedValue = idioma.ToString();
                    var Home = ServicioSistema<HomeInversores>.GetAll()
                   .Where(x => x.Seccion.Idioma == idioma && x.Seccion.DesSeccion == seleccionDd)
                   .OrderByDescending(x => x.Orden).ToList();
                    dgrHome.DataSource = Home;
                    dgrHome.DataBind();
                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void CambiarIdioma()
        {

            CargarGrilla(Convert.ToInt16(ddIdioma.SelectedValue));

        }

        protected void PaginarBusqueda(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgrHome.CurrentPageIndex = e.NewPageIndex;

            Idioma = Convert.ToInt16(ddIdioma.SelectedValue);

            CargarGrilla(Idioma);
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
