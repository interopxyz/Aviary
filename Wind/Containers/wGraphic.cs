using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Wind.Presets;
using Wind.Types;

namespace Wind.Containers
{

    public class wGraphic
    {
        public string Layer = " ";
        public enum FillTypes { Solid, LinearGradient, RadialGradient, Pattern, Bitmap};

        public int CustomFills = 0;
        public int CustomFonts = 0;
        public int CustomStrokes = 0;

        public FillTypes FillType = FillTypes.Solid;

        public wImage FillBitmap = null;

        public wColor Background = new wColors().White();
        public wColor Foreground = new wColors().Black();
        public wColor StrokeColor = new wColors().Black();

        public Brush WpfFill = new SolidColorBrush(Colors.White);
        public DrawingBrush WpfPattern = new DrawingBrush();

        public string FillPattern = "Solid";

        public double[] StrokeWeight = new double[4] { 0, 0, 0, 0 };
        public double[] Radius = new double[4] { 0, 0, 0, 0 };
        public double[] Padding = new double[4] { 0, 0, 0, 0 };
        public double[] Margin = new double[4] { 0, 0, 0, 0 };

        public wGradient Gradient = new wGradient();

        public double Width = 0;
        public double Height = 0;
        public double Scale = 1.0;
        public bool PadRadius = false;

        public enum StrokeCaps { Flat, Round,Square,Triangle};
        public enum StrokeCorners { Bevel, Mitre, Round};

        public double[] StrokePattern = { 0 };

        public StrokeCaps StrokeCap = StrokeCaps.Flat;
        public StrokeCorners StrokeCorner = StrokeCorners.Bevel;

        public wFont FontObject = new wFont();

        public wGraphic()
        {
        }

        public wGraphic(wColor BackColor)
        {
            Background = BackColor;
            WpfFill = new SolidColorBrush(Background.ToMediaColor());
        }

        public wGraphic(wColor BackColor, wColor ForeColor, wColor LineColor, double Stroke)
        {
            Background = BackColor;
            Foreground = ForeColor;

            StrokeColor = LineColor;
            StrokeWeight[0] = Stroke;
            StrokeWeight[1] = Stroke;
            StrokeWeight[2] = Stroke;
            StrokeWeight[3] = Stroke;

            WpfFill = new SolidColorBrush(Background.ToMediaColor());
        }

        public wGraphic(wColor BackColor, wColor ForeColor, wColor LineColor, double Stroke, double WidthValue, double HeightValue)
        {
            Background = BackColor;
            Foreground = ForeColor;

            StrokeColor = LineColor;
            StrokeWeight[0] = Stroke;
            StrokeWeight[1] = Stroke;
            StrokeWeight[2] = Stroke;
            StrokeWeight[3] = Stroke;

            Width = WidthValue;
            Height = HeightValue;

            WpfFill = new SolidColorBrush(Background.ToMediaColor());
        }

        public void SetUniformStrokeWeight(double UniformWeight)
        {
            StrokeWeight[0] = UniformWeight;
            StrokeWeight[1] = UniformWeight;
            StrokeWeight[2] = UniformWeight;
            StrokeWeight[3] = UniformWeight;

        }

        public void SetPaddingFromCorners()
        {
            if (Radius[0] > Radius[3]) { Padding[0] = Radius[0]; } else { Padding[0] = Radius[3]; }
            if (Radius[1] > Radius[0]) { Padding[1] = Radius[1]; } else { Padding[1] = Radius[0]; }
            if (Radius[2] > Radius[1]) { Padding[2] = Radius[2]; } else { Padding[2] = Radius[1]; }
            if (Radius[3] > Radius[2]) { Padding[3] = Radius[3]; } else { Padding[3] = Radius[2]; }
        }

        public void SetUniformPadding(double UniformRadius)
        {
            Padding[0] = UniformRadius;
            Padding[1] = UniformRadius;
            Padding[2] = UniformRadius;
            Padding[3] = UniformRadius;
        }

        public void SetUniformMargin(double UniformRadius)
        {
            Margin[0] = UniformRadius;
            Margin[1] = UniformRadius;
            Margin[2] = UniformRadius;
            Margin[3] = UniformRadius;
        }

        public Thickness GetStroke()
        {
            return new Thickness(StrokeWeight[0], StrokeWeight[1], StrokeWeight[2], StrokeWeight[3]);
        }

        public Thickness GetMargin()
        {
            return new Thickness(Margin[0], Margin[1], Margin[2], Margin[3]);
        }

        public Thickness GetPadding()
        {
            return new Thickness(Padding[0], Padding[1], Padding[2], Padding[3]);
        }

        public CornerRadius GetCorner()
        {
            return new CornerRadius(Radius[0], Radius[1], Radius[2], Radius[3]);
        }

        public SolidColorBrush GetBackgroundBrush()
        {
            return new SolidColorBrush(Background.ToMediaColor());
        }

        public SolidColorBrush GetForegroundBrush()
        {
            return new SolidColorBrush(Foreground.ToMediaColor());
        }

        public SolidColorBrush GetStrokeBrush()
        {
            return new SolidColorBrush(StrokeColor.ToMediaColor());
        }

        public SolidColorBrush GetFontBrush()
        {
            return new SolidColorBrush(FontObject.FontColor.ToMediaColor());
        }

        public wGraphic BlackOutline()
        {
            return new wGraphic(new wColors().Transparent(), new wColors().Transparent(), new wColors().Black(), 1);
        }

        public wGraphic BlackFill()
        {
            return new wGraphic(new wColors().Black(), new wColors().Black(), new wColors().Transparent(), 0);
        }

    }
}
