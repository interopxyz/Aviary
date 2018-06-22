using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mDitherBayer : mFilter
    {
        BayerDithering Effect = new BayerDithering();
        
        public mDitherBayer()
        {

            BitmapType =  BitmapTypes.GrayscaleBT709;

            filter = new BayerDithering();
            
        }

    }
}

