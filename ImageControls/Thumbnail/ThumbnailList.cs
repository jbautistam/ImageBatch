using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Bau.Controls.ImageControls.Thumbnail
{
	/// <summary>
	///		Control para mostrar una imagen y permitir el zoom sobre ella
	/// </summary>
	public class ThumbnailList : ScrollableControl
	{ // Enumerados públicos
			public enum ModeThumblist
				{ Normal,
					Extended
				}
		// Eventos públicos
			public event EventHandler SelectedIndexChanged;
		// Variables privadas
			private InterpolationMode intInterpolationMode = InterpolationMode.Bilinear;
			private ImageThumbnailsCollection objColThumbnails = new ImageThumbnailsCollection();
			private int intWidthThumb = 100;
			private int intPaddingWidth = 10, intPaddingHeight = 10;
			private int intSpaceBetweenColumns = 5, intSpaceBetweenRows = 10;
			private int intBeginUpdate = 0;
			private ModeThumblist intMode = ModeThumblist.Normal;
			private Color clrBackColor = Color.Blue;
			private bool blnEnsureVisible = true;

    public ThumbnailList()
    {	// Cambia los estilos del control
				SetStyle(ControlStyles.DoubleBuffer, true);
				SetStyle(ControlStyles.UserPaint, true);
				SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
				SetStyle(ControlStyles.ContainerControl, false);
			// Añade el manejador de eventos a la colección de thumbnails
				objColThumbnails.OnRepaint += new EventHandler(objColThumbnails_OnRepaint);
			// Indica que se debe hacer un scroll si el tamaño de las imágenes es mayor que el tamaño de la página
				AutoScroll = true;
    }
		
		/// <summary>
		///		Rutina que indica que se comienza a modificar la lista (no hace falta repintarla)
		/// </summary>
		public void BeginUpdate()
		{ intBeginUpdate++;
		}
		
		/// <summary>
		///		Rutina que indica que se ha finalizado de modificar la lista (no hace falta repintarla)
		/// </summary>
		public void EndUpdate()
		{ intBeginUpdate--;
			Repaint();
		}
		
		/// <summary>
		///		Limpia los thumbnails de la lista
		/// </summary>
		public void Clear()
		{ objColThumbnails.Clear();
		}
		
		/// <summary>
		///		Selecciona un elemento
		/// </summary>
		public void Select(int intThumbIndex)
		{ // Comienza la modificación
				BeginUpdate();
			// Selecciona el elemento
				objColThumbnails.Select(intThumbIndex);
			// Finaliza la modificación (para que la siguiente instrucción redibuje
				EndUpdate();
			// Posiciona el control (no sé porqué hay que hacerlo dos veces para que el scroll vertical cambie)
				if (EnsureVisible && intThumbIndex >= 0 && intThumbIndex < objColThumbnails.Count &&
						objColThumbnails[intThumbIndex].Y < VerticalScroll.Maximum)
					{	VerticalScroll.Value = objColThumbnails[intThumbIndex].Y;
						VerticalScroll.Value = objColThumbnails[intThumbIndex].Y;
					}
		}
		
		/// <summary>
		///		Actualiza la lista
		/// </summary>
		public new void Refresh()
		{ Repaint();
		}
		
		/// <summary>
		///		Repinta la imagen
		/// </summary>
		private void Repaint()
		{ // Recalcula los thumbnails
				objColThumbnails.ComputeThumbnails(intWidthThumb);
			// Coloca las imágenes
				objColThumbnails.ComputePositions(Width, 2 * ThumbnailPaddingWidth + SpaceBetweenColumns, 
																					2 * ThumbnailPaddingHeight + SpaceBetweenRows);
			// Cambia el tamaño del scroll mínimo
				AutoScrollMinSize = new Size(0, objColThumbnails.GetMaxHeight(2 * ThumbnailPaddingHeight + SpaceBetweenRows));
			// Repinta
				Invalidate();
		}
		
		/// <summary>
		///		Sobrescribe el evento OnMouseWheel
		/// </summary>
		protected override void OnMouseWheel(MouseEventArgs e)
		{	int intLastY = AutoScrollOffset.Y;
		
				// Cambia la posición si estamos moviendo hacia arriba o hacia abajo
					if (e.Delta > 0)
						AutoScrollOffset = new Point(AutoScrollOffset.X, AutoScrollOffset.Y + 10);
					else
						AutoScrollOffset = new Point(AutoScrollOffset.X, AutoScrollOffset.Y - 10);
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
		///		Sobrescribe el evento OnPaint para dibujar las imágenes 
		/// </summary>
    protected override void OnPaint(PaintEventArgs pe)
    {	if (intBeginUpdate <= 0)
				{	// Cambia el tipo de interpolación
						if (pe.Graphics.InterpolationMode != InterpolationMode)
							pe.Graphics.InterpolationMode = InterpolationMode;
					// Dibuja las imágenes
						foreach (ImageThumbnail objThumbnail in objColThumbnails)
							DrawThumbnail(pe.Graphics, objThumbnail);
				}
    }

		/// <summary>
		///		Dibuja una imagen
		/// </summary>
		private void DrawThumbnail(Graphics grpCanvas, ImageThumbnail objThumbnail)
		{	if (objThumbnail.Position.Y + objThumbnail.Thumbnail.Height - VerticalScroll.Value >= 0 &&
					objThumbnail.Position.Y - VerticalScroll.Value <= Height)
				{	Color clrBackGround = BackColor;
					Rectangle rctThumbnail = new Rectangle(objThumbnail.Position.X, objThumbnail.Position.Y - VerticalScroll.Value,
																								 objThumbnail.Thumbnail.Width + 2 * ThumbnailPaddingWidth, 
																								 objThumbnail.Thumbnail.Height + 2 * ThumbnailPaddingHeight);
					Rectangle rctImage = new Rectangle(objThumbnail.Position.X + (rctThumbnail.Width - objThumbnail.Thumbnail.Width) / 2, 
																						 objThumbnail.Position.Y - VerticalScroll.Value + 
																								(rctThumbnail.Height - objThumbnail.Thumbnail.Height) / 2,
																						 objThumbnail.Thumbnail.Width, objThumbnail.Thumbnail.Height);
				
						// Obtiene el color del fondo
							if (objThumbnail.Selected)
								clrBackGround = BackColorSelected;
						// Dibuja el fondo
							using (SolidBrush brsFill = new SolidBrush(clrBackGround))
								grpCanvas.FillRectangle(brsFill, rctThumbnail);
						// Dibuja la imagen
							grpCanvas.DrawImageUnscaled(objThumbnail.Thumbnail, rctImage);
						// Dibuja un borde alrededor de la imagen
							using (Pen penBorder = new Pen(Color.Black))
								grpCanvas.DrawRectangle(penBorder, rctThumbnail);
				}
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
					{	// Indica que comienzan modificaciones en la lista
							BeginUpdate();
						// Selecciona una imagen
							objColThumbnails.Select(e.X, e.Y + VerticalScroll.Value);
						// Lanza el evento
							if (SelectedIndexChanged != null)
								SelectedIndexChanged(this, EventArgs.Empty);
						// Indica que han finalizado las modificaciones en la lista
							EndUpdate();
						// Repinta
							Repaint();
					}
			// Llama al evento base
				base.OnMouseDown(e);
		}

		/// <summary>
		///		Sobrescribe el evento OnScroll
		/// </summary>
		protected override void OnScroll(ScrollEventArgs se)
		{ // Repinta
				Invalidate();
			// Llama al evento base
				base.OnScroll(se);
		}    

		/// <summary>
		///		Traba el evento de repintado
		/// </summary>
		private void objColThumbnails_OnRepaint(object sender, EventArgs e)
		{ Repaint();
		}
		
    public System.Drawing.Drawing2D.InterpolationMode InterpolationMode
    { get { return intInterpolationMode; }
			set 
				{ intInterpolationMode = value; 
					Repaint();
				}
    }

		[Description("Ancho de los thumbnails"), DefaultValue(100)]
    public int ThumbnailWidth
    { get { return intWidthThumb; }
			set
				{ // Normaliza el ancho
						if (value < 50)
							value = 100;
						else if (value > 500)
							value = 500;
					// Cambia el ancho
						if (intWidthThumb != value)
							{ intWidthThumb = value;
								Repaint();
							}
				}
    }

		/// <summary>
		///		Ancho de espaciado entre el borde y la imagen
		/// </summary>
		[DefaultValue(10)]    
		public int ThumbnailPaddingWidth
		{ get { return intPaddingWidth; }
			set 
				{ intPaddingWidth = value; 
					Repaint();
				}
		}

		/// <summary>
		///		Alto de espaciado entre el borde y la imagen
		/// </summary>
		[DefaultValue(10)]
		public int ThumbnailPaddingHeight
		{ get { return intPaddingHeight; }
			set 
				{ intPaddingHeight = value; 
					Repaint();
				}
		}
		
		/// <summary>
		///		Espacio entre columnas
		/// </summary>
		[DefaultValue(5)]    
		public int SpaceBetweenColumns
		{ get { return intSpaceBetweenColumns; }
			set 
				{ intSpaceBetweenColumns = value; 
					Repaint();
				}
		}

		/// <summary>
		///		Espacio entre filas
		/// </summary>
		[DefaultValue(10)]
		public int SpaceBetweenRows    
		{ get { return intSpaceBetweenRows; }
			set 
				{ intSpaceBetweenRows = value; 
					Repaint();
				}
		}

		// [DefaultValue(System.Drawing.Color.Blue)]
		public Color BackColorSelected
		{ get { return clrBackColor; }
			set { clrBackColor = value; }
		}
		
    /// <summary>
    ///		Colección de imágenes
    /// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public ImageThumbnailsCollection Thumbnails
    { get { return objColThumbnails; }
    }

		[Browsable(true)]
		public ModeThumblist Mode
		{ get { return intMode; }
			set { intMode = value; }
		}    

		/// <summary>
		///		Indica si se debe mostrar siempre la imagen seleccionada
		/// </summary>
		[DefaultValue(true)]
		public bool EnsureVisible
		{ get { return blnEnsureVisible; }
			set { blnEnsureVisible = value; }
		}		
		
    /// <summary>
    ///		Obtiene la colección de imágenes seleccionadas
    /// </summary>
    public ImageThumbnailsCollection SelectedItems
    { get { return objColThumbnails.GetSelectedItems(); }
    }
	}
}
