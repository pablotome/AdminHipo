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
	public class SizeFormat
	{
		#region Private Declarations
		private SizeDef _size_def = new SizeDef();
		#endregion

		#region Constructors
		public SizeFormat()
		{
		}
		#endregion

		#region Public Methods
		public System.Drawing.Size getFormat(SizeFormatType type)
		{
			return this._size_def.getFormat(type);
		}

		public void setFormat(SizeFormatType type, System.Drawing.Size size)
		{
			this._size_def.setFormat(type, size);
		}
		#endregion
	}
}
