using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Security;
using System.Security.Principal;
using System.Threading;

using BH.Sysnet;
using BH.WebControls;
using BH.Util;
using BH.EPotecario.Adm.Componentes;

namespace BH.EPotecario.Adm
{
    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public partial class WebFormBase : System.Web.UI.Page
    {

        string tituloPagina = null;
        protected string titulo = null;   //Titulo Aplicacion  + Titulo de pagina						

        /*protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
        }*/


        public void CheckSecurity(string permiso)
        {
            SysNetPrincipal principal = (SysNetPrincipal)BHContext.Current.User;
            if (!principal.Permissions.Contains(permiso))
                throw new SecurityException("No tiene permisos");

        }

        public static ConfigMenu GetMenuPrincipal()
        {

            HttpContext context = HttpContext.Current;
            if (context.Application["menuPrincipal"] == null)
            {
                context.Application.Lock();
                if (context.Application["menuPrincipal"] == null)
                {

                    ConfigMenu configMenu = new ConfigMenu();
                    configMenu.TituloAplicacion = "Administrador del site E-potecario";

                    //Items
                    ItemMenu item; ItemMenu subItem;
                    //ItemMenu subSubItem;

                    //ROOT item
                    ItemMenu rootItem = new ItemMenu(configMenu);
                    item = new ItemMenu("Home", "default.aspx", "Home", false, "Administrador");
                    rootItem.AddChild(item);

                    item = new ItemMenu("Beneficios", "Beneficios/ImportarExcels.aspx", "Beneficios", false, "Beneficios");
                    rootItem.AddChild(item);

                    subItem = new ItemMenu("admImportarExcels", "Beneficios/ImportarExcels.aspx", "Importar Excels", false, "Importar Excels");
                    item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficios", "Beneficios/AdmBeneficios.aspx", "Beneficios", false, "Beneficios");
					item.AddChild(subItem);

					subItem = new ItemMenu("admAlianzasBeneficios", "Beneficios/AdmAlianzas.aspx", "Alianzas", false, "Alianzas");
					item.AddChild(subItem);

					subItem = new ItemMenu("admSucursalesAlianzas", "Beneficios/AdmSucursales.aspx", "Sucursales", false, "Sucursales");
					item.AddChild(subItem);

					/////////////Sucursales	
					item = new ItemMenu("Sucursales", "Sucursales/AdmSucursales.aspx", "Sucursales", false, "ADM. Sucursales");
                    rootItem.AddChild(item);

                    subItem = new ItemMenu("admSucursales", "Sucursales/AdmSucursales.aspx", "Sucursales", false, "ADM. Sucursales");
                    item.AddChild(subItem);

                    subItem = new ItemMenu("admTelefonos", "Sucursales/AdmTelefonos.aspx", "Teléfonos", false, "Teléfonos");
                    item.AddChild(subItem);

                    item = new ItemMenu("adminParametros", "Parametros/AdmParametros.aspx", "Parametros", false, "Administracion Parámetros");
                    rootItem.AddChild(item);                    

                    subItem = new ItemMenu("adminParametros2", "Parametros/AdmParametros.aspx", "Parámetros", false, "Administracion Parámetros");
                    item.AddChild(subItem);


                  

                    context.Application["menuPrincipal"] = configMenu;
                }
                context.Application.UnLock();
            }
            return (ConfigMenu)context.Application["menuPrincipal"];
        }


        public string TituloPagina
        {
            get { return tituloPagina; }
            set { tituloPagina = value; }
        }

        public string TituloHtml
        {
            get { return titulo; }
            set { titulo = value; }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}
