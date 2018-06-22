using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdSIS : mFilter
    {
        SISThreshold Effect = new SISThreshold();

        public mThresholdSIS()
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            Effect = new SISThreshold();
            
            filter = Effect;
        }

    }
}
