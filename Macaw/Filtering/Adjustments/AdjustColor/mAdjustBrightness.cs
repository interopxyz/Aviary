using AForge.Imaging.Filters;
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
        BrightnessCorrection Effect = new BrightnessCorrection();

        int ShiftValue = 1;

        public mAdjustBrightness(int ShiftCount)
        {

            ShiftValue = ShiftCount;

            BitmapType = 1;
            FilterIterator Effect = new FilterIterator(new BrightnessCorrection(), ShiftValue);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
