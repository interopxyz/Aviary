using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Vectors;

namespace Macaw.Utilities
{
    public class mSampleBitmap
    {

        Bitmap bmp = null;
        Color color = Color.White;

        public mSampleBitmap()
        {
        }

        public mSampleBitmap(Bitmap SourceBitmap)
        {
            bmp = SourceBitmap;
        }

        public Color Sample(double X, double Y)
        {
            return bmp.GetPixel((int)(bmp.Width * X), bmp.Height-(int)(bmp.Height * Y)-1);
        }

    }
}
