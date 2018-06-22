using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdSimple : mFilter
    {
        Threshold Effect = new Threshold();
        int ThresholdValue = 10;

        public mThresholdSimple(int thresholdValue)
        {
            ThresholdValue = thresholdValue;

            BitmapType = BitmapTypes.GrayscaleBT709;

            Effect = new Threshold(ThresholdValue);

            filter = Effect;
        }

    }
}