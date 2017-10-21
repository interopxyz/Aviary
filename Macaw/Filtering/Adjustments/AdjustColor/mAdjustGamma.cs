using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustGamma : mFilter
    {
        GammaCorrection Effect = new GammaCorrection();

        double AdjustValue = 1.0;

        public mAdjustGamma(double Value)
        {

            AdjustValue = Value;

            BitmapType = 2;

            Effect = new GammaCorrection(AdjustValue);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
