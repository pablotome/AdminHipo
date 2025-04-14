<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditHomeInversores.aspx.cs"
    Inherits="BH.EPotecario.Adm.Inversores.EditHomeInversores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administracion de Archivos</title>

    <script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../scripts/JQuery/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

    <script src="../scripts/JQuery/ArchivosInversores.js" type="text/javascript"></script>

    <script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>

    <style type="text/css">
        .seccionControles td
        {
            height: 15px;
        }
    </style>
    <link href="../Default.css" type="text/css" rel="stylesheet">
    <link href="../Menu1.css" type="text/css" rel="stylesheet">
    <script type="text/javascript">
        $(document).ready(function() {

            $('#txtCopete,#txtTitulo,#txtLink').bind("cut copy paste", function(e) {
                e.preventDefault();
            });
            $('#txtCopete,#txtTitulo,#txtLink').blur(function() {
                var regNombre = new RegExp("^[a-zA-ZñÑáÁéÉíÍóÓúÚ ]*$", "gi");
                this.value = this.value.replace(/%/g, "");
            });

        });
        function SolamenteNumeros(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode
            if (unicode != 8 && unicode != 44) {
                if (unicode < 48 || unicode > 57) //if not a number
                { return false } //disable key press    
            }
        }
    </script>
</head>
<body>
    <div id="controles" class="seccionControles">
        <form id="form1" runat="server">
        <uc1:MenuTab ID="MenuTab" runat="server"></uc1:MenuTab>
        <h1>
            Adm. Home</h1>
        <table style="height: 114px; width: 458px; margin-bottom: 0px;">
            <tr>
                <td>
                    Descripcion
                </td>
                <td>
                    <asp:DropDownList ID="ddSeccionHome" runat="server" Width="197px">
                        <asp:ListItem Value="Información Destacada" Selected="True">Informacion Destacada</asp:ListItem>
                        <asp:ListItem Value="Agenda">Agenda</asp:ListItem>
                        <asp:ListItem Value="Hechos Relevantes">Hechos Relevantes</asp:ListItem>
                        <asp:ListItem Value="Menu Derecho">Menu Derecho</asp:ListItem>
                        <asp:ListItem Value="Páginas Relacionadas">Paginas Relacionadas</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Orden
                
                </td>
                <td>
                    <asp:TextBox ID="txtOrden" runat="server" Width="100px" MaxLength="1" onkeypress="return SolamenteNumeros(event);"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vtxtOrden" runat="server" ControlToValidate="txtOrden"
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator id="RegularExpressionValidator1"
                   ControlToValidate="txtOrden"
                   ValidationExpression="\d+"
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage="El campo orden debe ser Numerico"
                   runat="server" SetFocusOnError="True"/>
                </td>             
            </tr>
            <tr>
                <td>
                    Idioma
                </td>
                <td>
                    <asp:DropDownList ID="ddIdioma" runat="server" Width="197px">
                        <asp:ListItem Selected="True" Value="1">Español</asp:ListItem>
                        <asp:ListItem Value="2">Ingles</asp:ListItem>                      
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Titulo
                </td>
                <td>
                    <asp:TextBox ID="txtTitulo" runat="server" Width="331px" MaxLength="50"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitulo"
                        ErrorMessage="*" SetFocusOnError="True" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Copete
                </td>
                <td>
                    <asp:TextBox ID="txtCopete" runat="server" Width="331px" MaxLength="250"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Link
                </td>
                <td>
                    <asp:TextBox ID="txtLink" runat="server" Width="331px" MaxLength="50"></asp:TextBox>
                     
                </td>
            </tr>
            <tr>
            <td></td>
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="120px" OnClick="btnAceptar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="120px" OnClick="btnCancelar_Click" />
                </td>
            </tr>
        </table>
        
        </form>
    </div>
</body>
</html>
