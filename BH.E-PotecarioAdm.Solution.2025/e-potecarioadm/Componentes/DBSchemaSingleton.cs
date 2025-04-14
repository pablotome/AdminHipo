using System;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Web;

using BH.Sysnet;



namespace BH.EPotecario.Adm.Componentes
{
	
	public class DBSchemaSingleton
	{
		//*************
		static TimeSpan tiempoExpiracion = TimeSpan.FromHours(4);
		//************

		static object syncObject = new object();				
		private DBSchemaSingleton(){}	
		
		

		public static ArrayList GetTablesInDB()
		{
			SysNet sysnet = SI.GetSysNet();
			string sql = "select name from sysobjects  where type in ('U')";
			DataSet dsTablas = sysnet.DB.ExecuteReturnDS(sql);
			
			ArrayList tablas = new ArrayList();
			DataSet dsSchema = new DataSet();
			foreach (DataRow row in dsTablas.Tables[0].Rows)
			{
				tablas.Add(row["name"].ToString());
			}

			return tablas;
			
		}
		
        
		public static void FillDsWithNewTable(DataSet ds, string tableName)
		{
			ds.Tables.Add(GetTable(tableName).Clone());
		}

		

		public static DataTable GetTable(string tableName)
		{
			DataSet dsCache = GetDBSchemaFromCache();
						
			if (! dsCache.Tables.Contains(tableName))				
			{
				lock (syncObject)
				{
					
					if (! dsCache.Tables.Contains(tableName))				
					{
						SysNet sysnet = SI.GetSysNet();			
						string sql = "select * from " + tableName;
						DataTable table = new DataTable(tableName);
						dsCache.Tables.Add(table);				
						sysnet.DB.FillSchema(sql, CommandType.Text, table, SchemaType.Source);									
					}
				}
			}			 
			
			return dsCache.Tables[tableName];
		}



		private static DataSet GetDBSchemaFromCache()
		{			
			string cacheName = "DBSchema";
			HttpContext context = HttpContext.Current;			
			if (context.Cache[cacheName] == null)
			{				
				lock (syncObject)
				{
					if (context.Cache[cacheName] == null)
					{						
						DataSet ds = new DataSet();
						context.Cache.Insert(cacheName, ds, null,
							DateTime.Now.Add(tiempoExpiracion), TimeSpan.Zero);
					}
				}
			}
			return (DataSet) context.Cache[cacheName];			
		}

	}
}
