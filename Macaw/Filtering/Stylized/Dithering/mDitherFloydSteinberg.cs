using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherFloydSteinberg : mFilter
    {
        FloydSteinbergDithering Effect = new FloydSteinbergDithering();

        byte ThresholdValue = 16;

        public mDitherFloydSteinberg(byte thresholdValue)
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            ThresholdValue = thresholdValue;

            Effect = new FloydSteinbergDithering();
            Effect.ThresholdValue = ThresholdValue;

            filter = Effect;
        }

    }
}

