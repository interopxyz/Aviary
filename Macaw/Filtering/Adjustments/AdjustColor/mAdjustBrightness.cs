using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustBrightness : mFilter
    {
        public int AdjustValue = 0;
        public mAdjustBrightness(int adjustValue)
        {
            AdjustValue = adjustValue;
            
            BitmapType = BitmapTypes.None;

            filter = new BrightnessCorrection(AdjustValue);

        }

    }
}
