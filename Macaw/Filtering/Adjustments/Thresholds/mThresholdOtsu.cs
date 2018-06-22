using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdOtsu : mFilter
    {
        OtsuThreshold Effect = new OtsuThreshold();

        public mThresholdOtsu()
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            Effect = new OtsuThreshold();

            filter = Effect;
        }

    }
}
