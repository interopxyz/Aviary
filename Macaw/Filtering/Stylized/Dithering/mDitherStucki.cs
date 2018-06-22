using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherStucki : mFilter
    {
        StuckiDithering Effect = new StuckiDithering();

        byte ThresholdValue = 42;

        public mDitherStucki(byte thresholdValue)
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            ThresholdValue = thresholdValue;

            Effect = new StuckiDithering();
            Effect.ThresholdValue = ThresholdValue;

            filter = Effect;
        }

    }
}

