using System;
using BH.Sysnet;
using System.Data;

namespace BH.EPotecario.Adm.Componentes
{
	/// <summary>
	/// Summary description for Sucursales.
	/// </summary>
	public class Promociones : ObjectBase
	{
		public Promociones()	{}

		public DataSet GetPromociones()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			/*string sql = @"	select codPromocion, desPromocion, fechaInicio, fechaFin
							from Promociones_Tipos 
							order by codPromocion";*/
			string sql = @"	select pt.codPromocion, pt.desPromocion, pt.EmailFrom, pt.EmailsTo, pt.fechaInicio, pt.fechaFin, count(*) as Cantidad
								from Promociones_Tipos pt inner join Promociones_Registrados pr 
									on (pt.codPromocion = pr.codPromocion) 
								group by pt.codPromocion, pt.desPromocion, pt.EmailFrom, pt.EmailsTo, pt.fechaInicio, pt.fechaFin
								union
								select codPromocion, desPromocion, EmailFrom, EmailsTo, fechaInicio, fechaFin, 0 as Cantidad
								from Promociones_Tipos pt
								where not exists (
									select top 1 *
									from Promociones_Registrados pr 
									where pt.codPromocion = pr.codPromocion
								)
								order by pt.codPromocion";


			sysnet.DB.FillDataSet(sql, ds, "Promociones_Tipos");
			return ds;
		}

		public DataSet GetTiposDocumentos()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"SELECT * FROM TipoDocumento order by IDTipoDocumento";

			sysnet.DB.FillDataSet(sql, ds, "TipoDocumento");
			return ds;
		}

		public DataSet UpdatePromociones(DataSet ds, string tableName)
		{
			this.Update(ds, tableName);
			return ds;
		}



		public DataTable ObtenerUsuariosRegistrados(int codTipoPromocion)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Format(@"	SELECT 
													PR.ID, nombre as 'Nombre',apellido as 'Apellido',TD.Descripcion as 'Tipo_Doc',Num_Doc,Sexo, f_nacimiento,
													email as 'E-Mail',Tel,Cel,CC.Descripcion as 'Compania',
													CASE  	WHEN RecibirSMS = 1 THEN 'Si'
															WHEN RecibirSMS = 0 THEN 'No'
													END AS 'SMS', f_registro, PR.Comentarios, PT.codPromocion, PT.desPromocion, localidad, P.NomProvincia, 
													isnull(PR.Edad, 0) as Edad, 
													CASE  	WHEN PR.EstudiosUniversitarios = 1 THEN 'Si'
															WHEN PR.EstudiosUniversitarios = 0 THEN 'No'
													END AS EstudiosUniversitarios, 
													isnull(PR.IngresosDemostrables, '') as IngresosDemostrables, 
													isnull(Empresa, '') as Empresa, 
													isnull(emailEmpresa, '') as EMailEmpresa,
                                                    isnull(codigoPostal, '') as CodigoPostal
											FROM Promociones_registrados PR LEFT JOIN TipoDocumento TD
													ON (TD.IDTipoDocumento = PR.Tipo_Doc)
                                                LEFT JOIN Provincias P
		                                            ON (PR.CodProvincia = P.CodProvincia)
												INNER JOIN Promociones_Tipos PT
													ON (PR.codPromocion = PT.codPromocion)

												LEFT JOIN CompaniasCel CC
													ON (CC.IDCompaniaCel = PR.CompaniaCel)
											WHERE PT.codPromocion = {0}
											ORDER BY PR.f_registro DESC, PR.ID ASC ", codTipoPromocion.ToString());

			sysnet.DB.FillDataSet(sql, ds, "Promociones_Registrados");
			return ds.Tables[0];
		}
	}
}
