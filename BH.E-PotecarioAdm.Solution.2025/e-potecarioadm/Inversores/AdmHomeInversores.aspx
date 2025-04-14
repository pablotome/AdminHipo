<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmHomeInversores.aspx.cs" Inherits="BH.EPotecario.Adm.Inversores.AdmHomeInversores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
     
    
    </style>
    <script type="text/javascript" src="../scripts/JQuery/jquery-1.3.2.min.js"></script>
    	<LINK href="../Default.css" type="text/css" rel="stylesheet">
		<LINK href="../Menu1.css" type="text/css" rel="stylesheet">
</head>
<body>
<div id="Grid" style="margin-bottom:20px">
        <form id="form" runat="server">
        <uc1:menutab id="MenuTab" runat="server"></uc1:menutab>
         <h1>Adm. Home</h1>
         <div id="controlesFiltro" style="margin-bottom:20px">
			&nbsp; Idioma :&nbsp;
             <asp:DropDownList ID="ddIdioma" runat="server" Width="100px" Height="25px" 
                 onselectedindexchanged="ddIdioma_SelectedIndexChanged" AutoPostBack="true">
                 <asp:ListItem Value="1">Español</asp:ListItem>
                 <asp:ListItem Value="2">Ingles</asp:ListItem>
             </asp:DropDownList>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Seccion :&nbsp;&nbsp;
			<asp:DropDownList ID="ddSecciones" runat="server"
                 onselectedindexchanged="ddSecciones_SelectedIndexChanged" AutoPostBack="true" Height="25px" >
             </asp:DropDownList>
			 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="btnNuevaHome" runat="server" 
                Text="Nuevo" onclick="btnNuevaHome_Click" Width="150px" Height="25px" 
                 BackColor="#003C5B" ForeColor="White" />
			&nbsp;
			<asp:Button ID="btnVer" runat="server" onclick="btnVer_Click" 
                Text="Ver" Visible="True" Width="150px" Height="25px" 
                 BackColor="#003C5B" ForeColor="White"/>
           </div> 
           
			<asp:DataGrid runat="server" ID="dgrHome" 
                AutoGenerateColumns="False"
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                CellPadding="2" ForeColor="Black" GridLines="Vertical"  
                 OnPageIndexChanged="PaginarBusqueda" AllowPaging="True" 
                OnItemDataBound="dgrHome_ItemDataBound" Width="100%" PageSize="20" 
                 onselectedindexchanged="dgrHome_SelectedIndexChanged">
			    <FooterStyle BackColor="#CCCC99"/>
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" 
                 Mode="NumericPages"  Position="Top"  />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#F7F7DE" />
			<Columns>
				<asp:BoundColumn DataField="CodSeccionHome" HeaderText="CodHome" Visible="false" >
				</asp:BoundColumn>
				<asp:BoundColumn DataField="DescSeccion" HeaderText="Seccion" Visible="false">
			
				</asp:BoundColumn>
				<asp:BoundColumn DataField="Titulo" HeaderText="Titulo" >

				</asp:BoundColumn>
				<asp:BoundColumn DataField="Copete" HeaderText="Copete">

				</asp:BoundColumn>
				<asp:BoundColumn DataField="Idioma" HeaderText="Idioma" Visible="false" >

				</asp:BoundColumn>
				<asp:BoundColumn DataField="Orden"  HeaderText="Orden">

				</asp:BoundColumn>				
				<asp:BoundColumn DataField="Link" HeaderText="Link">

				</asp:BoundColumn>
				
				<asp:TemplateColumn>
					<ItemTemplate>
						<asp:HyperLink runat="server" ID="lnkEditar" Text="Editar" ></asp:HyperLink>
						<asp:HyperLink runat="server" ID="lnkEliminar" Text="Eliminar"></asp:HyperLink>
					</ItemTemplate>

<ItemStyle Width="50px"></ItemStyle>

				</asp:TemplateColumn>
			</Columns>
			    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
			</asp:DataGrid>
			
			</form>
</div>
    <p>
&nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
