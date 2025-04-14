using System;
using System.Data;

using BH.Sysnet;

namespace BH.EPotecario.Adm
{
	public abstract class ObjectBase
	{
		public ObjectBase()	{}

		public DataSet Update(DataSet ds, string tableName)
		{
			return Update(ds, tableName, false);
		}

		public DataSet Update(DataSet ds, string tableName, bool returnIdentity)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet dsSchema = new DataSet();
			string sql = "select * from " + tableName;

			DataTable table = new DataTable(tableName);
			dsSchema.Tables.Add(table);
			sysnet.DB.FillSchema(sql, CommandType.Text, table, SchemaType.Source);

			sysnet.DB.DataAdapters.Clear();
			sysnet.DB.DataAdapters.Add( tableName);
			sysnet.DB.DataAdapters[tableName].CreateCommands(dsSchema.Tables[tableName]);

			sysnet.DB.DataAdapters[tableName].ReturnIdentity = returnIdentity;

			sysnet.DB.DataAdapters[0].Update(ds, tableName);

			return ds;
		}

	}
}
