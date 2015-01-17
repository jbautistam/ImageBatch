using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bau.Controls.ImageControls.Print
{
	/// <summary>
	///		Control para previsualizaci�n de los datos del <see cref="Bau.Controls.TableDataPrinter"/>
	/// </summary>
	public partial class ImagePrinterPreview : UserControl
	{ // Variables privadas
			private ImagePrinter objPrinterData = new ImagePrinter();
			private int intShowPages = 1;
			
		public ImagePrinterPreview()
		{	InitializeComponent();
		}
		
		/// <summary>
		///		Actualiza la previsualizaci�n
		/// </summary>
		public void RefreshPreview()
		{ prvControl.InvalidatePreview();
		}
		
		/// <summary>
		///		Abre el cuadro de configuraci�n de p�gina
		/// </summary>
		public void PageSetup()
		{ // Cambia al configuraci�n de p�gina
				objPrinterData.PageSetup();
			// Cambia el n�mero de p�ginas
				SetPageNumber("1");
			// Previsualiza
				RefreshPreview();
		}
		
		/// <summary>
		///		Imprime el documento
		/// </summary>
		public void Print()
		{ objPrinterData.Print();
		}
		
		/// <summary>
		///		Imprime con un cuadro de di�logo
		/// </summary>
		public void PrintWithDialog()
		{ objPrinterData.PrintWithDialog();
		}
		
		/// <summary>
		///		Cambia el n�mero de p�ginas que se muestran en el control de previsualizaci�n
		/// </summary>
		private void SetPageNumber(string strPages)
		{ int intPages;
		
				// Obtiene el n�mero de p�ginas
					if (int.TryParse(strPages, out intPages))
						intShowPages = intPages;
					if (intShowPages > 6)
						intShowPages = 6;
				// Cambia el n�mero de filas y columnas en el control de previsualizaci�n
					switch (intShowPages) 
						{	case 1:
							case 2:
							case 3:
									prvControl.Rows = 1;
									prvControl.Columns = intShowPages;
								break;
							default:
									prvControl.Rows = 2;
									prvControl.Columns = (intShowPages - 1) / 2 + 1;
								break;
					}
		}
		
		/// <summary>
		///		Muestra la p�gina anterior / siguiente
		/// </summary>
		private void ShowPage(bool blnPrevious)
		{ // Cambia la p�gina inicial
				if (blnPrevious && prvControl.StartPage > 0)
					prvControl.StartPage--;
				else if (!blnPrevious)
					prvControl.StartPage++;
			// Muestra la p�gina en la etiqueta
				lblPage.Text = (prvControl.StartPage + 1).ToString();
		}
		
		[Browsable(false)]
		public ImagePrinter Document
		{ get { return objPrinterData; }
			set 
				{ objPrinterData = value; 
					prvControl.Document = objPrinterData;
				}
		}

		private void mnuZoom200_Click(object sender, EventArgs e)
		{ prvControl.Zoom = 2.0;
		}

		private void mnuZoom100_Click(object sender, EventArgs e)
		{ prvControl.Zoom = 1.0;
		}

		private void mnuZoom75_Click(object sender, EventArgs e)
		{ prvControl.Zoom = 0.75;
		}

		private void mnuZoom50_Click(object sender, EventArgs e)
		{ prvControl.Zoom = 0.5;
		}

		private void mnuZoom25_Click(object sender, EventArgs e)
		{ prvControl.Zoom = 0.25;
		}

		private void mnuZoomAuto_Click(object sender, EventArgs e)
		{ prvControl.Zoom = 1.0;
			prvControl.AutoZoom = true;
		}

		private void cmdPageSetup_Click(object sender, EventArgs e)
		{ PageSetup();
		}

		private void cmdPrint_Click(object sender, EventArgs e)
		{ Print();
		}

		private void txtPages_TextChanged(object sender, EventArgs e)
		{ SetPageNumber(txtPages.Text);
		}

		private void cmdPageLeft_Click(object sender, EventArgs e)
		{ ShowPage(true);
		}

		private void cmdPageRight_Click(object sender, EventArgs e)
		{ ShowPage(false);
		}
	}
}