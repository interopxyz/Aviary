using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mEffectJitter : mFilter
    {

        Jitter Effect = new Jitter();

        int Radius = 5;

        public mEffectJitter(int radius)
        {

            BitmapType = mFilter.BitmapTypes.None;

            Radius = radius;

            Effect = new Jitter();

            Effect.Radius = Radius;

            filter = Effect;
        }

    }
}
