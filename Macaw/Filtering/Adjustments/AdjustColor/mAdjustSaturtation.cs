using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustSaturation : mFilter
    {

        public float AdjustValue = 0.0f;
        
        public mAdjustSaturation(float adjustValue)
        {

            AdjustValue = adjustValue;

            BitmapType = BitmapTypes.None;

            filter = new SaturationCorrection(AdjustValue);

        }

    }
}
