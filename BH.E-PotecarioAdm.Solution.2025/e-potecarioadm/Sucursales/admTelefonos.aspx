<%@ Page language="c#" Codebehind="admTelefonos.aspx.cs" AutoEventWireup="True" Inherits="BH.EPotecario.Adm.Sucursales.admTelefonos" %>
<%@ Register TagPrefix="uc1" TagName="Fecha" Src="../UserControls/Fecha.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>admTelefonos</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Default.css" type="text/css" rel="stylesheet">
		<LINK href="../Menu1.css" type="text/css" rel="stylesheet">
		<LINK href="AdmTelefonos.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../Util.js" type="text/javascript"></script>
	</HEAD>
	<body>
		<form id="admTelefonos" method="post" runat="server">
			<uc1:menutab id="MenuTab1" runat="server"></uc1:menutab>
			<h1>Adm. Teléfonos</h1>
			<asp:Label id="lblError" runat="server" CssClass="LabelError"></asp:Label>
			<asp:Panel id="pnlListaTelefonos" runat="server">
				<asp:Button id="btnAgregar" runat="server" Text="Agregar" onclick="btnAgregar_Click"></asp:Button>
				<BR>
				<BR>
				<asp:DataGrid id="dgTelefonos" runat="server" AllowSorting="True" AllowPaging="True" PageSize="15"
					AutoGenerateColumns="False">
					<Columns>
						<asp:BoundColumn Visible="False" DataField="codTelefono" HeaderText="codTelefono"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="Nombre" SortExpression="OrdenNombre" HeaderText="Nombre"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="codSucursal" SortExpression="OrdenProvincia" HeaderText="codSucursal"></asp:BoundColumn>
						<asp:BoundColumn DataField="desSucursal" SortExpression="OrdenSucursal" HeaderText="Sucursal"></asp:BoundColumn>
						<asp:BoundColumn DataField="Numero" HeaderText="N&#250;mero"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" DataField="codTipoTelefono" SortExpression="OrdenTipoTelefono" HeaderText="codTipoTelefono"></asp:BoundColumn>
						<asp:BoundColumn DataField="Tipo" SortExpression="OrdenTipoTelefono" HeaderText="Tipo de T&#233;lefono"></asp:BoundColumn>
						<asp:BoundColumn DataField="NombreContacto" HeaderText="Nombre Contacto"></asp:BoundColumn>
						<asp:BoundColumn DataField="EMailContacto" HeaderText="Email Contacto"></asp:BoundColumn>
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
			<asp:panel id="pnlAdmTelefonos" runat="server" Visible="False">
				<TABLE id="TablaDatosTelefono" cellSpacing="0">
					<TR>
						<TD>
							<asp:Label id="Label2" runat="server" CssClass="Labels">Tipo de Telefono:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:DropDownList id="cboTipoTelefono" runat="server" CssClass="Combos"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label1" runat="server" CssClass="Labels">Sucursal:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:DropDownList id="cboSucursal" runat="server" CssClass="Combos"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD id="tdLegajo">
							<asp:Label id="lblCodTelefono" runat="server" CssClass="Labels">Número de Telefono:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:TextBox id="txtNumeroTelefono" runat="server" CssClass="TextBoxes"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblUsuarioNT" runat="server" CssClass="Labels">Nombre Contacto:</asp:Label></TD>
						<TD></TD>
						<TD>
							<asp:TextBox id="txtNombreContacto" runat="server" CssClass="TextBoxes"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblDomicilio" runat="server" CssClass="Labels">Email Contacto:</asp:Label></TD>
						<TD>&nbsp;&nbsp;&nbsp;
						</TD>
						<TD>
							<asp:TextBox id="txtEmailContacto" runat="server" CssClass="TextBoxes"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<P>&nbsp;</P>
				<P>
					<asp:button id="btnAceptar" runat="server" Text="Aceptar" onclick="btnAceptar_Click"></asp:button>&nbsp;&nbsp;
					<asp:button id="btnCancelar" runat="server" Text="Cancelar" onclick="btnCancelar_Click"></asp:button></P>
			</asp:panel>
		</form>
	</body>
</HTML>
