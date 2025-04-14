<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmSeccionPrensa.aspx.cs" Inherits="BH.EPotecario.Adm.Prensa.AdmSeccionPrensa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<style type="text/css">
		.seccionGrid {
			position: absolute; /* podría ser relative */
			left: 100px;
			top: 50px;
			width: 80%;
			height: 80%;
			border-left: 1px solid #0070A8;
			border-bottom: 1px solid #0070A8;
			border-right: 1px solid #0070A8;
			border-top: 1px solid #0070A8;
			background-color: White;
			margin-left: 50px;
			margin-top: 25px;
			margin-bottom: 50px;
			margin-right: 100px;
		}
	</style>
	<script type="text/javascript" src="../scripts/JQuery/jquery-1.3.2.min.js"></script>
	<link href="../Default.css" type="text/css" rel="stylesheet" />
	<link href="../Menu1.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<div id="Grid">
		<form id="form" runat="server">
			<uc1:MenuTab ID="MenuTab2" runat="server"></uc1:MenuTab>
			<h1>Adm. Secciones Prensa</h1>
			<div id="controlesFiltro">
				<asp:Button ID="btnNuevaSeccion" runat="server"
					Text="Nuevo" OnClick="btnNuevaSeccion_Click" Width="150px" Height="25px"
					BackColor="#003C5B" ForeColor="White" Visible="false" />
			</div>
			<asp:DataGrid runat="server" ID="dgrSeccion"
				AutoGenerateColumns="False"
				BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
				CellPadding="4" ForeColor="Black" GridLines="Vertical"
				OnPageIndexChanged="PaginarBusqueda" AllowPaging="false"
				OnItemDataBound="dgrSeccion_ItemDataBound" Width="100%" PageSize="20">
				<FooterStyle BackColor="#CCCC99" />
				<SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
				<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right"
					Mode="NumericPages" Position="Top" />
				<AlternatingItemStyle BackColor="White" />
				<ItemStyle BackColor="#F7F7DE" />
				<Columns>
					<asp:BoundColumn DataField="CodSeccion" HeaderText="CodSeccion" Visible="false"></asp:BoundColumn>
					<asp:BoundColumn DataField="DesSeccion" HeaderText="Descripcion">
						<ItemStyle HorizontalAlign="left" Width="35%" />
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Url" HeaderText="Url">
						<ItemStyle HorizontalAlign="left" Width="35%" />
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Tipo Archivo">
						<ItemStyle HorizontalAlign="left" Width="25%" />
						<ItemTemplate>
							<asp:Literal runat="server" ID="litTipoArchivo"></asp:Literal>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="DesVigente" HeaderText="Vigente">
						<ItemStyle HorizontalAlign="left" Width="25%" />
					</asp:BoundColumn>

					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:HyperLink runat="server" ID="lnkEditar" Text="Editar"></asp:HyperLink>
							<%--<asp:HyperLink runat="server" ID="lnkEliminar" Text="Eliminar"></asp:HyperLink>--%>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
				<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
			</asp:DataGrid>
		</form>
	</div>
</body>
</html>
