using Accord.Imaging.Filters;
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

        public byte Interval = 5;
        public int PixelMode = 0;

        public mEffectPosterization(byte interval, int pixelMode)
        {

            BitmapType = mFilter.BitmapTypes.None;

            Interval = interval;
            PixelMode = pixelMode;

            Effect = new SimplePosterization();

            switch (PixelMode)
            {
                default:
                    Effect.FillingType = SimplePosterization.PosterizationFillingType.Average;
                    break;
                case 1:
                    Effect.FillingType = SimplePosterization.PosterizationFillingType.Min;
                    break;
                case 2:
                    Effect.FillingType = SimplePosterization.PosterizationFillingType.Max;
                    break;
            }
            Effect.PosterizationInterval = Interval;

            filter = Effect;
        }

    }
}
