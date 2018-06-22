using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mSmoothAdaptive : mFilter
    {
        AdaptiveSmoothing Effect = new AdaptiveSmoothing();

        public double Factor = 1.0;

        public mSmoothAdaptive(double factor)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Factor = factor;

            Effect = new AdaptiveSmoothing();
            Effect.Factor = Factor;

            filter = Effect;
        }

    }
}
