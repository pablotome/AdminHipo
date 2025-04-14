using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

using BH.Sysnet;
using BH.EPotecario.Adm.Componentes;

namespace BH.EPotecario.Adm.Componentes
{
	public class ResultadoParseo
	{
		IList<Premio> premios;
		IList<ErrorImportacion> errores;

		public IList<Premio> Premios
		{
			get { return premios; }
			set { premios = value; }
		}

		public IList<ErrorImportacion> Errores
		{
			get { return errores; }
			set { errores = value; }
		}
	}
	
	public class CategoriaPremio
	{
		int codCategoriaPremio;
		string categoria;
        byte[] botonApagado;
        byte[] botonPrendido;
        bool habilitado;

		public int CodCategoriaPremio
		{
			get { return codCategoriaPremio; }
			set { codCategoriaPremio = value; }
		}

		public string Categoria
		{
			get { return categoria; }
			set { categoria = value; }
		}

        public byte[] BotonApagado
        {
            get { return botonApagado; }
            set { botonApagado = value; }
        }

        public byte[] BotonPrendido
        {
            get { return botonPrendido; }
            set { botonPrendido = value; }
        }

        public bool Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }
	}

	public class PaisOrigenPremio
	{
		int codPaisOrigenPremio;
		string pais;

		public int CodPaisOrigenPremio
		{
			get { return codPaisOrigenPremio; }
			set { codPaisOrigenPremio = value; }
		}

		public string Pais
		{
			get { return pais; }
			set { pais = value; }
		}
	}

	public class Premio
	{ 
		int codPremio;
		string codProducto;
		string codProductoPuntosPesos;
		int stock;
		int puntos;
		string puntosPesos;
		DateTime vigencia;
        DateTime vigenciaFin;
		string descripcionCorta;
		string descripcionLarga;
		PaisOrigenPremio pais;
		string garantia;
		float peso;
		CategoriaPremio categoria;
		string accion;
		bool habilitado;
		DateTime fechaUltimaModificacion;
		bool destacado;
		byte[] imagen;
        bool nuevo;
        bool agotado;
		bool soloPorHoy;
		bool descuento10;
		bool descuento20;
		bool promocion;

		public const int CantidadCamposEspacioDueños = 28;

		public int CodPremio
		{
			get { return codPremio; }
			set { codPremio = value; }
		}

		public string CodProducto
		{
			get { return codProducto; }
			set { codProducto = value; }
		}

		public int Stock
		{
			get { return stock; }
			set { stock = value; }
		}

		public int Puntos
		{
			get { return puntos; }
			set { puntos = value; }
		}

		public DateTime Vigencia
		{
			get { return vigencia; }
			set { vigencia = value; }
		}

        public DateTime VigenciaFin
        {
            get { return vigenciaFin; }
            set { vigenciaFin = value; }
        }

		public string DescripcionCorta
		{
			get { return descripcionCorta; }
			set { descripcionCorta = value; }
		}

		public string DescripcionLarga
		{
			get { return descripcionLarga; }
			set { descripcionLarga = value; }
		}

		public PaisOrigenPremio Pais
		{
			get { return pais; }
			set { pais = value; }
		}

		public string Garantia
		{
			get { return garantia; }
			set { garantia = value; }
		}

		public float Peso
		{
			get { return peso; }
			set { peso = value; }
		}

		public CategoriaPremio Categoria
		{
			get { return categoria; }
			set { categoria = value; }
		}

		public string Accion
		{
			get { return accion; }
			set { accion = value; }
		}

		public bool Habilitado
		{
			get { return habilitado; }
			set { habilitado = value; }
		}

		public DateTime FechaUltimaModificacion
		{
			get { return fechaUltimaModificacion; }
			set { fechaUltimaModificacion = value; }
		}

		public bool Destacado
		{
			get { return destacado; }
			set { destacado = value; }
		}

		public byte[] Image
		{
			get { return imagen; }
			set { imagen = value; }
		}

        public bool Nuevo
        {
            get { return nuevo; }
            set { nuevo = value; }
		}

		public bool Agotado
		{
			get { return agotado; }
			set { agotado = value; }
		}

		public bool SoloPorHoy
		{
			get { return soloPorHoy; }
			set { soloPorHoy = value; }
		}

		public bool Descuento10
		{
			get { return descuento10; }
			set { descuento10 = value; }
		}

		public bool Descuento20
		{
			get { return descuento20; }
			set { descuento20 = value; }
		}

		public bool Promocion
		{
			get { return promocion; }
			set { promocion = value; }
		}

		public string CodProductoPuntosPesos
		{
			get { return codProductoPuntosPesos; }
			set { codProductoPuntosPesos = value; }
		}

		public string PuntosPesos
		{
			get { return puntosPesos; }
			set { puntosPesos = value; }
		}
	}

	public class ErrorImportacion
	{
		string lineaArchivo;
		string mensajeError;

		public string LineaArchivo
		{
			get { return lineaArchivo; }
			set { lineaArchivo = value; }
		}

		public string MensajeError
		{
			get { return mensajeError; }
			set { mensajeError = value; }
		}
	}

	public class EspacioDuenios : ObjectBase
	{
		public EspacioDuenios()	{}

		public ResultadoParseo ParsearArchivoPremios(StreamReader streamReader)
		{
			//string archivo = streamReader.ReadToEnd();
			//string pattern = @"(?<Grupo>\d{4})-(?<Orden>\d{3})[ ]*(?<Modelo>[A-Za-z0-9]{5})[ ]*(?<TAjustado>[0-9]+\.[0-9]{2})[ ]*(?<TLicitado>[0-9]+\.[0-9]{2})(?<Observacion>.{10})[ ]*(?<Concesionario>\d{0,5})";
			string linea, valorActual;
			string[] camposPremio;
			IList<Premio> premios = new List<Premio>();
			Premio premio;
			PaisOrigenPremio paisOrigenPremio;
			CategoriaPremio categoriaPremio;
			int numLinea = 1;
			IList<ErrorImportacion> errores = new List<ErrorImportacion>();
			ResultadoParseo resultadoParseo = new ResultadoParseo();

			while ((linea = streamReader.ReadLine()) != null)
			{
                if (numLinea != 1)
                {
                    camposPremio = linea.Split("|".ToCharArray());
                    try
                    {
						if (camposPremio.Length == Premio.CantidadCamposEspacioDueños)
                        {
                            premio = new Premio();
                            paisOrigenPremio = new PaisOrigenPremio();
                            categoriaPremio = new CategoriaPremio();

                            //1er Campo: Accion, valores A, B o M
                            valorActual = camposPremio[0].Trim();
                            if (Regex.IsMatch(valorActual, @"^A|B|M$"))
                                premio.Accion = valorActual[0].ToString();
                            else
                                throw new ApplicationException(string.Format("El primer campo debe ser la acción, solo valores A, B o M. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //2do Campo: CodigoProducto, hasta 8 dígitos
                            valorActual = camposPremio[1].Trim();
                            if (Regex.IsMatch(valorActual, @"^\d{1,8}$"))
                                premio.CodProducto = int.Parse(valorActual).ToString();
                            else
                                throw new ApplicationException(string.Format("El segundo campo debe ser el código de producto, hasta 8 dígitos. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //3er Campo: Stock, hasta 6 dígitos
                            valorActual = camposPremio[2].Trim();
                            if (Regex.IsMatch(valorActual, @"^\-?\d{1,6}$"))
                                premio.Stock = int.Parse(valorActual);
                            else
                                throw new ApplicationException(string.Format("El tercer campo debe ser el stock del producto, hasta 6 dígitos. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //5to Campo: Puntos, hasta 6 dígitos
                            valorActual = camposPremio[4].Trim();
                            valorActual = valorActual.Replace(".", ""); //Se le saca los puntos porque el campo en la base es Int
                            if (Regex.IsMatch(valorActual, @"^\-?\d{1,6}$"))
                                premio.Puntos = int.Parse(valorActual);
                            else if (valorActual == string.Empty)
                                premio.Puntos = 0;
                            else
                                throw new ApplicationException(string.Format("El quinto campo deben ser los puntos del producto, hasta 6 dígitos. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //6to Campo: Categoría, lo que venga
                            valorActual = camposPremio[5].Trim();
                            if (valorActual.Length <= 100 && valorActual != string.Empty)
                            {
                                categoriaPremio.Categoria = valorActual;
                                premio.Categoria = categoriaPremio;
                            }
                            else
                                throw new ApplicationException(string.Format("El sexto primer campo debe ser la categoría del producto, hasta 100 caracteres. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //7mo Campo: VigenciaDesde, fecha, en el formato dd/mm/aaaa
                            valorActual = camposPremio[6].Trim();
                            if (Regex.IsMatch(valorActual, @"^\d{2}/\d{2}/\d{4}$"))
                            {
                                premio.Vigencia = DateTime.ParseExact(valorActual, "dd/MM/yyyy", null);
                            }
                            else
                                throw new ApplicationException(string.Format("El séptimo campo debe ser la Fecha de Inicio del producto, una fecha, en el formato dd/mm/aaaa. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //8vo Campo: VigenciaFin, fecha, en el formato dd/mm/aaaa
                            valorActual = camposPremio[7].Trim();
                            if (Regex.IsMatch(valorActual, @"^\d{2}/\d{2}/\d{4}$"))
                            {
                                premio.VigenciaFin = DateTime.ParseExact(valorActual, "dd/MM/yyyy", null);
                            }
                            else
                                throw new ApplicationException(string.Format("El octavo campo debe ser la Fecha de Fin del producto, una fecha, en el formato dd/mm/aaaa. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //9no Campo: Descripción corta, lo que venga
                            valorActual = camposPremio[8].Trim();
                            if (valorActual.Length <= 100 && valorActual != string.Empty)
                                premio.DescripcionCorta = valorActual;
                            else
                                throw new ApplicationException(string.Format("El noveno campo debe ser la descripción corta del producto, hasta 100 caracteres. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //10mo Campo: Descripción larga, lo que venga
                            valorActual = camposPremio[9].Trim();
                            if (valorActual.Length <= 500 && valorActual != string.Empty)
                                premio.DescripcionLarga = valorActual;
                            else
                                throw new ApplicationException(string.Format("El décimo campo debe ser la descripción larga del producto, hasta 500 caracteres. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //11vo Campo: Pais de Origen, lo que venga
                            valorActual = camposPremio[10].Trim();
                            if (valorActual.Length <= 100 && valorActual != string.Empty)
                            {
                                paisOrigenPremio.Pais = valorActual;
                                premio.Pais = paisOrigenPremio;
                            }
                            else
                                throw new ApplicationException(string.Format("El décimo primero campo debe ser el pais de origen del producto, hasta 100 caracteres. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //12no Campo: Garantía, lo que venga
                            valorActual = camposPremio[11].Trim();
                            if (valorActual.Length <= 100)
                                premio.Garantia = valorActual;
                            else
                                throw new ApplicationException(string.Format("El décimo segundo campo deben ser los meses de garantía del producto, hasta 100 caracteres. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //13mo Peso: Peso, 4 dígitos para la parte entera y dos para la parte decimal
                            valorActual = camposPremio[12].Trim();
                            if (Regex.IsMatch(valorActual, @"^\d{1,4}(\,\d{1,2})?$"))
                                premio.Peso = float.Parse(valorActual, HelperWeb.GetFormatoDecimalComa());
                            else
                                throw new ApplicationException(string.Format("El décimo tercer campo debe ser el peso del producto, un número decimal, 4 dígitos para la parte entera y dos para la parte decimal. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //21mo Nuevo: Nuevo, 1 dígitos puede ser 0 ó 1
                            valorActual = camposPremio[20].Trim();
                            if (Regex.IsMatch(valorActual, @"^\d{1,1}?$"))
                            {
                                if (valorActual == "0" || valorActual == "1")
                                    premio.Nuevo = (valorActual == "0") ? false : true;
								else
									throw new ApplicationException(string.Format("El vigésimo primer campo debe indicar si el producto es nuevo o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));
							}
                            else
                                throw new ApplicationException(string.Format("El vigésimo primer campo debe indicar si el producto es nuevo o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));

							//22mo Agotado: Agotado, 1 dígitos puede ser 0 ó 1
							valorActual = camposPremio[21].Trim();
							if (Regex.IsMatch(valorActual, @"^\d{1,1}?$"))
							{
								if (valorActual == "0" || valorActual == "1")
									premio.Agotado = (valorActual == "0") ? false : true;
								else
									throw new ApplicationException(string.Format("El vigésimo segundo campo debe indicar si el producto esta agotado o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));
							}
							else
								throw new ApplicationException(string.Format("El vigésimo segundo campo debe indicar si el producto esta agotado o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));

							//23mo SoloPorHoy: SoloPorHoy, 1 dígitos puede ser 0 ó 1
							valorActual = camposPremio[22].Trim();
							if (Regex.IsMatch(valorActual, @"^\d{1,1}?$"))
							{
								if (valorActual == "0" || valorActual == "1")
									premio.SoloPorHoy = (valorActual == "0") ? false : true;
								else
									throw new ApplicationException(string.Format("El vigésimo tercer campo debe indicar si el producto esta vigente solo por hoy o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));
							}
							else
								throw new ApplicationException(string.Format("El vigésimo tercer campo debe indicar si el producto esta vigente solo por hoy o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));

							//24mo Descuento10: Descuento10, 1 dígitos puede ser 0 ó 1
							valorActual = camposPremio[23].Trim();
							if (Regex.IsMatch(valorActual, @"^\d{1,1}?$"))
							{
								if (valorActual == "0" || valorActual == "1")
									premio.Descuento10 = (valorActual == "0") ? false : true;
								else
									throw new ApplicationException(string.Format("El vigésimo cuarto campo debe indicar si el producto tiene 10% de descuento o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));
							}
							else
								throw new ApplicationException(string.Format("El vigésimo cuarto debe indicar si el producto tiene 10% de descuento o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));

							//25mo Descuento20: Descuento20, 1 dígitos puede ser 0 ó 1
							valorActual = camposPremio[24].Trim();
							if (Regex.IsMatch(valorActual, @"^\d{1,1}?$"))
							{
								if (valorActual == "0" || valorActual == "1")
									premio.Descuento20 = (valorActual == "0") ? false : true;
								else
									throw new ApplicationException(string.Format("El vigésimo quinto campo debe indicar si el producto tiene 20% de descuento o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));
							}
							else
								throw new ApplicationException(string.Format("El vigésimo quinto debe indicar si el producto tiene 20% de descuento o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));

							//26mo Promoción: Promoción, 1 dígitos puede ser 0 ó 1
							valorActual = camposPremio[25].Trim();
							if (Regex.IsMatch(valorActual, @"^\d{1,1}?$"))
							{
								if (valorActual == "0" || valorActual == "1")
									premio.Promocion = (valorActual == "0") ? false : true;
								else
									throw new ApplicationException(string.Format("El vigésimo sexto campo debe indicar si el producto está en promoción o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));
							}
							else
								throw new ApplicationException(string.Format("El vigésimo sexto debe indicar si el producto está en promoción o no, un número decimal, 1 dígito puede ser 1 ó 0. Valor: {0} - Línea {1}", valorActual, numLinea));

							//27mo Campo: CodProductoPuntosPesos, hasta 8 dígitos
							valorActual = camposPremio[26].Trim();
							if (valorActual == string.Empty)
								premio.CodProductoPuntosPesos = string.Empty;
							else if (Regex.IsMatch(valorActual, @"^\d{1,8}$"))
								premio.CodProductoPuntosPesos = int.Parse(valorActual).ToString();
							else
								throw new ApplicationException(string.Format("El vigésimo séptimo campo debe ser el código de producto para la opción puntos + pesos, hasta 8 dígitos. Valor: {0} - Línea {1}", valorActual, numLinea));

							//28to Campo: Puntos más pesos, hasta 30 caracteres
							valorActual = camposPremio[27].Trim();
							if (valorActual == string.Empty)
								premio.PuntosPesos = string.Empty;
							else if (Regex.IsMatch(valorActual, @"^.{1,30}$"))
								premio.PuntosPesos = valorActual.ToString();
							else
								throw new ApplicationException(string.Format("El vigésimo octavo campo debe ser los puntos del producto, hasta 6 dígitos. Valor: {0} - Línea {1}", valorActual, numLinea));

                            //Habilito lo que estoy importando, el usuario lo debe habilitar
                            premio.Habilitado = true;
                            premio.FechaUltimaModificacion = DateTime.Now;
                            premio.Destacado = false;

                            premios.Add(premio);
                        }
                        else
                            throw new ApplicationException(string.Format("Se deben respetar {0} campos por línea, separados por pipe.", Premio.CantidadCamposEspacioDueños));
                    }
                    catch (ApplicationException appEx)
                    {
                        ErrorImportacion error = new ErrorImportacion();
                        error.LineaArchivo = linea;
                        error.MensajeError = appEx.Message;
                        errores.Add(error);
                    }
                }
				numLinea++;
			}

			resultadoParseo.Premios = premios;
			resultadoParseo.Errores = errores;

			return resultadoParseo;
		}

		public Premio AgregarPremio(Premio premio)
		{
			SysNet sysnet = SI.GetSysNet();
			
			string sql;
            string accion;

			try
			{
				sysnet.DB.BeginTransaction();
                accion = premio.Accion;
                if (accion != "B")
                {
                    premio.Pais = AgregarPaisOrigen(premio.Pais);
                    premio.Categoria = AgregarCategoria(premio.Categoria);
                }

				Premio premioExiste = ObtenerPremio(premio.CodProducto);

                if (premioExiste == null && accion != "B")
				{
					sql = "insert into Premio ( CodProducto, Stock, Puntos, Vigencia, DescripcionCorta, DescripcionLarga, CodPaisOrigenPremio, Garantia, Peso, CodCategoria, Habilitado, FechaUltimaModificacion, Destacado, VigenciaFin, Nuevo, Agotado, SoloPorHoy, Descuento10, Descuento20, Promocion, CodProductoPuntosPesos, PuntosPesos ) values ( @CodProducto, @Stock, @Puntos, @Vigencia, @DescripcionCorta, @DescripcionLarga, @CodPaisOrigenPremio, @Garantia, @Peso, @CodCategoria, @Habilitado, @FechaUltimaModificacion, @Destacado, @VigenciaFin, @Nuevo, @Agotado, @SoloPorHoy, @Descuento10, @Descuento20, @Promocion, @CodProductoPuntosPesos, @PuntosPesos )";
					sysnet.DB.Parameters.Add("@CodProducto", premio.CodProducto);
					sysnet.DB.Parameters.Add("@Stock", premio.Stock);
					sysnet.DB.Parameters.Add("@Puntos", premio.Puntos);
					sysnet.DB.Parameters.Add("@Vigencia", premio.Vigencia);
                    sysnet.DB.Parameters.Add("@VigenciaFin", premio.VigenciaFin);
					sysnet.DB.Parameters.Add("@DescripcionCorta", premio.DescripcionCorta);
					sysnet.DB.Parameters.Add("@DescripcionLarga", premio.DescripcionLarga);
					sysnet.DB.Parameters.Add("@CodPaisOrigenPremio", premio.Pais.CodPaisOrigenPremio);
					sysnet.DB.Parameters.Add("@Garantia", premio.Garantia);
					sysnet.DB.Parameters.Add("@Peso", premio.Peso);
					sysnet.DB.Parameters.Add("@CodCategoria", premio.Categoria.CodCategoriaPremio);
					sysnet.DB.Parameters.Add("@Habilitado", premio.Habilitado);
					sysnet.DB.Parameters.Add("@FechaUltimaModificacion", premio.FechaUltimaModificacion);
					sysnet.DB.Parameters.Add("@Destacado", premio.Destacado);
                    sysnet.DB.Parameters.Add("@Nuevo", premio.Nuevo);
					sysnet.DB.Parameters.Add("@Agotado", premio.Agotado);
					sysnet.DB.Parameters.Add("@SoloPorHoy", premio.SoloPorHoy);
					sysnet.DB.Parameters.Add("@Descuento10", premio.Descuento10);
					sysnet.DB.Parameters.Add("@Descuento20", premio.Descuento20);
					sysnet.DB.Parameters.Add("@Promocion", premio.Promocion);
					sysnet.DB.Parameters.Add("@CodProductoPuntosPesos", premio.CodProductoPuntosPesos);
					sysnet.DB.Parameters.Add("@PuntosPesos", premio.PuntosPesos);

					premio.CodPremio = (int)sysnet.DB.Execute(sql, true);
				}
				else
				{
                    if (accion == "B")
                    {
                        try
                        {
                            sql = "Delete Premio where CodPremio = @CodPremio";
                            sysnet.DB.Parameters.Add("@CodPremio", premioExiste.CodPremio);
                            sysnet.DB.Execute(sql);

                            ////Elimino la categoria
                            
                            sql = "Delete CategoriaPremio where CodCategoriaPremio = @CodCategoriaPremio";
                            sysnet.DB.Parameters.Add("@CodCategoriaPremio", premioExiste.Categoria.CodCategoriaPremio);
                            sysnet.DB.Execute(sql);
                        }
                        catch //(Exception ex)
                        {

                        }
                    }
                    else
                    {
						sql = "update Premio set CodProducto= @CodProducto, Stock = @Stock, Puntos = @Puntos, Vigencia = @Vigencia, DescripcionCorta = @DescripcionCorta, DescripcionLarga = @DescripcionLarga, CodPaisOrigenPremio = @CodPaisOrigenPremio, Garantia = @Garantia, Peso = @Peso, CodCategoria = @CodCategoria, Habilitado = @Habilitado, FechaUltimaModificacion = @FechaUltimaModificacion, Destacado = @Destacado, VigenciaFin = @VigenciaFin, Nuevo = @Nuevo, Agotado = @Agotado, SoloPorHoy = @SoloPorHoy, Descuento10 = @Descuento10, Descuento20 = @Descuento20, Promocion = @Promocion, CodProductoPuntosPesos=@CodProductoPuntosPesos, PuntosPesos=@PuntosPesos where CodPremio = @CodPremio";
                        sysnet.DB.Parameters.Add("@CodPremio", premioExiste.CodPremio);
                        sysnet.DB.Parameters.Add("@CodProducto", premio.CodProducto);
                        sysnet.DB.Parameters.Add("@Stock", premio.Stock);
                        sysnet.DB.Parameters.Add("@Puntos", premio.Puntos);
                        sysnet.DB.Parameters.Add("@Vigencia", premio.Vigencia);
                        sysnet.DB.Parameters.Add("@VigenciaFin", premio.VigenciaFin);
                        sysnet.DB.Parameters.Add("@DescripcionCorta", premio.DescripcionCorta);
                        sysnet.DB.Parameters.Add("@DescripcionLarga", premio.DescripcionLarga);
                        sysnet.DB.Parameters.Add("@CodPaisOrigenPremio", premio.Pais.CodPaisOrigenPremio);
                        sysnet.DB.Parameters.Add("@Garantia", premio.Garantia);
                        sysnet.DB.Parameters.Add("@Peso", premio.Peso);
                        sysnet.DB.Parameters.Add("@CodCategoria", premio.Categoria.CodCategoriaPremio);
                        sysnet.DB.Parameters.Add("@Habilitado", premio.Habilitado);
                        sysnet.DB.Parameters.Add("@FechaUltimaModificacion", DateTime.Now);
                        sysnet.DB.Parameters.Add("@Destacado", premio.Destacado);
                        sysnet.DB.Parameters.Add("@Nuevo", premio.Nuevo);
                        sysnet.DB.Parameters.Add("@Agotado", premio.Agotado);
						sysnet.DB.Parameters.Add("@SoloPorHoy", premio.SoloPorHoy);
						sysnet.DB.Parameters.Add("@Descuento10", premio.Descuento10);
						sysnet.DB.Parameters.Add("@Descuento20", premio.Descuento20);
						sysnet.DB.Parameters.Add("@Promocion", premio.Promocion);
						sysnet.DB.Parameters.Add("@CodProductoPuntosPesos", premio.CodProductoPuntosPesos);
						sysnet.DB.Parameters.Add("@PuntosPesos", premio.PuntosPesos);

                        premio.CodPremio = premioExiste.CodPremio;
                        sysnet.DB.Execute(sql);
                    }
                    
				}
				sysnet.DB.CommitTransaction();

				return premio;
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				//string mensaje = string.Format("Error al cargar provincia.\ncodProvinciaBeneficioWeb: {0}, codProvinciaBeneficioWebProveedor: {1}, desProvinciaBeneficioWeb: {2}, codProveedorBeneficioWeb: {3}\n{4}", codProvinciaBeneficioWeb, codProvinciaBeneficioWebProveedor, desProvinciaBeneficioWeb, codProveedorBeneficioWeb, ex.Message);
				string mensaje = string.Format("Error al cargar premio: <b>{0}</b>", ex.Message);
				throw new ApplicationException(mensaje, ex);
			}
		}

		public Premio ObtenerPremio(string codProducto)
		{
			SysNet sysnet = SI.GetSysNet();

			Premio premio = null;

			string sql = "select CodPremio, CodProducto, Stock, Puntos, Vigencia, DescripcionCorta, DescripcionLarga, CodPaisOrigenPremio, Garantia, Peso, CodCategoria, Habilitado, FechaUltimaModificacion, Destacado, VigenciaFin, Nuevo, Agotado, SoloPorHoy, Descuento10, Descuento20, Promocion, CodProductoPuntosPesos, PuntosPesos from Premio where CodProducto like @CodProducto";
			sysnet.DB.Parameters.Add("@CodProducto", codProducto);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				premio = new Premio();
                premio.CodPremio = int.Parse(reader["CodPremio"].ToString());
                premio.CodProducto = reader["CodProducto"].ToString();
                premio.Stock = int.Parse(reader["Stock"].ToString());
                premio.Puntos = int.Parse(reader["Puntos"].ToString());
                premio.Vigencia = DateTime.Parse(reader["Vigencia"].ToString());
                premio.DescripcionCorta = reader["DescripcionCorta"].ToString();
                premio.DescripcionLarga = reader["DescripcionLarga"].ToString();
                premio.Pais = ObtenerPais(int.Parse(reader["CodPaisOrigenPremio"].ToString()));
                premio.Garantia = reader["Garantia"].ToString();
                premio.Peso = float.Parse(reader["Peso"].ToString());
                premio.Categoria = ObtenerCategoria(int.Parse(reader["CodCategoria"].ToString()));
                premio.Habilitado = bool.Parse(reader["Habilitado"].ToString());
                premio.Destacado = bool.Parse(reader["Destacado"].ToString());
                premio.Nuevo = bool.Parse(reader["Nuevo"].ToString());
                premio.Agotado = bool.Parse(reader["Agotado"].ToString());
				premio.SoloPorHoy = bool.Parse(reader["SoloPorHoy"].ToString());
				premio.Descuento10 = bool.Parse(reader["Descuento10"].ToString());
				premio.Descuento20 = bool.Parse(reader["Descuento20"].ToString());
				premio.Promocion = bool.Parse(reader["Promocion"].ToString());
				premio.CodProductoPuntosPesos = reader["CodProductoPuntosPesos"].ToString();
				premio.PuntosPesos = reader["PuntosPesos"].ToString();

                premio.FechaUltimaModificacion = DateTime.Parse(reader["FechaUltimaModificacion"].ToString());
                if (reader["VigenciaFin"].ToString() != "")
                    premio.VigenciaFin = DateTime.Parse(reader["VigenciaFin"].ToString());
			}
			reader.Close();

			return premio;
		}

		public IList<Premio> ObtenerPremios()
		{
			SysNet sysnet = SI.GetSysNet();

			IList<Premio> premios = new List<Premio>();

			string sql = "select CodPremio, CodProducto, Stock, Puntos, Vigencia, DescripcionCorta, DescripcionLarga, CodPaisOrigenPremio, Garantia, Peso, CodCategoria, Habilitado, FechaUltimaModificacion, Destacado, VigenciaFin, Nuevo, Agotado, SoloPorHoy, Descuento10, Descuento20, Promocion, CodProductoPuntosPesos, PuntosPesos from Premio order by CodProducto";
			
			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			
			return ObtenerPremios(reader);
		}

		public byte[] ObtenerImagenPremio(string codProducto)
		{
			SysNet sysnet = SI.GetSysNet();

			byte[] imagen = null;

			string sql = "select Imagen from Premio where CodProducto like @CodProducto";
			sysnet.DB.Parameters.Add("@CodProducto", codProducto);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                    imagen = (byte[])reader[0];
            }
			reader.Close();

			return imagen;
		}

        public byte[] ObtenerImagenCategoria(string codCategoria, bool esApagado)
        {
            SysNet sysnet = SI.GetSysNet();

            byte[] imagen = null;

            string sql = "Select ";
            if (esApagado)
                sql += "BotonApagado";
            else
                sql += "BotonPrendido";

            sql+= " from categoriaPremio where CodCategoriaPremio like @codCategoria";

            sysnet.DB.Parameters.Add("@codCategoria", codCategoria);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                    imagen = (byte[])reader[0];
            }
            reader.Close();

            return imagen;
        }

		public PaisOrigenPremio ObtenerPais(string strPais)
		{
			SysNet sysnet = SI.GetSysNet();
			
			PaisOrigenPremio pais = null;
			
			string sql = "select CodPaisOrigenPremio, Pais from PaisOrigenPremio where Pais like @Pais";
			sysnet.DB.Parameters.Add("@Pais", strPais);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				pais = new PaisOrigenPremio();
				pais.CodPaisOrigenPremio = int.Parse(reader[0].ToString());
				pais.Pais = reader[1].ToString();
			}
			reader.Close();

			return pais;
		}

		public PaisOrigenPremio ObtenerPais(int codPais)
		{
			SysNet sysnet = SI.GetSysNet();

			PaisOrigenPremio pais = null;

			string sql = "select CodPaisOrigenPremio, Pais from PaisOrigenPremio where CodPaisOrigenPremio = @CodPaisOrigenPremio";
			sysnet.DB.Parameters.Add("@CodPaisOrigenPremio", codPais);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				pais = new PaisOrigenPremio();
				pais.CodPaisOrigenPremio = int.Parse(reader[0].ToString());
				pais.Pais = reader[1].ToString();
			}
			reader.Close();

			return pais;
		}

		public IList<PaisOrigenPremio> ObtenerPaises()
		{
			SysNet sysnet = SI.GetSysNet();

			IList<PaisOrigenPremio> paises = new List<PaisOrigenPremio>();
			PaisOrigenPremio pais;

			string sql = "select CodPaisOrigenPremio, Pais from PaisOrigenPremio order by Pais";

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			while (reader.Read())
			{
				pais = new PaisOrigenPremio();
				pais.CodPaisOrigenPremio = int.Parse(reader[0].ToString());
				pais.Pais = reader[1].ToString();
				paises.Add(pais);
			}
			reader.Close();

			return paises;
		}

		public CategoriaPremio ObtenerCategoria(string strCategoria)
		{
			SysNet sysnet = SI.GetSysNet();

			CategoriaPremio categoria = null;

			string sql = "select CodCategoriaPremio, Categoria, Habilitado from CategoriaPremio where Categoria like @Categoria";
			sysnet.DB.Parameters.Add("@Categoria", strCategoria);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				categoria = new CategoriaPremio();
				categoria.CodCategoriaPremio = int.Parse(reader[0].ToString());
				categoria.Categoria = reader[1].ToString();
                categoria.Habilitado = bool.Parse(reader[2].ToString());
			}
			reader.Close();

			return categoria;
		}

		public CategoriaPremio ObtenerCategoria(int codCategoria)
		{
			SysNet sysnet = SI.GetSysNet();

			CategoriaPremio categoria = null;

            string sql = "select CodCategoriaPremio, Categoria, Habilitado from CategoriaPremio where CodCategoriaPremio = @CodCategoriaPremio";
			sysnet.DB.Parameters.Add("@CodCategoriaPremio", codCategoria);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				categoria = new CategoriaPremio();
				categoria.CodCategoriaPremio = int.Parse(reader[0].ToString());
				categoria.Categoria = reader[1].ToString();
                categoria.Habilitado = bool.Parse(reader[2].ToString());
			}
			reader.Close();

			return categoria;
		}

		public IList<CategoriaPremio> ObtenerCategorias()
		{
			SysNet sysnet = SI.GetSysNet();

			IList<CategoriaPremio> categorias = new List<CategoriaPremio>();
			CategoriaPremio categoria;

			string sql = "select CodCategoriaPremio, Categoria, Habilitado from CategoriaPremio order by Categoria";

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			while (reader.Read())
			{
				categoria = new CategoriaPremio();
				categoria.CodCategoriaPremio = int.Parse(reader[0].ToString());
				categoria.Categoria = reader[1].ToString();
                categoria.Habilitado = bool.Parse(reader[2].ToString());
				categorias.Add(categoria);
			}
			reader.Close();

			return categorias;
		}

		public PaisOrigenPremio AgregarPaisOrigen(PaisOrigenPremio pais)
		{
			SysNet sysnet = SI.GetSysNet();

			PaisOrigenPremio paisExiste = ObtenerPais(pais.Pais);

			if (paisExiste != null)
				return paisExiste;

			string sql = "insert into PaisOrigenPremio ( Pais ) values ( @Pais ) ";
			sysnet.DB.Parameters.Add("@Pais", pais.Pais);

			pais.CodPaisOrigenPremio = (int)sysnet.DB.Execute(sql, true);

			return pais;
		}

		public CategoriaPremio AgregarCategoria(CategoriaPremio categoria)
		{
			SysNet sysnet = SI.GetSysNet();

			string sql;

			CategoriaPremio categoriaExiste = ObtenerCategoria(categoria.Categoria);

			if (categoriaExiste != null)
				return categoriaExiste;

            sql = "insert into CategoriaPremio ( Categoria, Habilitado ) values ( @Categoria, @Habilitado ) ";
			sysnet.DB.Parameters.Add("@Categoria", categoria.Categoria);
            sysnet.DB.Parameters.Add("@Habilitado", true);

			categoria.CodCategoriaPremio = (int)sysnet.DB.Execute(sql, true);

			return categoria;
		}

		public string ActualizarImagenPremio(Premio premio)
		{
			SysNet sysnet = SI.GetSysNet();

			string sql;

			try
			{
				sql = "update Premio set Imagen = @Imagen where CodPremio = @CodPremio";
				sysnet.DB.Parameters.Add("@Imagen", premio.Image);
				sysnet.DB.Parameters.Add("@CodPremio", premio.CodPremio);

				sysnet.DB.Execute(sql);

				return "Actualizacion OK";
			}
			catch (Exception ex)
			{
				//string mensaje = string.Format("Error al cargar provincia.\ncodProvinciaBeneficioWeb: {0}, codProvinciaBeneficioWebProveedor: {1}, desProvinciaBeneficioWeb: {2}, codProveedorBeneficioWeb: {3}\n{4}", codProvinciaBeneficioWeb, codProvinciaBeneficioWebProveedor, desProvinciaBeneficioWeb, codProveedorBeneficioWeb, ex.Message);
				string mensaje = string.Format("Error al cargar premio");
				do
				{
					mensaje = string.Format("{0}\n{1}", mensaje, ex.Message);
					ex = ex.InnerException;
				}
				while (ex != null);
				return mensaje;
				//throw new ApplicationException(mensaje, ex);
			}
		}

        public string ActualizarImagenCategoria(CategoriaPremio cPremio, bool esApagado)
        {
            SysNet sysnet = SI.GetSysNet();

            string sql;

            try
            {
                sql = "update CategoriaPremio set ";

                if (esApagado)
                    sql += "BotonApagado = @botonApagado";
                else
                    sql += "BotonPrendido = @botonPrendido";
                
                sql +=" where CodCategoriaPremio = @CodCategoria";

                if(esApagado)
                    sysnet.DB.Parameters.Add("@botonApagado", cPremio.BotonApagado);
                else
                    sysnet.DB.Parameters.Add("@botonPrendido", cPremio.BotonPrendido);

                sysnet.DB.Parameters.Add("@CodCategoria", cPremio.CodCategoriaPremio);

                sysnet.DB.Execute(sql);

                return "Actualizacion OK";
            }
            catch (Exception ex)
            {
                //string mensaje = string.Format("Error al cargar provincia.\ncodProvinciaBeneficioWeb: {0}, codProvinciaBeneficioWebProveedor: {1}, desProvinciaBeneficioWeb: {2}, codProveedorBeneficioWeb: {3}\n{4}", codProvinciaBeneficioWeb, codProvinciaBeneficioWebProveedor, desProvinciaBeneficioWeb, codProveedorBeneficioWeb, ex.Message);
                string mensaje = string.Format("Error al cargar premio");
                do
                {
                    mensaje = string.Format("{0}\n{1}", mensaje, ex.Message);
                    ex = ex.InnerException;
                }
                while (ex != null);
                return mensaje;
                //throw new ApplicationException(mensaje, ex);
            }
        }

        public void EliminarPremios()
        {
            SysNet sysnet = SI.GetSysNet();
			sysnet.DB.Execute("exec DBO.LimpiarBaseEspacioDuenios");
        }


        public string EditarVigencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Update Parametros set valor=@CatalogoFechaDesde where Parametro = 'CatalogoFechaDesde'";
                sysnet.DB.Parameters.Add("@CatalogoFechaDesde", ObtenerVigencia(fechaDesde));
                sysnet.DB.Execute(sql);

                sql = "Update Parametros set valor=@CatalogoFechaHasta where Parametro = 'CatalogoFechaHasta'";
                sysnet.DB.Parameters.Add("@CatalogoFechaHasta", ObtenerVigencia(fechaHasta));
                sysnet.DB.Execute(sql);
                sysnet.DB.CommitTransaction();
                return "Actualizacion OK";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error actualizar la Vigencia");
                do
                {
                    mensaje = string.Format("{0}\n{1}", mensaje, ex.Message);
                    ex = ex.InnerException;
                }
                while (ex != null);
                return mensaje;
            }
        }

        public string EditarVigencia(string fechaDesde, string fechaHasta)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Update Parametros set valor=@CatalogoFechaDesde where Parametro = 'CatalogoFechaDesde'";
                sysnet.DB.Parameters.Add("@CatalogoFechaDesde", fechaDesde);
                sysnet.DB.Execute(sql);

                sql = "Update Parametros set valor=@CatalogoFechaHasta where Parametro = 'CatalogoFechaHasta'";
                sysnet.DB.Parameters.Add("@CatalogoFechaHasta", fechaHasta);
                sysnet.DB.Execute(sql);
                sysnet.DB.CommitTransaction();
                return "Actualizacion OK";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error actualizar la Vigencia");
                do
                {
                    mensaje = string.Format("{0}\n{1}", mensaje, ex.Message);
                    ex = ex.InnerException;
                }
                while (ex != null);
                return mensaje;
            }
        }

        private string ObtenerVigencia(DateTime fecha)
        {
            string auxDia = string.Empty;
            string auxMes = string.Empty;

            if(fecha.Day.ToString().Length == 1)
                auxDia = "0" + fecha.Day.ToString();
            else
                auxDia = fecha.Day.ToString();

            if(fecha.Month.ToString().Length == 1)
                auxMes = "0" + fecha.Month.ToString();
            else
                auxMes = fecha.Month.ToString();

            return auxDia + "/" + auxMes + "/" + fecha.Year.ToString();
        }

        public void EditarVigenciaTodos(string fechaDesde, string fechaHasta)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Update Premio set Vigencia=@Vigencia, VigenciaFin=@VigenciaFin";
                sysnet.DB.Parameters.Add("@Vigencia", DateTime.ParseExact(fechaDesde, "dd/MM/yyyy", null));
                sysnet.DB.Parameters.Add("@VigenciaFin", DateTime.ParseExact(fechaHasta, "dd/MM/yyyy", null));

                sysnet.DB.Execute(sql);
                sysnet.DB.CommitTransaction();
            }
            catch //(Exception ex)
            {
                sysnet.DB.Rollback();
            }
        }

        public string ObtenerParametro(string Parametro)
        {
            SysNet sysnet = SI.GetSysNet();

            string Valor = String.Empty;

            string sql = "select Parametro, Valor from Parametros where Parametro = @Parametro";
            sysnet.DB.Parameters.Add("@Parametro", Parametro);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                Valor = reader[1].ToString();
            }
            reader.Close();

            return Valor;
        }

        public void ActualizarCategoria(int CodCategoria)
        {
            SysNet sysnet = SI.GetSysNet();
			
			string sql;
			try
			{
                CategoriaPremio Cp = new CategoriaPremio();
                Cp = ObtenerCategoria(CodCategoria);
                if (Cp != null)
                {
                    if (Cp.Habilitado)
                        Cp.Habilitado = false;
                    else
                        Cp.Habilitado = true;

                    sysnet.DB.BeginTransaction();

                    sql = "update CategoriaPremio set Habilitado = @Habilitado where CodCategoriaPremio = @CodCategoriaPremio";
                    sysnet.DB.Parameters.Add("@CodCategoriaPremio", Cp.CodCategoriaPremio);
                    sysnet.DB.Parameters.Add("@Habilitado", Cp.Habilitado);

                    sysnet.DB.Execute(sql);

                    sysnet.DB.CommitTransaction();
				}
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al habilitar la Categoria");
				throw new ApplicationException(mensaje, ex);
			}
        }

        public void EliminarCategoria(int CodCategoria)
        {
            SysNet sysnet = SI.GetSysNet();

            string sql;
            try
            {
                CategoriaPremio Cp = new CategoriaPremio();
                Cp = ObtenerCategoria(CodCategoria);
                if (Cp != null)
                {
                    sysnet.DB.BeginTransaction();

                    sql = "Delete CategoriaPremio where CodCategoriaPremio = @CodCategoriaPremio";
                    sysnet.DB.Parameters.Add("@CodCategoriaPremio", Cp.CodCategoriaPremio);
                    
                    sysnet.DB.Execute(sql);

                    sysnet.DB.CommitTransaction();
                } 
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar la Categoria");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public void EliminarPremio(string CodPremio)
        {
            SysNet sysnet = SI.GetSysNet();

            string sql;
            try
            {
                Premio P = new Premio();
                P = this.ObtenerPremio(CodPremio.ToString());
                if (P != null)
                {
                    sysnet.DB.BeginTransaction();

                    sql = "Delete Premio where CodPremio = @CodPremio";
                    sysnet.DB.Parameters.Add("@CodPremio", P.CodPremio);
                    
                    sysnet.DB.Execute(sql);

                    sysnet.DB.CommitTransaction();
                } 
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar la Categoria");
                throw new ApplicationException(mensaje, ex);
            }
        }


		public IList<Premio> ObtenerPremios(int codCategoria)
		{
			SysNet sysnet = SI.GetSysNet();

			IList<Premio> premios = new List<Premio>();
			
			string sql = "select CodPremio, CodProducto, Stock, Puntos, Vigencia, DescripcionCorta, DescripcionLarga, CodPaisOrigenPremio, Garantia, Peso, CodCategoria, Habilitado, FechaUltimaModificacion, Destacado, VigenciaFin, Nuevo, Agotado, SoloPorHoy, Descuento10, Descuento20, Promocion, CodProductoPuntosPesos, PuntosPesos from Premio where 1=1 order by CodProducto";

			if (codCategoria != -1)
			{
				sql = sql.Replace("1=1", "CodCategoria = @CodCategoria");
				sysnet.DB.Parameters.Add("@CodCategoria", codCategoria);
			}

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			
			return ObtenerPremios(reader);
		}

		public IList<Premio> ObtenerPremios(string descPremio)
		{
			SysNet sysnet = SI.GetSysNet();

			IList<Premio> premios = new List<Premio>();

			string sql = "select CodPremio, CodProducto, Stock, Puntos, Vigencia, DescripcionCorta, DescripcionLarga, CodPaisOrigenPremio, Garantia, Peso, CodCategoria, Habilitado, FechaUltimaModificacion, Destacado, VigenciaFin, Nuevo, Agotado, SoloPorHoy, Descuento10, Descuento20, Promocion, CodProductoPuntosPesos, PuntosPesos from Premio where DescripcionCorta like @DescPremio or DescripcionLarga like @DescPremio order by CodProducto";

			sysnet.DB.Parameters.Add("@DescPremio", descPremio);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			
			return ObtenerPremios(reader);
		}

		protected IList<Premio> ObtenerPremios(SqlDataReader reader)
		{
			IList<Premio> premios = new List<Premio>();
			Premio premio = null;

			while(reader.Read())
			{
				premio = new Premio();
                premio.CodPremio = int.Parse(reader["CodPremio"].ToString());
                premio.CodProducto = reader["CodProducto"].ToString();
                premio.Stock = int.Parse(reader["Stock"].ToString());
                premio.Puntos = int.Parse(reader["Puntos"].ToString());
                premio.Vigencia = DateTime.Parse(reader["Vigencia"].ToString());
                premio.DescripcionCorta = reader["DescripcionCorta"].ToString();
                premio.DescripcionLarga = reader["DescripcionLarga"].ToString();
                premio.Pais = ObtenerPais(int.Parse(reader["CodPaisOrigenPremio"].ToString()));
                premio.Garantia = reader["Garantia"].ToString();
                premio.Peso = float.Parse(reader["Peso"].ToString());
                premio.Categoria = ObtenerCategoria(int.Parse(reader["CodCategoria"].ToString()));
                premio.Habilitado = bool.Parse(reader["Habilitado"].ToString());
                premio.Destacado = bool.Parse(reader["Destacado"].ToString());
                premio.Nuevo = bool.Parse(reader["Nuevo"].ToString());
				premio.Agotado = bool.Parse(reader["Agotado"].ToString());
				premio.SoloPorHoy = bool.Parse(reader["SoloPorHoy"].ToString());
				premio.Descuento10 = bool.Parse(reader["Descuento10"].ToString());
				premio.Descuento20 = bool.Parse(reader["Descuento20"].ToString());
				premio.Promocion = bool.Parse(reader["Promocion"].ToString());
				premio.CodProductoPuntosPesos = reader["CodProductoPuntosPesos"].ToString();
				premio.PuntosPesos = reader["PuntosPesos"].ToString();
                premio.FechaUltimaModificacion = DateTime.Parse(reader["FechaUltimaModificacion"].ToString());
                if (reader["VigenciaFin"].ToString() != "")
                    premio.VigenciaFin = DateTime.Parse(reader["VigenciaFin"].ToString());
				premios.Add(premio);
			}
			reader.Close();

			return premios;
		}

	}
}
