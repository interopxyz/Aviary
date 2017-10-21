using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdOtsu : mFilter
    {
        OtsuThreshold Effect = new OtsuThreshold();

        public mThresholdOtsu()
        {

            BitmapType = 0;

            Effect = new OtsuThreshold();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
