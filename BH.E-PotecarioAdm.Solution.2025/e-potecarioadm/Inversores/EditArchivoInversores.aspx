<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditArchivoInversores.aspx.cs" Inherits="BH.EPotecario.Adm.Inversores.EditArchivoInversores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administracion de Archivos</title>   

    <script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../scripts/JQuery/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <script src="../scripts/JQuery/ArchivosInversores.js" type="text/javascript"></script>
    <script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
    <link href="../Default.css" type="text/css" rel="stylesheet"/>
	<link href="../Menu1.css" type="text/css" rel="stylesheet"/>
	<link href="../Content/Css/Inversores.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" method = "post">
    <uc1:menutab id="MenuTab1" runat="server"></uc1:menutab>
    <h1> <asp:Label ID="lblTitulo" runat="server" Text="Adm. Archivos"/></h1>
        <div Id"controles" class="controles">
    <table style="height: 317px; width: 458px" >
        <tr>
            <td>Año</td>
            <td></td>
            <td>
                <asp:DropDownList ID="ddAño" runat="server" Height="22px" Width="128px">
                </asp:DropDownList>
            </td>
             <td ></td>
        </tr>
        <tr>
            <td>Archivo</td>
            <td></td>
            <td>
            <br />
             <div id ="FileUploadContainer">
            <div id="FileUploadItem">
                <input id="file" name = "file" type="file" onchange="validoTipoArchivo(this)" multiple runat="server"/>
                <input id="Button" type="button" value="Remove"  onclick = "removeFileUpload(this)" runat="server"/>
            </div>
            </div>
            <br />                  
            </td>
             <td ></td>
        </tr>
         <tr>
            <td>Seccion</td>
            <td></td>
            <td>
                <asp:TextBox ID="txtSeccion" runat="server" Width="200px" ReadOnly="True" MaxLength="50"></asp:TextBox>
             </td>
              <td ></td>
        </tr>
         <tr>
            <td>Idioma</td>
            <td></td>
            <td>
                <asp:TextBox ID="txtIdioma" runat="server" Width="200px" ReadOnly="True" MaxLength="50"></asp:TextBox>
             </td>
              <td ></td>
        </tr>
         <tr>
            <td>Titulo</td>
            <td></td>
            <td>
                <asp:TextBox ID="txtTitulo" runat="server" Width="399px" MaxLength="100"></asp:TextBox>
             </td>
              <td ></td>
        </tr>
         <tr>
            <td>Copete</td>
            <td></td>
            <td>
                <asp:TextBox ID="txtCopete" runat="server" Width="399px" Height="88px" 
                    TextMode="MultiLine" MaxLength="250" ></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ID="valInput"
                    ControlToValidate="txtCopete"
                    ValidationExpression="^[\s\S]{0,100}$"
                    ErrorMessage="Please enter a maximum of 100 characters"
                    Display="Dynamic">*</asp:RegularExpressionValidator>
             </td>
              <td ></td>
        </tr>
         <tr style="display:none">
            <td>Ruta</td>
            <td></td>
            <td>
                <asp:TextBox ID="txtRuta" runat="server" Width="400px" MaxLength="50"></asp:TextBox>
             </td>
              <td ></td>
        </tr>
        <tr>
        <td>&nbsp;</td>
        <td></td>
        <td><asp:Label id="lblErrores" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        </td>
        <td></td>
        </tr>
         <tr>
            <td></td>
            <td></td>
            <td>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="120px" 
                    onclick="btnAceptar_Click" />&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="120px" 
                    onclick="btnCancelar_Click" />
             </td>
            <td ></td>
        </tr>
    </table>
    </div>
        <div id="divArchivos" class="datagrid"></div> 
    </form>
</body>
</html>
