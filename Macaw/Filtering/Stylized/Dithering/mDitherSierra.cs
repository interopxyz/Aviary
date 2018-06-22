using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherSierra : mFilter
    {
        SierraDithering Effect = new SierraDithering();

        byte ThresholdValue = 32;

        public mDitherSierra(byte thresholdValue)
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            ThresholdValue = thresholdValue;

            Effect = new SierraDithering();
            Effect.ThresholdValue = ThresholdValue;

            filter = Effect;
        }

    }
}

