using System.Collections.Generic;
using System.Drawing;

namespace Macaw.Utilities
{
    public class mGetHSL
    {
        
        public List<double> H = new List<double>();
        public List<double> S = new List<double>();
        public List<double> L = new List<double>();

        public mGetHSL(Bitmap BaseBitmap)
        {
            Bitmap bmp = new Bitmap(BaseBitmap);
            
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    H.Add(bmp.GetPixel(i, j).GetHue());
                    S.Add(bmp.GetPixel(i, j).GetSaturation());
                    L.Add(bmp.GetPixel(i, j).GetBrightness());
                }
            }
        }


    }
}