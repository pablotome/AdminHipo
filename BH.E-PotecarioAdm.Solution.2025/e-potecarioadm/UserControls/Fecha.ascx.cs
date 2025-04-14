using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace BH.EPotecario.Adm
{
	/// <summary>
	///		Summary description for UserControlHeader.
	/// </summary>
	public partial  class UCFecha : System.Web.UI.UserControl
	{
		
		DateTime fechaMaxima = new DateTime(2100, 12, 31);
		string lastError = null;
		bool usarNombreCortos = false;
		


		protected void Page_Load(object sender, System.EventArgs e)
		{				
		
		}		

		public DateTime FechaMaxima
		{
			set {fechaMaxima = value;}
			get {return fechaMaxima;}
		}

		public bool UsarNombreCortos
		{
			set {usarNombreCortos = value;}
			get {return usarNombreCortos;}
		}
		
		public DateTime Fecha
		{
			get
			{
				
				if (EsFechaValida)				
					return GetFecha();														
				else
					throw new Exception("Fecha Invalida." + this.lastError);								
			}

			set
			{
				txtDia.Text = value.Day.ToString();
				txtMes.Text = value.Month.ToString();
				txtAnio.Text = value.Year.ToString();

				if (!EsFechaValida)				
					throw new Exception("Fecha Invalida." + this.lastError);				
			}		
			
		}

		public void SetNullFecha()
		{
			this.txtDia.Text = String.Empty;
			this.txtMes.Text = String.Empty;
			this.txtAnio.Text = String.Empty;
		}

		private DateTime GetFecha()
		{
			try
			{
				int dia;
				if (txtDia.Text.Length != 0 || txtDia.Visible)
					dia = int.Parse(txtDia.Text);
				else
					dia = 1;
				
				int mes = int.Parse(txtMes.Text);
				int anio = int.Parse(txtAnio.Text);			
					
				DateTime date = new DateTime(anio, mes, dia, 0, 0, 0, 0);		 					
				return date;
			}
			catch
			{
				throw new Exception("Fecha Invalida");	
			}
		}


		

		public string LastError
		{
			get {return lastError;}			
		}

		private void CheckFecha()
		{
						
			
			//year
			int anio = CheckRango(txtAnio, 1900, fechaMaxima.Year, "año");			
			
			int mes = CheckRango(txtMes, 1, 12, "mes");			

			//Dia
			int dia = 1;
			if (txtDia.Visible)			
				dia = CheckRango(txtDia, 1, DateTime.DaysInMonth(anio,mes), "día");							

			
			
			DateTime fecha = new DateTime(
				int.Parse(txtAnio.Text), 
				int.Parse(txtMes.Text),
				dia);				
						
			
			if (fecha > fechaMaxima)
				throw new Exception("La fecha no puede ser mayor a " + fechaMaxima.ToShortDateString());

			
		}

		private int CheckRango(TextBox textBox, int min, int max, string componente)
		{
			if (textBox.Text.Length == 0)
				throw new Exception("Debe ingresar el " + componente);
					
			if (!IsInt(textBox.Text))
				throw new Exception("Debe ingresar el " + componente + " con números");

			int numero = int.Parse(textBox.Text);
			if (numero < min || numero > max)
				throw new Exception("Debe ingresar el " + componente + " entre " + min + " y " + max);

			return numero;
		}

		public bool EsFechaValida
		{
			get
			{				
				try
				{					
					CheckFecha();					
					return true;
				}
				catch (Exception ex)
				{					
					this.lastError = ex.Message;
					return false;
				}				
			}
			
		}

		public bool IsInt(string numero)
		{
			bool res = true;
			foreach (char chr in numero)
			{
				if (chr < 48 || chr > 57)
					res = false;
			}

			return res;
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
			this.PreRender += new System.EventHandler(this.UCFecha_PreRender);

		}
		#endregion

		protected void UCFecha_PreRender(object sender, System.EventArgs e)
		{
			if (this.usarNombreCortos)
			{
				lblDia.Text = "D";
				lblMes.Text = "M";
				lblAño.Text = "A";
			}
			else
			{
				lblDia.Text = "Día";
				lblMes.Text = "Mes";
				lblAño.Text = "Año";
			}
		}
	}
}
