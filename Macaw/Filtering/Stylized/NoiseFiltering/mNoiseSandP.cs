using Accord.Imaging.Filters;
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

        double Amount;

        public mNoiseSandP(double NoiseAmount)
        {

            Amount = NoiseAmount;

            BitmapType = mFilter.BitmapTypes.None;

            filter = new SaltAndPepperNoise(Amount);
            

        }
    }
}
