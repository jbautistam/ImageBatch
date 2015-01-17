using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;

namespace Bau.Libraries.ImageFilters.Filters
{
  /// <summary>
  /// Resize filter class. A simple functionality to resize images
  /// </summary>
  public class ResizeFilter : BaseFilter
  {	// Variables privadas
			private int _width = 50;
			private int _height = 50;
			private float _aspectRatio;
			private bool _keepAspectRatio = false;
			private bool _lockRatioByHeight = false;
			private bool _lockRatioByWidth = true;
			InterpolationMode _interpolationType = InterpolationMode.Bicubic;
		// Constantes públicas
			public static string WIDTH_PARAM_TOKEN = "width";
			public static string HEIGHT_PARAM_TOKEN = "height";
			public static string INTERPOLATION_TYPE_TOKEN = "interpolationType";

    /// <summary>
    /// Executes this filter on the input image and returns the result
    /// </summary>
    /// <param name="inputImage">input image</param>
    /// <returns>transformed image</returns>
    /// <example>
    /// <code>
    /// Image transformed;
    /// ResizeFilter resize = new ResizeFilter();
    /// resize.Width = 100;
    /// resize.Height = 70;
    /// transformed = resize.ExecuteFilter(myImg);
    /// </code>
    /// </example>
    public override Image ExecuteFilter(Image inputImage)
    {
      if (_keepAspectRatio)
      {
        _aspectRatio = CalcAspectRatio(inputImage.Width, inputImage.Height);
        if (_lockRatioByHeight)
          _width = (int)(_aspectRatio * Height);
        else
          _height = (int)(_width / _aspectRatio);
      }
      if (_height == 0)
				_height = _width;
      Bitmap result = new Bitmap(_width, _height);
      Graphics g = Graphics.FromImage(result);
      g.InterpolationMode = _interpolationType;
      g.DrawImage(inputImage, 0, 0, _width, _height);
      return result;
    }

    /// <summary>
    /// Demonostration Function. Calls the filter with default properties
    /// To be used for presentation purposes.
    /// </summary>
    /// <param name="inputImage"></param>
    /// <returns>Transformed image</returns>
    /// <summary>
    public Image ExecuteFilterDemo(Image inputImage, NameValueCollection filterProperties)
    { return this.ExecuteFilter(inputImage);
    }

    /// <summary>
    /// Calculates the picture aspect ratio
    /// by dividing the width by the height
    /// </summary>
    /// <param name="width">image width</param>
    /// <param name="height">image height</param>
    /// <returns>aspect ratio</returns>
    private float CalcAspectRatio(int width, int height)
    { if (height != 0)
        return (float)width / (float)height;
      else
        return 0;
    }

    /// <summary>
    /// Image width in pixels
    /// </summary>
    public int Width
    { get { return _width; }
			set 
				{	_width = value;
					_lockRatioByWidth = true;
					_lockRatioByHeight = false;
      }      
    }
    
    /// <summary>
    /// Image height in pixels
    /// </summary>
    public int Height
    { get { return _height; }
			set
				{ _height = value;
					_lockRatioByWidth = false;
					_lockRatioByHeight = true;
				}      
    }
    
    /// <summary>
    /// Scaling interpolation mode . can be HighQualityBicubic, Bilinear, etc.
    /// </summary>
    public InterpolationMode InterpolationType
    { get { return _interpolationType; }
			set { _interpolationType = value; }
    }
    
    /// <summary>
    /// Determines whether to keep the original aspect ratio
    /// false by default. Should be turned on before setting the width or height to 
    /// keep the original ratio
    /// </summary>
    public bool KeepAspectRatio
    { get { return _keepAspectRatio; }
      set { _keepAspectRatio = value; }
    }

    /// <summary>
    /// Locks the aspect ratio while keeping the height as reference.
    /// Meaning that setting a new height will calc the width according to the original ratio
    /// </summary>
    public bool LockRatioByHeight
    { get { return _lockRatioByHeight; }
      set
      { _lockRatioByHeight = value;
        _keepAspectRatio = true;
        _lockRatioByWidth = !_lockRatioByHeight;
      }
    }

    /// <summary>
    /// Locks the aspect ratio while keeping the width as reference.
    /// Meaning that setting a new width will calc the height according to the original ratio
    /// </summary>
    public bool LockRatioByWidth
    { get { return _lockRatioByWidth; }
      set
      {
        _lockRatioByWidth = value;
        _keepAspectRatio = true;
        _lockRatioByHeight = !_lockRatioByWidth;
      }
    }
  }
}
