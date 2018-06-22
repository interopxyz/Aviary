using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionOpening : mFilter
    {
        Opening Effect = new Opening();
        

        public mErosionOpening()
        {

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;
            
            Effect = new Opening();

            filter = Effect;
        }

    }
}