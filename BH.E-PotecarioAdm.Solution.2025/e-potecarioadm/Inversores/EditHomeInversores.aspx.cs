using MicroSitios.Componentes;
using MicroSitios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BH.EPotecario.Adm.Inversores
{

    public partial class EditHomeInversores : WebFormBase
    {
        protected BH.EPotecario.Adm.MenuTab MenuTab2;
        protected DataGrid dgrHome;


        public int CodSeccionHome
        {
            get
            {
                return (Request.QueryString["CodHome"] != null) ? int.Parse(Request.QueryString["CodHome"]) : 0;
            }
        }

        public string SeccionHome
        {
            get
            {
                return Request.QueryString["Seccion"] == null ? "" : Request.QueryString["Seccion"].ToString();
            }
        }

        public bool Eliminar
        {
            get
            {
                return (Request.QueryString["Eliminar"] != null) ? Convert.ToBoolean(Request.QueryString["Eliminar"]) : false;
            }
        }

        #region Manejadores

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");

            this.MenuTab.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab.CurrentMenuItem = "AdmHome";
            lblError.Visible = false;
            if (!Page.IsPostBack)
            {
                txtOrden.Text = "0";//por defecto
                CargarSeccion();
            }


        }

        public void CargarSeccion()
        {


            if (CodSeccionHome != 0)
            {

                HomeInversores seccionHome = ServicioSistema<HomeInversores>.GetById(x => x.CodSeccionHome == CodSeccionHome);

                ddSeccionHome.SelectedItem.Text = seccionHome.Seccion.DesSeccion;
                ddSeccionHome.SelectedValue = seccionHome.Seccion.CodSeccion.ToString();
                ddSeccionHome.Enabled = false;//no se edita nuevamente la seccion 
                txtOrden.Text = seccionHome.Orden.ToString();
                ddIdioma.SelectedValue = seccionHome.Idioma.ToString() == "Ingles" ? "2" : "1";
                ddIdioma.Enabled = false;
                txtTitulo.Text = seccionHome.Titulo == null ? "" : seccionHome.Titulo.ToString();
                txtCopete.Text = seccionHome.Copete == null ? "" : seccionHome.Copete.ToString();
                txtLink.Text = seccionHome.Link == null ? "" : seccionHome.Link.ToString();

            }
            if (!string.IsNullOrEmpty(SeccionHome))
            {
                SeccionInversores Seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == Convert.ToInt16(SeccionHome));

                ddSeccionHome.SelectedItem.Text = Seccion.DesSeccion;
                ddSeccionHome.SelectedValue = Seccion.CodSeccion.ToString();
                ddSeccionHome.Enabled = false;//no se edita nuevamente la seccion 
            }
            if (Eliminar)
            {
                ddSeccionHome.Enabled = false;
                ddIdioma.Enabled = false;
                txtTitulo.ReadOnly = true;
                txtCopete.ReadOnly = true;
                txtLink.ReadOnly = true;
                txtOrden.ReadOnly = true;
            }

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    string codSeccion = string.Empty;

                    int idioma = Convert.ToInt16(ddIdioma.SelectedValue);
                    //obtengo la seccion a la que va a pertenecer el item 
                    SeccionInversores seccion = ServicioSistema<SeccionInversores>.GetById(x => x.DesSeccion == ddSeccionHome.SelectedItem.Text);


                    if (CodSeccionHome != 0)
                    {
                        if (Eliminar)//eliminamos
                        {
                            HomeInversores seccionHome = ServicioSistema<HomeInversores>.GetById(x => x.CodSeccionHome == CodSeccionHome);
                            ServicioSistema<HomeInversores>.Delete(seccionHome);
                            codSeccion = seccion.CodSeccion.ToString();
                            //ordenamos

                        }
                        else
                        {//es una modificacion

                            HomeInversores seccionHome = ServicioSistema<HomeInversores>.GetById(x => x.CodSeccionHome == CodSeccionHome);
                            seccionHome.Orden = Convert.ToInt16(txtOrden.Text);
                            //no se edita el Idioma
                            seccionHome.Titulo = txtTitulo.Text;
                            seccionHome.Copete = txtCopete.Text;
                            seccionHome.Link = txtLink.Text;

                            //verifico si cambio el orden anterior y ordeno nuevamente 
                            OrdenoItemDeSeccion(seccionHome);
                            codSeccion = seccion.CodSeccion.ToString();
                        }
                    }
                    else
                    {
                        //damos de alta una nueva seccion
                        HomeInversores seccionHome = new HomeInversores();
                        seccionHome.Seccion = seccion;
                        seccionHome.Orden = Convert.ToInt16(txtOrden.Text.ToString());
                        seccionHome.Titulo = txtTitulo.Text;
                        seccionHome.Copete = txtCopete.Text;
                        if (seccion.DesSeccion.Equals("Agenda"))
                        {
                            txtLink.Enabled = false;
                        }
                        else { seccionHome.Link = txtLink.Text; }

                        //verifico si cambio el orden anterior y ordeno nuevamente 
                        OrdenoItemDeSeccion(seccionHome);
                        codSeccion = seccion.CodSeccion.ToString();
                    }

                    Session["idioma"] = ddIdioma.SelectedValue.ToString();
                    Response.Redirect(string.Format("~/Inversores/AdmHomeInversores.aspx?CodSeccion={0}", codSeccion));
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Complete los datos";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void OrdenoItemDeSeccion(HomeInversores home)
        {
            List<HomeInversores> secciones = null;
            //obtengo todas las secciones 

            secciones = ServicioSistema<HomeInversores>.GetAll().Where(x => x.Seccion.CodSeccion == home.Seccion.CodSeccion).ToList();

            try
            {

                List<HomeInversores> ListaOrdenada = new List<HomeInversores>();


                if (home.CodSeccionHome == 0)  //es un alta 
                {
                    var homePorOrden = secciones.OrderByDescending(x => x.Orden);
                    var homeUltimaEnOrden = secciones.OrderBy(x => x.Orden).LastOrDefault().Orden;

                    //verifico si el orden es 0 ya que el item es orden 0 por estar en primer lugar 
                    if (home.Orden == 0)
                    {
                        ServicioSistema<HomeInversores>.SaveOrUpdate(home); //lo guardo y ordeno el resto 
                        foreach (HomeInversores arc in homePorOrden)
                        {
                            //agrego todos los elementos a la lista 
                            ListaOrdenada.Add(arc);
                        }
                        //los demas items quedan en no destacado y se ordenan
                        int newOrden = 0;
                        var nuevaListaOrdenada = ListaOrdenada.ToList().OrderByDescending(i => i.Orden);
                        foreach (HomeInversores homeSeccion in nuevaListaOrdenada)
                        {
                            newOrden += 1;
                            homeSeccion.Orden = newOrden;
                            ServicioSistema<HomeInversores>.SaveOrUpdate(homeSeccion);
                        }
                    }
                    else
                    {
                        //puede ser cualquier numero 
                        ServicioSistema<HomeInversores>.SaveOrUpdate(home);//guardo la original
                        //verifico que no este en la lista ordenada 
                        foreach (HomeInversores arc in homePorOrden)
                        {
                            if (home.Orden == arc.Orden)
                            {
                                arc.Orden = homeUltimaEnOrden + 1;//subo el orden a la repetida 
                                ListaOrdenada.Add(arc);//la agrego en la lista 
                            }
                            else
                            {
                                //agrego todos los elementos a la lista 
                                ListaOrdenada.Add(arc);
                            }
                        }
                        //los demas items quedan en no destacado y se ordenan
                        int newOrden = -1;//arranca en cero 
                        var nuevaListaOrdenada = ListaOrdenada.ToList().OrderByDescending(i => i.Orden);
                        foreach (HomeInversores homeSeccion in nuevaListaOrdenada)
                        {
                            newOrden += 1;
                            homeSeccion.Orden = newOrden;
                            ServicioSistema<HomeInversores>.SaveOrUpdate(homeSeccion);
                        }
                    }

                }
                else //es un a edicion 
                {
                    var homePorOrdenEdita = secciones.Where(x => x.CodSeccionHome != home.CodSeccionHome).OrderByDescending(x => x.Orden);
                    var homeUltimaEnOrdenEdita = secciones.OrderBy(x => x.Orden).LastOrDefault().Orden;
                    if (home.Orden == 0)
                    {

                        ServicioSistema<HomeInversores>.SaveOrUpdate(home);

                        foreach (HomeInversores arc in homePorOrdenEdita)//ordeno Nuevamente
                        {
                            //agrego todos los elementos a la lista 
                            ListaOrdenada.Add(arc);
                        }
                        //ordenamiento de los items
                        int newOrden = 0;
                        var nuevaListaOrdenada = ListaOrdenada.ToList().OrderByDescending(i => i.Orden);
                        foreach (HomeInversores homeSeccion in nuevaListaOrdenada)
                        {
                            if (homeSeccion.CodSeccionHome != home.CodSeccionHome)
                            {
                                newOrden += 1;
                                homeSeccion.Orden = newOrden;
                                ServicioSistema<HomeInversores>.SaveOrUpdate(homeSeccion);
                            }
                            //else
                            //{
                            //    ServicioSistema<HomeInversores>.SaveOrUpdate(home);//va a ser destacado 
                            //}

                        }
                    }
                    else
                    {//puede tener cualquier orden 


                        foreach (HomeInversores arc in homePorOrdenEdita)//ordeno Nuevamente
                        {
                            ServicioSistema<HomeInversores>.SaveOrUpdate(home);//guardo la original

                            if (home.Orden == arc.Orden)
                            {
                                arc.Orden = homeUltimaEnOrdenEdita + 1;//subo el orden a la repetida 
                                ListaOrdenada.Add(arc);//la agrego en la lista 
                            }
                            else
                            {
                                //agrego todos los elementos a la lista 
                                ListaOrdenada.Add(arc);
                            }

                        }
                        //ordenamiento de los items
                        //int newOrden = -1;
                        var nuevaListaOrdenada = ListaOrdenada.ToList().OrderByDescending(i => i.Orden);
                        foreach (HomeInversores homeSeccion in nuevaListaOrdenada)
                        {
                            if (homeSeccion.CodSeccionHome != home.CodSeccionHome)
                            {
                                //newOrden += 1;
                                //homeSeccion.Orden = newOrden;
                                ServicioSistema<HomeInversores>.SaveOrUpdate(homeSeccion);
                            }
                            //else
                            //{
                            //    ServicioSistema<HomeInversores>.SaveOrUpdate(home);//va a ser destacado 
                            //}

                        }

                    }
                }

            }
            catch //(Exception ex)
            {

                HelperWeb.LogEvent("ordeno Destacados", "error Ordenando destacados", "error en web de prensa", true);
            }

        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["idioma"] = ddIdioma.SelectedValue.ToString();
            Response.Redirect("~/Inversores/AdmHomeInversores.aspx");
        }
        #endregion

        #region Metodos



        /// <summary>
        /// Cargamos los controles en la pagina 
        /// </summary>
        public void CargarControles()
        {

        }

        //motramos un mensaje 
        public void MostrarAlert(string msg)
        {

            string script = @"<script type='text/javascript'>
                            alert('{0}');
                        </script>";

            script = string.Format(script, msg);

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }

        #endregion

    }
}
