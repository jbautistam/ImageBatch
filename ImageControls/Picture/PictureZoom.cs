using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Bau.Controls.ImageControls.Picture
{
	/// <summary>
	///		Control para mostrar una imagen y permitir el zoom sobre ella
	/// </summary>
	public class PictureZoom : ScrollableControl
	{ // Delegados públicos
			public delegate void ZoomChangedHandler(object objSender, EventPictureArgs objEventPictureArgs);
			public delegate void PositionChangedHandler(object objSender, EventPictureArgs objEventPictureArgs);
		// Eventos públicos
			public event ZoomChangedHandler ZoomChanged;
			public event PositionChangedHandler PositionChanged;
			public event EventHandler StartPageReached;
			public event EventHandler EndPageReached;
		// Enumerados públicos
			public enum ZoomMode
				{ Normal,
					FitWidth,
					FitHeight,
					FitPage
				}
		// Variables privadas
			private double dblZoom = 1;
			private Point pntZoom, pntStartDragging;
			private Image imgImage;
			private Bitmap bmpResized;
			private InterpolationMode intInterpolationMode = InterpolationMode.Bilinear;
			private bool blnIsDragging = false, blnShiftPressed = false, blnImageChanged = false;
			private ZoomMode intZoomMode = ZoomMode.Normal;

    public PictureZoom()
    {	// Cambia los estilos del control
				SetStyle(ControlStyles.DoubleBuffer, true);
				SetStyle(ControlStyles.UserPaint, true);
				SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
				SetStyle(ControlStyles.ContainerControl, false);
			// Indica que se debe hacer un scroll si la imagen es mayor que el tamaño de la página
				AutoScroll = true;
    }
		
		/// <summary>
		///		Genera la imagen redimensionada
		/// </summary>
		private void GenerateResizedImage()
		{	if (Picture != null)
				{	int intTop = 0, intLeft = 0;
					int intWidth = (int) (Picture.Width * Zoom);
					int intHeight = (int) (Picture.Height * Zoom);
						
						// Calcula la posición horizontal y vertical de la imagen (para centrarla sobre el control)
							if (intWidth < Width)
								intLeft = (Width - intWidth) / 2;
							if (intHeight < Height)
								intTop = (Height - intHeight) / 2;
						// Crea una nueva imagen redimensionada
							bmpResized = new Bitmap(intLeft + intWidth, intTop + intHeight);
						// Dibuja la imagen sobre la imagen redimensionada
							using (Graphics grpCanvas = Graphics.FromImage(bmpResized))
								{ // Cambia el tipo de interpolación
										grpCanvas.InterpolationMode = InterpolationMode;
									// Dibuja el fondo
										grpCanvas.FillRectangle(new SolidBrush(BackColor), 0, 0, bmpResized.Width, bmpResized.Height);
									// Dibuja la imagen
										grpCanvas.DrawImage(Picture, new Rectangle(intLeft, intTop, 
																															 bmpResized.Width - intLeft, bmpResized.Height - intTop),
																				new Rectangle(0, 0, Picture.Width, Picture.Height), 
																				GraphicsUnit.Pixel);
								}
				}
		}
		
		/// <summary>
		///		Repinta la imagen
		/// </summary>
		private void Repaint()
		{ // Genera la imagen redimensionada
				GenerateResizedImage();
			// Cambia el scroll
				if (bmpResized != null)
					AutoScrollMinSize = bmpResized.Size;
				else
					AutoScrollMinSize = new Size(4000, 4000);
			// Repinta la imagen
				Invalidate();
		}
		
		/// <summary>
		///		Calcula el zoom
		/// </summary>
		private void ComputeZoom()
		{ if (Picture != null && ZoomView != ZoomMode.Normal && Picture.Height != 0 && Picture.Width != 0)
				{	double dblZoomPercent = 100;
				
						// Calcula el porcentaje de zoom
							switch (ZoomView)
								{ case ZoomMode.FitHeight:
											dblZoomPercent = ComputeZoomFitHeight();
										break;
									case ZoomMode.FitWidth:
											dblZoomPercent = ComputeZoomFitWidth();
										break;
									case ZoomMode.FitPage:
											if (imgImage.Width < imgImage.Height)
												dblZoomPercent = ComputeZoomFitHeight();
											else
												dblZoomPercent = ComputeZoomFitWidth();
										break;
								}
						// Cambia el zoom
							Zoom = dblZoomPercent / 100;
				}
		}
		
		/// <summary>
		///		Calcula el zoom para ajustar al alto
		/// </summary>
		private double ComputeZoomFitHeight()
		{	if (Height > Picture.Height)
				return 100 * Picture.Height / Height;
			else
				return 100 * Height / Picture.Height;
		}
		
		/// <summary>
		///		Calcula el zoom para ajustar al alto
		/// </summary>
		private double ComputeZoomFitWidth()
		{	if (Width > Picture.Width)
				return 100 * Picture.Width / Width;
			else
				return 100 * (Width - 30) / Picture.Width;
		}
		
		/// <summary>
		///		Lanza el evento de cambio de posición
		/// </summary>
		private void RaiseEventPositionChanged()
		{ if (PositionChanged != null)
				PositionChanged(this, GetPictureEventArgs());
		}
		
		/// <summary>
		///		Lanza el evento de cambio de zoom
		/// </summary>
		private void RaiseEventZoomChanged()
		{ if (ZoomChanged != null)
				ZoomChanged(this, GetPictureEventArgs());
		}

		/// <summary>
		///		Lanza el evento de principio o fin de página
		/// </summary>
		private void RaiseEventChangePage(bool blnEndPage)
		{ if (blnEndPage)
				{	if (EndPageReached != null)
						EndPageReached(this, null);
				}
			else
				{ if (StartPageReached != null)
						StartPageReached(this, null);
				}
		}
		
		/// <summary>
		///		Obtiene los argumentos del evento
		/// </summary>
		private EventPictureArgs GetPictureEventArgs()
		{ return new EventPictureArgs(-AutoScrollPosition.Y, AutoScrollPosition.X,
																	Width, Height, Zoom);			
		}
	
		/// <summary>
		///		Rotación
		/// </summary>
		public void Rotate(RotateFlipType intType)
		{ if (imgImage != null && intType != RotateFlipType.RotateNoneFlipNone)
				{ Bau.Libraries.ImageFilters.Filters.FlipFilter objFilterFlip = new Bau.Libraries.ImageFilters.Filters.FlipFilter();
				
						// Inicializa el filtro
							objFilterFlip.RotateFlip = intType;
						// Ejecuta el filtro
							imgImage = objFilterFlip.ExecuteFilter(imgImage);
						// Repinta
							Repaint();
				}
		}

		/// <summary>
		///		Sobrescribe el evento OnMouseWheel
		/// </summary>
		protected override void OnMouseWheel(MouseEventArgs e)
		{	// Cambia el zoom o la posición de la imagen
				if (blnShiftPressed) // ... cambia el zoom
					{	// Recoge el punto donde estaba el ratón para hacer el zoom
							if (dblZoom == 1)
								pntZoom = new Point(e.X, e.Y);
						// Calcula el nivel de zoom
							if (e.Delta > 0)
								Zoom += 0.25;
							else if (e.Delta < 1)
								Zoom -= 0.25;
					}
				else // ... cambia la posición
					{	int intLastY = AutoScrollOffset.Y;
					
							// Cambia la posición si estamos moviendo hacia arriba o hacia abajo
								if (e.Delta > 0)
									AutoScrollOffset = new Point(AutoScrollOffset.X, AutoScrollOffset.Y + 10);
								else
									AutoScrollOffset = new Point(AutoScrollOffset.X, AutoScrollOffset.Y - 10);
							// Lanza el evento de cambio de posición
								RaiseEventPositionChanged();
							// Si se ha alcanzado el final, lanza el evento de fin de página
								if (e.Delta < 0 && (!base.VScroll ||
																		base.VerticalScroll.Value + base.VerticalScroll.LargeChange > base.VerticalScroll.Maximum))
									RaiseEventChangePage(true);
								else if (e.Delta > 0 && (!base.VScroll || base.VerticalScroll.Value == 0))
									RaiseEventChangePage(false);								
					}
      // Llama al tratamiento del evento base
				base.OnMouseWheel(e);
			// Redibuja la imagen
        Invalidate();
		}

		/// <summary>
		///		Sobrescribe el evento OnClick para activar la imagen cuando se pulsa sobre ella
		/// </summary>
		protected override void OnClick(EventArgs e)
		{ Form frmParent = Helper.clsFormUtils.GetParentForm(this);
		
				// Procesa el evento en el base
					base.OnClick(e);
				// Activa la imagen
					if (frmParent != null)
						frmParent.ActiveControl = this;
		}

		/// <summary>
		///		Sobrescribe el evento OnPaint para dibujar la imagen al nivel de zoom adecuado
		/// </summary>
    protected override void OnPaint(PaintEventArgs pe)
    {	// Si no se ha creado la imagen redimensionada, se crea
				if (bmpResized == null)
					GenerateResizedImage();
			// Si se ha cambiado la imagen, cambia la posición y el desplazamiento a la parte superior de la imagen
				if (blnImageChanged)
					{	AutoScrollPosition = new Point(0, 0);
						AutoScrollOffset = new Point(0, 0);
						blnImageChanged = false;
					}
			// Dibuja la imagen
				if (bmpResized != null)
					pe.Graphics.DrawImageUnscaled(bmpResized, AutoScrollPosition.X, AutoScrollPosition.Y, 
																				bmpResized.Width, bmpResized.Height);
			// Dibuja el rectángulo de selección
				if (RectangleSelected != Rectangle.Empty)
						pe.Graphics.DrawRectangle(Pens.GreenYellow, RectangleSelected);
				// pe.Graphics.DrawReversibleFrame(RectangleSelected, Color.Gray, FrameStyle.Dashed);
    }

		/// <summary>
		///		Sobrescribe el evento OnResize para redimensionar el control
		/// </summary>
		protected override void OnResize(EventArgs e)
		{	// Redibuja
				Repaint();
			// Llama al evento base
				base.OnResize(e);
		}
		
		/// <summary>
		///		Sobrescribe el evento OnMouseDown
		/// </summary>
		protected override void OnMouseDown(MouseEventArgs e)
		{	// Indica que ha comenzado a arrastar la imagen y recoge la posición
				if (e.Button == MouseButtons.Left)
					{ if (CanSelectRectangle)
							{	RectangleSelected = new Rectangle(e.Location, new Size(1, 1));
								Invalidate();
							}
						else
							{	blnIsDragging = true;
								pntStartDragging = new Point(AutoScrollPosition.X + e.X, AutoScrollPosition.Y + e.Y);
								RectangleSelected = Rectangle.Empty;
							}
					}
			// Llama al evento base
				base.OnMouseDown(e);
		}

		/// <summary>
		///		Sobrescribe el evento OnMouseMove
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{ // Mueve la imagen
				if (e.Button == MouseButtons.Left)
					{	if (blnIsDragging)
							AutoScrollPosition = new Point(e.X - pntStartDragging.X, e.Y - pntStartDragging.Y);
						else if (!RectangleSelected.IsEmpty) 
							{	// Asigna el rectángulo
									RectangleSelected = new Rectangle(RectangleSelected.X, RectangleSelected.Y, 
																										Math.Abs(RectangleSelected.Location.X - e.Location.X), 
																										Math.Abs(RectangleSelected.Location.Y - e.Location.Y));
								// Repinta
									Invalidate();
							}
					}
			// Llama al evento base
				base.OnMouseMove(e);
		}

		/// <summary>
		///		Sobrescribe el evento OnMouseUp
		/// </summary>
		protected override void OnMouseUp(MouseEventArgs e)
		{	// Indica que ha dejado de arrastrar la imagen
				blnIsDragging = false;
			// Llama al evento base
				base.OnMouseUp(e);
		}

		/// <summary>
		///		Sobrescribe el evento OnKeyDown
		/// </summary>
		protected override void OnKeyDown(KeyEventArgs e)
		{	// Recoge si se está pulsando la tecla de mayúsculas
				blnShiftPressed = e.Shift;
			// Llama al evento base
				base.OnKeyDown(e);
		}

		/// <summary>
		///		Sobrescribe el evento OnKeyUp
		/// </summary>
		protected override void OnKeyUp(KeyEventArgs e)
		{ // Recoge si se está pulsando la tecla de mayúsculas
				blnShiftPressed = e.Shift;
			// Llama al evento base
				base.OnKeyUp(e);
		}
		
    public Image Picture
    {	get { return imgImage; }
			set 
				{	// Asigna los valores
						imgImage = value;
						bmpResized = null;
						blnImageChanged = true;
					// Vacía el rectángulo seleccionado
						RectangleSelected = Rectangle.Empty;
					// Calcula el zoom si estamos en modo de ajuste
						ComputeZoom();
					// Repinta la imagen
						Repaint();
        }
    }
    
    public System.Drawing.Drawing2D.InterpolationMode InterpolationMode
    { get { return intInterpolationMode; }
			set 
				{ intInterpolationMode = value; 
					Repaint();
				}
    }
    
    /// <summary>
    ///		Zoom actual
    /// </summary>
    public double Zoom
    { get { return dblZoom; }
			set
				{ // Cambia el zoom actual
						dblZoom = value;
					// Normaliza el zoom
						if (dblZoom > 3)
							dblZoom = 3;
						else if (dblZoom <= 0)
							dblZoom = 0.25;
					// Lanza el evento
						RaiseEventZoomChanged();
					// Repinta
						Repaint();
				}
    }
    
    /// <summary>
    ///		Modo del zoom
    /// </summary>
    public ZoomMode ZoomView
    { get { return intZoomMode; }
			set 
				{ intZoomMode = value;
					ComputeZoom();
				}
    }

    /// <summary>
    ///		Indica si se puede seleccionar un elemento
    /// </summary>
    public bool CanSelectRectangle { get; set; }
    
    /// <summary>
    ///		Rectángulos seleccionado
    /// </summary>
    public Rectangle RectangleSelected { get; set; }
	}
}
