using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Edging
{
    public class mEdgeCanny : mFilter
    {
        CannyEdgeDetector Effect = new CannyEdgeDetector();

        int Size = 10;
        int LowValue = 20;
        int HighValue = 100;

        public mEdgeCanny(int LowVal, int HighVal, int SampleSize)
        {
            Size = SampleSize;
            LowValue = LowVal;
            HighValue = HighVal;

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;

            Effect = new CannyEdgeDetector();

            Effect.LowThreshold = (byte)LowValue;
            Effect.HighThreshold = (byte)HighValue;

            Effect.GaussianSize = Size;

            filter = Effect;
        }

    }
}