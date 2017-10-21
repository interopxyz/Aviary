using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustLevels : mFilter
    {
        LevelsLinear Effect = new LevelsLinear();

        wDomain RedIn = new wDomain(0, 255);
        wDomain GreenIn = new wDomain(0, 255);
        wDomain BlueIn = new wDomain(0, 255);

        wDomain RedOut = new wDomain(0, 255);
        wDomain GreenOut = new wDomain(0, 255);
        wDomain BlueOut = new wDomain(0, 255);

        public mAdjustLevels(wDomain RedValueIn, wDomain GreenValueIn, wDomain BlueValueIn, wDomain RedValueOut, wDomain GreenValueOut, wDomain BlueValueOut)
        {

            RedIn = RedValueIn;
            GreenIn = GreenValueIn;
            BlueIn = BlueValueIn;

            RedOut = RedValueOut;
            GreenOut = GreenValueOut;
            BlueOut = BlueValueOut;

            BitmapType = 1;

            Effect = new LevelsLinear();

            Effect.InRed = new AForge.IntRange((int)RedIn.T0, (int)RedIn.T1);
            Effect.InRed = new AForge.IntRange((int)RedOut.T0, (int)RedOut.T1);

            Effect.InGreen = new AForge.IntRange((int)GreenIn.T0, (int)GreenIn.T1);
            Effect.OutGreen = new AForge.IntRange((int)GreenOut.T0, (int)GreenOut.T1);

            Effect.InBlue = new AForge.IntRange((int)BlueIn.T0, (int)BlueIn.T1);
            Effect.OutBlue = new AForge.IntRange((int)BlueOut.T0, (int)BlueOut.T1);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
