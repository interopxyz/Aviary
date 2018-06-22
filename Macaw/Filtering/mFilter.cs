using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging.Filters;
using Accord.Imaging;

namespace Macaw.Filtering
{
    public class mFilter
    {

        public string Type = "Filter";

        public double value = 0.0;

        public enum BitmapTypes { GrayscaleBT709, GrayscaleRMY, GrayscaleY, GrayScale16bpp, Rgb16bpp, Rgb24bpp, None };
        public BitmapTypes BitmapType = BitmapTypes.None;

        public IFilter filter = new Invert();

        public mFilter()
        {

        }

    }
}
