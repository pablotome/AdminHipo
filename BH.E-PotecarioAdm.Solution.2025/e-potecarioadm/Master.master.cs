using BH.Sysnet;
using BH.Util;
using BH.WebControls;
using System;
using System.Security;
using System.Web;

namespace BH.EPotecario.Adm
{
	public partial class Master : System.Web.UI.MasterPage
	{
		string tituloPagina = null;
		//protected string titulo = null;   //Titulo Aplicacion  + Titulo de pagina						

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Session["menuPrincipal"] = HelperWeb.GetMenuPrincipal();
				ConfigMenu menu = (ConfigMenu)Session["menuPrincipal"];
			}
		}

		public string TituloPagina
		{
			get { return litTituloPagina.Text; }
			set { litTituloPagina.Text = value; }
		}

		public string TituloHTML
		{
			get { return TituloHtml.Text; }
			set { TituloHtml.Text = value; }
		}

		public string CurrentMenuItem
		{
			get { return this.MainMenu.CurrentMenuItem; }
			set { this.MainMenu.CurrentMenuItem = value; }
		}

	}
}