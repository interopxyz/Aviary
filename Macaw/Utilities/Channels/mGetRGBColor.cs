using System.Collections.Generic;
using System.Drawing;
using Wind.Geometry.Vectors;

namespace Macaw.Utilities.Channels
{
    public class mGetRGBColor : mGetChannel
    {

        public mGetRGBColor(Bitmap BaseBitmap)
        {
            Bitmap bmp = new Bitmap(BaseBitmap);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Colors.Add(bmp.GetPixel(i, j));
                }
            }
        }

        public mGetRGBColor(Bitmap BaseBitmap, List<double> X, List<double> Y)
        {
            Bitmap bmp = new Bitmap(BaseBitmap);
            for (int i = 0; i < X.Count; i++)
            {
                Colors.Add(bmp.GetPixel((int)((bmp.Width-1) * X[i]), (int)((bmp.Height-1) * Y[i])));
            }
        }


    }
}