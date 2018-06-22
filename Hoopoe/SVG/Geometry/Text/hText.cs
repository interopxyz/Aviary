using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves;
using Wind.Geometry.Shapes;
using Wind.Geometry.Vectors;
using Wind.Types;

namespace Hoopoe.SVG.Geometry.Text
{
    public class hText : hCurve
    {

        public string Text = "";
        public wFont Font = new wFont();
        public double Angle = 0;
        public wPoint Origin = new wPoint();
        public enum hAlign {start, middle, end };
        public enum vAlign { baseline, middle, central };

        public hAlign HorizontalAlignment = hAlign.start;
        public vAlign VerticalAlignment = vAlign.baseline;

        public enum fStyle { normal, italic};
        public enum fWeight { normal, bold}

        public fStyle FontItalic = fStyle.normal;
        public fWeight FontBold = fWeight.normal;

        public hText()
        {

        }

        public hText(wTextObject WindText)
        {
            Text = WindText.Text.Text;
            Font = WindText.Text.Font;
            Origin = WindText.Plane.Origin;
            Angle = WindText.Angle;

            HorizontalAlignment = (hAlign)WindText.Text.Font.HorizontalAlignment;
            VerticalAlignment = (vAlign)WindText.Text.Font.VerticalAlignment;
            FontItalic = (fStyle)Convert.ToInt32(WindText.Text.Font.IsItalic);
            FontBold = (fWeight)Convert.ToInt32(WindText.Text.Font.IsBold);

        }

        public hText(wTextObject WindText, wFont WindFont)
        {
            Text = WindText.Text.Text;
            Font = WindFont;
            Origin = WindText.Plane.Origin;
            Angle = WindText.Angle;

            HorizontalAlignment = (hAlign)(int)Font.HorizontalAlignment;
            VerticalAlignment = (vAlign)(int)Font.VerticalAlignment;
            FontItalic = (fStyle)Convert.ToInt32(WindFont.IsItalic);
            FontBold = (fWeight)Convert.ToInt32(WindFont.IsBold);
            
        }

        public override void BuildSVGCurve()
        {
            Curve.Clear();
            Curve.Append("<text x=\"0\" y=\"0\" " + Environment.NewLine);
            Curve.Append("font-style=\"" + FontItalic.ToString() + "\" " + Environment.NewLine);
            Curve.Append("font-weight=\"" + FontBold.ToString() + "\" " + Environment.NewLine);
            Curve.Append("text-anchor=\"" + HorizontalAlignment.ToString() + "\" " + Environment.NewLine);
            Curve.Append("alignment-baseline=\"" + VerticalAlignment.ToString() + "\" " + Environment.NewLine);
            Curve.Append("transform=\"rotate(" + Angle + " " + Origin.X + " " + Origin.Y + ") scale(1,-1) translate(" + Origin.X + " " + (-1 * Origin.Y) +") \""  + Environment.NewLine);
            Curve.Append("style=\"font-family: " + Font.Name + ";"+ Environment.NewLine);
            Curve.Append("font-size: " + Font.Size + "px;" + Environment.NewLine);
            Curve.Append("fill: rgb(" + Font.FontColor.R + "," + Font.FontColor.G + "," + Font.FontColor.B + ");\"> " + Environment.NewLine);
            Curve.Append(Text + Environment.NewLine);
            Curve.Append("</text> " + Environment.NewLine);
        }

    }
}
