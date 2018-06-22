using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustGamma : mFilter
    {

        public double Gamma = 0;

        public mAdjustGamma(double gamma)
        {

            Gamma = gamma;

            BitmapType = BitmapTypes.Rgb24bpp;

            filter = new GammaCorrection(Gamma);
        }

    }
}
