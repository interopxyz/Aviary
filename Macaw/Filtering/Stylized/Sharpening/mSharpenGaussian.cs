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
    public class mSharpenGaussian : mFilter
    {
        public double Sigma = 4;
        public int KernalSize = 11;

        public mSharpenGaussian(int kernalSize, double sigma)
        {

            BitmapType = BitmapTypes.None;
            
            Sigma = sigma;
            KernalSize = kernalSize;

            filter = new GaussianSharpen(Sigma, KernalSize);
            
        }

    }
}
