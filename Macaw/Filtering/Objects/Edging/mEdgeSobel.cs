using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Edging
{
    public class mEdgeSobel : mFilter
    {
        SobelEdgeDetector Effect = new SobelEdgeDetector();
        

        public mEdgeSobel(bool scale)
        {
            BitmapType = BitmapTypes.GrayscaleBT709;

            Effect = new SobelEdgeDetector();
            Effect.ScaleIntensity = scale; 
            filter = Effect;
        }

    }
}