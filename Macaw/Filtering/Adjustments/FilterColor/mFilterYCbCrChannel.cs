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
    public class mFilterYCbCrChannel : mFilter
    {
        YCbCrExtractChannel Effect = new YCbCrExtractChannel();

        wDomain Y = new wDomain(0,1.0);
        wDomain Cb = new wDomain(-0.5, 0.5);
        wDomain Cr = new wDomain(-0.5, 0.5);

        public mFilterYCbCrChannel(wDomain Luminance, wDomain Blue, wDomain Red)
        {
            Y = Luminance;
            Cb = Blue;
            Cr = Red;

            BitmapType = 1;

            Effect = new YCbCrExtractChannel();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}