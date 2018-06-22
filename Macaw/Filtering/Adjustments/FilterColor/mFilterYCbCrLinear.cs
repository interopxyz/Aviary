using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Accord;
using Accord.Imaging.Filters;
using Wind.Types;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterYCbCrLinear : mFilter
    {
        YCbCrLinear Effect = new YCbCrLinear();

        wDomain Yin = new wDomain(0,1);
        wDomain Yout = new wDomain(0, 1);
        wDomain Cbin = new wDomain(-0.5, 0.5);
        wDomain Cbout = new wDomain(-0.5, 0.5);
        wDomain Crin = new wDomain(-0.5, 0.5);
        wDomain Crout = new wDomain(-0.5, 0.5);

        public mFilterYCbCrLinear(wDomain YinRange, wDomain CBinRange, wDomain CRinRange, wDomain YoutRange, wDomain CBoutRange, wDomain CRoutRange)
        {
            Yin = YinRange;
            Yout = YoutRange;
            Cbin = CBinRange;
            Cbout = CBoutRange;
            Crin = CRinRange;
            Crout = CRoutRange;

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new YCbCrLinear();

            Effect.InY = new Range((float)Yin.T0,(float)Yin.T1);
            Effect.InCb = new Range((float)Cbin.T0, (float)Cbin.T1);
            Effect.InCr = new Range((float)Crin.T0, (float)Crin.T1);

            Effect.OutY = new Range((float)Yout.T0, (float)Yout.T1);
            Effect.OutCb = new Range((float)Cbout.T0, (float)Cbout.T1);
            Effect.OutCr = new Range((float)Crout.T0, (float)Crout.T1);

            filter = Effect;
        }

    }
}