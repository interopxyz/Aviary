
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge;
using AForge.Imaging.Filters;
using Macaw.Filtering;

using Wind.Types;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterARGBColor : mFilter
    {
        ColorFiltering Effect = new ColorFiltering();

        wDomain Red = new wDomain(0, 255);
        wDomain Green = new wDomain(100, 255);
        wDomain Blue = new wDomain(100, 255);

        public mFilterARGBColor(wDomain RedRange, wDomain GreenRange, wDomain BlueRange)
        {
            Red = RedRange;
            Green = GreenRange;
            Blue = BlueRange;

            BitmapType = 1;

            Effect = new ColorFiltering(new IntRange((int)Red.T0, (int)Red.T1), new IntRange((int)Green.T0, (int)Green.T1), new IntRange((int)Blue.T0, (int)Blue.T1));

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}