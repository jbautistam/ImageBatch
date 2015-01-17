using System;
using System.Windows.Forms;

namespace Bau.Controls.ImageControls.Helper
{
	/// <summary>
	///		Clase de utilidad para los controles
	/// </summary>
	internal class clsFormUtils
	{
		/// <summary>
		///		Busca el formulario padre del control
		/// </summary>
		internal static Form GetParentForm(Control ctlControl)
		{ Control ctlParent = ctlControl.Parent;
		
				// Busca el formulario padre
					do
						{ if (!(ctlParent is Form))
								ctlParent = ctlParent.Parent;
						}
					while (ctlParent != null && !(ctlParent is Form));
				// Devuelve el formulario padre (si hay alguno)
					if (ctlParent is Form)
						return ctlParent as Form;
					else
						return null;
		}
	}
}
