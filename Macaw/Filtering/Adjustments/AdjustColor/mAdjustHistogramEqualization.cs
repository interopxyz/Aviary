using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustHistogramEqualization : mFilter
    {
        

        public mAdjustHistogramEqualization()
        {

            BitmapType = mFilter.BitmapTypes.None;

            filter = new HistogramEqualization();
        }

    }
}
