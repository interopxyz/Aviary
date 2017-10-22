using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using Wind.Types;

namespace Wind.Containers
{
    public class wGraphic
    {
        public wColor Background = new wColor().White();
        public wColor Foreground = new wColor().Black();
        public wColor StrokeColor = new wColor().Black();

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
        public bool PadRadius = false;

        public enum StrokeCaps { Flat, Round,Square,Triangle};
        public enum StrokeCorners { None, Sharp, Round, Mitre };

        public StrokeCaps StrokeCap = StrokeCaps.Flat;
        public StrokeCorners StrokeCorner = StrokeCorners.None;

        public wFont FontObject;

        public wGraphic()
        {
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
        
        public wGraphic BlackOutline()
        {
            return new wGraphic(new wColor().Transparent(), new wColor().Transparent(), new wColor().Black(), 1);
        }

        public wGraphic BlackFill()
        {
            return new wGraphic(new wColor().Black(), new wColor().Black(), new wColor().Transparent(), 0);
        }

    }
}
