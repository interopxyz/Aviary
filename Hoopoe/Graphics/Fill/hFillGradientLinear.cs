using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Hoopoe.Graphics.Fill
{
    public class hFillGradientLinear : hGraphic
    {
        public string Style = " ";
        public enum GradientSpace { userSpaceOnUse, objectBoundingBox }
        public GradientSpace GradientFillSpace = GradientSpace.objectBoundingBox;


        public hFillGradientLinear()
        {
            Value = "fill=\"none\"";
        }

        public void Empty()
        {
            Value = " ";
        }

        public void None()
        {
            Value = "fill=\"none\"";
        }

        public hFillGradientLinear(int Index, wGradient WindGradient)
        {
            double R = WindGradient.Angle / 180 * Math.PI;
            double XA = Math.Round(50 + Math.Sin(R) * 50);
            double YA = Math.Round(50 + Math.Cos(R) * 50);
            double XB = Math.Round(50 + Math.Sin(R + Math.PI) * 50);
            double YB = Math.Round(50 + Math.Cos(R + Math.PI) * 50);

            GradientFillSpace = (GradientSpace)(int)WindGradient.FillMode;
            StringBuilder StyleAssembly = new StringBuilder();

            StyleAssembly.Append("<defs>" + Environment.NewLine);
            StyleAssembly.Append("<linearGradient id=\"grad" + Index + "\" x1=\"" + XA + "%\" y1=\"" + YA + "%\" x2=\"" + XB + "%\" y2=\"" + YB + "%\" gradientUnits=\"" + GradientFillSpace.ToString() + "\" >" + Environment.NewLine);
            for (int i = 0;i<WindGradient.ColorSet.Count;i++)
            { 
                StyleAssembly.Append("<stop offset=\"" + (WindGradient.ParameterSet[i]*100.00) + "%\" style=\"stop-color:rgb(" + WindGradient.ColorSet[i].R + "," + WindGradient.ColorSet[i].G + "," + WindGradient.ColorSet[i].B + ");stop-opacity:" + WindGradient.ColorSet[i].A/255 + "\" />" + Environment.NewLine);
            }
            StyleAssembly.Append("</linearGradient>" + Environment.NewLine);
            StyleAssembly.Append("</defs>" + Environment.NewLine);

            Style = StyleAssembly.ToString();
            Value = "fill=\"url(#grad" + Index + ")\"" + Environment.NewLine;
        }

    }
}