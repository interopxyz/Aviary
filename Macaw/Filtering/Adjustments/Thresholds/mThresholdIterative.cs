using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdIterative : mFilter
    {
        IterativeThreshold Effect = new IterativeThreshold();

        int Threshold = 50;

        public mThresholdIterative(int ThresholdValue)
        {

            Threshold = ThresholdValue;

            BitmapType = 0;

            Effect = new IterativeThreshold(2,Threshold);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
