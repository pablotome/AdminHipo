using System;
using System.Data;

using BH.Sysnet;

namespace BH.EPotecario.Adm
{
	public class CedulaHipotecaria
	{
		private CedulaHipotecaria() {}

		public static DataSet GetCedula(int codTipoCedulaHipotecaria)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();	

			string sql = @"	SELECT		CedulaHipotecaria.CodCedulaHipotecaria, CedulaHipotecaria.FechaAlta, CedulaHipotecaria.FechaBaja, 
										CedulaHipotecaria.ImporteApertura, CedulaHipotecaria.ImporteCierre, CedulaHipotecaria.UsuarioNT, CedulaHipotecaria.UltimoHash, 
										TipoCedulaHipotecaria.Descripcion, TipoCedulaHipotecaria.NombreEnArchivo, TipoCedulaHipotecaria.CodTipoCedulaHipotecaria
							FROM        CedulaHipotecaria INNER JOIN
										TipoCedulaHipotecaria ON CedulaHipotecaria.CodTipoCedulaHipotecaria = TipoCedulaHipotecaria.CodTipoCedulaHipotecaria ";

			if(codTipoCedulaHipotecaria != IDs.Todos)
			{
				sql += "WHERE CedulaHipotecaria.CodTipoCedulaHipotecaria = @CodTipoCedulaHipotecaria ";
				sysnet.DB.Parameters.Add("@CodTipoCedulaHipotecaria", codTipoCedulaHipotecaria);
			}

			sql += "ORDER BY	TipoCedulaHipotecaria.CodTipoCedulaHipotecaria";

			sysnet.DB.FillDataSet(sql, ds, "CedulaHipotecaria");

			return ds;
		}

		public static DataSet GetCedulasPublicadas()
		{
			SysNet sysnet = SI.GetSysNet();
			string sql = @"	SELECT		CedulaHipotecaria.CodCedulaHipotecaria, CedulaHipotecaria.FechaAlta, CedulaHipotecaria.FechaBaja, 
										CedulaHipotecaria.ImporteApertura, CedulaHipotecaria.ImporteCierre, CedulaHipotecaria.UsuarioNT, CedulaHipotecaria.UltimoHash, 
										TipoCedulaHipotecaria.Descripcion, TipoCedulaHipotecaria.NombreEnArchivo, TipoCedulaHipotecaria.CodTipoCedulaHipotecaria
							FROM        CedulaHipotecaria INNER JOIN
										TipoCedulaHipotecaria ON CedulaHipotecaria.CodTipoCedulaHipotecaria = TipoCedulaHipotecaria.CodTipoCedulaHipotecaria
							WHERE       CedulaHipotecaria.FechaBaja IS NULL AND
										TipoCedulaHipotecaria.Publicada = 1 
							ORDER BY	TipoCedulaHipotecaria.CodTipoCedulaHipotecaria";

			return sysnet.DB.ExecuteReturnDS(sql, "CedulaHipotecaria");
		}


		public static DataSet GetTipoCedulas()
		{
			SysNet sysnet = SI.GetSysNet();
			string sql = @"	SELECT     CodTipoCedulaHipotecaria, Descripcion, NombreEnArchivo, Publicada 
							FROM       TipoCedulaHipotecaria
							ORDER BY   CodTipoCedulaHipotecaria";

			return sysnet.DB.ExecuteReturnDS(sql, "TipoCedulaHipotecaria");
		}

		public static void UpdateCedulas(DataSet ds, string tableName)
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

			sysnet.DB.DataAdapters[0].Update(ds, tableName);
		}
	}
}