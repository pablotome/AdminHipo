<%@ Page Language="c#" CodeBehind="admSucursales.aspx.cs" AutoEventWireup="True" Inherits="BH.EPotecario.Adm.Sucursales.admSucursales" MasterPageFile="~/Master.master" %>

<asp:Content runat="server" ContentPlaceHolderID="cphHead">
    <link href="AdmSucursales.css?v=2" type="text/css" rel="stylesheet">
    <script language="javascript" src="../Util.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBody">

    <h1>Adm. Sucursales</h1>
    <asp:Label ID="lblError" runat="server" CssClass="LabelError"></asp:Label>
    <asp:Panel ID="pnlListaSucursales" runat="server">
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"></asp:Button>
        <br>
        <br>
        <asp:DataGrid ID="dgSucursales" runat="server" AllowSorting="True" AllowPaging="True" PageSize="15"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundColumn DataField="codSucursal" SortExpression="OrdenSucursal" HeaderText="Cod. Sucursal"></asp:BoundColumn>
                <asp:BoundColumn DataField="desSucursal" SortExpression="OrdenNombre" HeaderText="Nombre"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="codProvincia" HeaderText="codProvincia"></asp:BoundColumn>
                <asp:BoundColumn DataField="nomProvincia" SortExpression="OrdenProvincia" HeaderText="Provincia"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="codTipoSucursal" HeaderText="codTipoSucursal"></asp:BoundColumn>
                <asp:BoundColumn DataField="desTipoSucursal" SortExpression="OrdenTipoSucursal" HeaderText="Tipo de Sucursal"></asp:BoundColumn>
                <asp:BoundColumn DataField="Domicilio" HeaderText="Domicilio"></asp:BoundColumn>
                <asp:BoundColumn DataField="CodigoPostal" HeaderText="Codigo Postal"></asp:BoundColumn>

                <asp:BoundColumn DataField="HorarioAtencion" HeaderText="Horario Atenci&#243;n"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Audio No Videntes">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="litAudioNoVidentes" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Latitud" HeaderText="Latitud" ItemStyle-Wrap="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="Longitud" HeaderText="Longitud" ItemStyle-Wrap="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="EMailOficialEmpresa" HeaderText="Email"></asp:BoundColumn>
                <asp:BoundColumn DataField="EmailOficialNYP" HeaderText="Email Oficial NYP"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Vigente">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="Vigente" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkModificar" runat="server" CommandName="Modificar">Modificar</asp:LinkButton>&nbsp;
								<asp:LinkButton ID="lnkEliminar" runat="server" CommandName="Eliminar">Eliminar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Position="TopAndBottom" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
    </asp:Panel>
    <asp:Panel ID="pnlAdmSucursales" runat="server" Visible="False">
        <table id="TablaDatosSucursal" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" CssClass="Labels">Tipo de Sucursal:</asp:Label></td>
                <td></td>
                <td>
                    <asp:DropDownList ID="cboTipoSucursal" runat="server" CssClass="Combos"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" CssClass="Labels">Provincia:</asp:Label></td>
                <td></td>
                <td>
                    <asp:DropDownList ID="cboProvincia" runat="server" CssClass="Combos"></asp:DropDownList></td>
            </tr>
            <tr>
                <td id="tdLegajo">
                    <asp:Label ID="lblCodSucursal" runat="server" CssClass="Labels">Código Sucursal:</asp:Label></td>
                <td></td>
                <td>

                    <asp:TextBox ID="txtCodSucursal" runat="server" CssClass="TextBoxes"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUsuarioNT" runat="server" CssClass="Labels">Nombre:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="TextBoxes"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDomicilio" runat="server" CssClass="Labels">Domicilio:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtDomicilio" runat="server" CssClass="TextBoxes"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblCodigoPostal" runat="server" CssClass="Labels">CodigoPostal:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="TextBoxes"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblApellido" runat="server" CssClass="Labels">Horario de Atención:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtHorarioAtencion" runat="server" CssClass="TextBoxes"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" CssClass="Labels">Audio para No Videntes:</asp:Label></td>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkAudioNoVidentes" runat="server" CssClass="TextBoxes"></asp:CheckBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLatitud" runat="server" CssClass="Labels">Latitud:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtLatitud" runat="server" CssClass="TextBoxes"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLongitud" runat="server" CssClass="Labels">Longitud:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtLongitud" runat="server" CssClass="TextBoxes"></asp:TextBox></td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblEmailOfEmpresa" runat="server" CssClass="Labels">Email:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtEmailOfEmpresa" runat="server" CssClass="TextBoxes" TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regularExpressionValidatorEmail" runat="server"
                        ControlToValidate="txtEmailOfEmpresa"
                        ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*;\s*|\s*$))*">
                    </asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmailNyp" runat="server" CssClass="Labels">Email Oficial NYP:</asp:Label></td>
                <td></td>
                <td>
                    <asp:TextBox ID="txtEmailNyp" runat="server" CssClass="TextBoxes" TextMode="MultiLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regularExpressionValidator1" runat="server"
                        ControlToValidate="txtEmailNyp"
                        ValidationExpression="(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*;\s*|\s*$))*">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="LabelVigente" runat="server" CssClass="Labels">Vigente:</asp:Label></td>
                <td></td>
                <td>
                    <asp:CheckBox ID="checkBoxV" runat="server" CssClass="TextBoxes"></asp:CheckBox></td>
            </tr>

        </table>
        <p>&nbsp;</p>
        <p>
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"></asp:Button>&nbsp;&nbsp;
					<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"></asp:Button>
        </p>
    </asp:Panel>


</asp:Content>
