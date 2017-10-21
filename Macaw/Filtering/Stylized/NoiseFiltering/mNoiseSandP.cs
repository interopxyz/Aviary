using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mNoiseSandP : mFilter
    {
        SaltAndPepperNoise Effect = new SaltAndPepperNoise();

        double Amount;

        public mNoiseSandP(double NoiseAmount)
        {

            Amount = NoiseAmount;

            BitmapType = 1;

            Effect = new SaltAndPepperNoise(Amount);

            Sequence.Clear();
            Sequence.Add(Effect);

        }
    }
}
