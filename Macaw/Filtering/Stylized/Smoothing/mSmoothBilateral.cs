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
    public class mSmoothBilateral : mFilter
    {
        BilateralSmoothing Effect = new BilateralSmoothing();

        public int KernelSize = 7;
        public double SpatialFactor = 10.0;
        public double ColorFactor = 60.0;
        public double ColorPower = 60.0;

        public mSmoothBilateral(int kernalSize, double spatialFactor, double colorFactor, double colorPower)
        {

            KernelSize = kernalSize;
            SpatialFactor = spatialFactor;
            ColorFactor = colorFactor;
            ColorPower = colorPower;

            BitmapType = BitmapTypes.None;

            Effect = new BilateralSmoothing();

            Effect.KernelSize = KernelSize;
            Effect.SpatialFactor = SpatialFactor;
            Effect.ColorFactor = ColorFactor;
            Effect.ColorPower = ColorPower;
            Effect.EnableParallelProcessing = true;

            filter = Effect;
        }

    }
}
