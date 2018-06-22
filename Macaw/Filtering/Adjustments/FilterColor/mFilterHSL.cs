using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;
using Macaw.Filtering;
using System.Drawing;
using Accord.Imaging;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterHSL : mFilter
    {
        HSLFiltering Effect = new HSLFiltering();

        public wDomain Hue = new wDomain(335,0);
        public wDomain Sat = new wDomain(0.5, 0);
        public wDomain Lum = new wDomain(0.1, 0);
        public bool IsOut = false;
        public Color FillColor = Color.Black;

        public mFilterHSL(wDomain HueValue, wDomain SaturationValue, wDomain LuminanceValue, bool isOut, Color fillColor)
        {

            Hue = HueValue;
            Sat = SaturationValue;
            Lum = LuminanceValue;
            IsOut = isOut;
            FillColor = fillColor;


            BitmapType = mFilter.BitmapTypes.None;

            Effect = new HSLFiltering( new Accord.IntRange((int)Hue.T0,(int)Hue.T1), new Accord.Range((float)Sat.T0, (float)Sat.T1), new Accord.Range((float)Lum.T0, (float)Lum.T1));

            Effect.FillOutsideRange = IsOut;
            Effect.FillColor = HSL.FromRGB(new RGB(FillColor));

            filter = Effect;

        }

    }
}
