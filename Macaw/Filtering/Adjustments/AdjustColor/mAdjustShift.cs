using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustShift : mFilter
    {
        RotateChannels Effect = new RotateChannels();

        int AdjustValue = 50;

        public mAdjustShift(int Value)
        {

            AdjustValue = Value;

            BitmapType = 1;

            Effect = new RotateChannels();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
