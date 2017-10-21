using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustContrast : mFilter
    {
        ContrastCorrection Effect = new ContrastCorrection();

        int AdjustValue = 50;

        public mAdjustContrast(int Value)
        {

            AdjustValue = Value;

            BitmapType = 1;

            Effect = new ContrastCorrection(AdjustValue);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}