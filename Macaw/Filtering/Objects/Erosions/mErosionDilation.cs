using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionDilation : mFilter
    {
        BinaryDilation3x3 Effect = new BinaryDilation3x3();
        

        public mErosionDilation()
        {

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;
            
            Effect = new BinaryDilation3x3();
            
            filter = Effect;
        }

    }
}