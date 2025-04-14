using System;
using System.Configuration;
using System.Data;

using BH.Sysnet;

namespace BH.EPotecario.Adm.Componentes
{
	public class Beneficio : ObjectBase
	{
		public Beneficio()	{}

		public DataSet GetMarca(int codMarca)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"	SELECT * FROM Marca ";

			if(codMarca != IDs.Todos)
			{
				sql += "WHERE CodMarca = @CodMarca ";
				sysnet.DB.Parameters.Add("@CodMarca", codMarca);
			}

			sql += "ORDER BY DesMarca";

			sysnet.DB.FillDataSet(sql, ds, "Marca");
			return ds;
		}


		public int CantBeneficiosProximosVencer()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();	

			string sql = @"	SELECT  Count(*) Cantidad
							FROM    Beneficio INNER JOIN
									Marca ON Beneficio.CodMarca = Marca.CodMarca INNER JOIN
									RubroBeneficio ON Beneficio.CodRubroBeneficio = RubroBeneficio.CodRubroBeneficio ";


			sql += " WHERE FechaHasta >= @FechaHoy AND FechaHasta < @FechaHasta ";
			int diasAnticipacion = int.Parse(ConfigurationManager.AppSettings["DiasAnticipacionBeneficios"]);
			sysnet.DB.Parameters.Add("@FechaHasta", DateTime.Today.AddDays(diasAnticipacion));
			sysnet.DB.Parameters.Add("@FechaHoy", DateTime.Today);
			sysnet.DB.FillDataSet(sql, ds, "Beneficio");


			return Convert.ToInt32(ds.Tables[0].Rows[0]["Cantidad"]);
			
		}


		public DataSet GetBeneficio(int codBeneficio)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();	

			string sql = @"	SELECT  Beneficio.DesBeneficio, Beneficio.Fecha, Beneficio.CodMarca, Marca.DesMarca, Marca.Logo, 
									RubroBeneficio.CodRubroBeneficio, RubroBeneficio.DesRubroBeneficio, Beneficio.CodBeneficio,
									Beneficio.FechaDesde, Beneficio.FechaHasta
							FROM    Beneficio INNER JOIN
									Marca ON Beneficio.CodMarca = Marca.CodMarca INNER JOIN
									RubroBeneficio ON Beneficio.CodRubroBeneficio = RubroBeneficio.CodRubroBeneficio ";

			if(codBeneficio == IDs.Vencidos)
			{
				sql += " WHERE FechaHasta < @FechaHoy ";
				sysnet.DB.Parameters.Add("@FechaHoy", DateTime.Today);

			}
			else if(codBeneficio == IDs.ProximosVencer)
			{
				sql += " WHERE FechaHasta >= @FechaHoy AND FechaHasta < @FechaHasta ";
				int diasAnticipacion = int.Parse(ConfigurationManager.AppSettings["DiasAnticipacionBeneficios"]);
				sysnet.DB.Parameters.Add("@FechaHasta", DateTime.Today.AddDays(diasAnticipacion));
				sysnet.DB.Parameters.Add("@FechaHoy", DateTime.Today);
			}
			else if(codBeneficio != IDs.Todos)
			{
				sql += " WHERE CodBeneficio = @CodBeneficio ";
				sysnet.DB.Parameters.Add("@CodBeneficio", codBeneficio);
			}

			sql += " ORDER BY Beneficio.DesBeneficio ";

			sysnet.DB.FillDataSet(sql, ds, "Beneficio");
			return ds;
		}


		public DataSet GetBeneficiosByMarca(int codMarca)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();	
			string sql;

			//Beneficios
			sql =@"	SELECT    Beneficio.CodBeneficio, Beneficio.DesBeneficio, Beneficio.Fecha, 
							  Beneficio.CodMarca, Beneficio.CodRubroBeneficio, RubroBeneficio.DesRubroBeneficio,
							  Beneficio.FechaDesde, Beneficio.FechaHasta
					FROM      Beneficio INNER JOIN  RubroBeneficio 
								ON Beneficio.CodRubroBeneficio = RubroBeneficio.CodRubroBeneficio ";

			if(codMarca != IDs.Todos)
			{
				sql += " WHERE Beneficio.CodMarca = @CodMarca ";
				sysnet.DB.Parameters.Add("@CodMarca", codMarca);
			}

			sql += " ORDER BY Beneficio.DesBeneficio ";
			sysnet.DB.FillDataSet(sql, ds, "Beneficio");

			return ds;
		}

		public DataSet GetRubroBeneficio(int codRubroBeneficio)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();	

			string sql =@"	SELECT * FROM RubroBeneficio ";

			if(codRubroBeneficio != IDs.Todos)
			{
				sql += " WHERE CodRubroBeneficio = @CodRubroBeneficio ";
				sysnet.DB.Parameters.Add("@CodRubroBeneficio", codRubroBeneficio);
			}

			sql += " ORDER BY DesRubroBeneficio ";

			sysnet.DB.FillDataSet(sql, ds, "RubroBeneficio");
			return ds;
		}

		public DataSet GetProvincia(int codProvincia)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql =@"	SELECT * FROM Provincias ";

			if(codProvincia != IDs.Todos)
			{
				sql += " WHERE CodProvincia = @CodProvincia ";
				sysnet.DB.Parameters.Add("@CodProvincia", codProvincia);
			}

			sql += " ORDER BY NomProvincia ";

			sysnet.DB.FillDataSet(sql, ds, "Provincia");
			return ds;
		}


		public DataSet GetProvinciaByBeneficio(int codBeneficio)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql =@"	SELECT	BeneficioProvincia.CodBeneficioProvincia, BeneficioProvincia.CodBeneficio, 
									BeneficioProvincia.CodProvincia, Provincias.NomProvincia
							FROM 	BeneficioProvincia INNER JOIN Provincias
									ON BeneficioProvincia.CodProvincia = Provincias.CodProvincia 
							WHERE	BeneficioProvincia.CodBeneficio = " + codBeneficio.ToString();

			sysnet.DB.FillDataSet(sql, ds, "BeneficioProvincia");
			return ds;
		}


		public DataSet GetTarjeta(int codTarjeta)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql =@"	SELECT * FROM Tarjeta ";

			if(codTarjeta != IDs.Todos)
			{
				sql += " WHERE CodTarjeta = @CodTarjeta ";
				sysnet.DB.Parameters.Add("@CodTarjeta", codTarjeta);
			}

			sql += " ORDER BY DesTarjeta ";

			sysnet.DB.FillDataSet(sql, ds, "Tarjeta");
			return ds;
		}

		public DataSet GetTarjetaByBeneficio(int codBeneficio)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql =@"	SELECT	BeneficioTarjeta.CodBeneficioTarjeta, BeneficioTarjeta.CodBeneficio, 
									BeneficioTarjeta.CodTarjeta, Tarjeta.DesTarjeta, Tarjeta.Logo
							FROM 	BeneficioTarjeta INNER JOIN Tarjeta
									ON BeneficioTarjeta.CodTarjeta = Tarjeta.CodTarjeta 
							WHERE	BeneficioTarjeta.CodBeneficio = " + codBeneficio.ToString();

			sysnet.DB.FillDataSet(sql, ds, "BeneficioTarjeta");
			return ds;
		}


		public DataSet UpdateMarca(DataSet ds, string tableName)
		{
			this.Update(ds, tableName);
			return ds;
		}

		public DataSet UpdateBeneficio(DataSet ds, string tableName)
		{
			this.Update(ds, tableName);
			return ds;
		}

		public DataSet UpdateRubroBeneficio(DataSet ds, string tableName)
		{
			this.Update(ds, tableName);
			return ds;
		}

		public DataSet UpdateBeneficioProvincia(DataSet ds, string tableName)
		{
			this.Update(ds, tableName);
			return ds;
		}

		public DataSet UpdateTarjeta(DataSet ds, string tableName)
		{
			this.Update(ds, tableName);
			return ds;
		}

		public DataSet UpdateBeneficioTarjeta(DataSet ds, string tableName)
		{
			this.Update(ds, tableName);
			return ds;
		}
	}
}
