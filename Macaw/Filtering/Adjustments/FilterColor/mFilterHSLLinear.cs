using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mFilterHSLLinear : mFilter
    {
        HSLLinear Effect = new HSLLinear();

        wDomain SatIn = new wDomain(0.0, 0.75);
        wDomain SatOut = new wDomain(0.25, 1.0);
        wDomain LumIn = new wDomain(0.0, 0.75);
        wDomain LumOut = new wDomain(0.25, 1.0);

        public mFilterHSLLinear(wDomain SaturationIn, wDomain SaturationOut, wDomain LuminanceIn, wDomain LuminanceOut)
        {

            SatIn = SaturationIn;
            SatOut = SaturationOut;
            LumIn = LuminanceIn;
            LumOut = LuminanceOut;

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new HSLLinear();
            
            Effect.InSaturation = new Accord.Range((float)SatIn.T0, (float)SatIn.T1);
            Effect.OutSaturation = new Accord.Range((float)SatOut.T0, (float)SatOut.T1);

            Effect.InLuminance = new Accord.Range((float)LumIn.T0, (float)LumIn.T1);
            Effect.OutLuminance = new Accord.Range((float)LumOut.T0, (float)LumOut.T1);

            filter = Effect;
        }

    }
}
