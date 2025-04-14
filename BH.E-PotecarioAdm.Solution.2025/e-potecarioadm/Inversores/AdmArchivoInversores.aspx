<%@ Page language="c#" Codebehind="AdmArchivoInversores.aspx.cs" AutoEventWireup="True" Inherits="BH.EPotecario.Inversores.AdmArchivoInversores" %>
<%@ Register Assembly="BH.WebControls" Namespace="BH.WebControls" TagPrefix="BH" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Administración de Archivo</title>
       <%--<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<LINK href="../Default.css" type="text/css" rel="stylesheet">
		<LINK href="../Menu1.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
	<div id="Grid" style="margin-bottom:20px">
		<form id="admPromociones" method="post" runat="server">
			<uc1:menutab id="MenuTab1" runat="server"></uc1:menutab>
			<h1><asp:Label ID="lblTitulo" runat="server" Text="Adm. Archivos"/></h1>
			<div id="controlesFiltro" style="margin-bottom:20px">
			&nbsp; Idioma :&nbsp;
             <asp:DropDownList ID="ddIdioma" runat="server" Width="100px" Height="25px" 
                 onselectedindexchanged="ddIdioma_SelectedIndexChanged" AutoPostBack="true">
                 <asp:ListItem Value="1">Español</asp:ListItem>
                 <asp:ListItem Value="2">Ingles</asp:ListItem>
             </asp:DropDownList>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Seccion :&nbsp;&nbsp;
            <asp:DropDownList ID="ddAño" runat="server" Width="100px" Height="25px" 
                    AutoPostBack="true" onselectedindexchanged="ddAño_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;
			<asp:Button ID="btnNuevo" runat="server" onclick="btnNuevo_Click" 
                Text="Nuevo" Visible="False" Width="150px" Height="25px" 
                 BackColor="#003C5B" ForeColor="White"/>
			&nbsp;
			<asp:Button ID="btnVer" runat="server" onclick="btnVer_Click" 
                Text="Ver" Visible="True" Width="150px" Height="25px" 
                 BackColor="#003C5B" ForeColor="White"/>
			</div>  
			<div id="controlesText" style="margin-bottom:20px;display:none">
			<asp:Label ID="lblTxtPatch" runat="server"></asp:Label>
                &nbsp;&nbsp;
                <asp:TextBox ID="txtPatchArchivos" runat="server" Width="555px" Height="25px"></asp:TextBox>
			</div>                 
			<asp:DataGrid runat="server" ID="dgParametrosSeccion" 
                AutoGenerateColumns="False" OnItemDataBound="dgParametros_ItemDataBound" 
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                OnPageIndexChanged="PaginarBusqueda" AllowPaging="True" Width="100%" PageSize="20" >
			    <FooterStyle BackColor="#CCCC99" />
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" 
                 Mode="NumericPages"  Position="Top"  />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#F7F7DE" />
			<Columns>
				<asp:BoundColumn DataField="CodArchivo" HeaderText="Codigo" Visible="false">
				</asp:BoundColumn>
				<asp:BoundColumn DataField="Anio" HeaderText="Año">
                            <ItemStyle HorizontalAlign="left" Width="5%" />
				</asp:BoundColumn>
				<asp:BoundColumn DataField="Titulo" HeaderText="Titulo"> 
                            <ItemStyle HorizontalAlign="left" Width="25%" /></asp:BoundColumn>
				<asp:BoundColumn DataField="Copete" HeaderText="Copete">
				 <ItemStyle HorizontalAlign="left" Width="25%"/>
				</asp:BoundColumn>	
				
				<asp:BoundColumn DataField="NombreArchivo" HeaderText="Nombre">
                            <ItemStyle HorizontalAlign="left" Width="25%" /></asp:BoundColumn>		
				<asp:BoundColumn DataField="Tamanio" HeaderText="Tamaño">				            
                            <ItemStyle HorizontalAlign="left" Width="10%"/></asp:BoundColumn>
				<asp:BoundColumn DataField="Idioma" HeaderText="Idioma" Visible="false">
				</asp:BoundColumn>
				
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:HyperLink runat="server" ID="lnkEditar" Text="Editar"></asp:HyperLink>
						<asp:HyperLink runat="server" ID="lnkEliminar" Text="Eliminar"></asp:HyperLink>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
			    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
			</asp:DataGrid>

		</form>
      </div>
	</body>
</HTML>
