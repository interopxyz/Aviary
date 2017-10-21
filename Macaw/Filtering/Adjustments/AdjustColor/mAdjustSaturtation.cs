using AForge.Imaging.Filters;
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
        SaturationCorrection Effect = new SaturationCorrection();

        float AdjustValue = 50;

        public mAdjustSaturation(float Value)
        {

            AdjustValue = Value;

            BitmapType = 1;

            Effect = new SaturationCorrection(AdjustValue);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
