using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Hoopoe.SVG.Graphics.Fill
{
    class hFillBitmap : hGraphic
    {
        wImage WindImage = new wImage();
        int Index = 0;

        double Width = 0;
        double Height = 0;

        public string Style = " ";
        public enum FillMode { userSpaceOnUse, objectBoundingBox }
        private int[] ContentModes = { 1, 0, 1 };
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

        public hFillBitmap(int PathIndex, wImage WindBitmap)
        {
            WindImage = WindBitmap;
            Index = PathIndex;

            Width = WindImage.BitmapImage.Width;
            Height = WindImage.BitmapImage.Height;

            SetBitmap();
        }

        public hFillBitmap(int PathIndex, wImage WindBitmap, double W, double H)
        {
            WindImage = WindBitmap;
            Index = PathIndex;

            Width = W;
            Height = H;

            SetBitmap();
        }

        public void SetBitmap()
        {
            double SX = 1.0;
            double SY = 1.0;

            if ((int)WindImage.Fitting == 0)
            {
                SX = WindImage.BitmapImage.Width / Width;
                SY = WindImage.BitmapImage.Height / Height;
                double TZ = 1.0;
                if (SY > SX) { TZ = SY; } else { TZ = SX; }
                SX = SX / TZ;
                SY = SY / TZ;
            }

            FillSpace = (FillMode)(int)WindImage.Fitting;

            Alignment = (AlignMode)WindImage.Alignment;
            Fitting = (FitMode)WindImage.Fitting;

            string FitAlignment = Alignment.ToString() + " " + Fitting.ToString();
            if (Fitting == FitMode.none) { FitAlignment = "none"; }

            if (WindImage.IsEmbedded)
            {
                Location = "data:image/png;base64," + Convert.ToBase64String(WindImage.ImageByteArray);
            }
            else
            {
                Location = "file:" + System.Text.Encoding.Default.GetString(WindImage.BitmapImage.GetPropertyItem(0).Value);
            }

            FillMode ModeContent = (FillMode)(ContentModes[(int)WindImage.Fitting]);

            StringBuilder StyleAssembly = new StringBuilder();
            StyleAssembly.Append("<defs>" + Environment.NewLine);
            StyleAssembly.Append("<pattern id=\"grad" + Index + "\" width=\"" + SX + "\" height=\"" + SY + "\" " + Environment.NewLine);
            StyleAssembly.Append("patternUnits=\"objectBoundingBox\" patternContentUnits =\"" + ModeContent.ToString() + "\" " + Environment.NewLine);
            StyleAssembly.Append("viewBox=\"0 0 1 1\" " + Environment.NewLine);
            StyleAssembly.Append("preserveAspectRatio=\"" + FitAlignment + "\" " + Environment.NewLine);
            StyleAssembly.Append("patternTransform=\" rotate(" + WindImage.Angle + ")\" >" + Environment.NewLine);

            StyleAssembly.Append("<image width=\"1\" height=\"1\" preserveAspectRatio=\"" + FitAlignment + "\" href =\"" + Location + "\" />" + Environment.NewLine);
            StyleAssembly.Append("</pattern>" + Environment.NewLine);
            StyleAssembly.Append("</defs>" + Environment.NewLine);

            Style = StyleAssembly.ToString();
            Value = "fill=\"url(#grad" + Index + ")\"" + Environment.NewLine;
        }

    }
}