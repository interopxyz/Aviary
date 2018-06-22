using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustBradley : mFilter
    {
        BradleyLocalThresholding Effect = new BradleyLocalThresholding();

        float DifferenceLimit = (float)0.5;

        public mAdjustBradley(float differenceLimit)
        {

            DifferenceLimit = differenceLimit;

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new BradleyLocalThresholding();

            Effect.PixelBrightnessDifferenceLimit = DifferenceLimit;

            filter = Effect;
        }

    }
}