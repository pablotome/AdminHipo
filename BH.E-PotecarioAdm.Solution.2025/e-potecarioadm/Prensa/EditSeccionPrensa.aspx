<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSeccionPrensa.aspx.cs" Inherits="BH.EPotecario.Adm.Prensa.EditSeccionPrensa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administracion de Archivos</title>   
    
    <script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../scripts/JQuery/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <script src="../scripts/JQuery/ArchivosInversores.js" type="text/javascript"></script>
    <script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
   <style type="text/css" >
       .seccionControles td
       {
           height: 15px;
       }
   </style>
   		<LINK href="../Default.css" type="text/css" rel="stylesheet">
		<LINK href="../Menu1.css" type="text/css" rel="stylesheet">
</head>
<body>   
 <div id="controles" class="seccionControles">
    <form id="form1" runat="server">
     <uc1:menutab id="MenuTab" runat="server"></uc1:menutab>
     <h1>Adm. Secciones</h1>
    <table style="height: 114px; width: 458px; margin-bottom: 0px;" >
         <tr>
            <td >Descripcion : </td>
            <td >
                <asp:TextBox ID="txtSeccion" runat="server" Width="331px" MaxLength="50"></asp:TextBox>
                 <asp:RequiredFieldValidator id="RequiredFieldValidator3"
                    ControlToValidate="txtSeccion"
                    Display="Static"
                    ErrorMessage="*"
                    runat="server" SetFocusOnError="True"/>
             </td>
        </tr>
         <tr>
            <td>Vigente </td>
            <td>
                <asp:CheckBox runat="server" id="chkVigente"/>
             </td>
        </tr>       
         <tr>
            <td >Url :</td>
            <td >
                <asp:TextBox ID="txtUrl" runat="server" Width="331px" MaxLength="100"></asp:TextBox>
                
             </td>
        </tr>
            <tr>
            <td></td>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
         <tr>
            <td></td>
            <td>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="120px" 
                    onclick="btnAceptar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="120px" 
                    onclick="btnCancelar_Click" />
             </td>
        </tr>
    </table>
    </form>
     </div>
</body>
</html>
