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
    public class mSmoothBilateral : mFilter
    {
        BilateralSmoothing Effect = new BilateralSmoothing();

        int KernelSize = 7;
        double SpatialFactor = 10.0;
        double ColorFactor = 60.0;
        double ColorPower = 60.0;

        public mSmoothBilateral(double Space, double Factor, double Power, int SizeValue)
        {

            KernelSize = SizeValue;
            SpatialFactor = Space;
            ColorFactor = Factor;
            ColorPower = Power;

            BitmapType = 1;

            Effect = new BilateralSmoothing();

            Effect.KernelSize = KernelSize;
            Effect.SpatialFactor = SpatialFactor;
            Effect.ColorFactor = ColorFactor;
            Effect.ColorPower = ColorPower;
            Effect.EnableParallelProcessing = true;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
