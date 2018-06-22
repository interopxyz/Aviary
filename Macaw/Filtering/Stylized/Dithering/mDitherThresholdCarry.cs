using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherThresholdCarry : mFilter
    {

        byte ThresholdValue = 50;

        public mDitherThresholdCarry(byte thresholdValue)
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            ThresholdValue = thresholdValue;

            filter = new ThresholdWithCarry(ThresholdValue);
        }

    }
}

