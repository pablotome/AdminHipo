using System;

namespace BH.EPotecario.Adm.Componentes
{
	/// <summary>
	/// Estos datos se corresponden con la tabla ProveedorBeneficioWeb
	/// </summary>
	public enum ProveedorBeneficios
	{
		SOLME = 1, 
		BH = 2
	}
	
	public enum CamposSolme
	{
		CODBENEFICIO = 0, 
		CODNOMBRECOMERCIO = 1, 
		ACCION = 2, 
		NOMBRECOMERCIO = 3, 
		CODRUBROCOMERCIO = 4, 
		RUBROCOMERCIO = 5, 
		CODSUBRUBROCOMERCIO = 6, 
		SUBRUBROCOMERCIO = 7, 
		DIRECCION = 8, 
		CODPROVINCIA = 9, 
		PROVINCIA = 10, 
		CODCIUDAD = 11, 
		CIUDAD = 12, 
		NROTELEFONO = 13, 
		EMAIL = 14, 
		WEB = 15, 
		COMERCIOVISA = 16, 
		COMERCIOMAESTRO = 17, 
		CUIT = 18, 
		DESCUENTO = 19, 
		DESCUENTOVISA = 20, 
		DESCUENTOMAESTRO = 21, 
		DESCUENTOBHVISA = 22, 
		DESCUENTOBHMAESTRO = 23, 
		CUOTASBANCO = 24, 
		BENEFICIODIAS = 25, 
		BENEFICIO2X1 = 26, 
		REGALO = 27, 
		DESCUENTOESPECIAL = 28, 
		OBSERVACIONES = 29, 
		CODIGOVISA = 30, 
		VISA = 31, 
		CODIGOMAESTRO = 32, 
		MAESTRO = 33, 
		VIGENCIADESDE = 34, 
		VIGENCIAHASTA = 35
	}

    public enum CamposXlsImportar
    { 
        Accion = 0,
        CodBeneficio = 1,
        Comercio = 2,
        Rubro = 3,
        SubRubro = 4,
        TDBH = 5,
        TDProcrear = 6,
        TDTodas = 7,
        TCBHDark = 8,
        TCBHGold = 9,
        TCBHInternacional = 10,
        TCBHNacional = 11,
        TCARDark = 12,
        TCARGold = 13,
        TCARInternacional = 14,
        TCTechoGold = 15,
        TCTechoInternacional = 16,
        TCRacingGold = 17,
        TCRacingInternacional = 18,
        TCHMLInternacional = 19,
        TCHMLNacional = 20,
        TCTodas = 21,
        Dias = 22,
        DescuentoDebito = 23,
        DescuentoCredito = 24,
        Tope = 25,
        RangoCuotas = 26,
        VigenciaDesde = 27,
        VigenciaHasta = 28,
        CUIT_CUIL = 29,
        CodSucursal = 30,
        Domicilio = 31,
        Ciudad = 32,
        Provincia = 33,
        Telefono = 34,
        Web = 35
    }

    public enum Tarjetas
    { 
        TC_BHDark = 7,
        TC_BHGold = 8,
        TC_BHInternacional = 9,
        TC_BHNacional = 10,
        TC_ARDark = 11,
        TC_ARGold = 12,
        TC_ARInternacional = 13,
        TC_TechoGold = 14,
        TC_TechoInternacional = 15,
        TC_RacingGold = 16,
        TC_RacingInternacional = 17,
        TC_HMLInternacional = 18,
        TC_HMLNacional = 19,
        TC_Todas = 20,
        TD_BH = 21,
        TD_Procrear = 22,
        TD_Todas = 23
    }
}
