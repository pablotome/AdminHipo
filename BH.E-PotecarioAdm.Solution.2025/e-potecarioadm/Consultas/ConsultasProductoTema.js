
function HabilitarTemaProducto(codProductoTema, hiperLink, lithabilitado, imageEspera)
{
	//Habilita vía ajax un ProductoTema
	//Actualiza el valor de la columna "Habilitado" a SI ó NO
	//Actualiza el text del link a "Deshabilitar"
	 $("#"+ imageEspera+"").css('display', '')
	 var evento ='HabilitaTemaProducto'
       $.ajax({
		type: "POST",
		url: "ConsultaH.ashx",
		data: {
			Accion      : evento,
			codProdTem	: codProductoTema
		},  
		dataType: "json", 		    
	    success: function(datos)
		{
		   alert("Se actualizo correctamente el Tema - Producto.");
		   if($("#" + hiperLink+ "").text()=='Habilitar')
		        {
                    $("#" + hiperLink+ "").text('Deshabilitar');
                    $("#" + lithabilitado+ "").text('SI');                    
                }
            else
                {
                    $("#" + hiperLink+ "").text('Habilitar');
                    $("#" + lithabilitado+ "").text('NO');
                }
           $("#"+ imageEspera+"").css('display', 'none');
		}
		, 
		error:function(XMLHttpRequest, textStatus, errorThrown){
			MostrarError(XMLHttpRequest);
		}
		});
}

function EliminarTemaProducto(codProductoTema, hiperLink, trId, imageEspera)
{
	//Habilita vía ajax un ProductoTema
	//Actualiza el valor de la columna "Habilitado" a SI ó NO
	//Actualiza el text del link a "Deshabilitar"
	$("#"+ imageEspera+"").css('display', '');
	 var evento ='EliminaTemaProducto'
       $.ajax({
		type: "POST",
		url: "ConsultaH.ashx",
		data: {
			Accion      : evento,
			codProdTem	: codProductoTema
		},  
		dataType: "json", 		    
	    success: function(datos)
		{
		    alert("Se elimino correctamente el Tema - Producto.");
		   //alert("trId" + codProductoTema);
		   $("#trId" + codProductoTema+ "").css('display', 'none')
		   $("#"+ imageEspera+"").css('display', 'none');
		}
		, 
		error:function(XMLHttpRequest, textStatus, errorThrown){
			MostrarError(XMLHttpRequest);
			 $("#"+ imageEspera+"").css('display', 'none');
		}
		});
}

function MostrarError(XMLHttpRequest)
{
    alert("Ocurrio un error al actualizar la relacion Producto-Tema: " + XMLHttpRequest.responseText)
}