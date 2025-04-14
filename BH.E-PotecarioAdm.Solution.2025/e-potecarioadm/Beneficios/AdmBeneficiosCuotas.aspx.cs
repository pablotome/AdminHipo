using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BH.EPotecario.Adm
{
	public partial class AdmBeneficiosCuotas : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			((Master)this.Master).CurrentMenuItem = "admBeneficiosCuotas";
		}
	}
}