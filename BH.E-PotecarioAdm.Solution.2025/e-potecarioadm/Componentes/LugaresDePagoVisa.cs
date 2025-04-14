using System;
using BH.Sysnet;
using System.Data;

namespace BH.EPotecario.Adm.Componentes
{
	/// <summary>
	/// Summary description for LugaresDePagoVisa.
	/// </summary>
	public class LugaresDePagoVisa
	{
		public LugaresDePagoVisa()	{}

		public DataSet GetLugaresDePagoVisa()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT l.CodLugarPagoVisa, l.denominacion, l.direccion, 
								  l.codEntidadPagoVisa, e.DesEntidadPagoVisa, 
								  l.CodLocalidad, loc.DesLocalidad, p.CodProvincia, p.NomProvincia
							FROM 
								  LugarPagoVisa l, EntidadPagoVisa e, Localidades loc, Provincias p
							WHERE 
								  l.CodEntidadPagoVisa = e.CodEntidadPagoVisa and
								  l.CodLocalidad = loc.CodLocalidad and
								  loc.CodProvincia = p.CodProvincia ";

			sql += "ORDER BY l.denominacion";

			sysnet.DB.FillDataSet(sql, ds, "LugarPagoVisa");
			return ds;
		}

		public DataSet GetLocalidades()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT * FROM Localidades ";

			sysnet.DB.FillDataSet(sql, ds, "Localidades");
			return ds;
		}

		public DataSet GetProvincias()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"select * from Provincias ";

			sysnet.DB.FillDataSet(sql, ds, "Provincias");
			return ds;
		}

		public DataSet GetEntidadPagoVisa()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT * FROM EntidadPagoVisa ";

			sysnet.DB.FillDataSet(sql, ds, "EntidadPagoVisa");
			return ds;
		}

		public void UpdateLugaresDePagoVisa(DataSet ds)
		{
			string tableName = "LugarPagoVisa";
			SysNet sysnet = SI.GetSysNet();
			DataSet dsSchema = new DataSet();
			string sql = "select * from "+tableName;

			DataTable table = new DataTable(tableName);
			dsSchema.Tables.Add(table);
			sysnet.DB.FillSchema(sql, CommandType.Text, table, SchemaType.Source);

			sysnet.DB.DataAdapters.Clear();
			sysnet.DB.DataAdapters.Add( tableName);
			sysnet.DB.DataAdapters[tableName].CreateCommands(dsSchema.Tables[tableName]);

			sysnet.DB.DataAdapters[0].Update(ds, tableName);
		}

		public void UpdateEntidadPagoVisa(DataSet ds)
		{
			string tableName = "EntidadPagoVisa";
			SysNet sysnet = SI.GetSysNet();
			DataSet dsSchema = new DataSet();
			string sql = "select * from "+tableName;

			DataTable table = new DataTable(tableName);
			dsSchema.Tables.Add(table);
			sysnet.DB.FillSchema(sql, CommandType.Text, table, SchemaType.Source);

			sysnet.DB.DataAdapters.Clear();
			sysnet.DB.DataAdapters.Add( tableName);
			sysnet.DB.DataAdapters[tableName].CreateCommands(dsSchema.Tables[tableName]);

			sysnet.DB.DataAdapters[0].Update(ds, tableName);
		}
	}
}
