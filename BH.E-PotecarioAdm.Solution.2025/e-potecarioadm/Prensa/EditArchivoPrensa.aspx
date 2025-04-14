<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditArchivoPrensa.aspx.cs" Inherits="BH.EPotecario.Prensa.EditArchivoPrensa" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="uc1" TagName="MenuTab" Src="../Menu/MenuTab.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Administracion de Archivos</title>
	<script src="../scripts/JQuery/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
	<script src="../scripts/JQuery/jquery.validate.min.js" type="text/javascript"></script>
	<script src="Prensa.js?v=2" type="text/javascript"></script>
	<link href="../Default.css" type="text/css" rel="stylesheet" />
	<link href="../Menu1.css" type="text/css" rel="stylesheet" />
	<link href="../scripts/JQuery/Jquery-UI/jquery-ui.css" rel="stylesheet" />
	<style type="text/css">
		.controles {
			float: left;
			width: 646px;
			height: 390px;
		}

		.datagrid {
			display: none;
			float: right;
			display: none;
			width: 40%;
			border-right-width: 1px;
			margin-right: 20px;
			margin-top: 20px;
		}

			.datagrid table {
				border-collapse: collapse;
				text-align: left;
				width: 100%;
				margin-bottom: 20px;
			}

		.datagrid {
			font: normal 12px/150% Arial, Helvetica, sans-serif;
			background: #fff;
			overflow: hidden;
			border: 1px solid #006699;
			-webkit-border-radius: 3px;
			-moz-border-radius: 3px;
		}

			.datagrid table td, .datagrid table th {
				padding: 3px 18px;
			}

			.datagrid table thead th {
				background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #E1420B), color-stop(1,#E1420B) );
				background: -moz-linear-gradient( center top, #E1420B 5%, #E1420B 100% );
				color: #FFFFFF;
				font-size: 15px;
				font-weight: bold;
				border-left: 1px solid #E1420B;
			}

				.datagrid table thead th:first-child {
					border: none;
				}

			.datagrid table tbody td {
				color: #00496B;
				border-left: 1px solid #E1EEF4;
				font-size: 12px;
				font-weight: normal;
			}

			.datagrid table tbody .alt td {
				background: #E1EEF4;
				color: #00496B;
			}

			.datagrid table tbody td:first-child {
				border-left: none;
			}

			.datagrid table tbody tr:last-child td {
				border-bottom: none;
			}

		.style1 {
			width: 23px;
		}

		.style2 {
			width: 81px;
		}

		.style3 {
			width: 482px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server" enctype="multipart/form-data" method="post">
		<uc1:MenuTab ID="MenuTab" runat="server"></uc1:MenuTab>
		<h1>
			<asp:Label ID="lblTitulo" runat="server" Text="Adm. Archivos" /></h1>
			<table style="width: 100%">
				<tr>
					<td style="width: 20%;">Sección</td>
					<td>
						<asp:DropDownList runat="server" ID="ddlSeccion" AutoPostBack="true" OnSelectedIndexChanged="ddlSeccion_SelectedIndexChanged" Width="200px"/>
					</td>
				</tr>
				<tr>
					<td>Fecha</td>
					<td>
						<asp:TextBox ID="txtFecha" runat="server" Width="197px" MaxLength="10"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>Estado (tildar si está vigente)</td>
					<td>
						<asp:CheckBox ID="chkVigente" runat="server" />
					</td>
				</tr>
				<tr>
					<td>Destacado</td>
					<td>
						<asp:CheckBox ID="chkDestacado" runat="server" AutoPostBack="false" onclick="javascript:GestionarDestacado();" />
					</td>
				</tr>
				<tr>
					<td>Orden</td>
					<td>
						<asp:TextBox ID="txtOrden" runat="server" Width="197px" MaxLength="100"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>Titulo</td>
					<td>
						<asp:TextBox ID="txtTitulo" runat="server" Width="447px" MaxLength="100"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>Copete</td>
					<td>
						<asp:TextBox ID="txtCopete" runat="server" Width="447px" Height="88px" MaxLength="300"
							TextMode="MultiLine"></asp:TextBox>
					</td>
				</tr>
				<tr runat="server" id="trURL">
					<td>Url</td>
					<td>
						<asp:TextBox ID="txtURL" runat="server" Width="447px" MaxLength="100"></asp:TextBox>
					</td>
				</tr>
				<tr runat="server" id="trArchivos">
					<td>
						Archivo
					</td>
					<td>
						<div id="FileUploadContainer">
							<div id="FileUploadItem">
								<input id="fileSeleccionarArchivo" name="file" type="file" runat="server" />
								<input id="btnQuitarArchivo" type="button" value="Quitar" onclick="RemoveFileUpload(this)" runat="server" />
							</div>
						</div>
					</td>
				</tr>
				<tr runat="server" id="trArchivoActual">
					<td>
						
					</td>
					<td>
						Archivo actual: <asp:HyperLink runat="server" ID="hplArchivoActual"></asp:HyperLink>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Label ID="lblErrores" runat="server" ForeColor="Red" Visible="False"></asp:Label>
					</td>
				</tr>
				<tr id="filaDestacado" runat="server">
					<td></td>
					<td>
						<asp:Button ID="btnAceptar" runat="server" Text="Aceptar" Width="120px" OnClick="btnAceptar_Click" />
						&nbsp;
						<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="120px" OnClick="btnCancelar_Click" />
					</td>
				</tr>
			</table>
	</form>
</body>
</html>
