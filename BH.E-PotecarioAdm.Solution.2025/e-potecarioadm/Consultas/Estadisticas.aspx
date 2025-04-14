<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="BH.EPotecario.Adm.Consultas.Estadisticas" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<%@ Register TagPrefix="uc1" TagName="Fecha" Src="~/UserControls/Fecha.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Estadísticas de Productos y Temas Consultados</title>
    <link href="../Default.css" type="text/css" rel="stylesheet" />
    <link href="../Menu1.css" type="text/css" rel="stylesheet" />
    <script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.maskedinput-1.2.2.js" type="text/javascript"></script>
	<script src="../scripts/Js/Valida.js" type="text/javascript"></script>
	<script type="text/javascript">
	$(document).ready(function() {
				$("#txtFechaDesde").mask("99/99/9999");
				$("#txtFechaHasta").mask("99/99/9999");
				$('#<%=txtFechaDesde.ClientID%>').css('width', '90px');
				$('#<%=txtFechaHasta.ClientID%>').css('width', '90px');
			});
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
							'<%=txtFechaHasta.UniqueID%>': {
								required: true,
								ValidarFechaIngresada: true, 
								maxlength: 10
							} 
						},
						messages: {
								'<%=txtFechaDesde.UniqueID%>': {
									required: " * Tenés que ingresar una Fecha", 
									maxlength: "  * La cantidad máxima de caracteres a ingresar es 10"
								},
								'<%=txtFechaHasta.UniqueID%>': {
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
	            Fecha Hasta:
	        </td>
	        <td style="padding-left:10px">
	            <asp:TextBox ID="txtFechaHasta" runat="server"></asp:TextBox>
	        </td>
	        <td style="padding-left:10px">
	            <asp:Button ID="btnBuscar" Text="Buscar" runat="server" OnClick="btnBuscar_Click" />
	        </td>
	    </tr>
	</table>
	<br />
	<br />	
	<asp:DataGrid runat="server" ID="dgProductoTema" AutoGenerateColumns="false" >
				<HeaderStyle HorizontalAlign="Center" Font-Bold="True" Width="100%" />
				<ItemStyle Height="35px" />
				<Columns>
					<asp:BoundColumn DataField="Producto" HeaderText="Producto Consultado" Visible="True" />
					<asp:BoundColumn DataField="Tema" HeaderText="Tema Consultado" Visible="True" />
					<asp:BoundColumn DataField="Cantidad" HeaderText="Cantidad de Veces consultados" Visible="True" />
				</Columns>
			</asp:DataGrid>	
	<asp:Label ID="lblError" CssClass="error" runat="server"></asp:Label>
    </form>
</body>
</html>
