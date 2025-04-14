using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
/*using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;*/

namespace BH.EPotecario.Adm.Componentes
{
	public class SizeHandler
	{
		#region Private Declarations
		//private SizeHandlerType _type = SizeHandlerType.None;
		private int _percent = -1;
		private int _width = -1;
		private int _height = -1;
		private string _image_src = string.Empty;
		private Stream _image_stream = null;
		#endregion

		#region Constructors
		public SizeHandler()
		{
		}

		public SizeHandler(int percent)
		{
			this._percent = percent;
			//this._type = SizeHandlerType.Percent;
		}

		public SizeHandler(int width, int height)
		{
			this._width = width;
			this._height = height;
			//this._type = SizeHandlerType.Size;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets percent value
		/// </summary>
		public int Percent
		{
			get
			{
				return this._percent;
			}
			set
			{
				this._percent = value;
			}
		}

		/// <summary>
		/// Gets or sets width value
		/// </summary>
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
			}
		}

		/// <summary>
		/// Gets or sets height value
		/// </summary>
		public int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
			}
		}

		/// <summary>
		/// Gets or sets image file value
		/// </summary>
		public string ImageSource
		{
			get
			{
				return this._image_src;
			}
			set
			{
				this._image_src = value;
			}
		}

		/// <summary>
		/// Gets or sets image stream
		/// </summary>
		/// <returns></returns>
		public Stream ImageStream
		{
			get { return _image_stream; }
			set { _image_stream = value; }
		}
		#endregion

		#region Public Methods
		public Image Scale()
		{
			System.Drawing.Image img = Image.FromStream(this._image_stream);

			this._width = img.Width;
			this._height = img.Height;

			return this.ScaleBySize();
		}

		public Image ScaleByPercent()
		{
			Image img = Image.FromFile(this.ImageSource);

			float nPercent = ((float)Percent / 100);

			int sourceWidth = img.Width;
			int sourceHeight = img.Height;
			int sourceX = 0;
			int sourceY = 0;

			int destX = 0;
			int destY = 0;
			int destWidth = (int)(sourceWidth * nPercent);
			int destHeight = (int)(sourceHeight * nPercent);

			Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
				PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(img.HorizontalResolution,
				img.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(img,
				new Rectangle(destX, destY, destWidth, destHeight),
				new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
				GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}

		public Image ScaleBySize()
		{
			Image img = Image.FromStream(this._image_stream);

			int sourceWidth = img.Width;
			int sourceHeight = img.Height;
			int sourceX = 0;
			int sourceY = 0;

			int destX = 0;
			int destY = 0;
			int destWidth = (this.Width != -1) ? this.Width : img.Width;
			int destHeight = (this.Height != -1) ? this.Height : img.Height;

			Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(img,
				new Rectangle(destX, destY, destWidth, destHeight),
				new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
				GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}

		public Image ScaleByHeight(int height)
		{
			Image img = Image.FromStream(this._image_stream);

			int sourceWidth = img.Width;
			int sourceHeight = img.Height;
			float relHW = (float)sourceHeight / (float)sourceWidth;

			int sourceX = 0;
			int sourceY = 0;

			int destX = 0;
			int destY = 0;

			int destWidth;
			int destHeight;

			/*if (img.Height > height)
			{
				destWidth = (int)(height / relHW);
				destHeight = height;
			}
			else
			{
				destWidth = img.Width;
				destHeight = img.Height;
			}*/

			//Clavo la altura en "height"
			if (height < sourceHeight)
			{
				destWidth = (int)(height / relHW);
				destHeight = height;
			}
			else
			{
				destWidth = sourceWidth;
				destHeight = sourceHeight;
			}

			Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(img,
				new Rectangle(destX-1, destY-1, destWidth+2, destHeight+2),
				new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
				GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}

		public Image ScaleByWidth(int width)
		{
			Image img = Image.FromStream(this._image_stream);

			int sourceWidth = img.Width;
			int sourceHeight = img.Height;
			float relHW = (float)sourceWidth / (float)sourceHeight;

			int sourceX = 0;
			int sourceY = 0;

			int destX = 0;
			int destY = 0;

			int destWidth;
			int destHeight;

			//Clavo la altura en "width"
			if (width < sourceWidth)
			{
				destWidth = width;
				destHeight = (int)(width / relHW);
			}
			else
			{
				destWidth = sourceWidth;
				destHeight = sourceHeight;
			}

			Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(img,
				new Rectangle(destX - 1, destY - 1, destWidth + 2, destHeight + 2),
				new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
				GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}

		public Image ScaleThumbnail()
		{
			Image img = Image.FromStream(this._image_stream);

			int sourceWidth = img.Width;
			int sourceHeight = img.Height;
			float relHW = (float)sourceHeight / (float)sourceWidth;

			int sourceX = 0;
			int sourceY = 0;

			int destX = 0;
			int destY = 0;

			int destWidth;
			int destHeight;

			if (img.Width > 185)
			{
				destWidth = 185;
				destHeight = (int)(relHW * 185);
			}
			else
			{
				destWidth = img.Width;
				destHeight = img.Height;
			}

			Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(img.HorizontalResolution, img.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(img,
				new Rectangle(destX, destY, destWidth, destHeight),
				new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
				GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}
		#endregion
	}
}
