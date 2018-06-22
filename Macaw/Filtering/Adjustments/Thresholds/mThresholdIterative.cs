using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdIterative : mFilter
    {
        IterativeThreshold Effect = new IterativeThreshold();

        int ThresholdValue = 50;
        int MinError = 5;

        public mThresholdIterative(int thresholdValue, int minError)
        {

            ThresholdValue = thresholdValue;
            MinError = minError;

            BitmapType = BitmapTypes.GrayscaleBT709;

            Effect = new IterativeThreshold(ThresholdValue,MinError);

            filter = Effect;
        }

    }
}
