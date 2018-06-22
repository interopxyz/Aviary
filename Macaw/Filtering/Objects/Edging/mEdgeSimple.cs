using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Edging
{
    public class mEdgeSimple : mFilter
    {
        Edges Effect = new Edges();

        int Divisor = 10;
        int ThresholdValue = 1;

        public mEdgeSimple(int ValueThreshold,  int DivisorValue, bool Dynamic)
        {
            Divisor = DivisorValue;
            ThresholdValue = ValueThreshold;

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Effect = new Edges();

            Effect.DynamicDivisorForEdges = Dynamic;
            Effect.Threshold = ThresholdValue;
            Effect.Divisor = Divisor;

            filter = Effect;
        }

    }
}