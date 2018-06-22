using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherOrdered : mFilter
    {
        OrderedDithering Effect = new OrderedDithering();
        
        public mDitherOrdered()
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            filter = new OrderedDithering();
            
        }

    }
}

