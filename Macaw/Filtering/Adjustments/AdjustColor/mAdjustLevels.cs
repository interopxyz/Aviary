using Accord.Imaging.Filters;
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

        public wDomain RedIn = new wDomain(0, 255);
        public wDomain GreenIn = new wDomain(0, 255);
        public wDomain BlueIn = new wDomain(0, 255);
        public wDomain GrayIn = new wDomain(0, 255);

        public wDomain RedOut = new wDomain(0, 255);
        public wDomain GreenOut = new wDomain(0, 255);
        public wDomain BlueOut = new wDomain(0, 255);
        public wDomain GrayOut = new wDomain(0, 255);

        public mAdjustLevels(wDomain grayIn, wDomain grayOut)
        {

            GrayIn = grayIn;
            GrayOut = grayOut;

            RunLevels();
        }

        public mAdjustLevels(wDomain redIn, wDomain redOut, wDomain greenIn, wDomain greenOut, wDomain blueIn, wDomain blueOut)
        {

            RedIn = redIn;
            GreenIn = greenIn;
            BlueIn = blueIn;

            RedOut = redOut;
            GreenOut = greenOut;
            BlueOut = blueOut;

            RunLevels();
        }

        public mAdjustLevels(wDomain redIn, wDomain redOut, wDomain greenIn, wDomain greenOut, wDomain blueIn, wDomain blueOut, wDomain grayIn, wDomain grayOut)
        {

            RedIn = redIn;
            GreenIn = greenIn;
            BlueIn = blueIn;
            GrayIn = grayIn;

            RedOut = redOut;
            GreenOut = greenOut;
            BlueOut = blueOut;
            GrayOut = grayOut;

            RunLevels();
        }

        private void RunLevels()
        {
            BitmapType = mFilter.BitmapTypes.None;

            Effect = new LevelsLinear();

            Effect.InRed = new Accord.IntRange((int)RedIn.T0, (int)RedIn.T1);
            Effect.OutRed = new Accord.IntRange((int)RedOut.T0, (int)RedOut.T1);

            Effect.InGreen = new Accord.IntRange((int)GreenIn.T0, (int)GreenIn.T1);
            Effect.OutGreen = new Accord.IntRange((int)GreenOut.T0, (int)GreenOut.T1);

            Effect.InBlue = new Accord.IntRange((int)BlueIn.T0, (int)BlueIn.T1);
            Effect.OutBlue = new Accord.IntRange((int)BlueOut.T0, (int)BlueOut.T1);

            Effect.InGray = new Accord.IntRange((int)GrayIn.T0, (int)GrayIn.T1);
            Effect.OutGray = new Accord.IntRange((int)GrayOut.T0, (int)GrayOut.T1);

            filter = Effect;
        }

    }
}
