using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdSIS : mFilter
    {
        SISThreshold Effect = new SISThreshold();

        public mThresholdSIS()
        {

            BitmapType = 0;

            Effect = new SISThreshold();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
