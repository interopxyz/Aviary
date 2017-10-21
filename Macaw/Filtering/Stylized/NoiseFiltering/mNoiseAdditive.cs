using AForge;
using AForge.Imaging.Filters;
using AForge.Math.Random;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Filtering.Stylized
{
    public class mNoiseAdditive : mFilter
    {
        AdditiveNoise Effect = new AdditiveNoise();

        wDomain Domain = new wDomain(-50, 50);
        
        public mNoiseAdditive(wDomain GeneratorDomain)
        {

            Domain = GeneratorDomain;

            BitmapType = 2;
            
            Effect = new AdditiveNoise(new UniformGenerator(new Range((float)Domain.T0, (float)Domain.T1)));

            Sequence.Clear();
            Sequence.Add(Effect);

        }
    }
}
