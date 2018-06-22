using Accord.Imaging.Filters;
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
        public int Factor = 0;
        public mAdjustContrast(int factor)
        {

            Factor = factor;

            BitmapType = BitmapTypes.None;

            filter = new ContrastCorrection(Factor);

        }

    }
}