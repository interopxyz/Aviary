using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherJarvisJudiceNinke : mFilter
    {
        JarvisJudiceNinkeDithering Effect = new JarvisJudiceNinkeDithering();

        byte ThresholdValue = 48;

        public mDitherJarvisJudiceNinke(byte thresholdValue)
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            ThresholdValue = thresholdValue;

            Effect = new JarvisJudiceNinkeDithering();
            Effect.ThresholdValue = ThresholdValue;

            filter = Effect;
        }

    }
}

