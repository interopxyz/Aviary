using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustGrayWorld : mFilter
    {

        public mAdjustGrayWorld()
        {

            BitmapType = mFilter.BitmapTypes.None;

            filter = new GrayWorld();

        }

    }
}