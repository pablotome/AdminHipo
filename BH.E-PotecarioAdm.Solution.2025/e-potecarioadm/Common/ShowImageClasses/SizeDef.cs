using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace BH.EPotecario.Adm.Componentes
{
	public class SizeDef
	{
		#region Private Declarations
		private System.Drawing.Size[] sizes;
		#endregion

		#region Constuctors
		public SizeDef()
		{
			InitValues();
		}
		#endregion

		#region Private Methods
		private void InitValues()
		{
			sizes = new System.Drawing.Size[6];

			// XXL
			sizes[0].Height = 430;
			sizes[0].Width = 650;

			// XL
			sizes[1].Height = 214;
			sizes[1].Width = 325;

			// L
			sizes[2].Height = 124;
			sizes[2].Width = 188;

			// M
			sizes[3].Height = 93;
			sizes[3].Width = 140;

			// S
			sizes[4].Height = 70;
			sizes[4].Width = 107;

			// XS
			sizes[5].Height = 50;
			sizes[5].Width = 50;
		}
		#endregion

		#region Public Methods
		public System.Drawing.Size getFormat(SizeFormatType type)
		{
			return sizes[(int)type];
		}

		public void setFormat(SizeFormatType type, System.Drawing.Size size)
		{
			sizes[(int)type] = size;
		}
		#endregion
	}
}
