<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmLicitacionesLEBACS.aspx.cs"
    Inherits="BH.EPotecario.Adm.Inversores.AdmResultadoUltimaLicitaciónLEBACS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Banco Hipotecario - Simulador de Lebacs</title>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/3.3.4/jquery.inputmask.bundle.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous" />
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
        crossorigin="anonymous"></script>
    <link href="../scripts/datepicker/datepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/datepicker/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../scripts/Js/InversoresLebacs.js?v=20180413"></script>
    <link rel="stylesheet" href="../Content/Css/InversoresLebacs.css?v=20180413" />
    <style type="text/css">
        @media (max-width: 780px)
        {
            .container
            {
                margin-right: 50px;
                margin-left: 50px;
            }
        }
        #Grid
        {
            margin-top: -50px;
            margin-bottom: 20px;
            margin-right: -50px;
            margin-left: -50px;
        }
        .espacio
        {
            height: 60px;
        }
    </style>
    <link href="../Default.css" type="text/css" rel="stylesheet">
    <link href="../Menu1.css" type="text/css" rel="stylesheet">
</head>
<body>
    <div id="Grid">
        <form id="admLicitaciones" method="post" runat="server" class="form-group">
        <uc1:MenuTab ID="MenuTab2" runat="server"></uc1:MenuTab>
        <h1>
            Parametría calculador Lebacs</h1>
        <div class="div-admin">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <ul class="nav nav-tabs" role="tablist">
                                <li id="navCalendario" runat="server" role="presentation" class="active"><a href="#calendario"
                                    aria-controls="calendario" role="tab" data-toggle="tab"><i class="fa fa-home"></i>
                                    <span>Calendario de Licitaciones</span></a></li>
                                <li id="navResultado" runat="server" role="presentation"><a href="#resultado" aria-controls="resultado"
                                    role="tab" data-toggle="tab"><i class="fa fa-user"></i><span>Resultado de la Última
                                        Licitación</span></a></li>
                            </ul>
                            <div class="tab-content">
                                <!--Calendario de licitaciones-->
                                <div role="tabpanel" class="tab-pane active" id="calendario" runat="server">
                                    <div class="row form-group col-xs-12">
                                        <tr>
                                            <td colspan="2" class="celdaCuadroInfo_Titulo">
                                                <b>LEBACs - Calendario de licitaciones</b>
                                            </td>
                                        </tr>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6 col-xs-12" style="margin-bottom: 20px">
                                            <asp:GridView ID="dgvCalendario" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-striped table-hover table-condensed small-top-margin celdaCuadroInfo_Dato pl10 tablaCuadroInfo">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdCalendarioLicitacionesLEBACS" ItemStyle-CssClass="oculto"
                                                        HeaderStyle-CssClass="oculto" HeaderText="Id Calendario Licitaciones LEBACS" />
                                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
                                                        <ItemStyle HorizontalAlign="center" Width="47%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="HorarioLicitacionHB" HeaderText="Horario de licitación por HB">
                                                        <ItemStyle HorizontalAlign="center" Width="53%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEliminar" OnClick="btnBorrarCalendario_Click" runat="server" Text="Eliminar"
                                                                Enabled="true" OnClientClick="return isDelete();" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="fecha">
                                                    Fecha:
                                                </label>
                                                <div id="datepicker" class="input-group date" data-date-format="dd-mm-yyyy">
                                                    <input runat="server" class="form-control" type="text" id="fecha" placeholder="Fecha..." />
                                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="horario">
                                                    Horario de Licitación:
                                                </label>
                                                <input runat="server" type="text" class="form-control" id="horario" placeholder="Horario de licitación..." />
                                            </div>
                                            <asp:Button ID="btnNuevoCalendario" runat="server" Text="Agregar" OnClick="btnNuevoCalendario_Click"
                                                Width="150px" Height="25px" BackColor="#003C5B" ForeColor="White" />
                                        </div>
                                    </div>
                                    <tr>
                                        <td class="labelErrores" colspan="2">
                                            <asp:Label runat="server" ID="lblErroresCal" ForeColor="Red" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </div>
                                <!--Resultado de la última licitación-->
                                <div role="tabpanel" class="tab-pane" id="resultado" runat="server">
                                    <div class="row form-group col-xs-12">
                                        <tr>
                                            <td style="width: 33%" colspan="2" class="celdaCuadroInfo_Titulo">
                                                <b>Resultado de la última licitación</b>
                                            </td>
                                        </tr>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-sm-6 col-xs-12" style="margin-bottom: 20px">
                                            <asp:GridView ID="dgvResultado" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-striped table-hover table-condensed small-top-margin celdaCuadroInfo_Dato pl10 tablaCuadroInfo">
                                                <Columns>
                                                    <asp:BoundField DataField="IdResultadoUltimaLicitacionLEBACS" ItemStyle-CssClass="oculto"
                                                        HeaderStyle-CssClass="oculto" HeaderText="Id Resultado Ultima Licitacion LEBACS" />
                                                    <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                                                        <ItemStyle HorizontalAlign="center" Width="30%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PrecioCorte" DataFormatString="{0:N6}" HeaderText="Precio De Corte">
                                                        <ItemStyle HorizontalAlign="center" Width="30%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TasaCorte" DataFormatString="{0:0#.0000} %" HeaderText="Tasa de corte - TNA">
                                                        <ItemStyle HorizontalAlign="center" Width="40%" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="EsUltimaLicitacion" ReadOnly="True" HeaderText="Es Última Licitación">
                                                        <ItemStyle HorizontalAlign="center" Width="40%" />
                                                    </asp:CheckBoxField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEliminar" OnClick="btnBorrarResultado_Click" runat="server" Text="Eliminar"
                                                                Enabled="true" OnClientClick="return isDelete();" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="form-group col-sm-6 col-xs-12">
                                            <div class="form-group">
                                                <label for="plazo">
                                                    Plazo:</label>
                                                <input runat="server" type="text" class="form-control" id="plazo" placeholder="Plazo..." />
                                            </div>
                                            <div class="form-group">
                                                <label for="tasaDeCorte">
                                                    Tasa de corte - TNA:</label>
                                                <input runat="server" type="text" class="form-control" id="tasaDeCorte" placeholder="Tasa de corte..." />
                                            </div>
                                            <div class="form-check">
                                                <input runat="server" type="checkbox" class="form-check-input" value="" id="EsUltimaLicitacion" />
                                                <label class="form-check-label" for="EsUltimaLicitacion">
                                                    Es Última Licitación</label>
                                            </div>
                                            <asp:Button ID="btnNuevoResultado" runat="server" Text="Agregar" OnClick="btnNuevoResultado_Click"
                                                Width="150px" Height="25px" BackColor="#003C5B" ForeColor="White" />
                                            <div class="espacio">
                                            </div>
                                            <div class="form-group">
                                                <label for="fechaUltimaLicitacion">
                                                    Fecha Última Licitación:</label>
                                                <div id="datepickerUltimaLicitacion" class="input-group date" data-date-format="dd-mm-yyyy">
                                                    <input runat="server" class="form-control" type="text" id="fechaUltimaLicitacion"
                                                        placeholder="Fecha Ultima Licitación..." />
                                                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                </div>
                                            </div>
                                            <asp:Button ID="btnFechaUltimaLicitacion" runat="server" Text="Cambiar" OnClick="btnFechaUltimaLicitacion_Click"
                                                Width="150px" Height="25px" BackColor="#003C5B" ForeColor="White" />
                                        </div>
                                    </div>
                                    <tr>
                                        <td class="labelErrores" colspan="2">
                                            <asp:Label runat="server" ID="lblErroresRes" ForeColor="Red" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
