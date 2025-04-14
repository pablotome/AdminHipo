<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmLicitacionesLECAPS.aspx.cs" Inherits="BH.EPotecario.Adm.Inversores.AdmLicitacionesLECAPS" %>
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>Banco Hipotecario - Simulador de Lecaps</title>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery.inputmask/3.3.4/jquery.inputmask.bundle.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <link href="../scripts/datepicker/datepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/datepicker/bootstrap-datepicker.js"></script>
    <link href="../Default.css" type="text/css" rel="stylesheet" />
    <link href="../Menu1.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../scripts/Js/InversoresLecaps.js?v=20180413"></script>
    <link rel="stylesheet" href="../Content/Css/InversoresLecaps.css?v=20180413" />
</head>
<body>
    <div id="Grid">
        <form id="admLicitaciones" method="post" runat="server" class="form-group">
        <uc1:MenuTab ID="MenuTab2" runat="server"></uc1:MenuTab>
        <h1>Parametría calculador Lecaps</h1>
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
                                                    <asp:BoundField DataField="IdCalendarioLicitacionesLECAPS" ItemStyle-CssClass="oculto"
                                                        HeaderStyle-CssClass="oculto" HeaderText="Id Calendario Licitaciones LECAPS" />
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
                                            <div class="col-lg-6 co-sm-12">
                                                <div class="form-group">
                                                    <label for="fecha">
                                                        Fecha:
                                                    </label>
                                                    <div id="datepicker" class="input-group date" data-date-format="dd-mm-yyyy">
                                                        <input runat="server" class="form-control" type="text" id="fecha" placeholder="Fecha..." autocomplete="off" />
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 co-sm-12">
                                                <div class="form-group">
                                                    <label for="horario">
                                                        Horario de Licitación:
                                                    </label>
                                                    <input runat="server" type="text" class="form-control" id="horario" placeholder="Horario de licitación..." />
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-lg-offset-4">
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
                                        <div class="col-lg-12">
                                            <asp:GridView ID="dgvResultado" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                                CssClass="table table-striped table-hover table-condensed small-top-margin celdaCuadroInfo_Dato pl10 tablaCuadroInfo">
                                                <Columns>
                                                    <asp:BoundField DataField="IdResultadoUltimaLicitacionLECAPS" ItemStyle-CssClass="oculto"
                                                        HeaderStyle-CssClass="oculto" HeaderText="Id Resultado Ultima Licitacion LEBACS" />
                                                    <asp:BoundField DataField="Plazo" HeaderText="Plazo">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DIAS360Emision" HeaderText="DIAS360-Emisión">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DIAS360Liquidacion" HeaderText="DIAS360-Liquidación">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaEmision" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Emisión">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaLiquidacion" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Liquidación">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FechaVencimiento" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vencimiento">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PrecioUnitrade" DataFormatString="{0:n6}" HeaderText="Precio de corte">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TM" HeaderText="TM" DataFormatString="{0:n4}%">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TNA" HeaderText="TNA" DataFormatString="{0:n4}%">
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="EsUltimaLicitacion" ReadOnly="True" HeaderText="Es Última Licitación">
                                                        <ItemStyle HorizontalAlign="center" />
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
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="plazo">
                                                    Plazo:</label>
                                                <input runat="server" type="text" class="form-control" id="plazo" placeholder="Plazo..." />
                                            </div>
                                        </div>
                                        <div class"col-lg-4>
                                            <div class="form-check">
                                                   <input runat="server" type="checkbox" class="form-check-input" value="" id="EsUltimaLicitacion" style="margin-top: 28px;margin-left: 14px;"/>
                                                   <label class="form-check-label" for="EsUltimaLicitacion">Es Última Licitación</label>                      
                                              </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="fechaEmision">Fecha Emisión:</label>
                                                <div id="datepickerfechaEmision" class="input-group date" data-date-format="dd-mm-yyyy">
                                                     <input runat="server" class="form-control" type="text" id="fechaEmision" placeholder ="Fecha Emisión..." autocomplete="off"/>
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                </div>
                                              </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="fechaEmision">Fecha Liquidación:</label>
                                                <div id="datepickerfechaLiquidacion" class="input-group date" data-date-format="dd-mm-yyyy">
                                                     <input runat="server" class="form-control" type="text" id="fechaLiquidacion" placeholder ="Fecha Liquidacíon..." autocomplete="off"/>
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                </div>
                                              </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="fechaEmision">Fecha Vencimiento:</label>
                                                <div id="datepickerfechaEmision" class="input-group date" data-date-format="dd-mm-yyyy">
                                                     <input runat="server" class="form-control" type="text" id="fechaVencimiento" placeholder ="Fecha Vencimiento..." autocomplete="off"/>
                                                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                </div>
                                              </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="TM">
                                                    TM:</label>
                                                <input runat="server" type="text" class="form-control" id="TM" placeholder="TM..." />
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label for="TNA">
                                                    TNA:</label>
                                                <input runat="server" type="text" class="form-control" id="TNA" placeholder="TNA..." />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4 col-lg-offset-2">
                                             <asp:Button CssClass="btn btn-primary btn-block" Text="Guardar" ID="btnNuevoResultado" runat="server" OnClick="btnNuevoResultado_Click"/>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <label for="fechaUltimaLicitacion">Fecha Última Licitación:</label>
                                            <div id="datepickerUltimaLicitacion" class="input-group date" data-date-format="dd-mm-yyyy">
                                                <input runat="server" class="form-control" type="text" id="fechaUltimaLicitacion" placeholder ="Fecha Ultima Licitación..." />
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                             </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Button CssClass="btn btn-primary btn-block" Text="Guardar" ID="btnFechaUltimaLicitacion" runat="server" OnClick="btnFechaUltimaLicitacion_Click"/>
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
