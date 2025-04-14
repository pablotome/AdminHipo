<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tema.aspx.cs" Inherits="BH.EPotecario.Adm.Consultas.Tema" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../Default.css" type="text/css" rel="stylesheet" />
    <link href="../Menu1.css" type="text/css" rel="stylesheet" />
    <script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
	<script type="text/javascript" language="javascript">
	    $(function(){            
					$("#frmConsultaMail").validate({
						rules: {
							'<%=txtDescripcion.UniqueID%>': {
								required: true, 
								maxlength: 50
							},
							'<%=txtOrden.UniqueID%>': {
							    number:true,
								required: true, 
								maxlength: 4
							}   
						},
						messages: {
								'<%=txtDescripcion.UniqueID%>': {
									required: "<br/> * Tenés que ingresar una Descripción.",
									maxlength: "<br/>  * La cantidad máxima de caracteres a ingresar es 50."
								},
								'<%=txtOrden.UniqueID%>': {
								    number: "<br/>  * Tenés que ingresar un Número entero.",
									required: "<br/> * Tenés que ingresar un Orden",
									maxlength: "<br/>  * La cantidad máxima de caracteres a ingresar es 4"
								}
						}             
					});
				});
	</script>
    <title>Administración de Temas</title>
</head>
<body>
    <form id="frmConsultaMail" method="post" enctype="multipart/form-data" runat="server">
    <uc1:MenuTab ID="MenuTab1" runat="server"></uc1:MenuTab>
		<h1>Administración de Temas</h1>
	<table cellpadding="0" cellspacing="0" width="100%">
	    <tr>
	        <td style="width:340px">
	            <asp:Button ID="btnNuevo" runat="server" CssClass="cancel" Text="Nuevo Tema" OnClick="btnNuevo_Click" />
	        </td>
	        <td>
	            <asp:DropDownList ID="ddlTemas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTemas_SelectedIndexChanged"></asp:DropDownList>
	        </td>
	    </tr>
	</table>
	<br />
	<asp:Panel ID="pnlDatos" runat="server">
    <table>
        <tr>
            <td>
                Descripción:
            </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" Width="428px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Orden:
            </td>
            <td>
                <asp:TextBox ID="txtOrden" runat="server" Width="46px"></asp:TextBox>
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
                <asp:Button ID="btnEliminar" Text="Eliminar" CssClass="cancel" runat="server" Visible="False" OnClick="btnEliminar_Click" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <br />
    <asp:Label ID="lblError" CssClass="error" runat="server"></asp:Label>
    </form>
</body>
</html>
