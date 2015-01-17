using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Bau.Libraries.ImageFilters.Filters
{
  /// <summary>
  /// Text Water Mark Filter .Can be used to add a watermark text to the image.
  /// The weater mark can be both horizontaly and verticaly aligned.
  /// Also provides simple captions functionality (Simple text on an image)
  /// </summary>
  public class TextWatermarkFilter : BaseWaterMarkFilter
  { 
  	public TextWatermarkFilter()
		{ FontName = "Arial";
			TextSize = 10;
			CaptionAlpha = 75;
			CaptionColor = Color.White;
			Caption = "Caption";
			AutomaticTextSize = false;			
		}
		
    /// <summary>
    /// Executes this filter on the input image and returns the image with the WaterMark
    /// </summary>
    /// <param name="rawImage">input image</param>
    /// <returns>transformed image</returns>
    /// <example>
    /// <code>
    /// Image transformed;
    /// TextWatermarkFilter textWaterMark = new TextWatermarkFilter();
    /// textWaterMark.Caption = "Pitzi";
    /// textWaterMark.TextSize = 20;
    /// textWaterMark.AutomaticTextSize = false;
    /// textWaterMark.Valign = TextWatermarkFilter.VAlign.Right;
    /// textWaterMark.Halign = TextWatermarkFilter.HAlign.Bottom;
    /// textWaterMark.CaptionColor = Color.Red;
    /// transformed = textWaterMark.ExecuteFilter(myImg);
    /// </code>
    /// </example>
    public override Image ExecuteFilter(Image rawImage)
    {
      
      _width = rawImage.Width;
      _height = rawImage.Height;

      //create a Bitmap the Size of the original photograph
      Bitmap bmPhoto = new Bitmap(rawImage.Width, rawImage.Height, PixelFormat.Format24bppRgb);

      bmPhoto.SetResolution(rawImage.HorizontalResolution, rawImage.VerticalResolution);

      //load the Bitmap into a Graphics object 
      Graphics grPhoto = Graphics.FromImage(bmPhoto);

      //Set the rendering quality for this Graphics object
      grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

      //Draws the photo Image object at original size to the graphics object.
      grPhoto.DrawImage(
        rawImage,                               // Photo Image object
        new Rectangle(0, 0, _width, _height), // Rectangle structure
        0,                                      // x-coordinate of the portion of the source image to draw. 
        0,                                      // y-coordinate of the portion of the source image to draw. 
        _width,                                // Width of the portion of the source image to draw. 
        _height,                               // Height of the portion of the source image to draw. 
        GraphicsUnit.Pixel);                    // Units of measure 


      //-------------------------------------------------------
      //Set up the automatic font settings
      //-------------------------------------------------------
      int[] sizes = new int[] { 128, 64, 32, 16, 14, 12, 10, 8, 6, 4 };

      Font crFont = null;
      SizeF crSize = new SizeF();


      if (AutomaticTextSize)
      {
        //If automatic sizing is turned on 
        //loop through the defined sizes checking the length of the caption string string
        for (int i = 0; i < sizes.Length; i++)
        {
          //set a Font object to FontName (i)pt, Bold
          crFont = new Font(FontName, sizes[i], FontStyle.Bold);
          //Measure the Copyright string in this Font
          crSize = grPhoto.MeasureString(Caption, crFont);
          if ((ushort)crSize.Width < (ushort)_width)
            break;
        }
      }
      else
      {
        crFont = new Font(FontName, TextSize, FontStyle.Bold);
        
      }
      crSize = grPhoto.MeasureString(Caption, crFont);

      //Since all photographs will have varying heights, determine a 
      //position 5% from the bottom of the image
      int yPixelsMargin = (int)(_height * .0002);

      float yPosFromBottom;
      float xPositionFromLeft;
      CalcDrawPosition((int)crSize.Width,(int)crSize.Height, yPixelsMargin, out yPosFromBottom, out xPositionFromLeft);


      //Define the text layout by setting the text alignment to centered
      StringFormat StrFormat = new StringFormat();
      //StrFormat.Alignment = StringAlignment.Near;

      //define a Brush which is semi trasparent black (Alpha set to 153)
      SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(CaptionAlpha, 0, 0, 0));

      //Draw the Copyright string
      grPhoto.DrawString(Caption,                 //string of text
        crFont,                                   //font
        semiTransBrush2,                           //Brush
        new PointF(xPositionFromLeft + 1, yPosFromBottom + 1),  //Position
        StrFormat);

      //define a Brush which is semi trasparent white (Alpha set to 153)
      
      SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(CaptionAlpha, CaptionColor.R,CaptionColor.G,CaptionColor.B));

      //Draw the Copyright string a second time to create a shadow effect
      //Make sure to move this text 1 pixel to the right and down 1 pixel
      grPhoto.DrawString(Caption,                 //string of text
        crFont,                                   //font
        semiTransBrush,                           //Brush
        new PointF(xPositionFromLeft, yPosFromBottom),  //Position
        StrFormat);                               //Text alignment



      grPhoto.Dispose();

      return bmPhoto;
    }

		/// <summary>
		///		Nombre de la fuente
		/// </summary>
		public string FontName { get; set; }
		
		/// <summary>
		///		Tamaño de la fuente
		/// </summary>
    public int TextSize { get; set; }

		/// <summary>
		///		Indica si se debe buscar el tamaño automáticamente
		/// </summary>
    public bool AutomaticTextSize { get; set; }

		/// <summary>
		///		Alpha del título
		/// </summary>
    public int CaptionAlpha { get; set; }

		/// <summary>
		///		Color del título
		/// </summary>
    public Color CaptionColor { get; set; }

		/// <summary>
		///		Título
		/// </summary>
    public string Caption { get; set; }
  }
}
