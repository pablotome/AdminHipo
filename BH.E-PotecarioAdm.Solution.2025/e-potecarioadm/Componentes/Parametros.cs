using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

using BH.Sysnet;

namespace BH.EPotecario.Adm.Componentes
{
    public class Parametro
    {
        int codParametro;
		string desParametro;
		string valor;

		public int CodParametro
        {
			get { return codParametro; }
			set { codParametro = value; }
        }

		public string DesParametro
        {
			get { return desParametro; }
			set { desParametro = value; }
        }

		public string Valor
        {
			get { return valor; }
			set { valor = value; }
        }
    }

    public class ParametrosDB : ObjectBase
    {
        public ParametrosDB() { }

		public static IList<Parametro> ObtenerParametros()
		{
			SysNet sysnet = SI.GetSysNet();

			Parametro parametro;
			IList<Parametro> parametros = new List<Parametro>();

			string sql = "select CodParametro, Parametro, Valor from Parametros order by CodParametro";
			
			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			while (reader.Read())
			{
				parametro = new Parametro();

				parametro.CodParametro = int.Parse(reader["CodParametro"].ToString());
				parametro.DesParametro = reader["Parametro"].ToString();
				parametro.Valor = reader["Valor"].ToString();

				parametros.Add(parametro);
			}
			reader.Close();

			return parametros;
		}

		public static Parametro ObtenerParametro(int codParametro)
		{
			SysNet sysnet = SI.GetSysNet();

			Parametro parametro = null;

			string sql = "select CodParametro, Parametro, Valor from Parametros where codParametro = @codParametro";

			sysnet.DB.Parameters.Add("@codParametro", codParametro);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				parametro = new Parametro();

				parametro.CodParametro = int.Parse(reader["CodParametro"].ToString());
				parametro.DesParametro = reader["Parametro"].ToString();
				parametro.Valor = reader["Valor"].ToString();

			}
			reader.Close();

			return parametro;
		}

		public static Parametro ObtenerParametro(string codParametro)
		{
			SysNet sysnet = SI.GetSysNet();

			Parametro parametro = null;

			string sql = "select CodParametro, Parametro, Valor from Parametros where parametro = @parametro";

			sysnet.DB.Parameters.Add("@parametro", codParametro);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				parametro = new Parametro();

				parametro.CodParametro = int.Parse(reader["CodParametro"].ToString());
				parametro.DesParametro = reader["Parametro"].ToString();
				parametro.Valor = reader["Valor"].ToString();

			}
			reader.Close();

			return parametro;
		}

		public static void ActualizarParametro(Parametro parametro)
		{
			SysNet sysnet = SI.GetSysNet();

			string sql = "update Parametros set Valor = @valor where codParametro = @codParametro";

			sysnet.DB.Parameters.Add("@valor", parametro.Valor);
			sysnet.DB.Parameters.Add("@codParametro", parametro.CodParametro);

			sysnet.DB.Execute(sql);
		}
    }
}
