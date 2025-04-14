using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MicroSitios.Componentes;
using MicroSitios.Dominio.Entidades;

namespace BH.EPotecario.Adm.Prensa
{
    public partial class AdmSeccionPrensa : WebFormBase
    {
        protected BH.EPotecario.Adm.MenuTab MenuTab2;
        protected DataGrid dgrSeccion;
        public int Idioma;

        #region Manejadores

        public void CargarGrilla(int seccion)
        {
            try
            {
                var secciones = ServicioSistema<SeccionPrensa>.GetAll().ToList();
                dgrSeccion.DataSource = secciones;
                dgrSeccion.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Metodos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMPRENSA,ADMINISTRACION");
            this.MenuTab2.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab2.CurrentMenuItem = "AdmSeccionPrensa";
            if (!Page.IsPostBack)
            {
               CargarGrilla(1);
            }
        }

        protected void btnNuevaSeccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditSeccionPrensa.aspx");
        }


        protected void PaginarBusqueda(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            dgrSeccion.CurrentPageIndex = e.NewPageIndex;

            CargarGrilla(1);
        }

        protected void dgrSeccion_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item)
            {
                HyperLink lnkEditar = (HyperLink)e.Item.FindControl("lnkEditar");
                HyperLink lnkEliminar = (HyperLink)e.Item.FindControl("lnkEliminar");
				Literal litTipoArchivo = (Literal)e.Item.FindControl("litTipoArchivo");

                SeccionPrensa seccion = (SeccionPrensa)e.Item.DataItem;

                if (lnkEditar != null)
                {
                    lnkEditar.NavigateUrl = string.Format("~/Prensa/EditSeccionPrensa.aspx?CodSeccion={0}", seccion.CodSeccion);
                    lnkEditar.Visible = true;
                }
                if (lnkEliminar != null)
                {
                    lnkEliminar.NavigateUrl = string.Format("~/Prensa/EditSeccionPrensa.aspx?CodSeccion={0}&Eliminar=true", seccion.CodSeccion);
                    lnkEliminar.Visible = true;
                }

				if (litTipoArchivo != null)
				{ 
					litTipoArchivo.Text = seccion.DesTipoArchivo;
				}

            }
        }
        #endregion

        protected void ddVigente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        

        

      
    }
}
