using AForge.Imaging.Filters;
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

        float ShiftValue = (float)0.5;

        public mAdjustBradley(float ShiftCount)
        {

            ShiftValue = ShiftCount;

            BitmapType = 0;

            Effect = new BradleyLocalThresholding();

            Effect.PixelBrightnessDifferenceLimit = ShiftValue;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}