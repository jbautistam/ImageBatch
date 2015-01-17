using System;
using System.Collections.Generic;

namespace Bau.Controls.ImageControls.Thumbnail
{
	/// <summary>
	///		Colecci�n de <see cref="ImageThumbnail"/>
	/// </summary>
	public class ImageThumbnailsCollection : List<ImageThumbnail>
	{ // Eventos privados
			internal event EventHandler OnRepaint;
		
		/// <summary>
		///		A�ade una imagen
		/// </summary>
		public void Add(string strFileName)
		{ Add(strFileName, null);
		}
		
		/// <summary>
		///		A�ade una imagen
		/// </summary>
		public void Add(string strFileName, object objTag)
		{ Add(new ImageThumbnail(Count + 1, strFileName, objTag));
		}
		
		/// <summary>
		///		A�ade una imagen
		/// </summary>
		private new void Add(ImageThumbnail objThumbnail)
		{ // A�ade la imagen
				base.Add(objThumbnail);
			// A�ade el manejador de evento
				objThumbnail.OnRepaint += new EventHandler(objThumbnail_OnRepaint);
			// Lanza el evento de repintado
				RaiseEventRepaint();
		}
		
		/// <summary>
		///		Limpia la lista
		/// </summary>
		public new void Clear()
		{ // Limpia la lista
				base.Clear();
			// Lanza el evento de repintar
				RaiseEventRepaint();
		}
		
		/// <summary>
		///		Elimina un thumb de la lista
		/// </summary>
		public void Remove(string strFileName)
		{ bool blnRemoved = false;
		
				// Elimina el thumbnail de la lista
					for (int intIndex = Count - 1; intIndex >= 0; intIndex--)
						if (this[intIndex].FileName.Equals(strFileName, StringComparison.CurrentCultureIgnoreCase))
							{ // Elimina el elemento
									RemoveAt(intIndex);
								// Indica que se ha borrado
									blnRemoved = true;
							}
				// Lanza el evento de repintado
					if (blnRemoved)
						RaiseEventRepaint();
		}
		
		/// <summary>
		///		Calcula las im�genes
		/// </summary>
		internal void ComputeThumbnails(int intWidth)
		{ foreach (ImageThumbnail objThumb in this)
				objThumb.ComputeThumbnail(intWidth);
		}

		/// <summary>
		///		Calcula las posiciones de las im�genes
		/// </summary>
		internal void ComputePositions(int intWidth, int intSpaceBetweenColumns, int intSpaceBetweenRows)
		{	int intIndex = 0;
			int intLeft = 5, intTop = 5, intRowHeigth = 0;
			
				foreach (ImageThumbnail objThumbnail in this)
					{ // Asigna el �ndice
							objThumbnail.Index = intIndex++;
						// Calcula la posici�n X e Y de la imagen
							if (intLeft + objThumbnail.Thumbnail.Width + intSpaceBetweenColumns > intWidth)
								{ // Si no cabe en la fila, se pone en la posici�n superior izquierda de la siguiente fila
										objThumbnail.X = 0;
										objThumbnail.Y = intTop + intRowHeigth + intSpaceBetweenRows;
									// Inicializa la altura inicial
										intRowHeigth = 0;
								}
							else // ... la imagen cabe en esta fila
								{ objThumbnail.X = intLeft;
									objThumbnail.Y = intTop;
								}
						// Guarda los valores necesarios para la siguiente imagen
							intLeft = objThumbnail.X + objThumbnail.Thumbnail.Width + intSpaceBetweenColumns;
							intTop = objThumbnail.Y;
						// Calcula la altura m�xima de la fila
							if (objThumbnail.Thumbnail.Height > intRowHeigth)
								intRowHeigth = objThumbnail.Thumbnail.Height;
					}
		}

		/// <summary>
		///		Lanza el evento de repintado
		/// </summary>
		private void RaiseEventRepaint()
		{ if (OnRepaint != null)
				OnRepaint(this, EventArgs.Empty);
		}

		/// <summary>
		///		Lanza el evento de repintado
		/// </summary>
		private void objThumbnail_OnRepaint(object sender, EventArgs e)
		{ RaiseEventRepaint();
		}

		/// <summary>
		///		Calcula la altura de las im�genes
		/// </summary>
		internal int GetMaxHeight(int intSpaceBetweenRows)
		{ int intMaxHeight = 0;
		
				// Calcula la altura m�xima
					foreach (ImageThumbnail objThumbnail in this)
						if (objThumbnail.Y + objThumbnail.Thumbnail.Height + intSpaceBetweenRows > intMaxHeight)
							intMaxHeight = objThumbnail.Y + objThumbnail.Thumbnail.Height + intSpaceBetweenRows;
				// Devuelve la altura m�xima
					return intMaxHeight;
		}

		/// <summary>
		///		Selecciona / deselecciona una imagen
		/// </summary>
		internal void Select(int intX, int intY)
		{ foreach (ImageThumbnail objThumbnail in this)
				objThumbnail.Selected = new System.Drawing.Rectangle(objThumbnail.Position, objThumbnail.Thumbnail.Size).Contains(intX, intY);
		}

		/// <summary>
		///		Selecciona una imagen
		/// </summary>
		internal void Select(int intThumbIndex)
		{ int intIndex = 0;
		
				foreach (ImageThumbnail objThumbnail in this)
					objThumbnail.Selected = intIndex++ == intThumbIndex;
		}
		
		/// <summary>
		///		Obtiene una colecci�n con las im�genes seleccionadas
		/// </summary>
		internal ImageThumbnailsCollection GetSelectedItems()
		{ ImageThumbnailsCollection objColThumbnails = new ImageThumbnailsCollection();
		
				// A�ade los elementos seleccionados
					foreach (ImageThumbnail objThumbnail in this)
						if (objThumbnail.Selected)
							objColThumbnails.Add(objThumbnail);
				// Devuelve la colecci�n
					return objColThumbnails;
		}
	}
}
