using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mSharpenGaussian : mFilter
    {
        GaussianSharpen Effect = new GaussianSharpen();

        double Sigma = 1.4;
        int EffectSize = 10;

        public mSharpenGaussian(double SigmaValue, int SizeValue)
        {

            BitmapType = 1;
            
            Sigma = SigmaValue;
            EffectSize = SizeValue;

            Effect = new GaussianSharpen(Sigma, EffectSize);
            

            Sequence.Clear();
            Sequence.Add(Effect);

        }

    }
}
