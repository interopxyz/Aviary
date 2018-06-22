using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Filtering.Stylized
{
    public class mEffectRipple : mFilter
    {

        WaterWave Effect = new WaterWave();

        public int HorizontalCount = 5;
        public int VerticalCount = 5;
        public int HorizontalAmplitude = 10;
        public int VerticalAmplitude = 5;


        public mEffectRipple(int horizontalAmplitude, int verticalAmplitude,int horizontalCount, int verticalCount)
        {

            BitmapType = mFilter.BitmapTypes.None;
            
            HorizontalCount = horizontalCount;
            VerticalCount = verticalCount;
            HorizontalAmplitude = horizontalAmplitude;
            VerticalAmplitude = verticalAmplitude;

            Effect = new WaterWave();

            Effect.HorizontalWavesAmplitude = HorizontalAmplitude;
            Effect.HorizontalWavesCount = HorizontalCount;

            Effect.VerticalWavesAmplitude = VerticalAmplitude;
            Effect.VerticalWavesCount = VerticalCount;

            filter = Effect;
        }

    }
}
