using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Vectors;

namespace Hoopoe.Assembly
{
    public class hDocument
    {

        public StringBuilder svgText = new StringBuilder();

        public enum RenderingMode { auto, optimizeSpeed, crispEdges, geometricPrecision };

        public RenderingMode RenderQuality = RenderingMode.geometricPrecision;

        public int Width = 800;
        public int Height = 600;
        public double Scale = 1.0;
        public wRectangle Crop = new wRectangle();
        public wVector Translate = new wVector(0,0);

        public hDocument()
        {

        }

        public void SetSize(int PanelWidth, int PanelHeight, wRectangle CropRectangle, double ViewScale)
        {
            Width = PanelWidth;
            Height = PanelHeight;
            Crop = CropRectangle;
            Scale = ViewScale;
        }
        
        public void SetQuality(RenderingMode RenderMode)
        {
            RenderQuality = RenderMode;
        }


        public void Build(string svgStyle, string svgAssembly)
        {
            double X = Crop.CornerPoints[0].X;
            double Y = Crop.CornerPoints[0].Y;
            double W = Crop.Width;
            double H = Crop.Height;

            svgText.Append("<svg ");
            svgText.Append("width =\"" + W*Scale + "\" ");
            svgText.Append("height =\"" + H*Scale + "\" ");
            svgText.Append("viewBox=\"" + X + " " + Y + " " + W + " " + H + "\" ");
            svgText.Append("shape-rendering=\"" + RenderQuality + "\" ");
            svgText.Append("xmlns = \"http://www.w3.org/2000/svg\" > " + Environment.NewLine);
            
            svgText.Append("<g class=\"Canvas\" id=\"Canvas\" transform=\"translate(0,"+ Y + ") scale(1,-1) translate(0," + (-1)*(Y+H) + ")\">" + Environment.NewLine);

            svgText.Append("<defs> " + Environment.NewLine);
            svgText.Append("<clipPath id=\"Frame\">" + Environment.NewLine);
            svgText.Append("<rect x=\"0\" y=\"0\" width=\"" + Width + "\" height=\"" + Height + "\" />" + Environment.NewLine);
            svgText.Append("</clipPath>" + Environment.NewLine);
            svgText.Append("</defs> " + Environment.NewLine);

            svgText.Append(svgStyle);
            svgText.Append(svgAssembly);

            svgText.Append(" </g>" + Environment.NewLine);

            svgText.Append(" </svg>");

        }
    }
}
