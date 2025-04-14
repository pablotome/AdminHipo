<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Fecha" Src="../UserControls/Fecha.ascx" %>
<%@ Page language="c#" Codebehind="admLugaresDePagoVisa.aspx.cs" AutoEventWireup="True" Inherits="BH.EPotecario.Adm.LugaresDePagoVisa.admLugaresDePagoVisa" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>admLugaresDePagoVisa</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Default.css" type="text/css" rel="stylesheet">
		<LINK href="../Menu1.css" type="text/css" rel="stylesheet">
		<LINK href="AdmLugaresDePagoVisa.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Util.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<form id="admLugaresDePagoVisa" method="post" runat="server">
			<uc1:menutab id="MenuTab1" runat="server"></uc1:menutab>
			<h1>Adm. LugaresDePagoVisa</h1>
			<asp:Label id="lblError" runat="server" CssClass="LabelError"></asp:Label>
			<asp:Panel id="pnlListaLugaresDePagoVisa" runat="server">
				<asp:Button id="btnAgregar" runat="server" Text="Agregar" onclick="btnAgregar_Click"></asp:Button>
				<BR>
				<BR>
				<asp:DataGrid id="dgLugaresDePagoVisa" runat="server" AllowSorting="True" AllowPaging="True" PageSize="15"
					AutoGenerateColumns="False">
					<Columns>
						<asp:BoundColumn Visible="False" DataField="CodLugarPagoVisa"></asp:BoundColumn>
						<asp:BoundColumn DataField="Denominacion" SortExpression="OrdenDenominacion" HeaderText="Denominaci&#243;n"></asp:BoundColumn>
						<asp:BoundColumn DataField="Direccion" HeaderText="Direcci&#243;n"></asp:BoundColumn>
						<asp:BoundColumn DataField="DesEntidadPagoVisa" SortExpression="OrdenEntidadDePago" HeaderText="Entidad de Pago"></asp:BoundColumn>
						<asp:BoundColumn DataField="DesLocalidad" SortExpression="OrdenLocalidad" HeaderText="Localidad"></asp:BoundColumn>
						<asp:BoundColumn DataField="NomProvincia" SortExpression="OrdenProvincia" HeaderText="Provincia"></asp:BoundColumn>
						<asp:TemplateColumn>
							<ItemTemplate>
								<asp:LinkButton id="lnkModificar" runat="server" CommandName="Modificar">Modificar</asp:LinkButton>&nbsp;
								<asp:LinkButton id="lnkEliminar" runat="server" CommandName="Eliminar">Eliminar</asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle Position="TopAndBottom" Mode="NumericPages"></PagerStyle>
				</asp:DataGrid>
			</asp:Panel>
			<asp:panel id="pnlAdmLugaresDePagoVisa" runat="server" Visible="False">
				<TABLE id="TablaDatosSucursal" cellSpacing="0">
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" CssClass="Labels">Provincia:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:DropDownList id="cboProvincia" runat="server" CssClass="Combos" AutoPostBack="True" onselectedindexchanged="cboProvincia_SelectedIndexChanged"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label1" runat="server" CssClass="Labels">Localidad:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:DropDownList id="cboLocalidad" runat="server" CssClass="Combos" AutoPostBack="True" onselectedindexchanged="cboLocalidad_SelectedIndexChanged"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label3" runat="server" CssClass="Labels">Entidad Pago:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:DropDownList id="cboEntidadPago" runat="server" CssClass="Combos"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD id="tdLegajo">
							<asp:Label id="lblCodSucursal" runat="server" CssClass="Labels">Denominación:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:TextBox id="txtDenominacion" runat="server" CssClass="TextBoxes"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblUsuarioNT" runat="server" CssClass="Labels">Dirección:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:TextBox id="txtDireccion" runat="server" CssClass="TextBoxes"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<P>&nbsp;</P>
				<P>
					<asp:button id="btnAceptar" runat="server" Text="Aceptar" onclick="btnAceptar_Click"></asp:button>&nbsp;&nbsp;
					<asp:button id="btnCancelar" runat="server" Text="Cancelar" onclick="btnCancelar_Click"></asp:button></P>
			</asp:panel>
			<asp:TextBox id="txtCodLugarPagoVisa" runat="server" Visible="False"></asp:TextBox>
		</form>
	</body>
</HTML>
