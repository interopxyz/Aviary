using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord;
using Accord.Imaging.Filters;
using Wind.Types;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterYCbCrChannel : mFilter
    {
        YCbCrExtractChannel Effect = new YCbCrExtractChannel();

        public wDomain Y = new wDomain(0,1.0);
        public wDomain Cb = new wDomain(-0.5, 0.5);
        public wDomain Cr = new wDomain(-0.5, 0.5);

        public mFilterYCbCrChannel(wDomain Luminance, wDomain Blue, wDomain Red)
        {
            Y = Luminance;
            Cb = Blue;
            Cr = Red;

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new YCbCrExtractChannel();



            filter = Effect;
        }

    }
}