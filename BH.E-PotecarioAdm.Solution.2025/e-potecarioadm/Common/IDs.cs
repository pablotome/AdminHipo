using System;

namespace BH.EPotecario.Adm
{
	
	[Serializable]
	public class IDs
	{						
		private IDs(){}

		public static int Nulo {get{return int.MinValue;}}
		public static int Nuevo {get{return -1;}}
		public static int Todos {get{return -2;}}
		public static int Ninguno {get{return -3;}}
		public static int Requerido {get{return -4;}}	
		public static int ProximosVencer {get{return -5;}}
		public static int Vencidos {get{return -6;}}	
	
	}
}
