mensajesArchivo = 'Debe seleccionar un archivo.';
$(document).ready(function() {
    $('#txtRuta').attr("readonly", "readonly");
    $('#txtCopete,#txtTitulo,#txtLink').bind("cut copy paste", function(e) {
        e.preventDefault();
    });
    $('#txtCopete,#txtTitulo,#txtLink').blur(function() {
        var regNombre = new RegExp("^[a-zA-ZñÑáÁéÉíÍóÓúÚ ]*$", "gi");
        this.value = this.value.replace(/%/g, "");
    });
});

function validamosArchivos(e) {
    if ($('#file').val() == '') {
        $('#lbErrores').text(mensajesArchivo);
        e.preventDefault();
        return false;
    }

}
function validoExtensionArchivo(extension) {

    try {

        var archivo = extension;
        switch (extension) {
            case 'pdf':
            case 'PDF':
                return true;
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
function validoTipoArchivo(extension) {

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

function removeFileUpload(div) {
    document.getElementById("FileUploadContainer").removeChild(div.parentNode);
    //descontamos el archivo de imagen
    //creamos nuevamente el div con el archivo
    var div = document.createElement('DIV');

    div.innerHTML = '<input id="file" name = "file" type="file" onchange="ValidoTipoArchivo(this)" multiple />' +
                       '<input id="Button" type="button" value="Remove"  onclick = "RemoveFileUpload(this)" />';

    document.getElementById("FileUploadContainer").appendChild(div);
}

function mostrarAlert(msg) {

    alert(msg);

}


function limpiarControles() {

    $('#txtTitulo').val('');
    $('#txtNombre').val('');
    $('#txtCopete').val('');

    var ruta = $('#txtRuta').val();

    //obtengo el nombre del archivo y lo muestro en los control txt
    var ok = ruta.lastIndexOf('\\') + 1;

    var q = ruta.substring(ruta.lastIndexOf('\\') + 1);
    //si es la primera ves no limpio el control 
    if ($.trim(q)) {
        var nombreArchivo = ruta.replace(/^.*[\\\/]/, '')

        var hasta = ruta.lastIndexOf(nombreArchivo);
        var nRuta = ruta.substring(0, hasta);
        $('#txtRuta').val(nRuta);
    }
}
