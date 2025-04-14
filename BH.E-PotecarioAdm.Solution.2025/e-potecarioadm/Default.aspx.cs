using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BH.WebControls;

namespace BH.EPotecario.Adm 
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class WebFormDefault : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{

			((Master)this.Master).CurrentMenuItem = "Home";

			//ItemMenu item;

			//if( menu.RootItem.ChildItems.Count != 0 )
			//{
			//    //item = (ItemMenu) menu.RootItem.ChildItems[0];
			//    //Response.Redirect(item.Pagina);
			//}
			//else
			//{
			//    Response.Write("No tiene permisos para acceder a esta aplicación.");
			//}
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
