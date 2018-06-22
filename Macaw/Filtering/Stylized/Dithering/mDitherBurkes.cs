using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized

{
    public class mDitherBurkes : mFilter
    {
        BurkesDithering Effect = new BurkesDithering();

        byte ThresholdValue = 32;

        public mDitherBurkes(byte thresholdValue)
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            ThresholdValue = thresholdValue;

            Effect = new BurkesDithering();
            Effect.ThresholdValue = ThresholdValue;

            filter = Effect;
        }

    }
}

