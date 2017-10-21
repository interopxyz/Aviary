using AForge.Imaging.Filters;
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

        public mEdgeSimple(int ValueThreshold,  int DivisorValue)
        {
            Divisor = DivisorValue;
            ThresholdValue = ValueThreshold;

            BitmapType = 1;

            Effect = new Edges();

            Effect.Threshold = ThresholdValue;
            Effect.Divisor = Divisor;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}