using System;
using System.Drawing;

namespace Bau.Libraries.ImageFilters.Filters
{
	/// <summary>
	///		Filtro base
	/// </summary>
	public abstract class BaseFilter : Interfaces.IFilter
  { 
		public BaseFilter()
		{ BackGroundColor = Color.FromArgb(0,0,0,0);
		}

    /// <summary>
    ///		Ejecuta el filtro sobre la imagen de entrada y devuelve el resultado
    /// </summary>
    public abstract Image ExecuteFilter(Image imgInput);

		/// <summary>
		///		Color de fondo
		/// </summary>
		public Color BackGroundColor { get; set; }
	}
}
