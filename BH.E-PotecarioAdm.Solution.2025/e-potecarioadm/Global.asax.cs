using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;


using System.Security.Principal;
using System.Threading;
using System.Globalization;

using BH.Sysnet;
using BH.Util;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using BH.EPotecario.Adm.Servicios;

namespace BH.EPotecario.Adm 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
        
		public static IServiceProvider ServiceProvider;

        private static readonly ILog logError = LogManager.GetLogger("HipotecarioAdmError");

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			BH.WebControls.DatePicker.LanguageScript = "~/scripts/calendar/calendar-es.js";

			log4net.Config.XmlConfigurator.Configure();

            var services = new ServiceCollection();

            // Registrar dependencias
            services.AddScoped<IRepositorioParametria, RepositorioParametria>();
            services.AddScoped<IExcelBeneficiosServicio, ExcelBeneficiosServicio>();

            ServiceProvider = services.BuildServiceProvider();
        }
 
		protected void Session_Start(Object sender, EventArgs e)
		{
			BHContext.Current.User = SI.GetValidatedSysNetPrincipal();
			Session["sysnetPrincipal"] = BHContext.Current.User;
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			string language = "es-AR"; //Español - Argentina							
			Thread.CurrentThread.CurrentCulture = new CultureInfo(language, false );
		}

		protected void Global_PreRequestHandlerExecute(object sender, System.EventArgs e)
		{
			if (BHContext.Current.User != null 
				&& BHContext.Current.User is SysNetPrincipal)
				BHContext.Current.User = (SysNetPrincipal) Session["sysnetPrincipal"];
			else
				BHContext.Current.User = SI.GetValidatedSysNetPrincipal();
		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			Exception ex = Server.GetLastError();							
			string msg = "Error en Admin hipotecario.com.ar." + "\r\n\r\n";			
			msg += "Page: " + Request.Url.AbsoluteUri + "\r\n\r\n";
			msg += GetBrowserInfo();
			
			#if (!DEBUG)							
				Server.ClearError();
				string server = Environment.MachineName;
				Response.Write("Ocurrió un error. Por favor guarde los siguiente datos y avise a soporte.");
				Response.Write("<br>");
				Response.Write("<br>");
				Response.Write("Server: "+ server  + "<br>");
				Response.Write("Código de log: " + id.ToString());
			#endif

			string infoEx = bhException.GetInfoWithInnerAndEnviroment(ex);
			logError.Error(infoEx, ex);
		}

		private string GetBrowserInfo()
		{
			//Agrego info de Browser
			HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;			
			string info = "Browser Information\r\n";
			info += "--------------------\r\n";	
			try
			{		
				info += "Type = " + bc.Type+ "    Name = " + bc.Browser +  "    Version = " + bc.Version + "    Is Beta = " + bc.Beta+ "\r\n";			
				info += "Platform = " + bc.Platform + "    Is Win16 = " + bc.Win16 +    "    Is Win32 = " + bc.Win32+ "\r\n";
				info += "Supports Cookies = " + bc.Cookies + "      Supports JavaScript = " + bc.EcmaScriptVersion;
			}
			catch (Exception exInfoBrowser)
			{
				info += "Error obteniendo información del Browser. Ex.Msg:" + exInfoBrowser.Message;
			}		

			return info;
		}


		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
			this.PreRequestHandlerExecute += new System.EventHandler(this.Global_PreRequestHandlerExecute);
		}
		#endregion
	}
}

