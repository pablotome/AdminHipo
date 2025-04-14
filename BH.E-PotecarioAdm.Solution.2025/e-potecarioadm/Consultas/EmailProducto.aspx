<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailProducto.aspx.cs" Inherits="BH.EPotecario.Adm.Consultas.EmailProducto" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<%@ Register TagPrefix="uc1" TagName="Fecha" Src="~/UserControls/Fecha.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Emails por Productos</title>
    <link href="../Default.css" type="text/css" rel="stylesheet" />
    <link href="../Menu1.css" type="text/css" rel="stylesheet" />
    <script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.maskedinput-1.2.2.js" type="text/javascript"></script>
	<script src="../scripts/Js/Valida.js" type="text/javascript"></script>
	<script type="text/javascript">
	$(document).ready(function() {
				$("#txtFechaDesde").mask("99/99/9999");
				$('#<%=txtFechaDesde.ClientID%>').css('width', '90px');
			});
			$.validator.addMethod('selectNone',          
				function(value, element) {              
					return this.optional(element) ||                
						(value.indexOf("-1") == -1);          
				}, "<br/>  * Tenés que seleccionar una Opción");
	        $.validator.addMethod('ValidarFechaIngresada',          
				function(value, element) {              
					return ValidaFecha(value, element);          
				}, "  * Fecha Inválida");
	            $(function(){            
					$("#form1").validate({
						rules: {
							    '<%=txtFechaDesde.UniqueID%>': {
								    required: true,
								    ValidarFechaIngresada: true, 
								    maxlength: 10
							    },
							    '<%=ddlProductos.ClientID %>': 
					            {
					                selectNone: true
					            } 
						    },
						messages: {
								'<%=txtFechaDesde.UniqueID%>': {
									required: " * Tenés que ingresar una Fecha", 
									maxlength: "  * La cantidad máxima de caracteres a ingresar es 10"
								}
						}             
					});
				});			
</script>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:MenuTab ID="MenuTab1" runat="server"></uc1:MenuTab>
		<h1>Estadísticas de Productos y Temas Consultados</h1>
	<table border="0" cellpadding="0" cellspacing="0">
	    <tr>
	        <td>
	            Fecha Desde:
	        </td>
	        <td style="padding-left:10px">
	            <asp:TextBox ID="txtFechaDesde" runat="server"></asp:TextBox>
	        </td>
	        <td style="padding-left:10px">
	            Producto:
	        </td>
	        <td style="padding-left:10px">
	            <asp:DropDownList ID="ddlProductos" runat="server" AutoPostBack="false"></asp:DropDownList>
	        </td>
	        <td style="padding-left:10px">
	            <asp:Button ID="btnBuscar" Text="Obtener Mails" runat="server" OnClick="btnBuscar_Click"/>
	        </td>
	    </tr>
	</table>
    </form>
</body>
</html>
