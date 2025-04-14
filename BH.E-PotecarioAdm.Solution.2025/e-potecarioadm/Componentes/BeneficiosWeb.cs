using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

using BH.Sysnet;

namespace BH.EPotecario.Adm.Componentes
{
	public class BeneficiosWeb : ObjectBase
	{
		public BeneficiosWeb()	{}

        //public DataSet GetProveedor(int codProveedor)
        //{
        //    SysNet sysnet = SI.GetSysNet();
        //    DataSet ds = new DataSet();

        //    string sql = @"	SELECT * FROM ProveedorBeneficioWeb ";

        //    if(codProveedor != IDs.Todos)
        //    {
        //        sql += "WHERE CodProveedorBeneficioWeb = @CodProveedorBeneficioWeb ";
        //        sysnet.DB.Parameters.Add("@CodProveedorBeneficioWeb", codProveedor);
        //    }

        //    sql += "ORDER BY DesProveedorBeneficioWeb";

        //    sysnet.DB.FillDataSet(sql, ds, "ProveedorBeneficioWeb");
        //    return ds;
        //}

		/*public DataSet GetCamposProveedor(int codProveedor)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = @"	SELECT * FROM CampoPorProveedorBeneficio ";
			sql += "WHERE CodProveedorBeneficioWeb = @CodProveedorBeneficioWeb ";
			sysnet.DB.Parameters.Add("@CodProveedorBeneficioWeb", codProveedor);

			sql += "ORDER BY Posicion";

			sysnet.DB.FillDataSet(sql, ds, "CamposProveedor");
			return ds;
		}*/

		public DataSet  GetTabla(string tabla, string[] camposFiltros, string campoOrden, object[] valoresFiltros)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Empty;
			
			sql = string.Format("SELECT * FROM {0} WHERE 1=1 ", tabla);

			if (camposFiltros != null && camposFiltros.Length > 0)
			{
				for(int i=0; i < camposFiltros.Length; i ++)
				{
					sql += string.Format("and {0} = @{1} ", camposFiltros[i], camposFiltros[i].Replace(".", string.Empty));

					sysnet.DB.Parameters.Add(string.Format("@{0}", camposFiltros[i].Replace(".", string.Empty)), valoresFiltros[i]);
				}
			}

			sql += "ORDER BY " + campoOrden;

			sysnet.DB.FillDataSet(sql, ds, tabla);
			
			return ds;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tabla"></param>
		/// <param name="camposSelect"></param>
		/// <param name="campoOrden"></param>
		/// <param name="campoFiltro"></param>
		/// <param name="valorFiltro"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		public string GetValoresAutocomplete(string tabla, string[] camposSelect, string campoOrden, string campoFiltro, string valorFiltro, int limit)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Empty;
			string salida = string.Empty;
			string campoIndex = string.Empty;
			string valor = string.Empty;
			Regex re = new Regex("\n");

			sql = string.Format("SELECT TOP {0} {1} FROM {2} WHERE {3} like '%{4}%' ORDER BY {5}", limit, HelperWeb.Concat(",", camposSelect), tabla, campoFiltro, valorFiltro, campoOrden);

			sysnet.DB.FillDataSet(sql, ds);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				salida += "{";
				foreach(string campo in camposSelect)
				{
					campoIndex = (campo.IndexOf(" as ") != -1) ? campo.Substring(campo.IndexOf(" as ")+4) : campo;
					campoIndex = (campo.IndexOf(".") != -1) ? campo.Split(".".ToCharArray())[1] : campo;
					valor = dr[campoIndex].ToString();
					valor = re.Replace(valor, "<br>");
					salida += string.Format("{0}:\"{1}\", ", campoIndex, valor);
				}
				salida = salida.Substring(0, salida.LastIndexOf(","));
				salida += "}, \n";
			}
			
			return (salida.Length > 2) ? "[" + salida.Substring(0, salida.LastIndexOf(",")) + "]" : "[" + salida + "]";
		}

		public string GetValoresAutocomplete(string tabla, string[] camposSelect, string campoOrden, string[] camposFiltros, string[] condicionesFiltros, string[] valoresFiltros, int limit)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Empty;
			string salida = string.Empty;
			string condiciones = string.Empty;
			string campoIndex = string.Empty;
			string valor = string.Empty;
			Regex re = new Regex("\r\n");
			
			for(int i=0; i<camposFiltros.Length; i++)
				condiciones += string.Format("and {0} {1} {2} ", camposFiltros[i], condicionesFiltros[i], valoresFiltros[i]);

			//sql = string.Format("SELECT {0} FROM {1} WHERE {2} like '{3}%' ORDER BY {0}", HelperWeb.Concat(",", camposSelect), tabla, campoFiltro, valorFiltro);
			sql = string.Format("SELECT TOP {0} {1} FROM {2} WHERE 1=1 {3} ORDER BY {4}", 
				limit, 
				HelperWeb.Concat(",", camposSelect), 
				tabla, 
				condiciones, 
				campoOrden);

			sysnet.DB.FillDataSet(sql, ds);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				salida += "{";
				foreach(string campo in camposSelect)
				{
					if (campo.IndexOf(" as ") != -1)
						campoIndex = campo.Substring(campo.IndexOf(" as ") + 4);
					else if (campo.IndexOf(".") != -1)
						campoIndex = campo.Split(".".ToCharArray())[1];
					else
						campoIndex = campo;

					valor = dr[campoIndex].ToString();
					valor = re.Replace(valor, "<br>");
					
					salida += string.Format("'{0}':'{1}', ", campoIndex, valor);
				}
				salida = salida.Substring(0, salida.LastIndexOf(","));
				salida += "}, \n";
				//salida += string.Format("{{Cod:\"{0}\", Des:\"{1}\"}}, \n", dr[0].ToString(), dr[1].ToString());
			}
			
			return (salida.Length > 2) ? "[" + salida.Substring(0, salida.LastIndexOf(",")) + "]" : "[" + salida + "]";
		}

        public int AgregarProvincia(string desProvinciaBeneficioWeb)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            string sql;
            int codProvinciaBeneficioWeb = -1;

            try
            {
                sysnet.DB.BeginTransaction();

                sql = "insert into ProvinciaBeneficioWeb ( DesProvinciaBeneficioWeb ) values ( @DesProvinciaBeneficioWeb ) ";
                sysnet.DB.Parameters.Add("@DesProvinciaBeneficioWeb", desProvinciaBeneficioWeb);

                codProvinciaBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar provincia.\n desProvinciaBeneficioWeb: {0}\n {1}", desProvinciaBeneficioWeb, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }

            return codProvinciaBeneficioWeb;
        }


        public int AgregarCiudad(string desCiudadBeneficioWeb, int codProvinciaBeneficioWeb)
        {
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();
			string sql;

            int codCiudadBeneficioWeb = -1;

			try
			{
				sysnet.DB.BeginTransaction();

				sql = "insert into CiudadBeneficioWeb ( DesCiudadBeneficioWeb, CodProvinciaBeneficioWeb ) values ( @DesCiudadBeneficioWeb, @CodProvinciaBeneficioWeb )";
				sysnet.DB.Parameters.Add("@DesCiudadBeneficioWeb", desCiudadBeneficioWeb);
				sysnet.DB.Parameters.Add("@CodProvinciaBeneficioWeb", codProvinciaBeneficioWeb);

				codCiudadBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

				sysnet.DB.CommitTransaction();
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al cargar ciudad.\ndesCiudadBeneficioWeb: {0}, codProvinciaBeneficioWeb: {1}\n {2}", desCiudadBeneficioWeb, codProvinciaBeneficioWeb, ex.Message);
				throw new ApplicationException(mensaje, ex);
			}

			return codCiudadBeneficioWeb;
        }

        public int AgregarComercio(string desComercioBeneficioWeb)
        {
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();
			string sql;
            int codComercioBeneficioWeb = -1;
			try
			{
				sysnet.DB.BeginTransaction();
				sql = "insert into ComercioBeneficioWeb ( DesComercioBeneficioWeb ) values ( @DesComercioBeneficioWeb ) ";
				sysnet.DB.Parameters.Add("@DesComercioBeneficioWeb", desComercioBeneficioWeb);

				codComercioBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

				sysnet.DB.Parameters.Clear();
				sql = "update ComercioBeneficioWeb set Logo = m.Logo from ComercioBeneficioWeb cbw inner join Marca m on (cbw.DesComercioBeneficioWeb = m.DesMarca) where cbw.DesComercioBeneficioWeb = @desComercioBeneficioWeb";
				sysnet.DB.Parameters.Add("@desComercioBeneficioWeb", desComercioBeneficioWeb);
				sysnet.DB.Execute(sql);

				sysnet.DB.CommitTransaction();
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al cargar comercio.\n desComercioBeneficioWeb: {0} \n{1}", desComercioBeneficioWeb, ex.Message);
				throw new ApplicationException(mensaje, ex);
			}

			return codComercioBeneficioWeb;
        }

        public int AgregarTarjeta(string desTarjeta, byte[] logo)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            string sql;
            int codTarjeta = -1;
            try
            {
                sql = "insert into Tarjeta ( DesTarjeta, logo ) values ( @DesTarjeta, @logo ) ";
                sysnet.DB.Parameters.Add("@DesTarjeta", desTarjeta);
                sysnet.DB.Parameters.Add("@logo", logo);

                codTarjeta = (int)sysnet.DB.Execute(sql, true);
            }
            catch (Exception ex)
            {
                string mensaje = string.Format("Error al cargar tarjeta.\n desTarjeta: {0} \n{1}", desTarjeta, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }

            return codTarjeta;
        }

        public int AgregarRubro(string desRubroBeneficioWeb)
        {
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();
			string sql;

            int codRubroBeneficioWeb = -1;

			try
			{
				sysnet.DB.BeginTransaction();

				sql = "insert into RubroBeneficioWeb ( DesRubroBeneficioWeb ) values ( @DesRubroBeneficioWeb )";
				sysnet.DB.Parameters.Add("@DesRubroBeneficioWeb", desRubroBeneficioWeb);

				codRubroBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

				sysnet.DB.CommitTransaction();
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al cargar rubro.\n desRubroBeneficioWeb: {0}\n{1}", desRubroBeneficioWeb, ex.Message);
				throw new ApplicationException(mensaje, ex);
			}

			return codRubroBeneficioWeb;
        }

        public int AgregarSubRubro(string desSubRubroBeneficioWeb, int codRubroBeneficioWeb)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();
			string sql;

            int codSubRubroBeneficioWeb = -1;

			try
			{
				sysnet.DB.BeginTransaction();

				sql = "insert into SubRubroBeneficioWeb ( DesSubRubroBeneficioWeb, CodRubroBeneficioWeb ) values ( @DesSubRubroBeneficioWeb, @CodRubroBeneficioWeb )";
				sysnet.DB.Parameters.Add("@DesSubRubroBeneficioWeb", desSubRubroBeneficioWeb);
				sysnet.DB.Parameters.Add("@CodRubroBeneficioWeb", codRubroBeneficioWeb);

				codSubRubroBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

				sysnet.DB.CommitTransaction();
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al cargar subrubro.\n desSubRubroBeneficioWeb: {0}, codRubroBeneficioWeb: {1}\n{2}", desSubRubroBeneficioWeb, codRubroBeneficioWeb, ex.Message);
				throw new ApplicationException(mensaje, ex);
			}

			return codSubRubroBeneficioWeb;
		}

		public int AgregarBeneficio(int codBeneficioWebProveedor, int codComercioBeneficioWeb, int codSubRubroBeneficioWeb, string nroTelefono, string email, string web, long comercioVisa, long comercioMaestro, string CUIT, string descuento, int descuentoVisa, int descuentoMaestro, int descuentoBHVisa, int descuentoBHMaestro, int cuotasBanco, string beneficioDias, bool beneficio2x1, bool regalo, bool descuentoEspecial, string observaciones, bool codigoVisa, bool codigoMaestro, DateTime vigenciaDesde, DateTime vigenciaHasta, char estado, bool destacado)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();
			int codBeneficioWeb;

			try
			{
				sysnet.DB.BeginTransaction();

				string sql = "insert into BeneficioWeb ( CodComercioBeneficioWeb, CodSubRubroBeneficioWeb, NroTelefono, EMail, Web, ComercioVisa, ComercioMaestro, CUIT, Descuento, DescuentoVisa, DescuentoMaestro, DescuentoBHVisa, DescuentoBHMaestro, CuotasBanco, BeneficioDias, Beneficio2x1, Regalo, DescuentoEspecial, Observaciones, CodigoVisa, CodigoMaestro, VigenciaDesde, VigenciaHasta, CodEstadoBeneficioWeb, destacado ) values ( @CodComercioBeneficioWeb, @CodSubRubroBeneficioWeb, @NroTelefono, @EMail, @Web, @ComercioVisa, @ComercioMaestro, @CUIT, @Descuento, @DescuentoVisa, @DescuentoMaestro, @DescuentoBHVisa, @DescuentoBHMaestro, @CuotasBanco, @BeneficioDias, @Beneficio2x1, @Regalo, @DescuentoEspecial, @Observaciones, @CodigoVisa, @CodigoMaestro, @VigenciaDesde, @VigenciaHasta, @estado, @destacado )";

				sysnet.DB.Parameters.Add("@CodComercioBeneficioWeb", codComercioBeneficioWeb);
				sysnet.DB.Parameters.Add("@CodSubRubroBeneficioWeb", codSubRubroBeneficioWeb);
				sysnet.DB.Parameters.Add("@NroTelefono", nroTelefono);
				sysnet.DB.Parameters.Add("@EMail", email);
				sysnet.DB.Parameters.Add("@Web", web);
				sysnet.DB.Parameters.Add("@ComercioVisa", comercioVisa);
				sysnet.DB.Parameters.Add("@ComercioMaestro", comercioMaestro);
				sysnet.DB.Parameters.Add("@CUIT", CUIT);
				sysnet.DB.Parameters.Add("@Descuento", descuento);
				sysnet.DB.Parameters.Add("@DescuentoVisa", descuentoVisa);
				sysnet.DB.Parameters.Add("@DescuentoMaestro", descuentoMaestro);
				sysnet.DB.Parameters.Add("@DescuentoBHVisa", descuentoBHVisa);
				sysnet.DB.Parameters.Add("@DescuentoBHMaestro", descuentoBHMaestro);
				sysnet.DB.Parameters.Add("@CuotasBanco", cuotasBanco);
				sysnet.DB.Parameters.Add("@BeneficioDias", beneficioDias);
				sysnet.DB.Parameters.Add("@Beneficio2x1", beneficio2x1);
				sysnet.DB.Parameters.Add("@Regalo", regalo);
				sysnet.DB.Parameters.Add("@DescuentoEspecial", descuentoEspecial);
				sysnet.DB.Parameters.Add("@Observaciones", observaciones);
				sysnet.DB.Parameters.Add("@CodigoVisa", codigoVisa);
				sysnet.DB.Parameters.Add("@CodigoMaestro", codigoMaestro);
				sysnet.DB.Parameters.Add("@VigenciaDesde", vigenciaDesde);
				sysnet.DB.Parameters.Add("@VigenciaHasta", vigenciaHasta);
				sysnet.DB.Parameters.Add("@estado", estado, ParameterType.Char);
				sysnet.DB.Parameters.Add("@destacado", destacado);

				codBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

                //sql = string.Format("select isnull(max(CodBeneficioWebProveedor) + 1, 1) as CodBeneficioWebProveedor from BeneficioWebProveedor where CodProveedorBeneficioWeb = {0}", codProveedorBeneficioWeb);
                //codBeneficioWebProveedor = (codBeneficioWebProveedor == -1) ? (int)sysnet.DB.ExecuteReturnScalar(sql) : codBeneficioWebProveedor;
				
                //sql = "insert into BeneficioWebProveedor (CodProveedorBeneficioWeb, CodBeneficioWebProveedor, CodBeneficioWeb) values (@CodProveedorBeneficioWeb, @CodBeneficioWebProveedor, @CodBeneficioWeb)";

                //sysnet.DB.Parameters.Add("@CodProveedorBeneficioWeb", codProveedorBeneficioWeb);
                //sysnet.DB.Parameters.Add("@CodBeneficioWebProveedor", codBeneficioWebProveedor);
                //sysnet.DB.Parameters.Add("@CodBeneficioWeb", codBeneficioWeb);

                //sysnet.DB.Execute(sql);

				sysnet.DB.CommitTransaction();
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al cargar beneficio: codBeneficioWebProveedor:{0}, codComercioBeneficioWeb:{1}, codSubRubroBeneficioWeb:{2}, nroTelefono:{3}, email:{4}, web:{5}, comercioVisa:{6}, comercioMaestro:{7}, CUIT:{8}, descuento:{9}, descuentoVisa:{10}, descuentoMaestro:{11}, descuentoBHVisa:{12}, descuentoBHMaestro:{13}, cuotasBanco:{14}, beneficioDias:{15}, beneficio2x1:{16}, regalo:{17}, descuentoEspecial:{18}, observaciones:{19}, codigoVisa:{20}, codigoMaestro:{21}, vigenciaDesde:{22}, vigenciaHasta:{23}, estado:{24}, destacado:{25}\n{26}", codBeneficioWebProveedor, codComercioBeneficioWeb, codSubRubroBeneficioWeb, nroTelefono, email, web, comercioVisa, comercioMaestro, CUIT, descuento, descuentoVisa, descuentoMaestro, descuentoBHVisa, descuentoBHMaestro, cuotasBanco, beneficioDias, beneficio2x1, regalo, descuentoEspecial, observaciones, codigoVisa, codigoMaestro, vigenciaDesde, vigenciaHasta, estado, destacado, ex.Message);
				throw new ApplicationException(mensaje, ex);
			}
			return codBeneficioWeb;
		}

		public void AgregarBeneficioWebProveedor(int codProveedorBeneficioWeb, int codBeneficioWebProveedor, int codBeneficioWeb)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = "insert into BeneficioWebProveedor (CodProveedorBeneficioWeb, CodBeneficioWebProveedor, CodBeneficioWeb) values (@CodProveedorBeneficioWeb, @CodBeneficioWebProveedor, @CodBeneficioWeb)";

			sysnet.DB.Parameters.Add("@CodProveedorBeneficioWeb", codProveedorBeneficioWeb);
			sysnet.DB.Parameters.Add("@CodBeneficioWebProveedor", codBeneficioWebProveedor);
			sysnet.DB.Parameters.Add("@CodBeneficioWeb", codBeneficioWeb);

			sysnet.DB.Execute(sql);
		}
		
		
		//public int ModificarBeneficio(int codBeneficioWeb, int codBeneficioWebDelProveedor, int codProveedorBeneficioWeb, int codProveedorBeneficioWeb, int codComercioBeneficioWeb, int codSubRubroBeneficioWeb, string nroTelefono, string email, string web, long comercioVisa, long comercioMaestro, string CUIT, string descuento, int descuentoVisa, int descuentoMaestro, int descuentoBHVisa, int descuentoBHMaestro, int cuotasBanco, string beneficioDias, bool beneficio2x1, bool regalo, bool descuentoEspecial, string observaciones, bool codigoVisa, bool codigoMaestro, DateTime vigenciaDesde, DateTime vigenciaHasta, char estado, bool destacado)
		public int ModificarBeneficio(int codBeneficioWeb, int codComercioBeneficioWeb, int codSubRubroBeneficioWeb, string nroTelefono, string email, string web, long comercioVisa, long comercioMaestro, string CUIT, string descuento, int descuentoVisa, int descuentoMaestro, int descuentoBHVisa, int descuentoBHMaestro, int cuotasBanco, string beneficioDias, bool beneficio2x1, bool regalo, bool descuentoEspecial, string observaciones, bool codigoVisa, bool codigoMaestro, DateTime vigenciaDesde, DateTime vigenciaHasta, char estado, bool destacado)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = "update BeneficioWeb set CodComercioBeneficioWeb = @CodComercioBeneficioWeb, CodSubRubroBeneficioWeb = @CodSubRubroBeneficioWeb, NroTelefono = @NroTelefono, EMail = @EMail, Web = @Web, ComercioVisa = @ComercioVisa, ComercioMaestro = @ComercioMaestro, CUIT = @CUIT, Descuento = @Descuento, DescuentoVisa = @DescuentoVisa, DescuentoMaestro = @DescuentoMaestro, DescuentoBHVisa = @DescuentoBHVisa, DescuentoBHMaestro = @DescuentoBHMaestro, CuotasBanco = @CuotasBanco, BeneficioDias = @BeneficioDias, Beneficio2x1 = @Beneficio2x1, Regalo = @Regalo, DescuentoEspecial = @DescuentoEspecial, Observaciones = @Observaciones, CodigoVisa = @CodigoVisa, CodigoMaestro = @CodigoMaestro, VigenciaDesde = @VigenciaDesde, VigenciaHasta = @VigenciaHasta, CodEstadoBeneficioWeb = @estado, destacado=@destacado where codBeneficioWeb = @codBeneficioWeb";

			sysnet.DB.Parameters.Add("@CodComercioBeneficioWeb",		codComercioBeneficioWeb);
			sysnet.DB.Parameters.Add("@CodSubRubroBeneficioWeb",		codSubRubroBeneficioWeb);
			sysnet.DB.Parameters.Add("@NroTelefono",					nroTelefono);
			sysnet.DB.Parameters.Add("@EMail",							email);
			sysnet.DB.Parameters.Add("@Web",							web);
			sysnet.DB.Parameters.Add("@ComercioVisa",					comercioVisa);
			sysnet.DB.Parameters.Add("@ComercioMaestro",				comercioMaestro);
			sysnet.DB.Parameters.Add("@CUIT",							CUIT);
			sysnet.DB.Parameters.Add("@Descuento",						descuento);
			sysnet.DB.Parameters.Add("@DescuentoVisa",					descuentoVisa);
			sysnet.DB.Parameters.Add("@DescuentoMaestro",				descuentoMaestro);
			sysnet.DB.Parameters.Add("@DescuentoBHVisa",				descuentoBHVisa);
			sysnet.DB.Parameters.Add("@DescuentoBHMaestro",				descuentoBHMaestro);
			sysnet.DB.Parameters.Add("@CuotasBanco",					cuotasBanco);
			sysnet.DB.Parameters.Add("@BeneficioDias",					beneficioDias);
			sysnet.DB.Parameters.Add("@Beneficio2x1",					beneficio2x1);
			sysnet.DB.Parameters.Add("@Regalo",							regalo);
			sysnet.DB.Parameters.Add("@DescuentoEspecial",				descuentoEspecial);
			sysnet.DB.Parameters.Add("@Observaciones",					observaciones);
			sysnet.DB.Parameters.Add("@CodigoVisa",						codigoVisa);
			sysnet.DB.Parameters.Add("@CodigoMaestro",					codigoMaestro);
			sysnet.DB.Parameters.Add("@VigenciaDesde",					vigenciaDesde);
			sysnet.DB.Parameters.Add("@VigenciaHasta",					vigenciaHasta);
			sysnet.DB.Parameters.Add("@codBeneficioWeb",				codBeneficioWeb);
			sysnet.DB.Parameters.Add("@estado",							estado, ParameterType.Char);
			sysnet.DB.Parameters.Add("@destacado",						destacado);

			sysnet.DB.Execute(sql);

			return codBeneficioWeb;
		}


		public int BorrarBeneficio(int codBeneficioWeb)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = "update BeneficioWeb set CodEstadoBeneficioWeb = 'N' where codBeneficioWeb = @codBeneficioWeb";

			sysnet.DB.Parameters.Add("@codBeneficioWeb", codBeneficioWeb);

			sysnet.DB.Execute(sql);

			return codBeneficioWeb;
		}

		
		/*DESDE*/
		public int AgregarSucursalBeneficio(int codBeneficioWeb, 
			int codCiudadBeneficioWeb, string direccion, string nroTelefono, 
			string email, string web, long comercioVisa, long comercioMaestro, string CUIT)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();
			int codSucursalBeneficioWeb;

			try
			{
				sysnet.DB.BeginTransaction();

				string sql = "insert into SucursalBeneficioWeb ( CodBeneficioWeb, CodCiudadBeneficioWeb, Direccion, NroTelefono, EMail, Web, ComercioVisa, ComercioMaestro, CUIT, CodEstadoBeneficioWeb ) values ( @CodBeneficioWeb, @CodCiudadBeneficioWeb, @Direccion, @NroTelefono, @EMail, @Web, @ComercioVisa, @ComercioMaestro, @CUIT, @CodEstadoBeneficioWeb )";

				sysnet.DB.Parameters.Add("@CodBeneficioWeb", codBeneficioWeb);
				sysnet.DB.Parameters.Add("@CodCiudadBeneficioWeb", codCiudadBeneficioWeb);
				sysnet.DB.Parameters.Add("@Direccion", direccion);
				sysnet.DB.Parameters.Add("@NroTelefono", nroTelefono);
				sysnet.DB.Parameters.Add("@EMail", email);
				sysnet.DB.Parameters.Add("@Web", web);
				sysnet.DB.Parameters.Add("@ComercioVisa", comercioVisa);
				sysnet.DB.Parameters.Add("@ComercioMaestro", comercioMaestro);
				sysnet.DB.Parameters.Add("@CUIT", CUIT);
				sysnet.DB.Parameters.Add("@CodEstadoBeneficioWeb", "A");

				codSucursalBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

                //sql = string.Format("select isnull(max(CodSucursalBeneficioWebProveedor) + 1, 1) as CodSucursalBeneficioWebProveedor from SucursalBeneficioWebProveedor where CodProveedorBeneficioWeb = {0}", codProveedorBeneficioWeb);
                //int codSucursalBeneficioWebProveedor = (int)sysnet.DB.ExecuteReturnScalar(sql);

                //sql = "insert into SucursalBeneficioWebProveedor (CodProveedorBeneficioWeb, CodSucursalBeneficioWebProveedor, CodSucursalBeneficioWeb) values (@CodProveedorBeneficioWeb, @CodSucursalBeneficioWebProveedor, @CodSucursalBeneficioWeb)";

                //sysnet.DB.Parameters.Add("@CodProveedorBeneficioWeb", codProveedorBeneficioWeb);
                //sysnet.DB.Parameters.Add("@CodSucursalBeneficioWebProveedor", codSucursalBeneficioWebProveedor);
                //sysnet.DB.Parameters.Add("@CodSucursalBeneficioWeb", codSucursalBeneficioWeb);

                //sysnet.DB.Execute(sql);

				sysnet.DB.CommitTransaction();
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al cargar sucursal: codBeneficioWeb:{0}, codCiudadBeneficioWeb:{1}, direccion:{2}, nroTelefono:{3}, email:{4}, web:{7}, comercioVisa:{8}, comercioMaestro:{9}, CUIT:{10}\n{11}", codBeneficioWeb, codCiudadBeneficioWeb, direccion, nroTelefono, email, web, comercioVisa, comercioMaestro, CUIT, ex.Message);
				throw new ApplicationException(mensaje, ex);
			}
			return codSucursalBeneficioWeb;
		}
		
		
		public int ModificarSucursalBeneficio(int codSucursalBeneficioWeb, int codBeneficioWeb, 
			int codBeneficioProveedorWeb, int codCiudadBeneficioWeb, string direccion, string nroTelefono, 
			string email, string web, long comercioVisa, long comercioMaestro, string CUIT)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			//string sql = string.Format("select isnull(max(CodBeneficioProveedorWeb) + 1, 1) as MaxCodBeneficioProveedorWeb from SucursalBeneficioWeb where CodProveedorBeneficioWeb = {0}", codProveedorBeneficioWeb);
			//codBeneficioProveedorWeb = (codBeneficioProveedorWeb == -1) ? (int)sysnet.DB.ExecuteReturnScalar(sql) : codBeneficioProveedorWeb;

			string sql = "update SucursalBeneficioWeb set CodBeneficioWeb = @CodBeneficioWeb, CodCiudadBeneficioWeb = @CodCiudadBeneficioWeb, Direccion = @Direccion, NroTelefono = @NroTelefono, EMail = @EMail, Web = @Web, ComercioVisa = @ComercioVisa, ComercioMaestro = @ComercioMaestro, CUIT = @CUIT, CodEstadoBeneficioWeb = @CodEstadoBeneficioWeb where codSucursalBeneficioWeb = @codSucursalBeneficioWeb";
			
			sysnet.DB.Parameters.Add("@CodSucursalBeneficioWeb",	codSucursalBeneficioWeb);
			sysnet.DB.Parameters.Add("@CodBeneficioWeb",			codBeneficioWeb);
			sysnet.DB.Parameters.Add("@CodCiudadBeneficioWeb",		codCiudadBeneficioWeb);
			sysnet.DB.Parameters.Add("@Direccion",					direccion);
			sysnet.DB.Parameters.Add("@NroTelefono",				nroTelefono);
			sysnet.DB.Parameters.Add("@EMail",						email);
			sysnet.DB.Parameters.Add("@Web",						web);
			sysnet.DB.Parameters.Add("@ComercioVisa",				comercioVisa);
			sysnet.DB.Parameters.Add("@ComercioMaestro",			comercioMaestro);
			sysnet.DB.Parameters.Add("@CUIT",						CUIT);
			sysnet.DB.Parameters.Add("@CodEstadoBeneficioWeb",		"A");

			sysnet.DB.Execute(sql);

			return codSucursalBeneficioWeb;
		}


		public int BorrarSucursalBeneficio(int codSucursalBeneficioWeb)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = "update SucursalBeneficioWeb set CodEstadoBeneficioWeb = 'N' where codSucursalBeneficioWeb = @codSucursalBeneficioWeb";

			sysnet.DB.Parameters.Add("@codSucursalBeneficioWeb", codSucursalBeneficioWeb);

			sysnet.DB.Execute(sql);

			return codSucursalBeneficioWeb;
		}

		/*HASTA*/

		public DataSet BuscarComercio(string textoBusqueda)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Empty;

			sql = string.Format("SELECT * FROM ComercioBeneficioWeb where DesComercioBeneficioWeb like '%{0}%' ORDER BY DesComercioBeneficioWeb", textoBusqueda);

			sysnet.DB.FillDataSet(sql, ds, "Comercios");

			return ds;
		}

        public DataSet BuscarTarjeta(string textoBusqueda)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            string sql = string.Empty;

            sql = string.Format("SELECT * FROM Tarjeta where DesTarjeta like '%{0}%' ORDER BY DesTarjeta", textoBusqueda);

            sysnet.DB.FillDataSet(sql, ds, "Tarjeta");

            return ds;
        }

        //public DataSet BuscarComercioProveedor(int codProveedorBeneficiosWeb, string textoBusqueda)
        //{
        //    SysNet sysnet = SI.GetSysNet();
        //    DataSet ds = new DataSet();

        //    string sql = string.Empty;

        //    sql = string.Format("SELECT c2.*, c1.CodComercioBeneficioWebProveedor FROM ComercioBeneficioWebProveedor c1 inner join ComercioBeneficioWeb c2 on (c1.CodComercioBeneficioWeb = c2.CodComercioBeneficioWeb) where c2.DesComercioBeneficioWeb like '%{0}%' and c1.CodProveedorBeneficioWeb = {1} ORDER BY DesComercioBeneficioWeb", textoBusqueda, codProveedorBeneficiosWeb);

        //    sysnet.DB.FillDataSet(sql, ds, "Comercios");

        //    return ds;
        //}

		public int ObtenerCantidadBeneficiosComercio(int codComercioBeneficioWeb)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Empty;
			
			sql = string.Format("select * from BeneficioWeb where CodComercioBeneficioWeb = {0}", codComercioBeneficioWeb);

			sysnet.DB.FillDataSet(sql, ds, "Comercios");
			
			return ds.Tables[0].Rows.Count;
		}

		public string GetCantidadesTablasBeneficios()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Empty;
			string salida = string.Empty;
			string[] tablas = new string[]{"SucursalBeneficioWeb", "BeneficioWeb", "ComercioBeneficioWeb", "SubRubroBeneficioWeb", "RubroBeneficioWeb", "CiudadBeneficioWeb", "ProvinciaBeneficioWeb"};
			
			foreach(string tabla in tablas)
			{
				sql = string.Format("SELECT count(*) as Cantidad{0} from {0}", tabla);
				int cantidad = (int)sysnet.DB.ExecuteReturnScalar(sql);

				salida += string.Format("{{Cantidad{0}:\"{1}\"}}, \n", tabla, cantidad);
			}
			
			return (salida.Length > 2) ? "[" + salida.Substring(0, salida.LastIndexOf(",")) + "]" : "[" + salida + "]";
		}

		public DataSet UpdateTable(DataSet ds, string tableName)
		{
			this.Update(ds, tableName, true);
			return ds;
		}

		public DataSet ObtenerBeneficiosComercio(int codComercioBeneficioWeb)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Format("select b.CodBeneficioWeb, b.Descuento, b.BeneficioDias, b.Beneficio2x1, b.Regalo, b.DescuentoEspecial, b.Observaciones, b.CodigoVisa, b.CodigoMaestro, b.VigenciaDesde, b.VigenciaHasta, b.CodEstadoBeneficioWeb, b.Destacado, r.DesRubroBeneficioWeb, s.DesSubRubroBeneficioWeb from BeneficioWeb b left join SubRubroBeneficioWeb s on (b.CodSubRubroBeneficioWeb = s.CodSubRubroBeneficioWeb) left join RubroBeneficioWeb r on (r.CodRubroBeneficioWeb = s.CodRubroBeneficioWeb) where b.CodComercioBeneficioWeb = {0}", codComercioBeneficioWeb);
			
			return sysnet.DB.ExecuteReturnDS(sql);
		}

		public DataSet ObtenerBeneficios()
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = string.Format(@"select 												
											cbw.CodComercioBeneficioWeb, cbw.DesComercioBeneficioWeb,
											b.CodBeneficioWeb, b.Descuento, b.VigenciaDesde, b.VigenciaHasta, b.CodEstadoBeneficioWeb, b.Destacado, 
											r.CodRubroBeneficioWeb, r.DesRubroBeneficioWeb, 
											sr.CodSubRubroBeneficioWeb, sr.DesSubRubroBeneficioWeb, 
											DBO.ObtenerCiudadesBeneficio(b.CodBeneficioWeb) as Localidades
										from BeneficioWeb b inner join ComercioBeneficioWeb cbw 
											on (b.CodComercioBeneficioWeb = cbw.CodComercioBeneficioWeb)
										inner join SubRubroBeneficioWeb sr
											on (b.CodSubRubroBeneficioWeb = sr.CodSubRubroBeneficioWeb)
										inner join RubroBeneficioWeb r
											on (sr.CodRubroBeneficioWeb = r.CodRubroBeneficioWeb) ");

			return sysnet.DB.ExecuteReturnDS(sql);
		}

		public void BorrarLogoComercio(int codComercio)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();

			string sql = "update ComercioBeneficioWeb set Logo = NULL where CodComercioBeneficioWeb = @codComercio ";

			sysnet.DB.Parameters.Add("@codComercio", codComercio);
			//sysnet.DB.Parameters.Add("@codProveedorBeneficioWeb", codProveedorBeneficioWeb);

			sysnet.DB.Execute(sql);
		}
		#region Comentados
        //public DataSet ObtenerComerciosProveedor(int codProveedorBeneficioWeb)
        //{
        //    SysNet sysnet = SI.GetSysNet();
        //    DataSet ds = new DataSet();

        //    string sql = "select * from ComercioBeneficioWeb where CodComercioBeneficioWeb in ( SELECT CodComercioBeneficioWeb FROM ComercioBeneficioWebProveedor WHERE CodProveedorBeneficioWeb = @CodProveedorBeneficioWeb)";

        //    sysnet.DB.Parameters.Add("@CodProveedorBeneficioWeb", codProveedorBeneficioWeb);

        //    return sysnet.DB.ExecuteReturnDS(sql);
        //}


		/*
		 * 
		 * public DataSet GetMarca(int codMarca)
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
			int diasAnticipacion = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["DiasAnticipacionBeneficios"]);
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
				int diasAnticipacion = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["DiasAnticipacionBeneficios"]);
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
		
		*/
		#endregion

        internal void BorrarTemporal()
        {
            SysNet sysnet = SI.GetSysNet();

            try
            {
                sysnet.DB.BeginTransaction();

                string sql = "DELETE FROM tmp_ImportacionBeneficios ";//"TRUNCATE TABLE tmp_ImportacionBeneficios ";

                sysnet.DB.Execute(sql);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al borrar tmp_ImportacionBeneficios.\n {0}", ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
        }

        public void AgregarTmpImportacion(string accion,int codAccion, int? codBeneficio, string Comercio, string Rubro, string Subrubro, string TDBH,  
            string TDProcrear, string TDTodas,
            string TCBHDark, string TCBHGold, string TCBHInternacional, string TCBHNacional,string TCARDark, string TCARGold, string TCARInternacional,
            string TCTechoGold, string TCTechoInternacional, string TCRacingGold, string TCRacingInternacional, 
            string TCHMLInternacional, string TCHMLNacional, string TCTodas,
            string BeneficioDias, int? DescuentoDebito, int? DescuentoCredito, string Tope, int? RangoCuotas, DateTime? VigenciaDesde, 
            DateTime? VigenciaHasta, string CUIT, int? CodigoSucursal, string Domicilio, string Ciudad, string Provincia, 
            string Telefono, string Web,string dias, string linea)
        {

            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            if(!string.IsNullOrEmpty(Web))
            {
                Web = Web.Trim();
            }

            if (!string.IsNullOrEmpty(CUIT))
            {
                CUIT = CUIT.Trim();
            }

            if (!string.IsNullOrEmpty(Domicilio))
            {
                Domicilio = Domicilio.Trim();
            }

            if (!string.IsNullOrEmpty(Telefono))
            {
                Telefono = Telefono.Trim();
            }

            string sql = string.Empty;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "insert into Tmp_ImportacionBeneficios (Accion, codAccion, codBeneficio, Comercio, Rubro, Subrubro, TDBH, TDProcrear, TDTodas, " +
                      " TCBHDark, TCBHGold, TCBHInternacional, TCBHNacional, TCARDark, TCARGold, TCARInternacional, " + 
                      " TCTechoGold, TCTechoInternacional, TCRacingGold, TCRacingInternacional, TCHMLInternacional, TCHMLNacional, TCTodas, " +
                      " Dias, BeneficioDias, DescuentoDebito, DescuentoCredito, Tope, RangoCuotas, " +
                      " VigenciaDesde, VigenciaHasta, CUIT, codSucursal, Domicilio, Ciudad, Provincia, Telefono, Web ) values " +
                      " ( @Accion, @codAccion, " + 
                      ((codBeneficio.HasValue)? "@codBeneficio" : "null") + 
                      ", @Comercio, @Rubro, @Subrubro, @TDBH, @TDProcrear, @TDTodas, " +
                      " @TCBHDark, @TCBHGold, @TCBHInternacional, @TCBHNacional, @TCARDark, @TCARGold, @TCARInternacional, " + 
                      " @TCTechoGold, @TCTechoInternacional, @TCRacingGold, @TCRacingInternacional,@TCHMLInternacional, @TCHMLNacional, @TCTodas, @Dias, @BeneficioDias," +                      
                      ((DescuentoDebito.HasValue)? "@DescuentoDebito" : "null" ) +  ", " + 
                      ((DescuentoCredito.HasValue)? "@DescuentoCredito" : "null") + ", @Tope , " + 
                      ((RangoCuotas.HasValue)? "@RangoCuotas" : "null") +  ", " +
                      ((VigenciaDesde.HasValue)? "@VigenciaDesde" : "null") + ", " + 
                      ((VigenciaHasta.HasValue)? "@VigenciaHasta" : "null") + ", @CUIT, " + 
                      ((CodigoSucursal.HasValue)? "@codSucursal" : "null" )+ ", @Domicilio, @Ciudad, @Provincia," +
                      " @Telefono, @Web) ";

                sysnet.DB.Parameters.Add("@Accion", accion);
                sysnet.DB.Parameters.Add("@codAccion", codAccion);

                if (codBeneficio.HasValue)
                {
                    sysnet.DB.Parameters.Add("@codBeneficio", codBeneficio);
                }

                sysnet.DB.Parameters.Add("@Comercio", Comercio);
                sysnet.DB.Parameters.Add("@Rubro", Rubro);
                sysnet.DB.Parameters.Add("@Subrubro", Subrubro);
                sysnet.DB.Parameters.Add("@TDBH", TDBH);
                sysnet.DB.Parameters.Add("@TDProcrear", TDProcrear);
                sysnet.DB.Parameters.Add("@TDTodas", TDTodas);
                sysnet.DB.Parameters.Add("@TCBHDark", TCBHDark);
                sysnet.DB.Parameters.Add("@TCBHGold", TCBHGold);
                sysnet.DB.Parameters.Add("@TCBHInternacional", TCBHInternacional);
                sysnet.DB.Parameters.Add("@TCBHNacional", TCBHNacional);
                sysnet.DB.Parameters.Add("@TCARDark", TCARDark);
                sysnet.DB.Parameters.Add("@TCARGold", TCARGold);
                sysnet.DB.Parameters.Add("@TCARInternacional", TCARInternacional);
                sysnet.DB.Parameters.Add("@TCTechoGold", TCTechoGold);
                sysnet.DB.Parameters.Add("@TCTechoInternacional", TCTechoInternacional);
                sysnet.DB.Parameters.Add("@TCRacingGold", TCRacingGold);
                sysnet.DB.Parameters.Add("@TCRacingInternacional", TCRacingInternacional);
                sysnet.DB.Parameters.Add("@TCHMLInternacional", TCHMLInternacional);
                sysnet.DB.Parameters.Add("@TCHMLNacional", TCHMLNacional);
                sysnet.DB.Parameters.Add("@TCTodas", TCTodas);
                sysnet.DB.Parameters.Add("@Dias", dias);
                sysnet.DB.Parameters.Add("@BeneficioDias", BeneficioDias);

                if (DescuentoDebito.HasValue)
                {
                    sysnet.DB.Parameters.Add("@DescuentoDebito", DescuentoDebito);
                }

                if (DescuentoCredito.HasValue)
                {
                    sysnet.DB.Parameters.Add("@DescuentoCredito", DescuentoCredito);
                }

                sysnet.DB.Parameters.Add("@Tope", Tope);

                if (RangoCuotas.HasValue)
                {
                    sysnet.DB.Parameters.Add("@RangoCuotas", RangoCuotas);
                }

                if (VigenciaDesde.HasValue)
                {
                    sysnet.DB.Parameters.Add("@VigenciaDesde", VigenciaDesde.Value.ToString("yyyyMMdd"));
                }

                if (VigenciaHasta.HasValue)
                {
                    sysnet.DB.Parameters.Add("@VigenciaHasta", VigenciaHasta.Value.ToString("yyyyMMdd"));
                }

                sysnet.DB.Parameters.Add("@CUIT", CUIT);

                if (CodigoSucursal.HasValue)
                {
                    sysnet.DB.Parameters.Add("@codSucursal", CodigoSucursal);
                }

                sysnet.DB.Parameters.Add("@Domicilio", Domicilio);
                sysnet.DB.Parameters.Add("@Ciudad", Ciudad);
                sysnet.DB.Parameters.Add("@Provincia", Provincia);
                sysnet.DB.Parameters.Add("@Telefono", Telefono);
                sysnet.DB.Parameters.Add("@Web", Web);

                sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al insertar la siguiente linea: {0}.\n {1}", linea, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }            
        }

        public DataSet ObtenerBeneficiosImportadosSinSucursales()
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            //Agrupo por Tarjetas tambien, así diferencio de otros descuentos
            string sql = string.Format(@"SELECT ACCION,codAccion ,CodBeneficio,Comercio, Rubro, Subrubro, TDBH, TDProcrear, TDTodas, TCBHDark, TCBHGold, TCBHInternacional, TCBHNacional,
                                        TCARDark, TCARGold, TCARInternacional, TCTechoGold, TCTechoInternacional,TCRacingGold,TCRacingInternacional, TCHMLInternacional, TCTodas,TCHMLNacional, Dias,BeneficioDias, DescuentoDebito, 
                                        DescuentoCredito, Tope, RangoCuotas, VigenciaDesde, VigenciaHasta, CUIT, WEB, count(*)  
                                        FROM tmp_ImportacionBeneficios
                                        GROUP BY ACCION, codAccion, CodBeneficio,Comercio, Rubro, Subrubro, TDBH, TDProcrear, TDTodas, TCBHDark, TCBHGold, TCBHInternacional, TCBHNacional,
                                        TCARDark, TCARGold, TCARInternacional, TCTechoGold, TCTechoInternacional,TCRacingGold,TCRacingInternacional, TCHMLInternacional,
                                        TCHMLNacional,TCTodas, Dias,BEneficioDias, DescuentoDebito, 
                                        DescuentoCredito, Tope, RangoCuotas, VigenciaDesde, VigenciaHasta, CUIT, WEB 
                                        ORDER BY codAccion, Comercio ");


            sysnet.DB.FillDataSet(sql, ds, "tmp_ImportacionBeneficios");
            return ds;
        }


        public int AgregarBeneficio(int codComercioBeneficioWeb, int codSubRubroBeneficioWeb,string Dias, string beneficioDias, int? descuentoDebito,
            int? descuentoCredito, int? rangoCuotas, string tope, DateTime? vigenciaDesde, DateTime? vigenciaHasta, string CUIT, string web)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            int codBeneficioWeb;

            //Valores no seteados
            char estado = 'A';

            string descuento = (descuentoDebito.HasValue) ? descuentoDebito.Value.ToString() : ((descuentoCredito.HasValue)? descuentoCredito.Value.ToString() : string.Empty);

            if (!string.IsNullOrEmpty(descuento))
            {
                descuento += "% de descuento, ";
            }

            if (rangoCuotas.HasValue)
            {
                descuento += (rangoCuotas > 1) ? "Y Hasta " + rangoCuotas.ToString() + " cuotas sin interés. " : ". ";
            }
            else
            {
                descuento += ". ";
            }

            descuento += Dias;

            string observaciones = string.Empty;

            int descuentoVisa, descuentoMaestro, descuentoBHVisa, descuentoBHMaestro;
            bool beneficio2x1, regalo, descuentoEspecial, codigoVisa, codigoMaestro, destacado;

            descuentoVisa = descuentoMaestro = 0;
            beneficio2x1 = regalo = descuentoEspecial = codigoVisa = codigoMaestro = destacado = false;

            descuentoBHMaestro = (descuentoDebito.HasValue)? descuentoDebito.Value : 0;
            descuentoBHVisa = (descuentoCredito.HasValue) ? descuentoCredito.Value : 0;

            if (descuentoCredito.HasValue || rangoCuotas.HasValue)
            {
                if (rangoCuotas > 0)
                {
                    codigoVisa = true;
                }
                else
                {
                    codigoVisa = false;
                }
            }
            else
            {
                codigoVisa = false;
            }

            codigoMaestro = descuentoDebito.HasValue;

            if (!string.IsNullOrEmpty(tope))
            {
                observaciones = "<b>Tope: " + tope + " </b>";
            }

            if (!rangoCuotas.HasValue)
            {
                rangoCuotas = 0;
            }

            if (beneficioDias.Length > 7)
            {
                beneficioDias = beneficioDias.Substring(0, 7);
            }

            try
            {
                sysnet.DB.BeginTransaction();

                string sql = "insert into BeneficioWeb ( CodComercioBeneficioWeb, CodSubRubroBeneficioWeb, Web, CUIT, Descuento, DescuentoVisa, DescuentoMaestro, DescuentoBHVisa, DescuentoBHMaestro, CuotasBanco, BeneficioDias, Beneficio2x1, Regalo, DescuentoEspecial, Observaciones, CodigoVisa, CodigoMaestro,";
                
                if(vigenciaDesde.HasValue)
                {
                    sql += "VigenciaDesde, ";
                }

                if(vigenciaHasta.HasValue)
                {
                    sql += "VigenciaHasta, ";
                }

                 sql += "CodEstadoBeneficioWeb, destacado ) values ( @CodComercioBeneficioWeb, @CodSubRubroBeneficioWeb, @Web, @CUIT, @Descuento, @DescuentoVisa, @DescuentoMaestro, @DescuentoBHVisa, @DescuentoBHMaestro, @CuotasBanco, @BeneficioDias, @Beneficio2x1, @Regalo, @DescuentoEspecial, @Observaciones, @CodigoVisa, @CodigoMaestro, ";
                
                if(vigenciaDesde.HasValue)
                {
                    sql += "@VigenciaDesde, ";
                }

                if (vigenciaHasta.HasValue)
                {
                    sql += "@VigenciaHasta, ";
                }
                
                sql += "@estado, @destacado )";

                sysnet.DB.Parameters.Add("@CodComercioBeneficioWeb", codComercioBeneficioWeb);
                sysnet.DB.Parameters.Add("@CodSubRubroBeneficioWeb", codSubRubroBeneficioWeb);
                sysnet.DB.Parameters.Add("@Web", web);
                sysnet.DB.Parameters.Add("@CUIT", CUIT);
                sysnet.DB.Parameters.Add("@Descuento", descuento);
                sysnet.DB.Parameters.Add("@DescuentoVisa", descuentoVisa);
                sysnet.DB.Parameters.Add("@DescuentoMaestro", descuentoMaestro);
                sysnet.DB.Parameters.Add("@DescuentoBHVisa", descuentoBHVisa);
                sysnet.DB.Parameters.Add("@DescuentoBHMaestro", descuentoBHMaestro);
                sysnet.DB.Parameters.Add("@CuotasBanco", rangoCuotas);
                sysnet.DB.Parameters.Add("@BeneficioDias", beneficioDias);
                sysnet.DB.Parameters.Add("@Beneficio2x1", beneficio2x1);
                sysnet.DB.Parameters.Add("@Regalo", regalo);
                sysnet.DB.Parameters.Add("@DescuentoEspecial", descuentoEspecial);
                sysnet.DB.Parameters.Add("@Observaciones", observaciones);
                sysnet.DB.Parameters.Add("@CodigoVisa", codigoVisa);
                sysnet.DB.Parameters.Add("@CodigoMaestro", codigoMaestro);

                if (vigenciaDesde.HasValue)
                {
                    sysnet.DB.Parameters.Add("@VigenciaDesde", vigenciaDesde);
                }

                if (vigenciaHasta.HasValue)
                {
                    sysnet.DB.Parameters.Add("@VigenciaHasta", vigenciaHasta);
                }

                sysnet.DB.Parameters.Add("@estado", estado, ParameterType.Char);
                sysnet.DB.Parameters.Add("@destacado", destacado);

                codBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar beneficio: codComercioBeneficioWeb: {0}  codSubRubroBeneficioWeb:{1}, web:{2}, CUIT:{3}, descuento:{4}, cuotasBanco:{5}, beneficioDias:{6}, vigenciaDesde:{7}, vigenciaHasta:{8}, estado:{9}\n{10}", codComercioBeneficioWeb, codSubRubroBeneficioWeb, web, CUIT, descuento, rangoCuotas.Value.ToString(), beneficioDias, vigenciaDesde, vigenciaHasta, estado, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
            return codBeneficioWeb;
        }

        internal DataSet ObtenerSucursalesBeneficio(string accion, string Comercio, string rubro, string subrubro, string TDBH, string TDProcrear, string TDTodas,           
            string TCBHDark,string TCBHGold,string TCBHInternacional,string TCBHNacional,string TCARDark,string TCARGold, 
            string TCARInternacional,string TCTechoGold,string TCTechoInternacional,string TCRacingGold,string TCRacingInternacional,
            string TCHMLInternacional, string TCHMLNacional, string TCTodas,
                
            string BeneficioDias, int? DescuentoDebito, int? DescuentoCredito,string Tope,int? RangoCuotas,
                DateTime? VigenciaDesde, DateTime? VigenciaHasta, string CUIT, string web)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();



            //Agrupo por Tarjetas tambien, así diferencio de otros descuentos
            string sql = @"SELECT Comercio, Rubro, Subrubro, TDBH, TDProcrear, TDTodas, TCBHDark, TCBHGold, TCBHInternacional, TCBHNacional,
                                        TCARDark, TCARGold, TCARInternacional, TCTechoGold, TCTechoInternacional, TCRacingGold,
                                        TCRacingInternacional, TCHMLInternacional, TCHMLNacional, TCTodas, 
                                        BEneficioDias, DescuentoDebito, 
                                        DescuentoCredito, Tope, RangoCuotas, VigenciaDesde, VigenciaHasta, CUIT,CodSucursal,
                                        Domicilio, Ciudad, Provincia, Telefono, Web
                                        FROM tmp_ImportacionBeneficios 
                                        WHERE Comercio = @Comercio AND rubro = @Rubro AND Subrubro = @Subrubro AND
                                              TDBH = @TDBH AND TDProcrear = @TDProcrear AND TDTodas = @TDTodas AND TCBHDark = @TCBHDark AND TCBHGold = @TCBHGold AND TCBHInternacional = @TCBHInternacional AND
                                              TCBHNacional = @TCBHNacional AND TCARDark = @TCARDark AND TCARGold = @TCARGold AND TCARInternacional = @TCARInternacional AND
                                              TCTechoGold = @TCTechoGold AND TCTechoInternacional = @TCTechoInternacional AND TCRacingGold = @TCRacingGold  AND
                                              TCRacingInternacional = @TCRacingInternacional AND TCHMLInternacional = @TCHMLInternacional AND 
                                              TCHMLNacional = @TCHMLNacional AND TCTodas = @TCTodas AND BeneficioDias = @BeneficioDias AND Accion = @Accion AND ";
            
            
            if(DescuentoDebito.HasValue)
            {
                sql += "DescuentoDebito = @DescuentoDebito AND ";
            }

            if(DescuentoCredito.HasValue)
            {
                sql += "DescuentoCredito = @DescuentoCredito AND ";
            }
                
             sql += "Tope = @Tope AND ";
            
            if(RangoCuotas.HasValue)
            {
              sql += "RangoCuotas =  @RangoCuotas AND ";
            }

            if(VigenciaDesde.HasValue)
            {
                sql += "VigenciaDesde = @VigenciaDesde AND ";
            }
             
            if(VigenciaHasta.HasValue)
            {
                sql += " VigenciaHasta = @VigenciaHasta AND ";
            }

            sql += "CUIT = @Cuit AND Web = @Web ORDER BY Domicilio, Ciudad, Provincia ";


            sysnet.DB.Parameters.Add("@Comercio", Comercio);
            sysnet.DB.Parameters.Add("@Rubro", rubro);
            sysnet.DB.Parameters.Add("@Subrubro", subrubro);
            sysnet.DB.Parameters.Add("@TDBH", TDBH);
            sysnet.DB.Parameters.Add("@TDProcrear", TDProcrear);
            sysnet.DB.Parameters.Add("@TDTodas", TDTodas    );
            
            sysnet.DB.Parameters.Add("@TCBHDark", TCBHDark);
            sysnet.DB.Parameters.Add("@TCBHGold", TCBHGold);
            sysnet.DB.Parameters.Add("@TCBHInternacional", TCBHInternacional);
            sysnet.DB.Parameters.Add("@TCBHNacional", TCBHNacional);
            sysnet.DB.Parameters.Add("@TCARDark", TCARDark);
            sysnet.DB.Parameters.Add("@TCARGold", TCARGold);
            sysnet.DB.Parameters.Add("@TCARInternacional", TCARInternacional);
            sysnet.DB.Parameters.Add("@TCTechoGold", TCTechoGold);
            sysnet.DB.Parameters.Add("@TCTechoInternacional", TCTechoInternacional);
            sysnet.DB.Parameters.Add("@TCRacingGold", TCRacingGold);
            sysnet.DB.Parameters.Add("@TCRacingInternacional", TCRacingInternacional);
            sysnet.DB.Parameters.Add("@TCHMLInternacional", TCHMLInternacional);
            sysnet.DB.Parameters.Add("@TCHMLNacional", TCHMLNacional);
            sysnet.DB.Parameters.Add("@TCTodas", TCTodas);
            sysnet.DB.Parameters.Add("@BeneficioDias", BeneficioDias);
            sysnet.DB.Parameters.Add("@Accion", accion);

            if (DescuentoDebito.HasValue)
            {
                sysnet.DB.Parameters.Add("@DescuentoDebito", DescuentoDebito);
            }

            if (DescuentoCredito.HasValue)
            {
                sysnet.DB.Parameters.Add("@DescuentoCredito", DescuentoCredito);
            }

            sysnet.DB.Parameters.Add("@Tope", Tope);

            if (RangoCuotas.HasValue)
            {
                sysnet.DB.Parameters.Add("@RangoCuotas", RangoCuotas);
            }

            if (VigenciaDesde.HasValue)
            {
                sysnet.DB.Parameters.Add("@VigenciaDesde", VigenciaDesde);
            }

            if (VigenciaHasta.HasValue)
            {
                sysnet.DB.Parameters.Add("@VigenciaHasta", VigenciaHasta);
            }

            sysnet.DB.Parameters.Add("@Cuit", CUIT);
            sysnet.DB.Parameters.Add("@Web", web);

            sysnet.DB.FillDataSet(sql, ds, "tmp_ImportacionBeneficios");
			return ds;
        }

        internal int AgregarSucursalesABeneficio(int codBeneficio, int codCiudad, string Direccion, 
                            string Telefono, string CUIT, string Web)
		{
			SysNet sysnet = SI.GetSysNet();
			DataSet ds = new DataSet();
			int codSucursalBeneficioWeb;

			try
			{
				sysnet.DB.BeginTransaction();

				string sql = "insert into SucursalBeneficioWeb ( CodBeneficioWeb, CodCiudadBeneficioWeb, Direccion, NroTelefono, Web, CUIT, CodEstadoBeneficioWeb ) values ( @CodBeneficioWeb, @CodCiudadBeneficioWeb, @Direccion, @NroTelefono, @Web, @CUIT, @CodEstadoBeneficioWeb )";

				sysnet.DB.Parameters.Add("@CodBeneficioWeb", codBeneficio);
				sysnet.DB.Parameters.Add("@CodCiudadBeneficioWeb", codCiudad);
				sysnet.DB.Parameters.Add("@Direccion", Direccion);
				sysnet.DB.Parameters.Add("@NroTelefono", Telefono);
				sysnet.DB.Parameters.Add("@Web", Web);
				sysnet.DB.Parameters.Add("@CUIT", CUIT);
				sysnet.DB.Parameters.Add("@CodEstadoBeneficioWeb", "A");

                codSucursalBeneficioWeb = (int)sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar la Sucursal: codBeneficioWeb: {0}, codCiudadBeneficioWeb:{1}, Direccion:{2}, NroTelefono:{3}, Web:{4}, CUIT:{5}\n{10}", codBeneficio, codCiudad, Direccion, Telefono, Web, CUIT, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
            return codSucursalBeneficioWeb;
        }

        internal void AgregarTarjetasABeneficio(int codBeneficio,string TDBH, string TDProcrear, string TDTodas, string TCBHDark, string TCBHGold, string TCBHInternacional, string TCBHNacional, 
            string TCARDark, string TCARGold, string TCARInternacional, string TCTechoGold, string TCTechoInternacional, string TCRacingGold,
            string TCRacingInternacional, string TCHMLInternacional, string TCHMLNacional, string TCTodas)
        {
            AgregarTarjetaABeneficioSiCorresponde(TCBHDark, (int)Tarjetas.TC_BHDark, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCBHGold, (int)Tarjetas.TC_BHGold, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCBHInternacional, (int)Tarjetas.TC_BHInternacional, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCBHNacional, (int)Tarjetas.TC_BHNacional, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCARDark, (int)Tarjetas.TC_ARDark, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCARGold, (int)Tarjetas.TC_ARGold, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCARInternacional, (int)Tarjetas.TC_ARInternacional, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCTechoGold, (int)Tarjetas.TC_TechoGold, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCTechoInternacional, (int)Tarjetas.TC_TechoInternacional, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCRacingGold, (int)Tarjetas.TC_RacingGold, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCRacingInternacional, (int)Tarjetas.TC_RacingInternacional, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCHMLInternacional, (int)Tarjetas.TC_HMLInternacional, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCHMLNacional, (int)Tarjetas.TC_HMLNacional, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TCTodas, (int)Tarjetas.TC_Todas, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TDBH, (int)Tarjetas.TD_BH, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TDProcrear, (int)Tarjetas.TD_Procrear, codBeneficio);
            AgregarTarjetaABeneficioSiCorresponde(TDTodas, (int)Tarjetas.TD_Todas, codBeneficio);
        }

        private void AgregarTarjetaABeneficioSiCorresponde(string codigoTCArchivo, int codTarjeta, int codBeneficio)
        {
            if (!string.IsNullOrEmpty(codigoTCArchivo.Trim()))
            {
                AgregarTarjetaABeneficio(codTarjeta, codBeneficio);
            }        
        }

        private void AgregarTarjetaABeneficio(int codTarjeta, int codBeneficio)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            string sql;

            try
            {
                sysnet.DB.BeginTransaction();

                sql = "insert into BeneficioTarjeta ( CodBeneficio, CodTarjeta ) values ( @CodBeneficio, @CodTarjeta ) ";

                sysnet.DB.Parameters.Add("@CodBeneficio", codBeneficio);
                sysnet.DB.Parameters.Add("@CodTarjeta", codTarjeta);

                sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar BeneficioTarjeta.\n codTarjeta: {0} codBeneficio: {1} \n {2}", codTarjeta, codBeneficio, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
        }

        internal void EliminarSucursalesBeneficio(int codBeneficio)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            string sql;

            try
            {
                sysnet.DB.BeginTransaction();

                sql = "DELETE FROM SucursalBeneficioWeb WHERE CodBeneficioWeb = @CodBeneficio ";

                sysnet.DB.Parameters.Add("@CodBeneficio", codBeneficio);

                sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al borrar Sucursal.\n codBeneficio: {0} \n {1}", codBeneficio, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
        }

        internal void EliminarTarjetasBeneficio(int codBeneficio)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            string sql;

            try
            {
                sysnet.DB.BeginTransaction();

                sql = "DELETE FROM BeneficioTarjeta WHERE CodBeneficio = @CodBeneficio ";

                sysnet.DB.Parameters.Add("@CodBeneficio", codBeneficio);

                sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al borrar BeneficioTarjeta.\n codBeneficio: {0} \n {1}", codBeneficio, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }            
        }

        internal void ActualizarSucursalesABeneficio(int codBeneficio, int codSucursal, int codCiudad, string domicilio, string telefono, string cuit
            , string web)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            try
            {
                sysnet.DB.BeginTransaction();

                string sql = "UPDATE SucursalBeneficioWeb SET  CodCiudadBeneficioWeb = @codCiudad, Direccion = @direccion, NroTelefono = @telefono, Web = @web, CUIT = @cuit, CodEstadoBeneficioWeb = @codEstadoBeneficio  WHERE CodBeneficioWeb = @codBeneficio AND CodSucursalBeneficioWeb = @codSucursal ";

                sysnet.DB.Parameters.Add("@codBeneficio", codBeneficio);
                sysnet.DB.Parameters.Add("@codSucursal", codSucursal);
                sysnet.DB.Parameters.Add("@codCiudad", codCiudad);
                sysnet.DB.Parameters.Add("@direccion", domicilio);
                sysnet.DB.Parameters.Add("@telefono", telefono);
                sysnet.DB.Parameters.Add("@web", web);
                sysnet.DB.Parameters.Add("@cuit", cuit);
                sysnet.DB.Parameters.Add("@codEstadoBeneficio", "A");


                sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar la Sucursal: codBeneficioWeb: {0}, codCiudadBeneficioWeb:{1}, Direccion:{2}, NroTelefono:{3}, Web:{4}, CUIT:{5}\n{10}", codBeneficio, codCiudad, domicilio, telefono, web, cuit, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
        }

        internal void ActualizarBeneficio(int codBeneficio, int codComercio, int codSubrubro, string Dias, string beneficioDias, int? descuentoDebito, int? descuentoCredito, int? rangoCuotas, string tope, DateTime? vigenciaDesde, DateTime? vigenciaHasta, string cuit, string web)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            //int codBeneficioWeb;

            //Valores no seteados
            char estado = 'A';

            string descuento = (descuentoDebito.HasValue) ? descuentoDebito.Value.ToString() : ((descuentoCredito.HasValue) ? descuentoCredito.Value.ToString() : string.Empty);

            if (!string.IsNullOrEmpty(descuento))
            {
                descuento += "% de descuento, ";
            }

            if (rangoCuotas.HasValue)
            {
                descuento += (rangoCuotas > 1) ? "Y Hasta " + rangoCuotas.ToString() + " sin interés. " : ". ";
            }
            else
            {
                descuento += ". ";
            }

            descuento += Dias;

            string observaciones = string.Empty;

            int descuentoVisa, descuentoMaestro, descuentoBHVisa, descuentoBHMaestro;
            
            descuentoVisa = descuentoMaestro = 0;
            
            descuentoBHMaestro = (descuentoDebito.HasValue) ? descuentoDebito.Value : 0;
            descuentoBHVisa = (descuentoCredito.HasValue) ? descuentoCredito.Value : 0;

            if (!string.IsNullOrEmpty(tope))
            {
                observaciones = "<b>Tope: " + tope + " </b>";
            }

            if (!rangoCuotas.HasValue)
            {
                rangoCuotas = 0;
            }

            if (beneficioDias.Length > 7)
            {
                beneficioDias = beneficioDias.Substring(0, 7);
            }

            try
            {
                sysnet.DB.BeginTransaction();

                string sql = "update BeneficioWeb SET codComercioBeneficioWeb = @codComercio, codSubrubroBeneficioWeb = @codSubrubro, web = @web, CUIT = @cuit, descuento = @descuento, DescuentoVisa = @descuentoVisa," +
                             "DescuentoMaestro = @descuentoMaestro, CuotasBanco = @cuotasBanco, BeneficioDias = @beneficioDias, Observaciones = @observaciones ";

                if (vigenciaDesde.HasValue)
                {
                    sql += ", VigenciaDesde = @vigenciaDesde ";
                }

                if (vigenciaHasta.HasValue)
                {
                    sql += ", VigenciaHasta = @vigenciaHasta ";
                }

                sysnet.DB.Parameters.Add("@codComercio", codComercio);
                sysnet.DB.Parameters.Add("@codSubRubro", codSubrubro);
                sysnet.DB.Parameters.Add("@web", web);
                sysnet.DB.Parameters.Add("@cuit", cuit);
                sysnet.DB.Parameters.Add("@descuento", descuento);
                sysnet.DB.Parameters.Add("@descuentoVisa", descuentoVisa);
                sysnet.DB.Parameters.Add("@descuentoMaestro", descuentoMaestro);
                sysnet.DB.Parameters.Add("@CuotasBanco", rangoCuotas);
                sysnet.DB.Parameters.Add("@BeneficioDias", beneficioDias);
                sysnet.DB.Parameters.Add("@observaciones", observaciones);

                if (vigenciaDesde.HasValue)
                {
                    sysnet.DB.Parameters.Add("@VigenciaDesde", vigenciaDesde);
                }

                if (vigenciaHasta.HasValue)
                {
                    sysnet.DB.Parameters.Add("@VigenciaHasta", vigenciaHasta);
                }

                sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar beneficio: codComercioBeneficioWeb: {0}  codSubRubroBeneficioWeb:{1}, web:{2}, CUIT:{3}, descuento:{4}, cuotasBanco:{5}, beneficioDias:{6}, vigenciaDesde:{7}, vigenciaHasta:{8}, estado:{9}\n{10}", codComercio, codSubrubro, web, cuit, descuento, rangoCuotas.Value.ToString(), beneficioDias, vigenciaDesde, vigenciaHasta, estado, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
        }

        internal void BorrarLogoTarjeta(int codTarjeta)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            string sql = "update Tarjeta set Logo = NULL where codTarjeta = @codTarjeta ";

            sysnet.DB.Parameters.Add("@codTarjeta", codTarjeta);
            //sysnet.DB.Parameters.Add("@codProveedorBeneficioWeb", codProveedorBeneficioWeb);

            sysnet.DB.Execute(sql);
        }

        internal DataSet ObtenerBeneficiosArchivo(string filtro)
        {
            SysNet sysnet = SI.GetSysNet();

            string sql = "SELECT 'M' as Accion, codBeneficioWeb, desComercioBeneficioWeb,desRubroBeneficioWeb, desSubRubroBeneficioWeb," +
                         "CASE WHEN TDBH IS NULL THEN '' ELSE 'X' END as TDBH," +
                         "CASE WHEN TDProcrear IS NULL THEN '' ELSE 'X' END as TDProcrear," +
                         "CASE WHEN TDTodas IS NULL THEN '' ELSE 'X' END as TDTodas," +
                         "CASE WHEN TCBHDark IS NULL THEN '' ELSE 'X' END as TCBHDark," +
                         "CASE WHEN TCBHGold IS NULL THEN '' ELSE 'X' END as TCBHGold," +
                         "CASE WHEN TCBHInternacional IS NULL THEN '' ELSE 'X' END as TCBHInternacional," +
                         "CASE WHEN TCBHNacional IS NULL THEN '' ELSE 'X' END as TCBHNacional," +
                         "CASE WHEN TCARDark IS NULL THEN '' ELSE 'X' END as TCARDark," +
                         "CASE WHEN TCARGold IS NULL THEN '' ELSE 'X' END as TCARGold," +
                         "CASE WHEN TCARInternacional IS NULL THEN '' ELSE 'X' END as TCARInternacional," +
                         "CASE WHEN TCTechoGold IS NULL THEN '' ELSE 'X' END as TCTechoGold," +
                         "CASE WHEN TCTechoInternacional IS NULL THEN '' ELSE 'X' END as TCTechoInternacional," +
                         "CASE WHEN TCRacingGold IS NULL THEN '' ELSE 'X' END as TCRacingGold," +
                         "CASE WHEN TCRacingInternacional IS NULL THEN '' ELSE 'X' END as TCRacingInternacional," +
                         "CASE WHEN TCHMLInternacional IS NULL THEN '' ELSE 'X' END as TCHMLInternacional," +
                         "CASE WHEN TCHMLNacional IS NULL THEN '' ELSE 'X' END as TCHMLNacional," +
                         "CASE WHEN TCTodas IS NULL THEN '' ELSE 'X' END as TCTodas," +
                         "dbo.ObtenerDescripcionDias(beneficioDias) as Dias, DescuentoBHMaestro, DescuentoBHVisa," +
                         "dbo.ObtenerDescripcionTope(codBeneficioWeb) as Tope, cuotasBanco as RangoCuotas," +
                         "vigenciaDesde, vigenciaHasta, ISNULL(CUIT,'') as CUIT," +
                         "ISNULL(codSucursalBeneficioWeb,'') as codSucursal, ISNULL(direccion,'') as direccion," +
                         "ISNULL(DesCiudadBeneficioWeb,'') as CIUDAD, ISNULL(DesProvinciaBeneficioWeb,'') as PROVINCIA, ISNULL(NroTelefono,'') as Telefono, Web " +
                         "FROM (" +
                         "SELECT b.codBeneficioWeb, C.desComercioBeneficioWeb, r.desRubroBeneficioWeb, " +
                         "s.desSubRubroBeneficioWeb, bt1.codTarjeta TCBHDark, bt2.codTarjeta TCBHGold, bt3.codTarjeta TCBHInternacional," +
                         "bt4.codTarjeta TCBHNacional,bt5.codTarjeta TCARDark,bt6.codTarjeta TCARGold, bt7.codTarjeta TCARInternacional," +
                         "bt8.codTarjeta TCTechoGold, bt9.codTarjeta TCTechoInternacional, bt10.codTarjeta TCRacingGold, bt11.codTarjeta TCRacingInternacional, " +
                         "bt12.codTarjeta TCHMLInternacional, bt13.codTarjeta TCHMLNacional, bt14.codTarjeta TCTodas, bt15.codTarjeta TDBH, bt16.codTarjeta TDProcrear, bt17.codTarjeta TDTodas,  b.beneficioDias, b.DescuentoBHMaestro, b.DescuentoBHVisa, " +
                         "b.cuotasBanco, b.vigenciaDesde, b.vigenciaHasta, B.CUIT, suc.codSucursalBeneficioWeb, suc.direccion, suc.NroTelefono, " +
                         "ciu.DesCiudadBeneficioWeb, pro.DesProvinciaBeneficioWeb, b.Web " +
                         "FROM BeneficioWeb b JOIN ComercioBeneficioWeb c ON b.codComercioBeneficioWeb = c.codComercioBeneficioWeb " +
                         "JOIN SubRubroBeneficioWeb s ON b.codSubrubroBeneficioWeb = s.codSubrubroBeneficioWeb " +
                         "JOIN RubroBeneficioWeb r ON s.codRubroBeneficioWeb = r.codRubroBeneficioWeb " +
                         "LEFT JOIN SucursalBeneficioWeb suc ON b.codBeneficioWeb = suc.codBeneficioWeb " +
                         "LEFT JOIN CiudadBeneficioWeb ciu ON suc.codCiudadBeneficioWeb = ciu.codCiudadBeneficioWeb " +
                         "LEFT JOIN ProvinciaBeneficioWeb pro ON pro.codProvinciaBeneficioWeb = ciu.codProvinciaBeneficioWeb " +
                         " LEFT JOIN BeneficioTarjeta bt1 ON b.codBeneficioWeb = bt1.codBeneficio AND bt1.codTarjeta = " + ((int)Tarjetas.TC_BHDark).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt2 ON b.codBeneficioWeb = bt2.codBeneficio AND bt2.codTarjeta = " + ((int)Tarjetas.TC_BHGold).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt3 ON b.codBeneficioWeb = bt3.codBeneficio AND bt3.codTarjeta = " + ((int)Tarjetas.TC_BHInternacional).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt4 ON b.codBeneficioWeb = bt4.codBeneficio AND bt4.codTarjeta = " + ((int)Tarjetas.TC_BHNacional).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt5 ON b.codBeneficioWeb = bt5.codBeneficio AND bt5.codTarjeta = " + ((int)Tarjetas.TC_ARDark).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt6 ON b.codBeneficioWeb = bt6.codBeneficio AND bt6.codTarjeta = " + ((int)Tarjetas.TC_ARGold).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt7 ON b.codBeneficioWeb = bt7.codBeneficio AND bt7.codTarjeta = " + ((int)Tarjetas.TC_ARInternacional).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt8 ON b.codBeneficioWeb = bt8.codBeneficio AND bt8.codTarjeta = " + ((int)Tarjetas.TC_TechoGold).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt9 ON b.codBeneficioWeb = bt9.codBeneficio AND bt9.codTarjeta = " + ((int)Tarjetas.TC_TechoInternacional).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt10 ON b.codBeneficioWeb = bt10.codBeneficio AND bt10.codTarjeta = " + ((int)Tarjetas.TC_RacingGold).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt11 ON b.codBeneficioWeb = bt11.codBeneficio AND bt11.codTarjeta = " + ((int)Tarjetas.TC_RacingInternacional).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt12 ON b.codBeneficioWeb = bt12.codBeneficio AND bt12.codTarjeta = " + ((int)Tarjetas.TC_HMLInternacional).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt13 ON b.codBeneficioWeb = bt13.codBeneficio AND bt13.codTarjeta = " + ((int)Tarjetas.TC_HMLNacional).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt14 ON b.codBeneficioWeb = bt14.codBeneficio AND bt14.codTarjeta = " + ((int)Tarjetas.TC_Todas).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt15 ON b.codBeneficioWeb = bt15.codBeneficio AND bt15.codTarjeta = " + ((int)Tarjetas.TD_BH).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt16 ON b.codBeneficioWeb = bt16.codBeneficio AND bt16.codTarjeta = " + ((int)Tarjetas.TD_Procrear).ToString() +
                         " LEFT JOIN BeneficioTarjeta bt17 ON b.codBeneficioWeb = bt17.codBeneficio AND bt17.codTarjeta = " + ((int)Tarjetas.TD_Todas).ToString() +
                         " WHERE b.codEstadoBeneficioWeb = 'A' AND suc.codEstadoBeneficioWeb = 'A' " + 
                         ((!string.IsNullOrEmpty(filtro))? " AND C.desComercioBeneficioWeb LIKE '%" + filtro + "%' " : string.Empty)  + 
                          ") as aux";

            return sysnet.DB.ExecuteReturnDS(sql);
        }

        internal int ExisteBeneficio(int codComercio, int codSubrubro, string dias, string beneficioDias, int? descuentoDebito, int? descuentoCredito, int? rangoCuotas, string tope, DateTime? vigenciaDesde, DateTime? vigenciaHasta, string cuit, string web)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            string sql = string.Empty;
            string salida = string.Empty;
            
            sql = "SELECT codBeneficioWeb FROM BeneficioWeb WHERE codComercioBeneficioWeb = @codComercio AND codSubrubroBeneficioWeb = @codSubrubro " + 
                            " AND beneficioDias = @beneficioDias " ;

            if(descuentoDebito.HasValue)
            {
                sql += "AND descuentoBHMaestro = @descuentoDebito ";
            }

            if (descuentoCredito.HasValue)
            {
                sql += "AND descuentoBHVisa = @descuentoCredito ";
            }

            if (rangoCuotas.HasValue)
            {
                sql += "AND cuotasBanco = @rangoCuotas ";
            }

            if (vigenciaDesde.HasValue)
            { 
                sql += "AND VigenciaDesde = @vigenciaDesde ";
            }

            if (vigenciaHasta.HasValue)
            {
                sql += "AND VigenciaHasta = @vigenciaHasta ";
            }

            sql += "AND CUIT = @cuit AND WEB = @web";

            sysnet.DB.Parameters.Add("@codComercio",codComercio);
            sysnet.DB.Parameters.Add("@codSubrubro",codSubrubro);
            sysnet.DB.Parameters.Add("@beneficioDias",beneficioDias);

            if (descuentoDebito.HasValue)
            {
                sysnet.DB.Parameters.Add("@descuentoDebito", descuentoDebito);
            }

            if (descuentoCredito.HasValue)
            {
                sysnet.DB.Parameters.Add("@descuentoCredito", descuentoCredito);
            }

            if (rangoCuotas.HasValue)
            {
                sysnet.DB.Parameters.Add("@rangoCuotas", rangoCuotas);
            }

            if (vigenciaDesde.HasValue)
            {
                sysnet.DB.Parameters.Add("@vigenciaDesde", vigenciaDesde.Value.ToString("yyyyMMdd"));
            }

            if (vigenciaHasta.HasValue)
            {
                sysnet.DB.Parameters.Add("@vigenciaHasta", vigenciaHasta.Value.ToString("yyyyMMdd"));
            }

            sysnet.DB.Parameters.Add("@web", web);
            sysnet.DB.Parameters.Add("@cuit", cuit);

            ds = sysnet.DB.ExecuteReturnDS(sql);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["codBeneficioWeb"]);
                }
            }

            return 0;
        }

        public int ExisteSucursal(int codBeneficio, int codCiudad, string direccion, string telefono, string cuit, string web)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            string sql = string.Empty;
            string salida = string.Empty;

            sql = "SELECT codSucursalBeneficioWeb FROM SucursalBeneficioWeb WHERE codBeneficioWeb = @codBeneficio AND codCiudadBeneficioWeb = @codCiudad AND " +
            "direccion = @direccion AND NroTelefono = @telefono AND CUIT = @cuit AND Web = @web " ;

            sysnet.DB.Parameters.Add("@codBeneficio", codBeneficio);
            sysnet.DB.Parameters.Add("@codCiudad", codCiudad);
            sysnet.DB.Parameters.Add("@direccion", direccion);
            sysnet.DB.Parameters.Add("@telefono", telefono);
            sysnet.DB.Parameters.Add("@cuit", cuit);
            sysnet.DB.Parameters.Add("@web", web);

            ds = sysnet.DB.ExecuteReturnDS(sql);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0]["codSucursalBeneficioWeb"]);
                }
            }

            return 0;
        }


        internal void AgregarTarjetaBeneficio(int codBeneficioWeb, int codTarjeta)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            string sql;

            try
            {
                sysnet.DB.BeginTransaction();

                sql = "insert into BeneficioTarjeta ( codBeneficio, codTarjeta) values ( @codBeneficio, @codTarjeta ) ";

                sysnet.DB.Parameters.Add("@codBeneficio", codBeneficioWeb);
                sysnet.DB.Parameters.Add("@codTarjeta", codTarjeta);

                sysnet.DB.Execute(sql, true);

                sysnet.DB.CommitTransaction();
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar tarjeta beneficio.\n codBeneficioWeb: {0}, codTarjeta : {1}\n {2}", codBeneficioWeb, codTarjeta, ex.Message);
                throw new ApplicationException(mensaje, ex);
            }
        }
    }
}
