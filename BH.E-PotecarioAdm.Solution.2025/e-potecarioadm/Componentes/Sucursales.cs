using System;
using BH.Sysnet;
using System.Data;

namespace BH.EPotecario.Adm.Componentes
{
	/// <summary>
	/// Summary description for Sucursales.
	/// </summary>
	public class Sucursales
    {
		public Sucursales() { }

		public DataSet GetSucursales()
 {
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT s.codSucursal, s.desSucursal, s.codProvincia, p.nomProvincia, 
						          s.codTipoSucursal, t.desTipoSucursal, s.Domicilio,s.CodigoPostal, s.HorarioAtencion, 
								  s.codZonaCotizacion, Latitud, Longitud, AudioNoVidentes, s.EMailOficialEmpresa, s.EmailOficialNYP, s.Vigente
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
			string sql = "select * from " + tableName;

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
			string sql = "select * from " + tableName;

			DataTable table = new DataTable(tableName);
			dsSchema.Tables.Add(table);
			sysnet.DB.FillSchema(sql, CommandType.Text, table, SchemaType.Source);

			sysnet.DB.DataAdapters.Clear();
			sysnet.DB.DataAdapters.Add(tableName);
			sysnet.DB.DataAdapters[tableName].CreateCommands(dsSchema.Tables[tableName]);

			sysnet.DB.DataAdapters[0].Update(ds, tableName);
			}
		}
}
