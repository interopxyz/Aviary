using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mBlurGaussian : mFilter
    {

        double Sigma = 4;
        int KernalSize = 11;

        public mBlurGaussian(double sigma, int kernalSize)
        {

            BitmapType = mFilter.BitmapTypes.None;
            
            Sigma = sigma;
            KernalSize = kernalSize;

            filter = new GaussianBlur(Sigma, KernalSize);
            

        }

    }
}
