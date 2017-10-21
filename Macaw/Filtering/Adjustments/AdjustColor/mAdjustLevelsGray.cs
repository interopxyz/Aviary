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
    public class mAdjustLevelsGray : mFilter
    {
        LevelsLinear Effect = new LevelsLinear();

        wDomain GrayIn = new wDomain(0, 255);
        wDomain GrayOut = new wDomain(0, 255);


        public mAdjustLevelsGray(wDomain GrayValueIn, wDomain GrayValueOut)
        {

            GrayIn = GrayValueIn;
            GrayOut = GrayValueOut;

            BitmapType = 1;

            Effect = new LevelsLinear();

            Effect.InGray = new AForge.IntRange((int)GrayIn.T0, (int)GrayIn.T1);
            Effect.OutGray = new AForge.IntRange((int)GrayOut.T0, (int)GrayOut.T1);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
