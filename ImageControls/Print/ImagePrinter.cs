using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace Bau.Controls.ImageControls.Print
{
	/// <summary>
	///		Imprime o previsualiza una serie de im�genes
	/// </summary>
  public class ImagePrinter : PrintDocument
  {	// Variables de control de impresi�n
			private int intPageNumber;
			private int intFirstPage = 1;
      private int intLastPage = 9999;
		// Lista de im�genes
			private List<Image> objColImages;

    public ImagePrinter()
    {	Images = new List<Image>();
    }
    
    /// <summary>
    ///		Muestra un cuadro de di�logo de configuraci�n de p�gina
    /// </summary>
    public void PageSetup()
    {	PageSetupDialog dlgPageSetup = new PageSetupDialog();
    
        dlgPageSetup.Document = this;
        dlgPageSetup.EnableMetric = true;
        dlgPageSetup.ShowDialog();
    }

    /// <summary>
    ///		Muestra una previsualizaci�n del documento
    /// </summary>
    public void PrintPreview()
    {	PrintPreviewDialog dlgPrintPreview = new PrintPreviewDialog();
    
        dlgPrintPreview.UseAntiAlias = true;
        dlgPrintPreview.Document = this;
        dlgPrintPreview.ShowDialog();
    }

    /// <summary>
    ///		Imprime el documento tras mostrar un cuadro de confirmaci�n
    /// </summary>
    public void PrintWithDialog()
    {	PrintDialog dlgPring = new PrintDialog();

				// Inicializa las propiedades    
					dlgPring.Document = this;
					dlgPring.AllowSelection = false;
					dlgPring.AllowSomePages = true;
				// Muestra el cuadro de di�logo est�ndar que permite seleccionar la impresora y la configuraci�n
					if (dlgPring.ShowDialog() == DialogResult.OK) 
						{	// Obtiene la primera y �ltima p�gina
								if (dlgPring.PrinterSettings.PrintRange == PrintRange.SomePages) 
									{	FirstPage = dlgPring.PrinterSettings.FromPage;
										LastPage = dlgPring.PrinterSettings.ToPage;
									} 
								else 
									{	FirstPage = 1;
										LastPage = Images.Count;
									}
							// Imprime
								Print();
						}
    }     

		/// <summary>
		///		Sobrescribe el evento OnBeginPrint
		/// </summary>
    override protected void OnBeginPrint(PrintEventArgs e)
    {	// Llama al evento base
        base.OnBeginPrint(e);
      // Inicializa la informaci�n de estado
				PageActual = 1;
    }

		/// <summary>
		///		Sobrescribe el evento OnPrintPage
		/// </summary>
    override protected void OnPrintPage(PrintPageEventArgs e)
    {	int intInter;
    
			// Llama al evento base
        base.OnPrintPage(e);
			// Normaliza las p�ginas de inicio y fin
				if (FirstPage < 1)
					FirstPage = 1;
				if (LastPage > Images.Count)
					LastPage = Images.Count;
				if (FirstPage > LastPage)
					{ intInter = FirstPage;
						FirstPage = LastPage;
						LastPage = intInter;
					}
			// Imprime la p�gina
				e.HasMorePages = PrintActualPage(e, PrintController.IsPreview);
    }

		/// <summary>
		///		Imprime la p�gina
		/// </summary>
		internal bool PrintActualPage(PrintPageEventArgs evntPrint, bool blnIsPreview)
		{ bool blnMustContinue = false;
			RectangleF rctPageBounds;
    
				// Calcula los rect�ngulos
					CalculateBounds(evntPrint, blnIsPreview, out rctPageBounds);
				// Si hay algo que imprimir...
					if (PageActual >= FirstPage  && PageActual <= LastPage)
						{ // Aplica el escalado
								evntPrint.Graphics.DrawImage(GetImageForPrint(Images[PageActual - 1], rctPageBounds), 
																						 rctPageBounds.Location.X, rctPageBounds.Location.Y,
																						 rctPageBounds.Width, rctPageBounds.Height);
							// Imprime la lista
								blnMustContinue = PageActual < LastPage;
							// Incrementa el n�mero de p�gina
								PageActual++;
						}
				// Devuelve el valor que indica si se debe continuar imprimiendo
					return PageActual < LastPage;
		}

    /// <summary>
    ///		Calcula el tama�o de la p�gina
    /// </summary>
    private void CalculateBounds(PrintPageEventArgs evntPrint, bool blnIsPreview, out RectangleF rctPageBounds)
    {	// Al imprimir en una impresora real no se tienen en cuenta los m�rgenes fijos
        if (blnIsPreview)
					rctPageBounds = (RectangleF) evntPrint.MarginBounds;
        else
					rctPageBounds = new RectangleF(evntPrint.MarginBounds.X - evntPrint.PageSettings.HardMarginX,		
																				 evntPrint.MarginBounds.Y - evntPrint.PageSettings.HardMarginY, 
																				 evntPrint.MarginBounds.Width, evntPrint.MarginBounds.Height);
    } 
		
		/// <summary>
		///		Genera la imagen redimensionada
		/// </summary>
		private Image GetImageForPrint(Image imgImage, RectangleF rctPageBounds)
		{	Image bmpResized = null;
		
				// Calcula la imagen ajustada a la p�gina
					if (imgImage != null)
						{	int intTop = 0, intLeft = 0;
							int intWidth = imgImage.Width;
							int intHeight = imgImage.Height;
							
								
								// Calcula la posici�n horizontal y vertical de la imagen (para centrarla sobre el control)
									if (intWidth < rctPageBounds.Width)
										intLeft = ((int) rctPageBounds.Width - intWidth) / 2;
									if (intHeight < rctPageBounds.Height)
										intTop = ((int) rctPageBounds.Height - intHeight) / 2;
								// Crea una nueva imagen redimensionada
									bmpResized = new Bitmap(intLeft + intWidth, intTop + intHeight);
								// Dibuja la imagen sobre la imagen redimensionada
									using (Graphics grpCanvas = Graphics.FromImage(bmpResized))
										{ // Cambia el tipo de interpolaci�n
												grpCanvas.InterpolationMode = InterpolationMode.Bilinear;
											// Dibuja la imagen
												grpCanvas.DrawImage(imgImage, new Rectangle(intLeft, intTop, 
																																	  bmpResized.Width - intLeft, bmpResized.Height - intTop),
																						new Rectangle(0, 0, imgImage.Width, imgImage.Height), 
																						GraphicsUnit.Pixel);
										}
								
						}
				// Devuelve la imagen
					return bmpResized;
		}

    /// <summary>
    ///		Primera p�gina del informe a imprimir
    /// </summary>
    [Category("Behaviour"),
     Description("Primera p�gina a imprimir"),
     DefaultValue(0)]
    public int FirstPage
    {	get { return intFirstPage; }
			set { intFirstPage = value; }
    }

    /// <summary>
    ///		Ultima p�gina del informe a imprimir
    /// </summary>
    [Category("Behaviour"),
     Description("Ultima p�gina a imprimir"),
     DefaultValue(9999)]
    public int LastPage
    {	get { return intLastPage; }
			set { intLastPage = value; }
    }
 
		/// <summary>
		///		P�gina que se est� imprimiendo actualmente
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]   
		public int PageActual
		{ get { return intPageNumber; }
			set { intPageNumber = value; }
		}
		
    /// <summary>
    ///		Lista de im�genes
    /// </summary>
    [Browsable(true)]
    public List<Image> Images
    { get { return objColImages; }
			set { objColImages = value; }
    }
  }
}