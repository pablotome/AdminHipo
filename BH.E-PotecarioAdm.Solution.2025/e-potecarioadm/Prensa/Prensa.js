var counter = 0;
var counterSize = 0;

function validaFechaDDMMAAAA(fecha) {
	var dtCh = "/";
	var minYear = 1900;
	var maxYear = 2100;

	function isInteger(s) {
		var i;
		for (i = 0; i < s.length; i++) {
			var c = s.charAt(i);
			if (((c < "0") || (c > "9"))) return false;
		}
		return true;
	}

	function stripCharsInBag(s, bag) {
		var i;
		var returnString = "";
		for (i = 0; i < s.length; i++) {
			var c = s.charAt(i);
			if (bag.indexOf(c) == -1) returnString += c;
		}
		return returnString;
	}

	function daysInFebruary(year) {
		return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
	}

	function DaysArray(n) {
		for (var i = 1; i <= n; i++) {
			this[i] = 31
			if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
			if (i == 2) { this[i] = 29 }
		}
		return this
	}

	function isDate(dtStr) {
		var daysInMonth = DaysArray(12)
		var pos1 = dtStr.indexOf(dtCh)
		var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
		var strDay = dtStr.substring(0, pos1)
		var strMonth = dtStr.substring(pos1 + 1, pos2)
		var strYear = dtStr.substring(pos2 + 1)
		strYr = strYear
		if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
		if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
		for (var i = 1; i <= 3; i++) {
			if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
		}
		month = parseInt(strMonth)
		day = parseInt(strDay)
		year = parseInt(strYr)
		if (pos1 == -1 || pos2 == -1) {
			return false
		}
		if (strMonth.length < 1 || month < 1 || month > 12) {
			return false
		}
		if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
			return false
		}
		if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
			return false
		}
		if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
			return false
		}
		return true
	}
	if (isDate(fecha)) {
		return true;
	} else {
		return false;
	}
}

function ValidoExtensionArchivo(extension) {

	try {
		switch (extension) {
			case 'pdf':
			case 'PDF':
				var tipoArchivo = $("#" + 'hiddenTipoArchivo').val();
				if ((tipoArchivo != 'PDF') && (tipoArchivo != 'Url')) {
					return false;
				} else { return true }

			case 'jpg':
			case 'JPG':
			case 'jpeg':
			case 'JPGE':
			case 'PNG':
			case 'png':
				var tipoArchivo = $("#" + 'hiddenTipoArchivo').val();
				if (tipoArchivo != 'Imagen') {

					return false;
				} else {

					return true;
				}
				break;
			default:

				return false;
				break;
		}
	}
	catch (e) {
		alert("Error is :" + e);
	}

}

function ValidoTipoArchivo(extension) {

	var result = $('#file')[0].files;

	var cantidadArchivos = result.length;

	for (var i = 0; i < result.length; i++) {

		var nombreArchivo = result[i].name

		var data = nombreArchivo.split('.');

		if (data.length > 2) {
			var extensionNombre = data.pop();
			var hasta = nombreArchivo.lastIndexOf('.');
			if (!ValidoExtensionArchivo(extensionNombre)) {
				alert("El Archivo " + nombreArchivo + " no es valido !!");
				$("#FileUploadContainer").find("div").remove();
				//descontamos el archivo de imagen
				//creamos nuevamente el div con el archivo
				var div = document.createElement('DIV');

				div.innerHTML = '<input id="file" name = "file" type="file" onchange="ValidoTipoArchivo(this)" multiple />' +
                    '<input id="Button" type="button" value="Remove"  onclick = "RemoveFileUpload(this)" />';

				document.getElementById("FileUploadContainer").appendChild(div);
			}
		} else {
			var extensionNombre = data[1];

			if (!ValidoExtensionArchivo(extensionNombre)) {
				alert("El Archivo " + nombreArchivo + " no es valido !!");

				$("#FileUploadContainer").find("div").remove();

				//descontamos el archivo de imagen
				//creamos nuevamente el div con el archivo
				var div = document.createElement('DIV');

				div.innerHTML = '<input id="file" name = "file" type="file" onchange="ValidoTipoArchivo(this)" multiple />' +
                    '<input id="Button" type="button" value="Remove"  onclick = "RemoveFileUpload(this)" />';

				document.getElementById("FileUploadContainer").appendChild(div);
			}
		}
	}
}

function SolamenteNumeros(e) {
	var unicode = e.charCode ? e.charCode : e.keyCode
	if (unicode != 8 && unicode != 44) {
		if (unicode < 48 || unicode > 57) //if not a number
		{ return false } //disable key press    
	}
}

function AddFileUpload() {

	var div = document.createElement('DIV');

	div.innerHTML = '<input id="file' + counter + '" name = "file' + counter +
                     '" type="file" multiple />' +
                     '<input id="Button' + counter + '" type="button" ' +
                     'value="Remove" onclick = "RemoveFileUpload(this)" />';

	document.getElementById("FileUploadContainer").appendChild(div);

	var filename = "#file" + counter;

	counter++;

	//calculamos la cantidad de kb cargados por archivo		
	$(filename).change(function () {

		var name = $(filename).val();

		//agregamos evento al control creado 
		counterSize = parseInt(counterSize) + parseInt(ObtenerTamañoArchivo(filename, name));

		if (counterSize > 10000) {
			alert("No puede superar los 10Mb en archivos")
			document.getElementById("FileUploadContainer").removeChild(div);
		}

	});
}

function RemoveFileUpload(div) {

	/*document.getElementById("FileUploadContainer").removeChild(div.parentNode);
	//descontamos el archivo de imagen
	//creamos nuevamente el div con el archivo
	var div = document.createElement('DIV');

	div.innerHTML = '<input id="file" name = "file" type="file" onchange="ValidoTipoArchivo(this)" multiple />' +
                     '<input id="Button" type="button" value="Remove"  onclick = "RemoveFileUpload(this)" />';

	document.getElementById("FileUploadContainer").appendChild(div);*/

	$("#fileSeleccionarArchivo").val('');
}

$(document).ready(function () {

	$("#txtFecha").datepicker({ dateFormat: "dd/mm/yy" });

	var MaxLengthMultiline = 250;
	$('#txtCopete').keypress(function (e) {
		if ($(this).val().length >= MaxLengthMultiline) {
			e.preventDefault();
		}
	});

	$('#txtCopete,#txtFecha,#txtUrl,#txtOrden').bind("cut copy paste", function (e) {
		e.preventDefault();
	});

	$('#txtCopete,#txtFecha,#txtUrl').blur(function () {
		var regNombre = new RegExp("^[a-zA-ZñÑáÁéÉíÍóÓúÚ ]*$", "gi");
		this.value = this.value.replace(/%/g, "");
	});

	$('#txtFecha').change(function () {

		var fecha = $('#txtFecha').val();
		if (!validaFechaDDMMAAAA(fecha)) {
			$('#txtFecha').val('');
		}
	});

	$('#txtOrden').keypress(function () {
		return SolamenteNumeros(event);
	});

});

function GestionarDestacado()
{
	if ($('#chkDestacado').attr('checked'))
	{
		$("#txtOrden").val("0");
		$("#txtOrden").attr("disabled", "disabled");
	}
	else {
		$("#txtOrden").val("");
		$("#txtOrden").removeAttr("disabled");
	}
}

function EliminarArchivo(codArchivo, nombre)
{
	if (confirm('¿Confirma la eliminación del archivo \"' + nombre + '\"?'))
	{
		$('#hdnCodArchivoEliminar').val(codArchivo);
		$('#btnEliminar').click();
	}
}