using System;
using System.Drawing; 
using System.Drawing.Drawing2D;

namespace Bau.Controls.ImageControls.Helper
{
	/// <summary>
	///		Clase de ayuda para el manejo de imágenes
	/// </summary>
	internal class clsImage
	{ // Variables privadas
			private static Image imgNoImage = null;
	
		/// <summary>
		///		Crea un thumbNail de una imagen
		/// </summary>
		public static Bitmap CreateThumbnail(Image imgSource, int intThumbnailWidth)
		{ int intThumbnailHeight = (int) (((double) imgSource.Height) / ((double) imgSource.Width) * intThumbnailWidth);
			Bitmap bmpImage = new Bitmap(intThumbnailWidth, intThumbnailHeight);
			
				// Dibuja el thumbnail de la imagen en un nuevo gráfico
					using (Graphics grCanvas = Graphics.FromImage(bmpImage))
						{	// Inicializa las propiedades del canvas
								grCanvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
								grCanvas.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
								grCanvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
							// Dibuja la imagen origen en la destino			
								grCanvas.DrawImage(imgSource, new Rectangle(0, 0, intThumbnailWidth, intThumbnailHeight), 
																	 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
						}
				// Devuelve la imagen	 
					return bmpImage;
		}
		
		/// <summary>
		///		Obtiene un rectánculo redondeado
		/// </summary>
		public static GraphicsPath RoundedRectangle(int left, int top, int width, int height, int roundedWidth, int roundedHeight)
		{	GraphicsPath path = new GraphicsPath();
		
				if (roundedHeight < 2 || roundedWidth < 2)
					path.AddRectangle(new Rectangle(left, top, width, height));
				else
					{	int num = left + width;
						int num2 = top + height;
						int num3 = roundedWidth * 2;
						int num4 = roundedHeight * 2;
						
							path.AddArc(left, top, num3, num4, 180f, 90f);
							path.AddArc(num - num3, top, num3, num4, 270f, 90f);
							path.AddArc(num - num3, num2 - num4, num3, num3, 0f, 90f);
							path.AddArc(left, num2 - num4, num3, num4, 90f, 90f);
					}
				path.CloseFigure();
				return path;
		}
 
		/// <summary>
		///		Imagen que se presenta cuando no hay ninguna imagen
		/// </summary>
		public static Image ThumbnailNoImage
		{ get
				{ // Obtiene la imagen si no existía
						if (imgNoImage == null)
							imgNoImage = CreateThumbnail(Properties.Resources.NoImage, 100);
					// Devuelve la imagen
						return imgNoImage;
				}
		}

/*
String src="c:/myImages/a.jpg";   //absolute location of source image

String dest="c:/myImages/a_th.jpg";    //absolute location of the new image created(thumbnail)
int thumbWidth=132;    //width of the image (thumbnail) to produce

System.Drawing.Image image = System.Drawing.Image.FromFile(src); 

int srcWidth=image.Width;
int srcHeight=image.Height; 

//we will get the sizeratio in decimal so that we dont get exception in case width is less then height.

Decimal sizeRatio = ((Decimal)srcHeight/srcWidth);
int thumbHeight=Decimal.ToInt32(sizeRatio*thumbWidth);
Bitmap bmp = new Bitmap(thumbWidth, thumbHeight);

System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);
gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);
gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, GraphicsUnit.Pixel);
bmp.Save(dest);


bmp.Dispose();
image.Dispose();
 */
	}
}
