using System;
using System.Drawing;

namespace Bau.Libraries.ImageFilters.Helper
{
	/// <summary>
	///		Clase de ayuda para el trabajo con filtros de imagen
	/// </summary>
	public static class FiltersHelpers
	{
		/// <summary>
		///		Carga una imagen
		/// </summary>
		public static Image Load(string strFileName)
		{ Image objImage = null;
		
				// Carga la imagen
					if (System.IO.File.Exists(strFileName))
						try
							{ objImage = Image.FromFile(strFileName);
							}
						catch {}
				// Devuelve la imagen
					return objImage;
		}

		/// <summary>
		///		Graba una imagen
		/// </summary>
		public static void Save(Image objImage, string strFileTarget)
		{ if (strFileTarget.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
					strFileTarget.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase))
				objImage.Save(strFileTarget, System.Drawing.Imaging.ImageFormat.Jpeg);
			else if (strFileTarget.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase))
				objImage.Save(strFileTarget, System.Drawing.Imaging.ImageFormat.Bmp);
			else if (strFileTarget.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase))
				objImage.Save(strFileTarget, System.Drawing.Imaging.ImageFormat.Gif);
			else if (strFileTarget.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase))
				objImage.Save(strFileTarget, System.Drawing.Imaging.ImageFormat.Png);
			else 
				objImage.Save(strFileTarget);
		}

		/// <summary>
		///		Graba el thumb de una imagen
		/// </summary>
		public static void SaveThumb(Image objImage, int intWidth, string strFileName, string strPrefixThumb = "th_")
		{ if (objImage.Width > intWidth)
				Save(Resize(objImage, intWidth), 
						 System.IO.Path.Combine(System.IO.Path.GetDirectoryName(strFileName),
																		strPrefixThumb + System.IO.Path.GetFileName(strFileName)));
		}

		/// <summary>
		///		Redimensiona una imagen
		/// </summary>
		public static Image Resize(Image objImage, int intWidth)
		{ return Resize(objImage, intWidth, 0);
		}
		
		/// <summary>
		///		Redimensiona una imagen
		/// </summary>
		public static Image Resize(Image objImage, int intWidth, int intHeight)
		{ Filters.ResizeFilter objResize = new Filters.ResizeFilter();
						
				// Asigna las propiedades al filtro
					objResize.Width = intWidth;
					if (intHeight > 0)
						objResize.Height = intHeight;
					else
						objResize.KeepAspectRatio = true;
				// Ejecuta el filtro
					return objResize.ExecuteFilter(objImage);
		}
		
		/// <summary>
		///		Crea una marca de agua ajustando el tamaño del texto
		/// </summary>
		public static Image WaterMark(Image objImage, string strFontName, string strText, Color clrColor, int intAlpha,
																	Filters.BaseWaterMarkFilter.HAlign intHorizontalAlign,
																	Filters.BaseWaterMarkFilter.VAlign intVerticalAlign)
		{ return WaterMark(objImage, strFontName, 0, strText, clrColor, intAlpha, intHorizontalAlign, intVerticalAlign);
		}
		
		/// <summary>
		///		Crea una marca de agua
		/// </summary>
		public static Image WaterMark(Image objImage, string strFontName, int intTextSize, 
																	string strText, Color clrColor, int intAlpha,
																	Filters.BaseWaterMarkFilter.HAlign intHorizontalAlign,
																	Filters.BaseWaterMarkFilter.VAlign intVerticalAlign)
		{ Filters.TextWatermarkFilter objWatermark = new Filters.TextWatermarkFilter();
		
				// Asigna las propiedades
					objWatermark.FontName = strFontName;
					if (intTextSize < 1)
						objWatermark.AutomaticTextSize = true;
					else
						objWatermark.TextSize = intTextSize;
					objWatermark.Caption = strText;
					objWatermark.CaptionColor = clrColor;
					objWatermark.CaptionAlpha = intAlpha;
					objWatermark.Halign = intHorizontalAlign;
					objWatermark.Valign = intVerticalAlign;
				// Devuelve la imagen con la marca de agua
					return objWatermark.ExecuteFilter(objImage);
		}

		/// <summary>
		///		Crea una marca de agua con una imagen
		/// </summary>
		public static Image WaterMarkImage(Image objImage, string strFileImage, Color clrTransparent, 
																			 double dblOpacity, 
																			 Filters.BaseWaterMarkFilter.HAlign intHorizontalAlign, 
																			 Filters.BaseWaterMarkFilter.VAlign intVerticalAlign)
		{ Image objWatermark = Load(strFileImage);
		
				if (objWatermark == null)
					return objImage;
				else
					{ Filters.ImageWatermarkFilter objFilter = new Filters.ImageWatermarkFilter();
					
							// Asigna las propiedades
								objFilter.WaterMarkImage = objWatermark;
								objFilter.TransparentColor = clrTransparent;
								objFilter.Alpha = (float) dblOpacity;
								objFilter.Halign = intHorizontalAlign;
								objFilter.Valign = intVerticalAlign;
							// Devuelve la imagen una vez realizado el filtro
								return objFilter.ExecuteFilter(objImage);
					}
		}

		/// <summary>
		///		Convierte una imagen a blanco y negro
		/// </summary>
		public static Image WhiteAndBlack(Image objImage, bool blnBrighter)
		{ Filters.BlackAndWhiteFilter objFilter = new Filters.BlackAndWhiteFilter();
		
				// Asigna las propiedad
					objFilter.Brighter = blnBrighter;
				// Devuelve la imagen convertida
					return objFilter.ExecuteFilter(objImage);
		}
	}
}
