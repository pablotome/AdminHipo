var data = "";
$(document).ready(function() {
	//Ciudad
	$("#txtDesCiudadBeneficioWeb").autocomplete("AutoComplete.ashx", {
		dataType: 'json',
		//delay: 40,
		autofill: true,
		selectFirst: false,
		highlight: false,
		matchContains:true,
		cacheLength:1, 
		//max:20, 
		parse: function(data)
		{
			var rows = new Array();
			for (var i = 0; i < data.length; i++)
				rows[i] = {data:data[i], CodCiudadBeneficioWeb:data[i].CodCiudadBeneficioWeb, DesCiudadBeneficioWeb:data[i].DesCiudadBeneficioWeb, value:data[i].DesCiudadBeneficioWeb};
			return rows;
		}, 
		formatItem: function(row) {
			return row.DesCiudadBeneficioWeb;
		}, 
		extraParams: {
			Tabla: 'Ciudad', 
			codProvincia: function() { return $("#hdnCodProvinciaBeneficioWeb").val(); }
		}
	});
	
	$("#txtDesCiudadBeneficioWeb").result(function(event, data, formatted) {
		if (data)
		{
			$("#txtDesCiudadBeneficioWeb").val(formatted);
			$("#hdnCodCiudadBeneficioWeb").val(data.CodCiudadBeneficioWeb);
			$("#txtDesProvinciaBeneficioWeb").val(data.DesProvinciaBeneficioWeb);
			$("#hdnCodProvinciaBeneficioWeb").val(data.CodProvinciaBeneficioWeb);
			$("#hdnCodProvinciaBeneficioWebCiudad").val(data.CodProvinciaBeneficioWeb);
		}
		else
		{
			$("#hdnCodCiudadBeneficioWeb").val("");
			$("#hdnCodProvinciaBeneficioWeb").val("");
			$("#hdnCodProvinciaBeneficioWebCiudad").val("");
		}
	});
	
	//Provincia
	$("#txtDesProvinciaBeneficioWeb").autocomplete("AutoComplete.ashx", {extraParams:{Tabla:'Provincia'}, 
		dataType: 'json',
		//delay: 40,
		autofill: true,
		selectFirst: false,
		highlight: false,
		matchContains:true,
		cacheLength:1, 
		parse:function(data)
		{
			var rows = new Array();
			for (var i = 0; i < data.length; i++)
				rows[i] = {data:data[i], CodProvinciaBeneficioWeb:data[i].CodProvinciaBeneficioWeb, DesProvinciaBeneficioWeb:data[i].DesProvinciaBeneficioWeb, value:data[i].DesProvinciaBeneficioWeb};
			return rows;
		}, 
		formatItem: function(row) {
			return row.DesProvinciaBeneficioWeb;
		}
	});
	
	$("#txtDesProvinciaBeneficioWeb").result(function(event, data, formatted) {
		if (data)
		{
			$("#txtDesProvinciaBeneficioWeb").val(formatted);
			$("#hdnCodProvinciaBeneficioWeb").val(data.CodProvinciaBeneficioWeb);
			if ($("#hdnCodProvinciaBeneficioWebCiudad").val() != $("#hdnCodProvinciaBeneficioWeb").val())
			{
				$("#txtDesCiudadBeneficioWeb").val("");
				$("#hdnCodCiudadBeneficioWeb").val("");
				$("#hdnCodProvinciaBeneficioWebCiudad").val("");
			}
		}
		else
		{
			$("#hdnCodProvinciaBeneficioWeb").val("");
			$("#hdnCodCiudadBeneficioWeb").val("");
			$("#hdnCodProvinciaBeneficioWebCiudad").val("");
		}
		$("#txtDesProvinciaBeneficioWeb").flushCache();
	});
	
	//Sub-Rubro
	$("#txtDesSubRubroBeneficioWeb").autocomplete("AutoComplete.ashx", {
		dataType: 'json',
		//delay: 40,
		autofill: true,
		selectFirst: false,
		highlight: false,
		matchContains:true,
		cacheLength:1, 
		//max:20, 
		parse: function(data)
		{
			var rows = new Array();
			for (var i = 0; i < data.length; i++)
				rows[i] = {data:data[i], CodSubRubroBeneficioWeb:data[i].CodSubRubroBeneficioWeb, DesSubRubroBeneficioWeb:data[i].DesSubRubroBeneficioWeb, value:data[i].DesSubRubroBeneficioWeb};
			return rows;
		}, 
		formatItem: function(row) {
			return row.DesSubRubroBeneficioWeb;
		}, 
		extraParams: {
			Tabla: 'SubRubro', 
			codRubro: function() { return $("#hdnCodRubroBeneficioWeb").val(); }
		}
	});
	
	$("#txtDesSubRubroBeneficioWeb").result(function(event, data, formatted) {
		if (data)
		{
			$("#txtDesSubRubroBeneficioWeb").val(formatted);
			$("#hdnCodSubRubroBeneficioWeb").val(data.CodSubRubroBeneficioWeb);
			$("#txtDesRubroBeneficioWeb").val(data.DesRubroBeneficioWeb);
			$("#hdnCodRubroBeneficioWeb").val(data.CodRubroBeneficioWeb);
			$("#hdnCodSubRubroBeneficioWebRubro").val(data.CodRubroBeneficioWeb);
		}
		else
		{
			$("#hdnCodSubRubroBeneficioWeb").val("");
			$("#hdnCodRubroBeneficioWeb").val("");
			$("#hdnCodSubRubroBeneficioWebRubro").val("");
		}
	});
	
	//Rubro
	$("#txtDesRubroBeneficioWeb").autocomplete("AutoComplete.ashx", {extraParams:{Tabla:'Rubro'}, 
		dataType: 'json',
		//delay: 40,
		autofill: true,
		selectFirst: false,
		highlight: false,
		matchContains:true,
		cacheLength:1, 
		parse:function(data)
		{
			var rows = new Array();
			for (var i = 0; i < data.length; i++)
				rows[i] = {data:data[i], CodRubroBeneficioWeb:data[i].CodRubroBeneficioWeb, DesRubroBeneficioWeb:data[i].DesRubroBeneficioWeb, value:data[i].DesRubroBeneficioWeb};
			return rows;
		}, 
		formatItem: function(row) {
			return row.DesRubroBeneficioWeb;
		}
	});
	
	$("#txtDesRubroBeneficioWeb").result(function(event, data, formatted) {
		if (data)
		{
			$("#txtDesRubroBeneficioWeb").val(formatted);
			$("#hdnCodRubroBeneficioWeb").val(data.CodRubroBeneficioWeb);
			if ($("#hdnCodSubRubroBeneficioWebRubro").val() != $("#hdnCodRubroBeneficioWeb").val())
			{
				$("#txtDesSubRubroBeneficioWeb").val("");
				$("#hdnCodSubRubroBeneficioWeb").val("");
				$("#hdnCodSubRubroBeneficioWebRubro").val("");
			}
		}
		else
		{
			$("#hdnCodRubroBeneficioWeb").val("");
			$("#hdnCodSubRubroBeneficioWeb").val("");
			$("#hdnCodSubRubroBeneficioWebRubro").val("");
		}
		$("#txtDesRubroBeneficioWeb").flushCache();
	});
	
	//Comercio
	$("#txtDesComercioBeneficioWeb").autocomplete("AutoComplete.ashx", {extraParams:{Tabla:'Comercio'}, 
		dataType: 'json',
		//delay: 40,
		autofill: true,
		selectFirst: false,
		highlight: false,
		matchContains:true,
		cacheLength:1, 
		parse:function(data)
		{
			var rows = new Array();
			for (var i = 0; i < data.length; i++)
				rows[i] = {data:data[i], CodComercioBeneficioWeb:data[i].CodComercioBeneficioWeb, DesComercioBeneficioWeb:data[i].DesComercioBeneficioWeb, CodProveedorBeneficioWeb:data[i].CodProveedorBeneficioWeb, CodComercioProveedor:data[i].CodComercioProveedor, value:data[i].DesComercioBeneficioWeb};
			return rows;
		}, 
		formatItem: function(row) {
			return row.DesComercioBeneficioWeb;
		}
	});
	
	$("#txtDesComercioBeneficioWeb").result(function(event, data, formatted) {
		if (data)
		{
			$("#txtDesComercioBeneficioWeb").val(formatted);
			/*CodProveedorBeneficioWeb
			hdnCodProveedorBeneficioWeb	CodProveedorBeneficioWeb
			hdnCodBeneficioProveedorWeb	CodComercioProveedor*/
			$("#hdnCodComercioBeneficioWeb").val(data.CodComercioBeneficioWeb);
		}
		else
		{
			$("#hdnCodComercioBeneficioWeb").val("");
		}
	});
	
}); 
