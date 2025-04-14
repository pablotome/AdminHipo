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

using BH.EPotecario.Adm.Componentes;
using BH.EPotecario.Adm;


namespace BH.EPotecario.Inversores
{
	/// <summary>
	/// Summary description for admPromociones.
	/// </summary>
    public partial class admInversores : WebFormBase
	{
		protected BH.EPotecario.Adm.MenuTab MenuTab1;
		protected DataGrid dgParametros;
		

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

        #region Manejadores

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");

            this.MenuTab1.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab1.CurrentMenuItem = "admInversores";

            //CargarGrilla();
        }

        protected void dgParametros_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
        }

        void lnkEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Metodos

        protected void CargarGrilla()
        {
            //dgParametros.DataSource = ParametrosDB.ObtenerParametros();
            //dgParametros.DataBind();
        }

        #endregion

		

		
	}

	
}
