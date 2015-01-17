using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bau.Controls.ImageControls.Magnifier
{
	/// <summary>
	///		Componente para el manejo de la lupa
	/// </summary>
	public class MagnifierController
	{ // Variables privadas
			private int intWidth, intHeight;
			private float fltSpeed, fltZoom;
			
		public MagnifierController()
		{ MagnifierWidth = 300;
			MagnifierHeight = 300;
			SpeedFactor = 1;
			ZoomFactor = 2;
		}
		
		/// <summary>
		///		Muestra el control de zoom
		/// </summary>
		public void Show()
		{ frmMagnifier frmNewMagnifier = new frmMagnifier();
		
				// Asigna el padre
					frmNewMagnifier.MagnifierParent = this;
				// Muestra el formulario
					frmNewMagnifier.Show();
		}
		
		public int MagnifierWidth
		{ get { return intWidth; }
			set { intWidth = value; }
		}
		
		public int MagnifierHeight
		{ get { return intHeight; }
			set { intHeight = value; }
		}
		
		public float SpeedFactor
		{ get { return fltSpeed; }
			set { fltSpeed = value; }
		}

		public float ZoomFactor
		{ get { return fltZoom; }
			set { fltZoom = value; }
		}		
	}
}
