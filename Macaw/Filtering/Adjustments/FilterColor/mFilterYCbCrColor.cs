using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord;
using Accord.Imaging.Filters;
using Wind.Types;
using Macaw.Filtering;
using System.Drawing;
using Accord.Imaging;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterYCbCrColor : mFilter
    {
        YCbCrFiltering Effect = new YCbCrFiltering();

        public wDomain Y = new wDomain(0,1.0);
        public wDomain Cb = new wDomain(-0.5, 0.5);
        public wDomain Cr = new wDomain(-0.5, 0.5);

        public bool IsOut = false;
        public Color FillColor = Color.Black;

        public mFilterYCbCrColor(wDomain Luminance, wDomain Blue, wDomain Red, bool isOut, Color fillColor)
        {
            Y = Luminance;
            Cb = Blue;
            Cr = Red;
            IsOut = isOut;
            FillColor = fillColor;

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Effect = new YCbCrFiltering(new Range((float)Y.T0, (float)Y.T1), new Range((float)Cb.T0, (float)Cb.T1), new Range((float)Cr.T0, (float)Cr.T1));

            Effect.FillOutsideRange = IsOut;
            Effect.FillColor = YCbCr.FromRGB(new RGB( FillColor));

        }

    }
}