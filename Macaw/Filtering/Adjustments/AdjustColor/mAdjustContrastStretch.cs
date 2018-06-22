using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustContrastStretch : mFilter
    {
        public mAdjustContrastStretch()
        {

            BitmapType = BitmapTypes.None;

            filter = new ContrastStretch();

        }

    }
}