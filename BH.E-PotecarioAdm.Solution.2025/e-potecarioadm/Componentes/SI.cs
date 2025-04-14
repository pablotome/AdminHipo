using System;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Data;

using BH.Util;
using BH.Sysnet;

namespace BH.EPotecario.Adm
{
	
	public class SI
	{
		private SI()
		{
			
		}

		public static IPrincipal GetValidatedSysNetPrincipal()
		{
			SysNet sysnet = GetSysNet();									
			//sysnet.SEC.AddPermissionsToPrincipal();			
			return SysNetId.GetSysNetPrincipal();	
		}


		public static SysNet GetSysNet()
		{
			return new SysNet(ProviderType.SqlServer, "Server=DESKTOP-APVP;Database=epotecario;UID=sa;PWD=123456;");
		}


		public static string GetUserFromSysNetID()
		{
			return  "JUAN PALOTES";
		}
	}
}
