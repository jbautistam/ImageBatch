using System;
using System.Collections.Generic;
using System.Text;

namespace Bau.Controls.ImageControls.Picture
{
	/// <summary>
	///		Evento de una imagen
	/// </summary>
	public class EventPictureArgs : EventArgs
	{ // Variables privadas
			private double dblZoom;
			private	int intTop, intLeft;
			private int intWidth, intHeight;
			
		private EventPictureArgs() {}
		
		public EventPictureArgs(int intTop, int intLeft, 
														int intWidth, int intHeight, double dblZoom)
		{ Top = intTop;
			Left = intLeft;
			ImageVisibleWidth = intWidth;
			ImageVisibleHeight = intHeight;
			Zoom = dblZoom;
		}
		
		public double Zoom
		{ get { return dblZoom; }
			private set { dblZoom = value; }
		}
		
		public int Top
		{ get { return intTop; }
			private set { intTop = value; }
		}
		
		public int Left
		{ get { return intLeft; }
			private set { intLeft = value; }
		}
		
		public int ImageVisibleWidth
		{ get { return intWidth; }
			private set { intWidth = value; }
		}
		
		public int ImageVisibleHeight
		{ get { return intHeight; }
			private set { intHeight = value; }
		}
	}
}
