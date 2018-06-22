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

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterARGBChannel : mFilter
    {
        ChannelFiltering Effect = new ChannelFiltering();

        public wDomain Red = new wDomain(0,255);
        public wDomain Green = new wDomain(100, 255);
        public wDomain Blue = new wDomain(100, 255);
        public bool IsOut = false;

        public mFilterARGBChannel(wDomain RedRange, wDomain GreenRange, wDomain BlueRange, bool isOut)
        {
            Red = RedRange;
            Green = GreenRange;
            Blue = BlueRange;
            IsOut = isOut;


            BitmapType = mFilter.BitmapTypes.None;
            
            Effect = new ChannelFiltering(new IntRange((int)Red.T0, (int)Red.T1), new IntRange((int)Green.T0, (int)Green.T1), new IntRange((int)Blue.T0, (int)Blue.T1));
            Effect.RedFillOutsideRange = IsOut;
            Effect.GreenFillOutsideRange = IsOut;
            Effect.BlueFillOutsideRange = IsOut;

            filter = Effect;
        }

    }
}