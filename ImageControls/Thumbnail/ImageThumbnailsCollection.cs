using System;
using System.Collections.Generic;

namespace Bau.Controls.ImageControls.Thumbnail
{
	/// <summary>
	///		Colección de <see cref="ImageThumbnail"/>
	/// </summary>
	public class ImageThumbnailsCollection : List<ImageThumbnail>
	{ // Eventos privados
			internal event EventHandler OnRepaint;
		
		/// <summary>
		///		Añade una imagen
		/// </summary>
		public void Add(string strFileName)
		{ Add(strFileName, null);
		}
		
		/// <summary>
		///		Añade una imagen
		/// </summary>
		public void Add(string strFileName, object objTag)
		{ Add(new ImageThumbnail(Count + 1, strFileName, objTag));
		}
		
		/// <summary>
		///		Añade una imagen
		/// </summary>
		private new void Add(ImageThumbnail objThumbnail)
		{ // Añade la imagen
				base.Add(objThumbnail);
			// Añade el manejador de evento
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
		///		Calcula las imágenes
		/// </summary>
		internal void ComputeThumbnails(int intWidth)
		{ foreach (ImageThumbnail objThumb in this)
				objThumb.ComputeThumbnail(intWidth);
		}

		/// <summary>
		///		Calcula las posiciones de las imágenes
		/// </summary>
		internal void ComputePositions(int intWidth, int intSpaceBetweenColumns, int intSpaceBetweenRows)
		{	int intIndex = 0;
			int intLeft = 5, intTop = 5, intRowHeigth = 0;
			
				foreach (ImageThumbnail objThumbnail in this)
					{ // Asigna el índice
							objThumbnail.Index = intIndex++;
						// Calcula la posición X e Y de la imagen
							if (intLeft + objThumbnail.Thumbnail.Width + intSpaceBetweenColumns > intWidth)
								{ // Si no cabe en la fila, se pone en la posición superior izquierda de la siguiente fila
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
						// Calcula la altura máxima de la fila
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
		///		Calcula la altura de las imágenes
		/// </summary>
		internal int GetMaxHeight(int intSpaceBetweenRows)
		{ int intMaxHeight = 0;
		
				// Calcula la altura máxima
					foreach (ImageThumbnail objThumbnail in this)
						if (objThumbnail.Y + objThumbnail.Thumbnail.Height + intSpaceBetweenRows > intMaxHeight)
							intMaxHeight = objThumbnail.Y + objThumbnail.Thumbnail.Height + intSpaceBetweenRows;
				// Devuelve la altura máxima
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
		///		Obtiene una colección con las imágenes seleccionadas
		/// </summary>
		internal ImageThumbnailsCollection GetSelectedItems()
		{ ImageThumbnailsCollection objColThumbnails = new ImageThumbnailsCollection();
		
				// Añade los elementos seleccionados
					foreach (ImageThumbnail objThumbnail in this)
						if (objThumbnail.Selected)
							objColThumbnails.Add(objThumbnail);
				// Devuelve la colección
					return objColThumbnails;
		}
	}
}
