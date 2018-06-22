using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mEffectPixelate : mFilter
    {

        public int PixelHeight = 5;
        public int PixelWidth = 5;

        public mEffectPixelate(int pixelWidth, int pixelHeight)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            PixelWidth = pixelWidth;
            PixelHeight = pixelHeight;

            filter = new Pixellate(PixelWidth, PixelHeight);
            
        }

    }
}
