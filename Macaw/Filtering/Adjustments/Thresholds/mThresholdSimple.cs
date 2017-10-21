using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.Thresholds
{
    public class mThresholdSimple : mFilter
    {
        Threshold Effect = new Threshold();

        public mThresholdSimple()
        {

            BitmapType = 0;

            Effect = new Threshold();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}