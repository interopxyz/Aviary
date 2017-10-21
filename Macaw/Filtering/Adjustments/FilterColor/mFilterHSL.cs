using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterHSL : mFilter
    {
        HSLFiltering Effect = new HSLFiltering();

        wDomain Hue = new wDomain(335,0);
        wDomain Sat = new wDomain(0.5, 0);
        wDomain Lum = new wDomain(0.1, 0);

        public mFilterHSL(wDomain HueValue, wDomain SaturationValue, wDomain LuminanceValue)
        {

            Hue = HueValue;
            Sat = SaturationValue;
            Lum = LuminanceValue;
            
            BitmapType = 1;

            Effect = new HSLFiltering( new AForge.IntRange((int)Hue.T0,(int)Hue.T1), new AForge.Range((float)Sat.T0, (float)Sat.T1), new AForge.Range((float)Lum.T0, (float)Lum.T1));

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
