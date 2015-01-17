using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Bau.Libraries.ImageFilters.Filters
{
  /// <summary>
  ///		Filtro de conversión a blanco y negro
  /// </summary>
  public class BlackAndWhiteFilter : BaseFilter
  { 
    /// <summary>
    ///		Ejecuta el filtro
    /// </summary>
    public override Image ExecuteFilter(Image objImage)
    { Bitmap objImageTarget = new Bitmap(objImage.Width, objImage.Height);
      ImageAttributes objAttributes = new ImageAttributes();
      ColorMatrix objColorMatrix;
      
      // Obtiene la matriz de color para brillante u oscuro
				if (Brighter)
					objColorMatrix = new ColorMatrix(new float[][]
																							{ new float[] { 0.5f, 0.5f, 0.5f, 0, 0 },
																								new float[] { 0.5f, 0.5f, 0.5f, 0, 0 },
																								new float[] { 0.5f, 0.5f, 0.5f, 0, 0 },
																								new float[] { 0, 0, 0, 1, 0, 0 },
																								new float[] { 0, 0, 0, 0, 1, 0 },
																								new float[] { 0, 0, 0, 0, 0, 1 }
																							 });
				else
					objColorMatrix = new ColorMatrix(new float[][]
																							{ new float[] { 0.3f, 0.3f, 0.3f, 0, 0 },
																								new float[] { 0.59f, 0.59f, 0.59f, 0, 0 },
																								new float[] { 0.11f, 0.11f, 0.11f, 0, 0 },
																								new float[] { 0, 0, 0, 1, 0, 0 },
																								new float[] { 0, 0, 0, 0, 1, 0 },
																								new float[] { 0, 0, 0, 0, 0, 1 }
																							});
			// Asigna la matriz de color
				objAttributes.SetColorMatrix(objColorMatrix);
			// Dibuja la imagen filtrada
				using (Graphics g = Graphics.FromImage(objImageTarget))
					{ // Dibuja la imagen
							g.DrawImage(objImage, new Rectangle(0, 0, objImage.Width, objImage.Height), 
													0, 0, objImage.Width, objImage.Height, GraphicsUnit.Pixel, objAttributes);
						// Libera los recursos
							g.Dispose();
					}
			// Devuelve la imagen convertida
				return objImageTarget;
    }

    /// <summary>
    ///		Brillo de la escala de grises. True para brillante, false para oscuro
    /// </summary>
    public bool Brighter { get; set; }
  }
}
