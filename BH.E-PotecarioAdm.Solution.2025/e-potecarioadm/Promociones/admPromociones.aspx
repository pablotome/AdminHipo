<%@ Page language="c#" Codebehind="admPromociones.aspx.cs" AutoEventWireup="True" Inherits="BH.EPotecario.Adm.Promociones.admPromociones" %>
<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<%@ Register TagPrefix="uc1" TagName="Fecha" Src="../UserControls/Fecha.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Administración de Promociones</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Default.css" type="text/css" rel="stylesheet">
		<LINK href="../Menu1.css" type="text/css" rel="stylesheet">
		<LINK href="admPromociones.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Util.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<form id="admPromociones" method="post" runat="server">
			<uc1:menutab id="MenuTab1" runat="server"></uc1:menutab>
			<h1>Adm. Promociones</h1>
			<asp:label id="lblError" runat="server" CssClass="LabelError"></asp:label><asp:panel id="pnlListaPromociones" runat="server">
				<asp:Button id="btnAgregar" runat="server" Text="Agregar" onclick="btnAgregar_Click"></asp:Button>
				<BR>
				<BR>
				<asp:DataGrid id="dgPromociones" runat="server" AllowSorting="False" AllowPaging="False" PageSize="15" AutoGenerateColumns="False" Width="100%">
					<Columns>
						<asp:TemplateColumn SortExpression="OrdenCodPromocion" HeaderText="Cod. Promocion">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.codPromocion") %>' ID="lblCodPromocion">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<input id="txtCodPromocion" type="text" runat="server" NAME="txtCodPromocion">
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="OrdenDesPromocion" HeaderText="Promocion">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.desPromocion") %>' ID="lblDesPromocion">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtPromocion" runat="server" CssClass="TextBoxes"></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="OrdenEmailFrom" HeaderText="Orig. E-Mails">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmailFrom") %>' ID="lblEmailFrom">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtEmailFrom" runat="server" CssClass="TextBoxes"></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="OrdenEmailsTO" HeaderText="Dest. E-Mails">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmailsTO") %>' ID="lblEmailsTO">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtEmailsTO" runat="server" CssClass="TextBoxes"></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="OrdenFechaInicio" HeaderText="Fecha Inicio">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fechaInicio", "{0:dd/MM/yyyy}") %>' ID="lblFechaInicio">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<BH:DATEPICKER id="dpFechaInicio" runat="server" ImageSource="../scripts/calendar/calendar.gif"></BH:DATEPICKER>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="OrdenFechaFin" HeaderText="Fecha Fin">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fechaFin", "{0:dd/MM/yyyy}") %>' ID="lblFechaFin">
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<BH:DATEPICKER id="dpFechaFin" runat="server" ImageSource="../scripts/calendar/calendar.gif"></BH:DATEPICKER>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn SortExpression="OrdenCantidad" HeaderText="Cant.Registrados">
							<ItemTemplate>
								<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cantidad") %>' ID="lblCantidad">
								</asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="ok" CancelText="Cancelar" EditText="Editar"></asp:EditCommandColumn>
						<asp:TemplateColumn>
							<ItemTemplate>
								<asp:LinkButton id="lnkEliminar" runat="server" CommandName="Eliminar">Eliminar</asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemTemplate>
								<asp:LinkButton id="lnkObtenerRegistrados" runat="server" CommandName="Registrados">Registrados</asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle Position="TopAndBottom" Mode="NumericPages"></PagerStyle>
				</asp:DataGrid>
			</asp:panel></form>
	</body>
</HTML>
