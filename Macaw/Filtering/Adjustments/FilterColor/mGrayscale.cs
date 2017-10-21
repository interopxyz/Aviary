using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AForge.Imaging;
using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mGrayscale : mFilter
    {
        Grayscale Effect = null;

        double RedCoefficients = 0.2125;
        double GreenCoefficients = 0.7154;
        double BlueCoefficients = 0.0721;

        public mGrayscale(double RedCoef, double GreenCoef, double BlueCoef)
        {

            RedCoefficients = RedCoef;
            GreenCoefficients = GreenCoef;
            BlueCoefficients = BlueCoef;

            BitmapType = 1;

            Effect = new Grayscale(RedCoefficients, GreenCoefficients, BlueCoefficients);

            Sequence.Clear();
            Sequence.Add(Effect);

        }
    }
}
