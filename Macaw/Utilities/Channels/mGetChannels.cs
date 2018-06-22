using System.Collections.Generic;
using System.Drawing;

namespace Macaw.Utilities
{
    public class mGetChannels
    {

        public List<int> A = new List<int>();
        public List<int> R = new List<int>();
        public List<int> G = new List<int>();
        public List<int> B = new List<int>();

        public List<double> H = new List<double>();
        public List<double> S = new List<double>();
        public List<double> L = new List<double>();

        public mGetChannels(Bitmap BaseBitmap)
        {
            Bitmap bmp = new Bitmap(BaseBitmap);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    A.Add(bmp.GetPixel(i, j).A);

                    R.Add(bmp.GetPixel(i, j).R);
                    G.Add(bmp.GetPixel(i, j).G);
                    B.Add(bmp.GetPixel(i, j).B);

                    H.Add(bmp.GetPixel(i, j).GetHue());
                    S.Add(bmp.GetPixel(i, j).GetSaturation());
                    L.Add(bmp.GetPixel(i, j).GetBrightness());
                }
            }
        }


    }
}