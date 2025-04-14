<%@ Page Language="c#" CodeBehind="AdmArchivoPrensa.aspx.cs" AutoEventWireup="True" Inherits="BH.EPotecario.Prensa.AdmArchivoPrensa" %>

<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>Administración de Archivo</title>
	<link href="../Default.css" type="text/css" rel="stylesheet">
	<link href="../Menu1.css" type="text/css" rel="stylesheet">
	<script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="Prensa.js"></script>
</head>
<body>
	<div id="Grid" style="margin-bottom: 20px">
		<form id="admPromociones" method="post" runat="server">
			<uc1:MenuTab ID="MenuTab1" runat="server"></uc1:MenuTab>
			<h1>Adm. Archivos</h1>
			<div id="controlesFiltro" style="margin-bottom: 20px">
				<table>
					<tr>
						<td style="width: 20%;">Seccion:
							<asp:DropDownList ID="ddlSeccion" runat="server" Width="200px" Height="32px" AutoPostBack="true" OnSelectedIndexChanged="ddSeccion_SelectedIndexChanged" />
						</td>
						<td style="width: 20%;">Mes / Año:
							<asp:DropDownList ID="ddlMesAnio" runat="server" Width="200px" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="ddlMesAnio_SelectedIndexChanged" />
						</td>
						<td style="width: 20%;">
							<asp:CheckBox runat="server" ID="chkSoloVigentes" Text="Mostrar solo vigentes" AutoPostBack="true" OnCheckedChanged="chkSoloVigentes_CheckedChanged" />
						</td>
						<td style="width: 60%; text-align: center;">&nbsp;
						</td>
					</tr>
				</table>
			</div>
			<asp:DataGrid runat="server" ID="dgArchivosSeccion"
				AutoGenerateColumns="False" OnItemDataBound="dgArchivosSeccion_ItemDataBound"
				BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
				CellPadding="4" ForeColor="Black" GridLines="Vertical"
				OnPageIndexChanged="PaginarBusqueda" AllowPaging="True" Width="100%" PageSize="25">
				<FooterStyle BackColor="#CCCC99" />
				<SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
				<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right"
					Mode="NumericPages" Position="Top" />
				<AlternatingItemStyle BackColor="White" />
				<ItemStyle BackColor="#F7F7DE" />
				<Columns>
					<asp:BoundColumn DataField="CodArchivo" HeaderText="Codigo" Visible="false"></asp:BoundColumn>

					<asp:BoundColumn DataField="Titulo" HeaderText="Titulo">
						<ItemStyle HorizontalAlign="left" Width="35%" />
					</asp:BoundColumn>

					<asp:TemplateColumn HeaderText="Fecha">
						<ItemStyle HorizontalAlign="left" Width="5%" />
						<ItemTemplate>
							<asp:Literal runat="server" ID="litFecha"></asp:Literal>
						</ItemTemplate>

					</asp:TemplateColumn>

					<asp:TemplateColumn HeaderText="Link">
						<ItemStyle HorizontalAlign="left" Width="5%" />
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="hplURL" Target="_blank"></asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:BoundColumn DataField="Orden" HeaderText="Orden">
						<ItemStyle HorizontalAlign="left" Width="5%" />
					</asp:BoundColumn>
					
					<asp:TemplateColumn HeaderText="Vigente">
						<ItemStyle HorizontalAlign="left" Width="5%" />
						<ItemTemplate>
							<asp:Literal runat="server" ID="litVigente"></asp:Literal>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Destacado">
						<ItemStyle HorizontalAlign="left" Width="5%" />
						<ItemTemplate>
							<asp:Literal runat="server" ID="litDestacado"></asp:Literal>
						</ItemTemplate>
					</asp:TemplateColumn>

					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="left" Width="5%" />
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="lnkEditar" Text="Editar"></asp:HyperLink>
							<asp:HyperLink runat="server" ID="lnkEliminar" Text="Eliminar"></asp:HyperLink>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
			</asp:DataGrid>
			<asp:HiddenField runat="server" ID="hdnCodArchivoEliminar" />
			<asp:Button runat="server" ID="btnEliminar" OnClick="btnEliminar_Click" style="display:none;" />
			<br />
			<br />

			<table width="100%">
				<tr>
					<td style="width: 50%;">
						<asp:Literal runat="server" ID="litSinArchivos" />
					</td>
					<td style="width: 50%; text-align: right;">
						<asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" Text="Nuevo" Width="150px" Height="25px" BackColor="#003C5B" ForeColor="White" />
					</td>
				</tr>
			</table>
		</form>
	</div>
</body>
</html>
