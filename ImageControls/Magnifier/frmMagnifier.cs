using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace Bau.Controls.ImageControls.Magnifier
{
	/// <summary>
	///		Formulario para mostrar la lupa
	/// </summary>
  internal partial class frmMagnifier : Form
  { // Delegados privados
			private delegate void RepositionAndShowDelegate();
		// Variables privadas
			private MagnifierController ctlParent;
      private Timer tmrMovement;
      private Image mImageMagnifier;
      private Image mBufferImage = null;
      private Image mScreenImage = null;
      private Point mStartPoint;
      private PointF mTargetPoint;
      private PointF mCurrentPoint;
      private Point mOffset;
      private bool mFirstTime = true;
      private Point mLastMagnifierPosition = Cursor.Position;			
      private Timer tmrCapture = new Timer();
      private bool blnCaptured = false;
      	
    public frmMagnifier()
    {	// Inicializa el componente
        InitializeComponent();
    }
    
    /// <summary>
    ///		Inicializa el formulario
    /// </summary>
    private void InitForm()
    {	// Inicializa el formulario
				Hide();
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        TopMost = true;
        Width = MagnifierParent.MagnifierWidth;
        Height = MagnifierParent.MagnifierHeight;
      // Crea el formulario circular
				CreateFormShape();
			// Crea la imagen
				mImageMagnifier = Properties.Resources.MagnifierGlass;
			// Crea el temporizador
        tmrMovement = new Timer();
        tmrMovement.Enabled = true;
        tmrMovement.Interval = 20;
        tmrMovement.Tick += new EventHandler(HandleTimer);
      // Inicializa el temporizador de captura
				tmrCapture.Enabled = true;
				tmrCapture.Interval = 100;
				tmrCapture.Tick += new EventHandler(tmrCapture_Tick);
			// Crea la imagen de la pantalla
        mScreenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			// Inicializa la posición
        mStartPoint = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
        mTargetPoint = Cursor.Position;
    }

		/// <summary>
		///		Crea el formulario circular
		/// </summary>
		private void CreateFormShape()
		{	GraphicsPath gp = new GraphicsPath();
      gp.AddEllipse(ClientRectangle);
      Region = new Region(gp);
		}

		/// <summary>
		///		Evento de captura de imagen
		/// </summary>
		private void tmrCapture_Tick(object sender, EventArgs e)
		{	if (!blnCaptured) // ... es la primera vez
				{ // Desactiva el temporizador de captura
						tmrCapture.Enabled = false;
					// Reposiciona el formulario y lo muestra
						RepositionAndShow();
				}
			else
				CaptureImage();
		}

		/// <summary>
		///		Captura la imagen
		/// </summary>
		private void CaptureImage()
		{ // Captura la imagen
				using (Graphics g = Graphics.FromImage(mScreenImage))
					{ g.CopyFromScreen(0, 0, 0, 0, new Size(mScreenImage.Width, mScreenImage.Height));
						g.Dispose();                
					}
			// Indica que ha capturado la imagen
				blnCaptured = true;
				Capture = true;
		}

		/// <summary>
		///		Recoloca el formulario
		/// </summary>
    private void RepositionAndShow()
    {	if (InvokeRequired)
        Invoke(new RepositionAndShowDelegate(RepositionAndShow));
      else
				{ // Captura la imagen
						CaptureImage();
	          
						mOffset = new Point(Width / 2 - Cursor.Position.X, Height / 2 - Cursor.Position.Y);
						mCurrentPoint = PointToScreen(new Point(Cursor.Position.X + mOffset.X, Cursor.Position.Y + mOffset.Y));
						mTargetPoint = mCurrentPoint;
						tmrMovement.Enabled = true;
					// Muestra el formulario
						Show();
				}
    }

    private void HandleTimer(object sender, EventArgs e)
    {
        float dx = MagnifierParent.SpeedFactor * (mTargetPoint.X - mCurrentPoint.X);
        float dy = MagnifierParent.SpeedFactor * (mTargetPoint.Y - mCurrentPoint.Y);

        if (mFirstTime)
        {
            mFirstTime = false;

            mCurrentPoint.X = mTargetPoint.X;
            mCurrentPoint.Y = mTargetPoint.Y;

            Left = (int)mCurrentPoint.X - Width / 2;
            Top = (int)mCurrentPoint.Y - Height / 2;
            
            return;
        }

        mCurrentPoint.X += dx;
        mCurrentPoint.Y += dy;

        if (Math.Abs(dx) < 1 && Math.Abs(dy) < 1)
        {
            tmrMovement.Enabled = false;
        }
        else
        {
            // Update location
            Left = (int)mCurrentPoint.X - Width / 2;
            Top = (int)mCurrentPoint.Y - Height / 2;
            mLastMagnifierPosition = new Point((int)mCurrentPoint.X, (int)mCurrentPoint.Y);
        }

        Refresh();
    }

		/// <summary>
		///		Cierra el formulario
		/// </summary>
		private void CloseForm()
		{ // Desactiva los temporizadores
				tmrCapture.Enabled = false;
				tmrMovement.Enabled = false;
			// Libera la memoria
				mScreenImage.Dispose();
			// Cierra el formulario
				Close();
				Dispose();
		}
		
		/// <summary>
		///		Sobrescribe el evento OnMouseDown
		/// </summary>
    protected override void OnMouseDown(MouseEventArgs e)
    { CloseForm();
    }

		/// <summary>
		///		Sobrescribe el evento OnMouseMove
		/// </summary>
    protected override void OnMouseMove(MouseEventArgs e)
    {	mTargetPoint = PointToScreen(new Point(e.X, e.Y));
      tmrMovement.Enabled = true;
    }
		
		/// <summary>
		///		Sobrescribe el evento OnMouseWheel
		/// </summary>
		protected override void OnMouseWheel(MouseEventArgs e)
		{	int intIncrement = 0;
			// Cambia el tamaño si estamos moviendo hacia arriba o hacia abajo
				if (e.Delta > 0 && Width < 500)
					intIncrement = 10;
				else if (e.Delta < 0 && Width > 200)
					intIncrement = -10;
			// Cambia el tamaño
				if (intIncrement != 0)
					{ Width += intIncrement;
						Height += intIncrement;
						CreateFormShape();
					}
      // Llama al tratamiento del evento base
				base.OnMouseWheel(e);
			// Redibuja la imagen
        Invalidate();
		}
		
		/// <summary>
		///		Sobrescribe el evento OnPaintBackground
		/// </summary>
    protected override void OnPaintBackground(PaintEventArgs e)
    {	// No pinta el fondo. Necesario para el doble buffer
    }

		/// <summary>
		///		Sobrescribe el evento OnPaint
		/// </summary>
    protected override void OnPaint(PaintEventArgs e)
    { if (blnCaptured)
				{
        if (mBufferImage == null)
        {
            mBufferImage = new Bitmap(Width, Height);
        }
        Graphics bufferGrf = Graphics.FromImage(mBufferImage);

        Graphics g;

        g = bufferGrf;

        if (mScreenImage != null)
        {
            Rectangle dest = new Rectangle(0, 0, Width, Height);
            int w = (int)(Width / MagnifierParent.ZoomFactor);
            int h = (int)(Height / MagnifierParent.ZoomFactor);
            int x = Left - w / 2 + Width / 2;
            int y = Top - h / 2 + Height / 2;

            g.DrawImage(
                mScreenImage,
                dest,
                x, y,
                w, h,
                GraphicsUnit.Pixel);
        }

        if (mImageMagnifier != null)
        {
            g.DrawImage(mImageMagnifier, 0, 0, Width, Height);
        }

        e.Graphics.DrawImage(mBufferImage, 0, 0, Width, Height);
        }
    }
      
		public MagnifierController MagnifierParent
		{ get { return ctlParent; }
			set { ctlParent = value; }
		}

		private void frmMagnifier_Load(object sender, EventArgs e)
		{ InitForm();
		}
  }
}