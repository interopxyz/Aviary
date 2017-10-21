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
    public class mFilterYCbCrColor : mFilter
    {
        YCbCrFiltering Effect = new YCbCrFiltering();

        wDomain Y = new wDomain(0,1.0);
        wDomain Cb = new wDomain(-0.5, 0.5);
        wDomain Cr = new wDomain(-0.5, 0.5);

        public mFilterYCbCrColor(wDomain Luminance, wDomain Blue, wDomain Red)
        {
            Y = Luminance;
            Cb = Blue;
            Cr = Red;

            BitmapType = 1;

            Effect = new YCbCrFiltering(new Range((float)Y.T0, (float)Y.T1), new Range((float)Cb.T0, (float)Cb.T1), new Range((float)Cr.T0, (float)Cr.T1));
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}