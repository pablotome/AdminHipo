using BH.Sysnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BH.EPotecario.Adm.Servicios
{
    public class Conexion
    {
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
            return "JUAN PALOTES";
        }

    }

}
