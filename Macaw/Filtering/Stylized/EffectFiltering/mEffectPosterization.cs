using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mEffectPosterization : mFilter
    {

        SimplePosterization Effect = new SimplePosterization();

        byte Interval = 5;

        public mEffectPosterization(byte IntervalCount)
        {

            BitmapType = 1;

            Interval = IntervalCount;

            Effect = new SimplePosterization(SimplePosterization.PosterizationFillingType.Average);

            Effect.PosterizationInterval = Interval;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
