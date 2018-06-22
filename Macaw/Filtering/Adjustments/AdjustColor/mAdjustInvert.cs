using Accord.Imaging.Filters;
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

        public mAdjustInvert()
        {

            BitmapType = BitmapTypes.Rgb24bpp;

            filter = new Invert();
        
        }

    }
}
