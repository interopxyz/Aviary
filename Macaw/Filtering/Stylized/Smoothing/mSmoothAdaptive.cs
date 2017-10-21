using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mSmoothAdaptive : mFilter
    {
        AdaptiveSmoothing Effect = new AdaptiveSmoothing();

        double Factor = 2.0;

        public mSmoothAdaptive(double SmoothFactor)
        {

            BitmapType = 2;

            Effect = new AdaptiveSmoothing();

            Effect.Factor = Factor;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
