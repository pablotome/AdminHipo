using MicroSitios.Componentes;
using MicroSitios.Dominio.Entidades;
using System;
using System.Web.UI;


namespace BH.EPotecario.Adm.Inversores
{
    public partial class EditSeccionInversores : WebFormBase
    {
        protected BH.EPotecario.Adm.MenuTab MenuTab;
        //protected DataGrid dgrHome;
        public int SeccionId {get;set;}

        public int CodSeccion
        {
            get
            {
                return (Request.QueryString["CodSeccion"] != null) ? int.Parse(Request.QueryString["CodSeccion"]) : 0;
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
            this.MenuTab.CurrentMenuItem = "AdmSecciones";
            lblError.Visible = false;
            if (!Page.IsPostBack)
            {
                CargarSeccion();
            }


        }

        public void CargarSeccion()
        {
          if(CodSeccion!=0)
          {
              SeccionInversores seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == CodSeccion);
              txtSeccion.Text = seccion.DesSeccion.ToString();
              txtSeccion.ReadOnly = true;
              txtDirectorio.Text = seccion.Directorio.ToString();
              ddIdioma.SelectedValue = seccion.Idioma.ToString();

          }
        
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                if (Page.IsValid)
                {
                    if (CodSeccion != 0)
                    {

                        if (Eliminar)//eliminamos
                        {
                            SeccionInversores seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == CodSeccion);
                            ServicioSistema<SeccionInversores>.Delete(seccion);

                        }
                        else
                        {//es una modificacion

                            SeccionInversores seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == CodSeccion);
                            seccion.Idioma = Convert.ToInt16(ddIdioma.SelectedValue.ToString());
                            seccion.Directorio = txtDirectorio.Text;

                            ServicioSistema<SeccionInversores>.SaveOrUpdate(seccion);
                        }


                    }
                    else
                    {
                        //damos de alta una nueva seccion
                        SeccionInversores seccion = new SeccionInversores();
                        seccion.DesSeccion = txtSeccion.Text;
                        seccion.Idioma = Convert.ToInt16(ddIdioma.SelectedValue.ToString());
                        seccion.Directorio = txtDirectorio.Text;

                        ServicioSistema<SeccionInversores>.SaveOrUpdate(seccion);

                    }

                    Session["idioma"] = ddIdioma.SelectedValue.ToString();
                    Response.Redirect("AdmSeccionInversores.aspx");
                }
                else {
                    lblError.Visible = true;
                    lblError.Text = "debe completar la descripcion";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["idioma"] = ddIdioma.SelectedValue.ToString();

            SeccionId = Convert.ToInt32(Request.QueryString["IdSeccion"]);

            SeccionInversores seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == SeccionId);

            Response.Redirect("AdmSeccionInversores.aspx");
        }
        #endregion

        #region Metodos

        

        /// <summary>
        /// Cargamos los controles en la pagina 
        /// </summary>
        public void CargarControles()
        {
            ////Con el id seccion Obtnego el Idioma y la ruta para el archivo que se esta por ingresaer 
            //SeccionId = Convert.ToInt32(Request.QueryString["IdSeccion"]);
            //var seccion = ServicioSistema<SeccionInversores>.GetById(x => x.CodSeccion == SeccionId);
            //txtSeccion.Text = seccion.DesSeccion;
            //txtRuta.Text = seccion.Directorio;
            //txtNombre.Text = "";
            //txtTitulo.Text = "";
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
