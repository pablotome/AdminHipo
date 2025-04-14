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


namespace BH.EPotecario.Parametros
{
	/// <summary>
	/// Summary description for admPromociones.
	/// </summary>
	public partial class admParametros : System.Web.UI.Page
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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			HelperWeb.CheckSecurity("ADMTASACOMBINADA,ADMINISTRACION");

			this.MenuTab1.ItemsMenu = HelperWeb.GetItemsMenuPrincipal();
			this.MenuTab1.CurrentMenuItem = "adminParametros2";

			CargarGrilla();
		}

		protected void CargarGrilla()
		{
			dgParametros.DataSource = ParametrosDB.ObtenerParametros();
			dgParametros.DataBind();
		}

		protected void dgParametros_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem
				|| e.Item.ItemType == ListItemType.Item)
			{
				LinkButton lnkEditar = (LinkButton)e.Item.FindControl("lnkEditar");

				Parametro p = (Parametro)e.Item.DataItem;

				lnkEditar.CommandArgument = string.Format("{0}", p.CodParametro);
				lnkEditar.Click += new EventHandler(lnkEditar_Click);
			}
		}

		void lnkEditar_Click(object sender, EventArgs e)
		{   

			dgParametros.Visible = false;
			pnlModificaciones.Visible = true;

			int codParametro = int.Parse(((LinkButton)sender).CommandArgument);

			Parametro p = ParametrosDB.ObtenerParametro(codParametro);
            var _valor = p.Valor;
            var _Param = p.DesParametro;
            if (_Param == "HTMLBeneficios1" || _Param == "HTMLBeneficios2" || _Param == "HTMLBeneficios3")
            {
                _valor = Server.HtmlDecode(p.Valor).ToString();
            }

			txtCodigo.Text = p.CodParametro.ToString();
			txtParametro.Text = p.DesParametro.ToString();
            txtValor.Text = _valor;
		}

		protected void btnAceptar_Click(object sender, EventArgs e)
		{

			dgParametros.Visible = true;
			pnlModificaciones.Visible = false;

			Parametro p = new Parametro();
            var _Param = txtParametro.Text;
            var _valor = txtValor.Text;
            if (_Param == "HTMLBeneficios1" || _Param == "HTMLBeneficios2" || _Param == "HTMLBeneficios3")
            {
                _valor = Server.HtmlEncode(txtValor.Text);
            }
			p.CodParametro = int.Parse(txtCodigo.Text);
			p.DesParametro = txtParametro.Text;
			p.Valor = _valor;

			ParametrosDB.ActualizarParametro(p);

			CargarGrilla();
		}

		protected void btnCancelar_Click(object sender, EventArgs e)
		{
			dgParametros.Visible = true;
			pnlModificaciones.Visible = false;
		}
	}

	
}
