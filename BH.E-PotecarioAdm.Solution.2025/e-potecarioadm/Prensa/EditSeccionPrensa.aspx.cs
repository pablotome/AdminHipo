using MicroSitios.Componentes;
using MicroSitios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;


namespace BH.EPotecario.Adm.Prensa
{
    public partial class EditSeccionPrensa : WebFormBase
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
            this.CheckSecurity("ADMPRENSA,ADMINISTRACION");

            this.MenuTab.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab.CurrentMenuItem = "admPrensa";
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
              SeccionPrensa seccion = ServicioSistema<SeccionPrensa>.GetById(x => x.CodSeccion == CodSeccion);
              txtSeccion.Text = seccion.DesSeccion.ToString();
              txtSeccion.ReadOnly = true;
              txtUrl.Text = seccion.Url.ToString();
              chkVigente.Checked = seccion.Vigente;

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
                            SeccionPrensa seccion = ServicioSistema<SeccionPrensa>.GetById(x => x.CodSeccion == CodSeccion);

                            List<ArchivoPrensa> ListArchivos = ServicioSistema<ArchivoPrensa>.GetAll().ToList();
                            //Elimino todos los archivos asociados a la seccion
                            if (ListArchivos != null)
                            {
                                foreach (ArchivoPrensa arch in ListArchivos)
                                {
                                    if (arch.Seccion.CodSeccion == CodSeccion)
                                    {
                                        ServicioSistema<ArchivoPrensa>.Delete(arch);
                                    }
                                }
                            }
                            
                            ServicioSistema<SeccionPrensa>.Delete(seccion);
                            Session["CodSeccion"] = null;

                        }
                        else
                        {//es una modificacion

                            SeccionPrensa seccion = ServicioSistema<SeccionPrensa>.GetById(x => x.CodSeccion == CodSeccion);
                            seccion.Vigente = chkVigente.Checked;
                            seccion.Url = txtUrl.Text;

                            ServicioSistema<SeccionPrensa>.SaveOrUpdate(seccion);
                        }


                    }
                    else
                    {
                        //damos de alta una nueva seccion
                        SeccionPrensa seccion = new SeccionPrensa();
                        seccion.DesSeccion = txtSeccion.Text;
                        seccion.Vigente = chkVigente.Checked;
                        seccion.Url = txtUrl.Text;

                        ServicioSistema<SeccionPrensa>.SaveOrUpdate(seccion);
                    }


                    Response.Redirect("AdmSeccionPrensa.aspx");
                }
                else {
                    lblError.Visible = true;
                    lblError.Text = "Debe completar la descripcion";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            /*SeccionId = Convert.ToInt32(Request.QueryString["CodSeccion"]);
			SeccionPrensa seccion = ServicioSistema<SeccionPrensa>.GetById(x => x.CodSeccion == SeccionId);*/

            Response.Redirect("AdmSeccionPrensa.aspx");
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
