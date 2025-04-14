using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Security.Principal;
using System.Globalization;

using BH.Util;
using log4net;
using BH.Sysnet;
using System.Security;
using BH.WebControls;

namespace BH.EPotecario.Adm
{
	/// <summary>
	/// Summary description for HelperWeb.
	/// </summary>
	public class HelperWeb
	{
		private static readonly ILog logError = LogManager.GetLogger("HipotecarioAdmError");

		private HelperWeb()
		{
			
		}

		public static int WriteToLog(string msg, Exception ex) 
		{
			logError.Error(msg, ex);
           
			return 0;
		}

		public static void RegisterStartupScript(Page page, string key, string script)
		{			
			if (! page.ClientScript.IsClientScriptBlockRegistered(key))
				page.ClientScript.RegisterStartupScript(page.GetType(), key, "<script language=\"JavaScript\">"
					+ script  + "</script>");
		}

		private static void SetClientRegExpValidation(WebControl control, string regExp , int length)
		{					
			control.Attributes.Add("onkeypress","checkOnKeyPress('" + regExp + "')");
			control.Attributes.Add("onPaste","return checkOnPaste('" + regExp + "')");
			control.Attributes.Add("maxlength", length.ToString());
		}

		private static void SetClientRegExpValidation(RegularExpressionValidator validator, int length)
		{				
			WebControl controlBuscado = (WebControl) validator.Parent.FindControl(validator.ControlToValidate);
			string regExp = validator.ValidationExpression;
			SetClientRegExpValidation(controlBuscado, regExp, length);
		}

		public static void SetControlValidations(WebControl control, RegularExpressionValidator validator, TipoRegExp type, int length)
		{
			validator.ValidationExpression = GetRegEx(type,length);			
			HelperWeb.SetClientRegExpValidation(validator, length);			
		}

		public static void SetControlValidations(WebControl control, TipoRegExp type, int length)
		{
			HelperWeb.SetClientRegExpValidation(control, GetRegEx(type,length), length);
		}

		public static void SetControlValidations(WebControl control, int length)
		{
			//Todos menos "Ascii 254=¶"
			string exp = @"^[^\xFE]" + GetLargoRegExp(length) + "$";
			HelperWeb.SetClientRegExpValidation(control, exp, length);			
		}

		private static string GetRegEx(TipoRegExp type, int length)
		{
			switch (type)
			{
				case TipoRegExp.Letra: 
					return HelperWeb.GetRegExSoloLetras(length);

				case TipoRegExp.AlfaNumerico: 
					return HelperWeb.GetRegExTexto(length);
					
				case TipoRegExp.Entero:
					return HelperWeb.GetRegExNumEnteros(length);					
				
				case TipoRegExp.Decimal:
					return HelperWeb.GetRegExNumDecimales(length);					
				
				default:				
					return "No se encuentra el tipo de dato";
				
			}
		}

		
		private static string GetLargoRegExp(int largo)
		{
			if (largo == -1)
				return null;

			return "{0," + largo.ToString()+ "}";
		}

		public static string GetRegExTexto()
		{
			return GetRegExTexto(-1);
		}
		
		public static string GetRegExTexto(int largo)
		{			
			//string exp = @"^[0-9a-zA-Z.,-@/():;#_∫∞Ò·ÈÌÛ˙¡…Õ”⁄—\r\n\\\\ \']" + GetLargoRegExp(largo) + "$";			
			string exp = @"^[^<>|]" + GetLargoRegExp(largo) + "$";						
			return exp;
		}
		public static string GetRegExSoloLetras(int largo)
		{				
			string exp = @"^[^<>|0-9]" + GetLargoRegExp(largo) + "$";						
			return exp;
		}


		public static string GetRegExNumEnteros()
		{
			return GetRegExNumEnteros(-1);
		}
		
		public static string GetRegExNumEnteros(int largo)
		{
			string exp = "^[0-9]" + GetLargoRegExp(largo) + "$";			
			return exp;
		}

		public static string GetRegExNumDecimales(int largo)
		{
			string exp = "^[0-9,.-]" + GetLargoRegExp(largo) + "$";			
			return exp;
		}

		public static void RemoveCache(string key)
		{
			HttpRuntime.Cache.Remove(key);
		}

		public static void RemoveAllCache()
		{
			ArrayList itemsPorBorrar = new ArrayList();
			foreach (DictionaryEntry item in HttpRuntime.Cache)
			{
				itemsPorBorrar.Add(item.Key);
			}


			foreach (string item in itemsPorBorrar)
			{
				RemoveCache(item);
			}
		}

		public static void LogEvent(string metodo, string strIn, string strOut, bool error)
		{
			
		}

		public static string Concat(string separator, params string[] strings)
		{
			string result = string.Empty;
			for (int i = 0; i < strings.Length; i++)
			{
				if (i > 0)
					result += separator;
				result += strings[i];
			}    
			return result;
		}

		public static NumberFormatInfo GetFormatoDecimalComa()
		{
			NumberFormatInfo nfi = new CultureInfo("es-AR").NumberFormat;
			nfi.CurrencyDecimalDigits = 2;
			nfi.CurrencyDecimalSeparator = ",";
			return nfi;
		}

		public static byte[] Stream2Byte(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}
/*
		public static string Encode(string cadena)
		{
			//cadena = cadena.replace(/\r\n/g,"\n");
			string utftext = "";

			for (int n = 0; n < cadena.Length; n++) 
			{
				char c = cadena[n];

				if (c < 128) 
				{
					utftext += string.from.from.fromCharCode(c);
				}
				else if((c > 127) && (c < 2048)) 
				{
					utftext += String.fromCharCode((c >> 6) | 192);
					utftext += String.fromCharCode((c & 63) | 128);
				}
				else 
				{
					utftext += String.fromCharCode((c >> 12) | 224);
					utftext += String.fromCharCode(((c >> 6) & 63) | 128);
					utftext += String.fromCharCode((c & 63) | 128);
				}

			}

			return utftext;
		}*/

		/*public static string Decode(string utftext)
		{
			string st = "";
			int i = 0;
			int c = c1 = c2 = 0;

			while ( i < utftext.Length ) {
				
				c = utftext[i];

				if (c < 128) {
					st += c;//String.fromCharCode(c);
					i++;
				}
				else if((c > 191) && (c < 224)) {
					c2 = utftext[]//utftext.charCodeAt(i+1);
					st += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
					i += 2;
				}
				else {
					c2 = utftext.charCodeAt(i+1);
					c3 = utftext.charCodeAt(i+2);
					st += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
					i += 3;
				}

			}

			return st;
		}

		}*/

		/*public static string Decode(string unicodeString)
		{
			//string unicodeString = "This string contains the unicode character Pi(\u03a0)";

			// Create two different encodings.
			Encoding ascii = Encoding.ASCII;
			Encoding unicode = Encoding.Unicode;

			// Convert the string into a byte[].
			byte[] unicodeBytes = unicode.GetBytes(unicodeString);

			// Perform the conversion from one encoding to the other.
			byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            
			// Convert the new byte[] into a char[] and then into a string.
			// This is a slightly different approach to converting to illustrate
			// the use of GetCharCount/GetChars.
			char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
			ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
			string asciiString = new string(asciiChars);

			// Display the strings created before and after the conversion.
			//Console.WriteLine("Original string: {0}", unicodeString);
			//Console.WriteLine("Ascii converted string: {0}", asciiString);
			return asciiString;
		}*/

		public static bool IsNumeric(System.Object Expression)
		{
			if (Expression == null || Expression is DateTime)
				return false;

			if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
				return true;

			try
			{
				if (Expression is string)
					Double.Parse(Expression as string);
				else
					Double.Parse(Expression.ToString());
				return true;
			}
			catch { } // just dismiss errors but return false
			return false;
		}

		public static string LinkSincroPortal(string urlArchivo)
		{
			HttpRequest req = HttpContext.Current.Request;
			return string.Format("{0}/{1}", req.Url.Scheme + "://" + req.Url.Authority + req.ApplicationPath.TrimEnd('/'), ConfigurationManager.AppSettings["urlFolderImage"].ToLower().Replace(req.PhysicalApplicationPath.ToLower(), string.Empty) + urlArchivo).Replace("\\", "/");
		}

		public static void CheckSecurity(string permiso)
		{
			return;
			SysNetPrincipal principal = (SysNetPrincipal)BHContext.Current.User;
			if (!principal.Permissions.Contains(permiso))
				throw new SecurityException("No tiene permisos");

		}

		public static ItemMenu GetItemsMenuPrincipal()
		{
			//Mantenido por compatibilidad;
			return null;
		}

		public static ConfigMenu GetMenuPrincipal()
		{

			HttpContext context = HttpContext.Current;
			if (context.Application["menuPrincipal"] == null)
			{
				context.Application.Lock();
				if (context.Application["menuPrincipal"] == null)
				{

					ConfigMenu configMenu = new ConfigMenu();
					configMenu.TituloAplicacion = "Administrador del site E-potecario";

					//Items
					ItemMenu item; ItemMenu subItem;
					//ItemMenu subSubItem;

					//ROOT item
					ItemMenu rootItem = new ItemMenu(configMenu);
					
					//Home
					item = new ItemMenu("Home", "default.aspx", "Home", false, "Administrador");
					rootItem.AddChild(item);

					//Beneficios
					item = new ItemMenu("Beneficios", "Beneficios/default.aspx", "Beneficios", false, "Beneficios");
					rootItem.AddChild(item);

					subItem = new ItemMenu("admImportarExcels", "Beneficios/ImportarExcels.aspx", "Importar Excels", false, "Importar Excels");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficios", "Beneficios/admBeneficios.aspx", "Beneficios", false, "Beneficios");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosAlianzas", "Beneficios/admBeneficiosAlianzas.aspx", "Alianzas", false, "Alianzas");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosSucursales", "Beneficios/admBeneficiosSucursales.aspx", "Sucursales", false, "Sucursales");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosAhorros", "Beneficios/admBeneficiosAhorros.aspx", "Ahorros", false, "Ahorros");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosTipoCliente", "Beneficios/admBeneficiosTipoCliente.aspx", "Tipos de Clientes", false, "Tipos de Clientes");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosCuotas", "Beneficios/admBeneficiosCuotas.aspx", "Cuotas", false, "Cuotas");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosDiaSemana", "Beneficios/admBeneficiosDiaSemana.aspx", "DÌas de la Semana", false, "DÌas de la Semana");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosMediosPago", "Beneficios/admBeneficiosMediosPago.aspx", "Medios de Pago", false, "Medios de Pago");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosProvincias", "Beneficios/admBeneficiosProvincias.aspx", "Provincias", false, "Provincias");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosLocalidades", "Beneficios/admBeneficiosLocalidades.aspx", "Localidades", false, "Localidades");
					item.AddChild(subItem);

					//subItem = new ItemMenu("admBeneficiosSucursales", "Beneficios/admBeneficiosSucursales.aspx", "Sucursales", false, "Sucursales");
					//item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosCategorias", "Beneficios/admBeneficiosCategorias.aspx", "Categorias", false, "Categorias");
					item.AddChild(subItem);

					subItem = new ItemMenu("admBeneficiosBasesYCondiciones", "Beneficios/admBeneficiosBasesYCondiciones.aspx", "Bases y Condiciones", false, "Bases y Condiciones");
					item.AddChild(subItem);



					//Sucursales	
					item = new ItemMenu("Sucursales", "Sucursales/AdmSucursales.aspx", "Sucursales", false, "ADM. Sucursales");
					rootItem.AddChild(item);

					subItem = new ItemMenu("admSucursales", "Sucursales/AdmSucursales.aspx", "Sucursales", false, "ADM. Sucursales");
					item.AddChild(subItem);

					subItem = new ItemMenu("admTelefonos", "Sucursales/AdmTelefonos.aspx", "TelÈfonos", false, "TelÈfonos");
					item.AddChild(subItem);

					item = new ItemMenu("adminParametros", "Parametros/AdmParametros.aspx", "Parametros", false, "Administracion Par·metros");
					rootItem.AddChild(item);

					subItem = new ItemMenu("adminParametros2", "Parametros/AdmParametros.aspx", "Par·metros", false, "Administracion Par·metros");
					item.AddChild(subItem);




					context.Application["menuPrincipal"] = configMenu;
				}
				context.Application.UnLock();
			}
			return (ConfigMenu)context.Application["menuPrincipal"];
		}

	}
}
