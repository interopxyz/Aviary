using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using AForge.Imaging.Filters;
using Wind.Types;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterARGBChannel : mFilter
    {
        ChannelFiltering Effect = new ChannelFiltering();

        wDomain Red = new wDomain(0,255);
        wDomain Green = new wDomain(100, 255);
        wDomain Blue = new wDomain(100, 255);

        public mFilterARGBChannel(wDomain RedRange, wDomain GreenRange, wDomain BlueRange)
        {
            Red = RedRange;
            Green = GreenRange;
            Blue = BlueRange;

            BitmapType = 1;

            Effect = new ChannelFiltering(new IntRange((int)Red.T0, (int)Red.T1), new IntRange((int)Green.T0, (int)Green.T1), new IntRange((int)Blue.T0, (int)Blue.T1));
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}