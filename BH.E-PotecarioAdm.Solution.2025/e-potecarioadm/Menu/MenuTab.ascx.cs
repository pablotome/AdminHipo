namespace BH.EPotecario.Adm
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Collections.Specialized;

	using BH.WebControls;
	using System.Web.UI;


	/// </summary>
	public partial  class MenuTab : System.Web.UI.UserControl
	{
		


		ItemMenu itemsMenu = null;
		string strCurrentMenuItem = null;						
		

		public string CurrentMenuItem
		{
			get { return strCurrentMenuItem; }
			set { strCurrentMenuItem = value; }
		}


		
		public ItemMenu ItemsMenu
		{
			get { return itemsMenu; }
			set { itemsMenu = value; }
		}
		
		
	

		protected void Page_Load(object sender, System.EventArgs e)
		{				
			MenuTab1.CreateMenu(HelperWeb.GetMenuPrincipal(), strCurrentMenuItem);			
			//((Master)this.Parent).TituloHTML = MenuTab1.TituloHTML;			
			
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion


	
	}
}
