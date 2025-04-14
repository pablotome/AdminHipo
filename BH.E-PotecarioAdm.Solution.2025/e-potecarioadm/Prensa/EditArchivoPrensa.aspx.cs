using System.Globalization;
using BH.EPotecario.Adm;
using BH.EPotecario.Adm.Componentes;
using MicroSitios.Componentes;
using MicroSitios.Dominio;
using MicroSitios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing.Drawing2D;


namespace BH.EPotecario.Prensa
{
	public partial class EditArchivoPrensa : WebFormBase
	{

		private int CodSeccion
		{
			get
			{
				return (Session["CodSeccion"] != null) ? int.Parse(Session["CodSeccion"].ToString()) : 0;
			}

			set
			{
				Session["CodSeccion"] = value;
			}
		}

		public int CodArchivo
		{
			get
			{
				return (Request.QueryString["CodArchivo"] != null) ? int.Parse(Request.QueryString["CodArchivo"]) : 0;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.CheckSecurity("ADMPRENSA,ADMINISTRACION");

			this.MenuTab.ItemsMenu = this.GetItemsMenuPrincipal();
			this.MenuTab.CurrentMenuItem = "AdmSeccionPrensa";

			lblErrores.Visible = false;

			if (!Page.IsPostBack)
			{
				CargarSecciones();
				CargarArchivo();
			}
		}

		public void CargarArchivo()
		{
			if (CodArchivo != 0)
			{
				ArchivoPrensa archivo = ServicioSistema<ArchivoPrensa>.GetById(x => x.CodArchivo == CodArchivo);
				txtTitulo.Text = archivo.Titulo;
				txtCopete.Text = archivo.Copete;
				txtURL.Text = archivo.Url;
				txtFecha.Text = archivo.Fecha.ToString("dd/MM/yyyy");
				txtOrden.Text = archivo.Orden.ToString();
				chkVigente.Checked = archivo.Vigente;
				chkDestacado.Checked = archivo.Destacado;
				txtOrden.Enabled = !chkDestacado.Checked;
				ddlSeccion.SelectedValue = archivo.Seccion.CodSeccion.ToString();
				ddlSeccion.Enabled = false;

				hplArchivoActual.Text = archivo.Titulo;
				//hplArchivoActual.NavigateUrl = (archivo.Seccion.CodTipoArchivo == TipoArchivo.Link) ? archivo.Url : HelperWeb.LinkSincroPortal(archivo.Url);
				hplArchivoActual.NavigateUrl = (archivo.Seccion.CodTipoArchivo == TipoArchivo.Link) ? archivo.Url : ConfigurationManager.AppSettings["linkMedia"] + archivo.Url;
				hplArchivoActual.Target = "_blank";

				CodSeccion = archivo.Seccion.CodSeccion;
				CargoInfoSeccion();
			}
		}



		/// <summary>
		/// Cargamos el combo con las secciones para el filtrado 
		/// </summary>
		public void CargarSecciones()
		{
			try
			{
				//obtenemos las secciones 
				List<SeccionPrensa> secciones = ServicioSistema<SeccionPrensa>.GetAll().Where(x => x.Vigente).ToList();

				secciones.Insert(0, new SeccionPrensa { CodSeccion = 0, DesSeccion = "Seleccione", Url = "", Vigente = true });

				//removemos estados contables y Informes para la edicion y filtro
				ddlSeccion.DataSource = secciones.Where(x => x.CodSeccion != 6 && x.CodSeccion != 10).ToList();;
				ddlSeccion.DataTextField = "DesSeccion";
				ddlSeccion.DataValueField = "CodSeccion";
				ddlSeccion.DataBind();

				ddlSeccion.SelectedValue = CodSeccion.ToString();

				CargoInfoSeccion();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message.ToString());
			}
		}

		protected void ddlSeccion_SelectedIndexChanged(object sender, EventArgs e)
		{
			CodSeccion = int.Parse(ddlSeccion.SelectedValue);
			CargoInfoSeccion();
		}

		public void CargoInfoSeccion()
		{
			if (CodSeccion != 0)
			{
				SeccionPrensa seccionPrensa = ServicioSistema<SeccionPrensa>.GetById(s => s.CodSeccion == CodSeccion);

				switch (seccionPrensa.CodTipoArchivo)
				{
					case 1:												//Archivos PDF
						fileSeleccionarArchivo.Visible = true;
						fileSeleccionarArchivo.Accept = ParametrosDB.ObtenerParametro("ContentTypePDF").Valor;
						btnQuitarArchivo.Visible = true;
						trURL.Visible = false;
						trArchivos.Visible = true;
						break;
					case 2:												//Imágenes
						fileSeleccionarArchivo.Visible = true;
						fileSeleccionarArchivo.Accept = ParametrosDB.ObtenerParametro("ContentTypeImagenes").Valor;
						fileSeleccionarArchivo.Attributes.Add("multiple", string.Empty);
						btnQuitarArchivo.Visible = true;
						trURL.Visible = false;
						trArchivos.Visible = true;
						break;
					case 3:												//Links
						fileSeleccionarArchivo.Visible = false;
						btnQuitarArchivo.Visible = false;
						trURL.Visible = true;
						trArchivos.Visible = false;
						break;
					//default:
					//break;
				}
			}
			else
			{
				trURL.Visible = false;
				trArchivos.Visible = false;
			}
		}

		protected bool DatosValidos
		{
			get { 
				
				string strError = string.Empty;
				DateTime fechaAux;
				int ordenAux;

				SeccionPrensa seccionPrensa = ServicioSistema<SeccionPrensa>.GetById(s => s.CodSeccion == CodSeccion);
				ArchivoPrensa archivoPrensa = ServicioSistema<ArchivoPrensa>.GetById(s => s.CodArchivo == CodArchivo);

				if (ddlSeccion.SelectedValue == "0")
					strError += "Debe seleccionar una sección<br />";

				if (txtFecha.Text.Trim() == string.Empty)
					strError += "Debe indicar una fecha<br />";

				if (!DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out fechaAux))
					strError += "La fecha debe estar en el formato dd/mm/aaaa<br />";

				if (!chkDestacado.Checked && (txtOrden.Text.Trim() == string.Empty || !int.TryParse(txtOrden.Text.Trim(), out ordenAux)))
					strError += "Debe indicar un número como orden<br />";

				if (txtTitulo.Text.Trim() == string.Empty)
					strError += "Debe ingresar un título<br />";

				if (seccionPrensa != null && seccionPrensa.CodTipoArchivo == TipoArchivo.Link && txtURL.Text.Trim() == string.Empty)
					strError += "Debe ingresar un link<br />";

				if (seccionPrensa != null && seccionPrensa.CodTipoArchivo == TipoArchivo.Link && txtURL.Text.Trim() != string.Empty && !Uri.IsWellFormedUriString(txtURL.Text, UriKind.Absolute))
					strError += "La url ingresada es incorrecta<br />";

				if ((seccionPrensa != null && archivoPrensa == null && (seccionPrensa.CodTipoArchivo == TipoArchivo.ArchivoPDF || seccionPrensa.CodTipoArchivo == TipoArchivo.Imagen) && (Request.Files.Count == 0 || fileSeleccionarArchivo.PostedFile.ContentLength == 0))
					|| archivoPrensa != null && archivoPrensa.Url == string.Empty)
					strError += "Debe seleccionar al menos un archivo<br />";

				if (strError != string.Empty)
				{
					lblErrores.Visible = true;
					lblErrores.Text = strError;
				}
				else
				{
					lblErrores.Visible = true;
					lblErrores.Text = string.Empty;
				}

				return strError == string.Empty;

			}
		}

		protected void btnAceptar_Click(object sender, EventArgs e)
		{
			if (DatosValidos)
			{
				CargamosDatos(); 
			}
		}

		protected void btnCancelar_Click(object sender, EventArgs e)
		{
			Response.Redirect(string.Format("AdmArchivoPrensa.aspx"));
		}

		public string NormalizoNombreArchivo(string nombre)
		{
			return string.Join("_", nombre.Split(Path.GetInvalidFileNameChars())).Replace("Ñ", "N").Replace("ñ", "n").Replace(" ", "_");
		}

		public void CreoImagenChica(HttpPostedFile imagen, string nombreImagenChica)
		{
			using (Image imagenGrande = Image.FromStream(imagen.InputStream, true, true))
			using (Image nueva = ScaleImage(imagenGrande, 90, 90))
			{
				nueva.Save(nombreImagenChica);
			}
		}

		public Image ScaleImage(Image image, int maxWidth, int maxHeight)
		{
			var ratioX = (double)maxWidth / image.Width;
			var ratioY = (double)maxHeight / image.Height;
			var ratio = Math.Min(ratioX, ratioY);
			var newWidth = (int)(image.Width * ratio);
			var newHeight = (int)(image.Height * ratio);
			var newImage = new System.Drawing.Bitmap(newWidth, newHeight);
			using (var graphics = Graphics.FromImage(newImage))
			{
				graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(image, 0, 0, newWidth, newHeight);
				graphics.Dispose();
			}
			image.Dispose();
			return newImage;
		}


		public void GuardarArchivo(HttpPostedFile postedfile, SeccionPrensa seccion)
		{
			try
			{
				string pathSeccion = ConfigurationManager.AppSettings["urlFolderImage"] + seccion.Url.Replace("/", "\\");
				pathSeccion = Page.ResolveUrl(pathSeccion).Replace("\\\\", "\\");
				string nombreArchivo = pathSeccion + NormalizoNombreArchivo(postedfile.FileName);

				if (!Directory.Exists(Path.GetDirectoryName(nombreArchivo)))
					Directory.CreateDirectory(Path.GetDirectoryName(nombreArchivo));

				//Es una imagen. La guardo en el tamaño original y en el tamaño pequeño
				if (seccion.CodTipoArchivo == TipoArchivo.Imagen)
				{
					string nombreImagenGrande = nombreArchivo;
					string nombreImagenChica = string.Format("{0}\\{1}_Chica{2}", Path.GetDirectoryName(nombreArchivo), Path.GetFileNameWithoutExtension(nombreArchivo), Path.GetExtension(nombreArchivo));

					if (File.Exists(nombreImagenGrande))
						File.Delete(nombreImagenGrande);

					if (File.Exists(nombreImagenChica))
						File.Delete(nombreImagenChica);

					//Grabo la imagen grande
					postedfile.SaveAs(nombreImagenGrande);
					
					//Grabo la imagen chica
					CreoImagenChica(postedfile, nombreImagenChica);
				}
				else if (seccion.CodTipoArchivo == TipoArchivo.ArchivoPDF)
				{
					if (File.Exists(nombreArchivo))
						File.Delete(nombreArchivo);

					postedfile.SaveAs(nombreArchivo);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void CargamosDatos()
		{
			SeccionPrensa seccion = ServicioSistema<SeccionPrensa>.GetById(x => x.CodSeccion == CodSeccion);
			ArchivoPrensa archivo = ServicioSistema<ArchivoPrensa>.GetById(x => x.CodArchivo == CodArchivo);
			string nombreArchivo;
			HttpPostedFile archivoEnviado;

			try
			{

				for (int i=0; i<Request.Files.Count; i++)
				{
					archivoEnviado = Request.Files[i];

					if (archivo == null || CodArchivo == 0)
						archivo = new ArchivoPrensa();

					if ((seccion.CodTipoArchivo == TipoArchivo.Imagen || seccion.CodTipoArchivo == TipoArchivo.ArchivoPDF) && archivoEnviado.ContentLength > 0)
					{
						GuardarArchivo(archivoEnviado, seccion);
						nombreArchivo = NormalizoNombreArchivo(archivoEnviado.FileName);
					}
					else
						nombreArchivo = archivo.Nombre;

					archivo.Seccion = seccion;
					archivo.Fecha = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", null);
					archivo.Nombre = nombreArchivo;
					archivo.TipoArchivo = string.Empty;
					archivo.Titulo = txtTitulo.Text.Trim();
					archivo.Copete = txtCopete.Text.Trim();
					archivo.Url = (seccion.CodTipoArchivo == TipoArchivo.ArchivoPDF || seccion.CodTipoArchivo == TipoArchivo.Imagen) ?
						archivo.Seccion.Url + nombreArchivo
						: txtURL.Text.Trim();
					archivo.Orden = chkDestacado.Checked ? 0 : int.Parse(txtOrden.Text);
					archivo.Vigente = chkVigente.Checked;
					archivo.Destacado = chkDestacado.Checked;

					archivo = ServicioSistema<ArchivoPrensa>.SaveOrUpdate(archivo);
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			Response.Redirect("AdmArchivoPrensa.aspx");
		}
	}
}
