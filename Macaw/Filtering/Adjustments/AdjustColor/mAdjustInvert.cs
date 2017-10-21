using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustInvert : mFilter
    {
        Invert Effect = new Invert();

        public mAdjustInvert()
        {

            BitmapType = 2;

            Effect = new Invert();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
