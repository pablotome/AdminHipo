using System;
using BH.Sysnet;
using System.Data;

namespace BH.EPotecario.Adm.Componentes
{
	/// <summary>
	/// Summary description for Sucursales.
	/// </summary>
	public class TareasAdmin
	{
		public TareasAdmin() { }

		public DataSet GetSystemTables()
		{
			string sql = @"SELECT name FROM dbo.sysobjects WHERE OBJECTPROPERTY(id, N'IsUserTable') = 1";

			SysNet sysnet = SI.GetSysNet();
			
			DataSet ds = new DataSet();
			sysnet.DB.FillDataSet(sql, CommandType.Text, ds, "SysTables");

			return ds;
		}

		public DataSet GetSelectFrom(string tableName)
		{
			string sql = @"SELECT * FROM " + tableName;

			SysNet sysnet = SI.GetSysNet();

			DataSet ds = new DataSet();
			sysnet.DB.FillDataSet(sql, CommandType.Text, ds, "SysTables");

			return ds;
		}

		public DataSet UpdateSystemTables(DataSet ds, string tableName)
		{
			return this.Update(ds, tableName, false);
		}

		public virtual DataSet Update(DataSet ds, string tableName, bool returnIdentity)
		{
			Persistor persistor = new Persistor(SI.GetSysNet());
			return persistor.Update(ds, tableName, returnIdentity);
		}

		public void ExecuteQuery(string query)
		{
			ContextoTrans contextoTrans = new ContextoTrans();

			try
			{
				SysNet sysnet = SI.GetSysNet();
				sysnet.DB.Execute(query);
				contextoTrans.CommitTransaction();
			}
			catch (Exception ex)
			{
				contextoTrans.Rollback();
				throw ex;
			}
		}

		/*public DataSet GetSucursales()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT s.codSucursal, s.desSucursal, s.codProvincia, p.nomProvincia, 
						          s.codTipoSucursal, t.desTipoSucursal, s.Domicilio, s.HorarioAtencion, 
								  s.codZonaCotizacion, Latitud, Longitud
						   FROM   Sucursales s, Provincias p, TiposSucursales t
						   WHERE  s.codProvincia = p.codProvincia
						   AND    s.codTipoSucursal = t.codTipoSucursal ";

			sql += "ORDER BY s.desSucursal";

			sysnet.DB.FillDataSet(sql, ds, "Sucursales");
			return ds;
		}

		public DataSet GetTelefonos()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT t.codTelefono, t.Nombre, t.Numero, t.codSucursal, s.desSucursal, t.codTipoTelefono, 
								  ti.Tipo, t.NombreContacto, t.EMailContacto
						   FROM   Telefonos t, Sucursales s, TiposTelefonos ti
						   WHERE  t.CodSucursal = s.CodSucursal
						   AND    t.codTipoTelefono = ti.codTipoTelefono ";

			sql += "ORDER BY t.codSucursal";

			sysnet.DB.FillDataSet(sql, ds, "Telefonos");
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

		public DataSet GetTiposSucursales()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT * FROM TiposSucursales ";

			sysnet.DB.FillDataSet(sql, ds, "TiposSucursales");
			return ds;
		}

		public DataSet GetTiposTelefonos()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT * FROM TiposTelefonos ";

			sysnet.DB.FillDataSet(sql, ds, "TiposTelefonos");
			return ds;
		}

		public void UpdateSucursales(DataSet ds)
		{
			string tableName = "Sucursales";
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

		public void UpdateTelefonos(DataSet ds)
		{
			string tableName = "Telefonos";
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
		}*/
	}
}
