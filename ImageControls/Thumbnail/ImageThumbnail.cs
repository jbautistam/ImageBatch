using System;
using System.Drawing;

namespace Bau.Controls.ImageControls.Thumbnail
{
	/// <summary>
	///		Clase con los datos de un Thumbnail
	/// </summary>
	public class ImageThumbnail
	{ // Eventos
			internal event EventHandler OnRepaint;
		// Variables privadas
			private int intIndex;
			private object objTag;
			private string strFileName;
			private Image imgImage;
			private int intWidth = 0;
			private Point pntPosition = new Point(0, 0);
			private bool blnSelected = false;

		public ImageThumbnail(int intIndex, string strFileName) : this(intIndex, strFileName, null) {	}
			
		public ImageThumbnail(int intIndex, string strFileName, object objTag)
		{	Index = intIndex;
			FileName = strFileName;
			Tag = objTag;
		}

		/// <summary>
		///		Genera la imagen reducida
		/// </summary>
		internal void ComputeThumbnail(int intNewWidth)
		{ if (!string.IsNullOrEmpty(FileName) && System.IO.File.Exists(FileName))
				if (imgImage == null || intNewWidth != intWidth)
					try
						{ // Crea el thumb
								imgImage = Helper.clsImage.CreateThumbnail(Image.FromFile(FileName), intNewWidth);
							// Guarda el ancho
								intWidth = intNewWidth;
						}
				catch {}
		}

		/// <summary>
		///		Lanza el evento de repintado
		/// </summary>
		private void RaiseEventRepaint()
		{ if (OnRepaint != null)
				OnRepaint(this, EventArgs.Empty);
		}		
		
		public int Index
		{ get { return intIndex; }
			internal set { intIndex = value; }
		}
		
		public string FileName
		{ get { return strFileName; }
			set 
				{ // Si cambia el nombre de archivo, limpia la imagen
						if (string.IsNullOrEmpty(value) || !value.Equals(strFileName, 
																														 StringComparison.CurrentCultureIgnoreCase))
							Thumbnail = null;
					// Cambia el nombre de archivo
						strFileName = value; 
					// Lanza el evento de repintado
						RaiseEventRepaint();
				}
		}
		
		public Image Thumbnail
		{ get 
				{ if (imgImage == null)
						return Helper.clsImage.ThumbnailNoImage;
					else
						return imgImage; 
				}
			private set { imgImage = value; }
		}

		public bool Selected
		{ get { return blnSelected; }
			set
				{ if (value != blnSelected)
						{ blnSelected = value;
							RaiseEventRepaint();
						}
				}
		}		

		public object Tag
		{ get { return objTag; }
			set { objTag = value; }
		}
				
		internal Point Position
		{ get { return pntPosition; }
			set { pntPosition = value; }
		}
		
		internal int X
		{ get { return Position.X; }
			set { Position = new Point(value, Position.Y); }
		}
		
		internal int Y
		{ get { return Position.Y; }
			set { Position = new Point(Position.X, value); }
		}
	}
}
