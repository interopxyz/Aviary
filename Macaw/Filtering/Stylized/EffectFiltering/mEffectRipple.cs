using AForge.Imaging.Filters;
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

        int HorizontalCount = 5;
        int VerticalCount = 5;
        wDomain Amplitude = new wDomain(5,10);

        public mEffectRipple(wDomain AmplitudeInterval, int HorizontalWaveCount, int VerticalWaveCount)
        {

            BitmapType = 1;

            Amplitude = AmplitudeInterval;
            HorizontalCount = HorizontalWaveCount;
            VerticalCount = VerticalWaveCount;

            Effect = new WaterWave();

            Effect.HorizontalWavesAmplitude = (int)Amplitude.T0;
            Effect.HorizontalWavesCount = HorizontalCount;

            Effect.VerticalWavesAmplitude = (int)Amplitude.T1;
            Effect.VerticalWavesCount = VerticalCount;
            

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
