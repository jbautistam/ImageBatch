using System;

namespace Bau.Libraries.ImageFilters.Interfaces
{
	/// <summary>
	///		Interface que deben cumplir los filtros
	/// </summary>
	public interface IFilter
	{  
		/// <summary>
		///		Rutina para la ejecución de un filtro sobre una imagen
		/// </summary>
		System.Drawing.Image ExecuteFilter(System.Drawing.Image inputImage);
	}
}
