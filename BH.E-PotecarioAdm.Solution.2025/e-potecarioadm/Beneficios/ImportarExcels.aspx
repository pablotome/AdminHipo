<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeBehind="ImportarExcels.aspx.cs" Inherits="BH.EPotecario.Adm.ImportarExcels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        body {
            padding: 10px;
            background-color: #faf5f5;
        }

        .upload-container {
            border: 2px dashed #ccc;
            padding: 20px;
            text-align: center;
            margin-bottom: 20px;
            border-radius: 5px;
        }

            .upload-container:hover {
                border-color: #007bff;
            }

        .result-container {
            margin-top: 20px;
            padding: 15px;
            border-radius: 5px;
        }

        .success {
            background-color: #d4edda;
            border: 1px solid #c3e6cb;
        }

        .error {
            background-color: #f8d7da;
            border: 1px solid #f5c6cb;
        }

        .nav-tabs .nav-link {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container-fluid">

        <div class="row">
            <div class="col-md-8 offset-md-2">
                <div class="upload-container">
                    <h4>Seleccione un archivo Excel</h4>

                    <div class="mb-3">
                        <asp:DropDownList ID="DropDownTipoImportacion" runat="server" CssClass="form-select">
                            <asp:ListItem Value="Beneficios">Importar Beneficios</asp:ListItem>
                            <asp:ListItem Value="Alianzas">Importar Alianzas</asp:ListItem>
                            <asp:ListItem Value="Sucursales">Importar Sucursales</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <p class="text-muted">El archivo debe contener una sola hoja con los datos correspondientes</p>

                    <asp:FileUpload ID="FileUploadExcel" runat="server" CssClass="form-control mb-3" accept=".xlsx" />

                    <asp:Button ID="BtnImportar" runat="server" Text="Importar Datos"
                        CssClass="btn btn-primary" OnClick="BtnImportar_Click" />
                </div>

                <asp:Panel ID="PanelResultado" runat="server" Visible="false" CssClass="result-container">
                    <h4>
                        <asp:Literal ID="LitResultadoTitulo" runat="server"></asp:Literal></h4>
                    <asp:Literal ID="LitResultadoDetalle" runat="server"></asp:Literal>
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="mt-4">
                    <ul class="nav nav-tabs" id="dataTabs" role="tablist">
                        <li class="nav-item" role="presentation">
                            <button class="nav-link active" id="beneficios-tab" data-bs-toggle="tab"
                                data-bs-target="#beneficios" type="button" role="tab">
                                Beneficios</button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button class="nav-link" id="alianzas-tab" data-bs-toggle="tab"
                                data-bs-target="#alianzas" type="button" role="tab">
                                Alianzas</button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button class="nav-link" id="sucursales-tab" data-bs-toggle="tab"
                                data-bs-target="#sucursales" type="button" role="tab">
                                Sucursales</button>
                        </li>
                    </ul>

                    <div class="tab-content" id="dataTabsContent">
                        <div class="tab-pane fade show active" id="beneficios" role="tabpanel">
                            <h5 class="mt-3">Importaciones de Beneficios:
                            <asp:DropDownList runat="server" ID="lbImportacionesBeneficios" AutoPostBack="true" OnSelectedIndexChanged="lbImportacionesBeneficios_SelectedIndexChanged">
                            </asp:DropDownList>
                            </h5>

                            <asp:GridView ID="GridViewBeneficios" runat="server" CssClass="table table-striped table-bordered"
                                AutoGenerateColumns="false" EmptyDataText="No hay datos para mostrar">
                                <Columns>
                                    <asp:BoundField DataField="CodBeneficio" HeaderText="CodBeneficio" />
                                    <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                                    <asp:BoundField DataField="Clientes" HeaderText="Clientes" />
                                    <asp:BoundField DataField="Ahorros" HeaderText="Ahorros" />
                                    <asp:BoundField DataField="Cuotas" HeaderText="Cuotas" />
                                    <asp:BoundField DataField="MediosPago" HeaderText="MediosPago" />
                                    <asp:BoundField DataField="Dias" HeaderText="Dias" />
                                    <asp:BoundField DataField="CFT" HeaderText="CFT" />
                                    <asp:BoundField DataField="BasesYCondiciones" HeaderText="BasesYCondiciones" Visible="false" />
                                    <asp:TemplateField HeaderText="BasesYCondiciones">
                                        <ItemTemplate>
                                            <span tooltip='<%# Eval("BasesYCondiciones") %>'>
                                                <%# Eval("BasesYCondicionesGrilla") %>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TopeReintegro" HeaderText="TopeReintegro" />
                                    <asp:BoundField DataField="BreveResumen" HeaderText="BreveResumen" />
                                    <asp:BoundField DataField="TipoDescuento" HeaderText="TipoDescuento" />
                                    <asp:BoundField DataField="Prioridad" HeaderText="Prioridad" />
                                    <asp:BoundField DataField="ID_BEN" HeaderText="ID_BEN" />
                                    <asp:BoundField DataField="IDImportacion" HeaderText="IDImportacion" />
                                    <asp:BoundField DataField="RegistroValido" HeaderText="Valido" />
                                    <asp:BoundField DataField="DetalleError" HeaderText="Error" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="tab-pane fade" id="alianzas" role="tabpanel">
                            <h5 class="mt-3">Importaciones de Alianzas:
                            <asp:DropDownList runat="server" ID="lbImportacionesAlianzas" AutoPostBack="true" OnSelectedIndexChanged="lbImportacionesAlianzas_SelectedIndexChanged">
                            </asp:DropDownList>
                            </h5>
                            <asp:GridView ID="GridViewAlianzas" runat="server" CssClass="table table-striped table-bordered"
                                AutoGenerateColumns="false" EmptyDataText="No hay datos para mostrar">
                                <Columns>
                                    <asp:BoundField DataField="CodAlianza" HeaderText="CodAlianza" />
                                    <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                                    <asp:BoundField DataField="Beneficios" HeaderText="Beneficios" />
                                    <asp:BoundField DataField="Categorias" HeaderText="Categorias" />
                                    <asp:BoundField DataField="CategoriaPrincipal" HeaderText="CategoriaPrincipal" />
                                    <asp:BoundField DataField="TieneSucursales" HeaderText="TieneSucursales" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" />
                                    <asp:BoundField DataField="Latitud" HeaderText="Latitud" />
                                    <asp:BoundField DataField="Longitud" HeaderText="Longitud" />
                                    <asp:BoundField DataField="Highlight" HeaderText="Highlight" />
                                    <asp:BoundField DataField="Logo" HeaderText="Logo" />
                                    <asp:BoundField DataField="SitioWeb" HeaderText="SitioWeb" />
                                    <asp:BoundField DataField="Orden" HeaderText="Orden" />
                                    <asp:BoundField DataField="Activo" HeaderText="Activo" />
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="IdImportacion" HeaderText="IdImportacion" />
                                    <asp:BoundField DataField="RegistroValido" HeaderText="Valido" />
                                    <asp:BoundField DataField="DetalleError" HeaderText="Error" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="tab-pane fade" id="sucursales" role="tabpanel">
                            <h5 class="mt-3">Sucursales Disponibles</h5>
                            <asp:GridView ID="GridViewSucursales" runat="server" CssClass="table table-striped table-bordered"
                                AutoGenerateColumns="false" EmptyDataText="No hay datos para mostrar">
                                <Columns>
                                    <asp:BoundField DataField="Titulo" HeaderText="Titulo " />
                                    <asp:BoundField DataField="Calle" HeaderText="Calle" />
                                    <asp:BoundField DataField="Numero" HeaderText="Numero" />
                                    <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                                    <asp:BoundField DataField="CodigoPostal" HeaderText="CodigoPostal" />
                                    <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                                    <asp:BoundField DataField="Latitud" HeaderText="Latitud" />
                                    <asp:BoundField DataField="Longitud" HeaderText="Longitud" />
                                    <asp:BoundField DataField="InfoAdicional" HeaderText="InfoAdicional" />
                                    <asp:BoundField DataField="IDAlianzas" HeaderText="IDAlianzas" />
                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                    <asp:BoundField DataField="IDImportacion" HeaderText="IDImportacion" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>