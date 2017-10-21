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
    public class mBlurGaussian : mFilter
    {
        GaussianBlur Effect = new GaussianBlur();

        double Sigma = 0;
        int EffectSize = 0;

        public mBlurGaussian(double SigmaValue, int SizeValue)
        {

            BitmapType = 1;
            
            Sigma = SigmaValue;
            EffectSize = SizeValue;

            Effect = new GaussianBlur(Sigma, EffectSize);
            

            Sequence.Clear();
            Sequence.Add(Effect);

        }

    }
}
