using Accord;
using Accord.Imaging.Filters;
using Accord.Math.Random;
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

        wDomain Domain = new wDomain(10, 50);
        
        public mNoiseAdditive(wDomain GeneratorDomain)
        {

            Domain = GeneratorDomain;

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            filter = new AdditiveNoise();

        }
    }
}
