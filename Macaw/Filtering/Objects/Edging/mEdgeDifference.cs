using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Edging
{
    public class mEdgeDifference : mFilter
    {
        DifferenceEdgeDetector Effect = new DifferenceEdgeDetector();
        

        public mEdgeDifference()
        {

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;

            Effect = new DifferenceEdgeDetector();
            
            filter = Effect;
        }

    }
}