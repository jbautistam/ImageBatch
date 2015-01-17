using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bau.Controls.ImageControls.Picture
{
	/// <summary>
	///		Control de imagen para mostrar la imagen visible
	/// </summary>
	public class PictureTrack : Control
	{ // Variables privadas
			private Image imgImage = null;
			private Image imgThumbnail = null;
			private Point pntPositionZoom = new Point(0, 0);
			private double dblZoom = 0;

    public PictureTrack()
    {	// Asigna los estilos al control
				SetStyle(ControlStyles.DoubleBuffer, true);
				SetStyle(ControlStyles.UserPaint, true);
				SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
				SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			// Asigna un color de fondo transparente
				BackColor = Color.Transparent;
    }

		/// <summary>
		///		Obtiene la región de resalte
		/// </summary>
		private Region GetRegionHighlight()
		{ return new Region(new Rectangle(pntPositionZoom.X, pntPositionZoom.Y, 70, 70));
		}
		
		/// <summary>
		///		Sobrescribe el método OnPaint
		/// </summary>
		protected override void OnPaint(PaintEventArgs e)
		{ if (Picture != null)
				{ // Crea el thumbNail si no existía
						if (imgThumbnail == null)
							imgThumbnail = Helper.clsImage.CreateThumbnail(imgImage, Width); 
					// Dibuja el thumbNail
						e.Graphics.DrawImage(imgThumbnail, 0, 0);
					// Pinta la región de resalte
						if (Picture.Width != 0 && Picture.Height != 0)
							{	using (Brush brsTransparent = new SolidBrush(Color.FromArgb(180, 0xc0, 0xc0, 0xc0)))
									{ int intTop = (int) (ZoomTop * Height / (Picture.Height * Zoom));
										int intLeft = (int) (ZoomLeft * Width / (Picture.Width * Zoom));
										Region rgnDark = new Region(new Rectangle(intLeft, intTop, 
																															(int) (imgThumbnail.Width * Zoom), 
																															(int) (imgThumbnail.Height * Zoom)));
									
											// Excluye la región resaltada
												rgnDark.Exclude(GetRegionHighlight());
											// Dibuja la región con la brocha translúcida
												e.Graphics.FillRegion(brsTransparent, rgnDark);
									}
							}
				}
		}

		/// <summary>
		///		Sobrescribe el evento OnResize
		/// </summary>
		protected override void OnResize(EventArgs e)
		{ // Elimina el thumbNail para que se vuelva a crear al repintar
				imgThumbnail = null;
			// Llama al evento base
				base.OnResize(e);
		}
		
		/// <summary>
		///		Libera la memoria
		/// </summary>
		protected override void Dispose(bool disposing)
		{	// Libera la memoria
				Picture = null;
				imgThumbnail = null;
			// Elimina los elementos de la base
				base.Dispose(disposing);
		}
		
		public Image Picture
		{ get { return imgImage; }
			set 
				{ // Guarda la imagen
						imgImage = value;
					// Limpia el thumbnail para que se cree a la siguiente vez que se dibuje
						imgThumbnail = null;
					// Repinta
						Invalidate();
				}
		}

		public int ZoomLeft
		{ get { return pntPositionZoom.X; }
			set 
				{ pntPositionZoom.X = value; 
					Invalidate();
				}
		}	
		
		public int ZoomTop
		{ get { return pntPositionZoom.Y; }
			set 
				{ pntPositionZoom.Y = value; 
					Invalidate();
				}
		}
		
		public double Zoom
		{ get { return dblZoom; }
			set 
				{ dblZoom = value; 
					Invalidate();
				}
		}	
	}
}
