using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Hoopoe.Graphics.Fill
{
    class hFillGradientRadial : hGraphic
    {
        public string Style = " ";
        public enum GradientSpace { userSpaceOnUse, objectBoundingBox }
        public GradientSpace GradientFillSpace = GradientSpace.objectBoundingBox;


        public hFillGradientRadial()
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

        public hFillGradientRadial(int Index, wGradient WindGradient)
        {
            double CX = (WindGradient.Location.T0 * 100);
            double CY = (WindGradient.Location.T1 * 100);
            double FX = (WindGradient.Focus.T0 * 100);
            double FY = (WindGradient.Focus.T1 * 100);
            double R = WindGradient.Radius * 100;

            GradientFillSpace = (GradientSpace)(int)WindGradient.FillMode;
            StringBuilder StyleAssembly = new StringBuilder();

            StyleAssembly.Append("<defs>" + Environment.NewLine);
            StyleAssembly.Append("<radialGradient id=\"grad" + Index + "\" cx=\"" + CX + "%\" cy=\"" + CY + "%\" fx=\"" + FX + "%\" fy=\"" + FY + "%\" r=\"" + R + "%\" gradientUnits=\"" + GradientFillSpace.ToString() + "\" >" + Environment.NewLine);
            for (int i = 0; i < WindGradient.ColorSet.Count; i++)
            {
                StyleAssembly.Append("<stop offset=\"" + (WindGradient.ParameterSet[i] * 100.00) + "%\" style=\"stop-color:rgb(" + WindGradient.ColorSet[i].R + "," + WindGradient.ColorSet[i].G + "," + WindGradient.ColorSet[i].B + ");stop-opacity:" + WindGradient.ColorSet[i].A / 255 + "\" />" + Environment.NewLine);
            }
            StyleAssembly.Append("</radialGradient>" + Environment.NewLine);
            StyleAssembly.Append("</defs>" + Environment.NewLine);

            Style = StyleAssembly.ToString();
            Value = "fill=\"url(#grad" + Index + ")\"" + Environment.NewLine;
        }

    }
}