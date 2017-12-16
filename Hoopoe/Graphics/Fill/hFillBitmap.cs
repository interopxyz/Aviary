using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Hoopoe.Graphics.Fill
{
    class hFillBitmap : hGraphic
    {
        public string Style = " ";
        public enum FillMode { userSpaceOnUse, objectBoundingBox }
        public FillMode FillSpace = FillMode.objectBoundingBox;
        public string Location = "";

        public enum AlignMode { xMinYMin,xMidYMin,xMaxYMin,xMinYMid,xMidYMid,xMaxYMid,xMinYMax,xMidYMax,xMaxYMax}
        public AlignMode Alignment = AlignMode.xMidYMid;
        public enum FitMode { meet, slice,none}
        public FitMode Fitting = FitMode.meet;

        public hFillBitmap()
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

        public hFillBitmap(int Index, wImage WindBitmap)
        {
            FillSpace = (FillMode)(int)WindBitmap.FillMode;
            StringBuilder StyleAssembly = new StringBuilder();

            Alignment = (AlignMode)WindBitmap.Alignment;
            Fitting = (FitMode)WindBitmap.Fitting;

            string FitAlignment = Fitting.ToString() + " " + Alignment.ToString();
            if(Fitting == FitMode.none) { FitAlignment = "none"; }

            if (WindBitmap.IsEmbedded)
            {
                Location = "data:image/png;base64," + Convert.ToBase64String(WindBitmap.ImageByteArray);
            }
            else
            {
                Location = "file:" + System.Text.Encoding.Default.GetString(WindBitmap.BitmapImage.GetPropertyItem(0).Value);
            }
            

            StyleAssembly.Append("<defs>" + Environment.NewLine);
            StyleAssembly.Append("<pattern id=\"grad" + Index + "\" width=\"100%\" height=\"100%\" patternUnits=\"" + FillSpace.ToString() + "\" viewBox=\"0 0 1 1\" preserveAspectRatio=\"" + Alignment.ToString() + " " + Fitting.ToString() + "\" patternTransform=\" rotate(" + WindBitmap.Angle + ")\" >" + Environment.NewLine);

            StyleAssembly.Append("<image width=\"1\" height=\"1\" preserveAspectRatio=\"" + FitAlignment + "\" href =\"" + Location + "\" />" + Environment.NewLine);
            StyleAssembly.Append("</pattern>" + Environment.NewLine);
            StyleAssembly.Append("</defs>" + Environment.NewLine);

            Style = StyleAssembly.ToString();
            Value = "fill=\"url(#grad" + Index + ")\"" + Environment.NewLine;
        }

    }
}