<%@ Page Language="c#" CodeBehind="TareasDeAdmin.aspx.cs" AutoEventWireup="True"
	Inherits="BH.EPotecario.Adm.WebFormTareasDeAdmin" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>Intranet - Proyecto Paraíso - Administrador de Novedades</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
	<meta name="CODE_LANGUAGE" content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<link href="../Default.css" type="text/css" rel="stylesheet">
	<script src="../Util.js" type="text/javascript"></script>

</head>
<body>
	<form id="TareasDeAdmin" method="post" runat="server">
	<h1>
		Tareas de Admin</h1>
	<br>
	<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
	<br>
	<asp:Panel ID="pnlBusqueda" runat="server">
		<table id="tablaTareasDeAdmin" cellspacing="0">
			<tr>
				<td>
					<asp:Label ID="Label1" runat="server">Tablas del Sistema</asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="cboTablas" runat="server" Width="200px">
					</asp:DropDownList>
				</td>
				<td>
					<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" OnClick="cmdBuscar_Click">
					</asp:Button>
				</td>
			</tr>
		</table>
		<br>
		<asp:DataGrid ID="dgResultado" runat="server">
			<Columns>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar">Eliminar</asp:LinkButton>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:LinkButton ID="lnkModificar" runat="server" CommandName="Modificar">Modif</asp:LinkButton>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
		<asp:Button ID="btnAgregar" runat="server" Text="Agregar" Visible="False" OnClick="btnAgregar_Click">
		</asp:Button>&nbsp;&nbsp;
		<asp:Button ID="btnExportarDatos" runat="server" Text="Exportar Datos" Visible="False"
			OnClick="btnExportarDatos_Click"></asp:Button><br>
		<asp:DataGrid ID="dgExportacion" runat="server">
		</asp:DataGrid>
	</asp:Panel>
	<asp:Panel ID="pnlModificar" runat="server" Visible="False">
		<table id="Table1" cellspacing="5" cellpadding="0" width="100%" border="0">
			<tr>
				<td>
					<asp:Table ID="tblGenerica" runat="server">
					</asp:Table>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click">
					</asp:Button>&nbsp;&nbsp;&nbsp;
					<asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" OnClick="btnGuardarCambios_Click">
					</asp:Button>
				</td>
			</tr>
		</table>
	</asp:Panel>
	<span style="color: white;" onclick="MostrarDivOculto();">Habilitar Query</span>
	<div id="divOculto" style="display: none">
		<strong>Tareas Comunes</strong>
		<br>
		<asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="10" Width="700px"></asp:TextBox><br>
		<br>
		<asp:Button ID="btnEjecutarQuery" runat="server" Text="Ejecutar" OnClick="btnEjecutarQuery_Click">
		</asp:Button>
	</div>
	</form>
</body>
</html>
