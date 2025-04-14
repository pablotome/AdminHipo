<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consultas.aspx.cs" Inherits="BH.EPotecario.Adm.Consultas.Consultas" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../Default.css" type="text/css" rel="stylesheet" />
    <link href="../Menu1.css" type="text/css" rel="stylesheet" />
    <script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
	<script src="ConsultasProductoTema.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
	<script type="text/javascript" language="javascript">
	    $.validator.addMethod('selectNone',          
				function(value, element) {              
					return this.optional(element) ||                
						(value.indexOf("-1") == -1);          
				}, "<br/>  * Tenés que seleccionar una Opción");
	    $(function(){            
			$("#frmConsultaMail").validate({
				rules: {
					'<%=ddlProductos.ClientID %>': 
					{
					    selectNone: true
					},
					'<%=ddlTemas.ClientID %>': {
						selectNone: true
					}   
				},
				messages: {
				}             
			});
		});
	</script>
    <title>Administración de relaciones Productos - Temas</title>
</head>
<body>
    <form id="frmConsultaMail" method="post" enctype="multipart/form-data" runat="server">
    <uc1:MenuTab ID="MenuTab1" runat="server"></uc1:MenuTab>
		<h1>Administración de relaciones Productos - Temas</h1>
	<asp:Button ID="btnNuevo" runat="server" CssClass="cancel" Text="Nueva relación Producto - Tema" OnClick="btnNuevo_Click" />
	<br />
	<br />
	<asp:Label ID="lblNoItems" CssClass="error" runat="server" Text=""></asp:Label>
	<br />
	<br />
    <asp:Panel ID="pnlDatos" runat="server" Visible="false">
        <table>
        <tr>
            <td>
                Producto:
            </td>
            <td>
                <asp:DropDownList ID="ddlProductos" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Tema:
            </td>
            <td>
                <asp:DropDownList ID="ddlTemas" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Habilitado:
            </td>
            <td>
                <asp:CheckBox ID="chkHabilitado" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAceptar" Text="Aceptar" runat="server" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnCancelar" Text="Cancelar" CssClass="cancel" runat="server" OnClick="btnCancelar_Click" />
            </td>
        </tr>
       </table>
       <br />
       <asp:Label ID="lblError" CssClass="error" runat="server"></asp:Label>
    </asp:Panel>
	<br />
	<asp:Panel ID="pnlDatosCargados" runat="server">
	<asp:DataGrid runat="server" ID="dgProductoTema" AutoGenerateColumns="false" OnItemDataBound="dgProductoTema_ItemDataBound">
				<HeaderStyle HorizontalAlign="Center" Font-Bold="True" Width="100%" />
				<ItemStyle Height="35px" />
				<Columns>
					<asp:TemplateColumn HeaderText="Producto">
						<ItemStyle HorizontalAlign="Center" />
						<ItemTemplate>
							<asp:Label runat="server" ID="litProducto" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Tema">
						<ItemStyle HorizontalAlign="Center" />
						<ItemTemplate>
							<asp:Label runat="server" ID="litTema" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Habilitado">
						<ItemStyle HorizontalAlign="Center" />
						<ItemTemplate>
							<asp:Label runat="server" ID="litHabilitado" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Acciones">
						<ItemStyle Wrap="False"/>
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="hplHabilitar">Habilitar</asp:HyperLink>
							&nbsp;<asp:HyperLink runat="server" ID="hplEliminar">Eliminar</asp:HyperLink>&nbsp;<asp:Image runat="server" ID="imgEspera" ImageUrl="~/EspacioDuenios/ajax-loader1.gif" BorderWidth="0" AlternateText="" />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>	
		</asp:Panel>
    </form>
</body>
</html>
