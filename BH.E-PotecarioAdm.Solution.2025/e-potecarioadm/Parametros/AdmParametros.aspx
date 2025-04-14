<%@ Page language="c#" Codebehind="admParametros.aspx.cs" AutoEventWireup="True" Inherits="BH.EPotecario.Parametros.admParametros" ValidateRequest="false" %>
<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Administración de Parámetros</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Default.css" type="text/css" rel="stylesheet">
		<LINK href="../Menu1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="admPromociones" method="post" runat="server">
			<uc1:menutab id="MenuTab1" runat="server"></uc1:menutab>
			<h1>Adm. Parametros</h1>
			<br />
			<asp:DataGrid runat="server" ID="dgParametros" AutoGenerateColumns="false" OnItemDataBound="dgParametros_ItemDataBound">
			<Columns>
				<asp:BoundColumn DataField="CodParametro" HeaderText="Codigo"></asp:BoundColumn>
				<asp:BoundColumn DataField="DesParametro" HeaderText="Parametro"></asp:BoundColumn>
				<asp:BoundColumn DataField="Valor" HeaderText="Valor"></asp:BoundColumn>
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:LinkButton runat="server" ID="lnkEditar" Text="Editar"></asp:LinkButton>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
			</asp:DataGrid>
			<asp:Panel runat="server" ID="pnlModificaciones" Visible="false">
			<table>
				<tr>
					<td>Codigo</td>
					<td><asp:TextBox runat="server" ID="txtCodigo" ReadOnly="true" Width="200px"/></td>
				</tr>
				<tr>
					<td>Parametro</td>
					<td><asp:TextBox runat="server" ID="txtParametro" ReadOnly="true" Width="200px"/></td>
				</tr>
				<tr>
					<td>Valor</td>
					<td><asp:TextBox runat="server" ID="txtValor" TextMode="MultiLine" Columns="120" Rows="5"/></td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Button runat="server" ID="btnAceptar" Text="Aceptar" OnClick="btnAceptar_Click"/>
						<asp:Button runat="server" ID="btnCancelar" Text="Cancelar" OnClick="btnCancelar_Click"/>
					</td>
				</tr>
			</table>
			</asp:Panel>
		</form>
	</body>
</HTML>
