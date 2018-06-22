
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord;
using Accord.Imaging.Filters;
using Macaw.Filtering;

using Wind.Types;
using System.Drawing;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterARGBColor : mFilter
    {
        ColorFiltering Effect = new ColorFiltering();

        public wDomain Red = new wDomain(0, 255);
        public wDomain Green = new wDomain(100, 255);
        public wDomain Blue = new wDomain(100, 255);
        public bool IsOut = false;
        public Color FillColor = Color.Black;

        public mFilterARGBColor(wDomain RedRange, wDomain GreenRange, wDomain BlueRange, bool isOut, Color fillColor)
        {
            Red = RedRange;
            Green = GreenRange;
            Blue = BlueRange;
            IsOut = isOut;
            FillColor = fillColor;

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new ColorFiltering(new IntRange((int)Red.T0, (int)Red.T1), new IntRange((int)Green.T0, (int)Green.T1), new IntRange((int)Blue.T0, (int)Blue.T1));

            Effect.FillOutsideRange = IsOut;
            Effect.FillColor = new Accord.Imaging.RGB( FillColor);

            filter = Effect;
        }

    }
}