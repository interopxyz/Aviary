using System.Collections.Generic;
using System.Drawing;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mSwapARGB
    {
        public Bitmap ModifiedBitmap = null;

        public List<int> A = new List<int>();
        public List<int> R = new List<int>();
        public List<int> G = new List<int>();
        public List<int> B = new List<int>();
        public List<double> H = new List<double>();
        public List<double> S = new List<double>();
        public List<double> L = new List<double>();

        public mSwapARGB(Bitmap BaseBitmap, int Avalue, int Rvalue, int Gvalue, int Bvalue)
        {
            Bitmap bmp = new Bitmap(BaseBitmap);
            

            int k = 0;
            for (int i= 0;i< bmp.Width;i++)
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

                    int[] Values = { A[k], R[k], G[k], B[k], (int)(255.0 *(H[k]/360.0)), (int)(255.0 * S[k]), (int)(255.0 * L[k]) };

                    bmp.SetPixel(i, j, Color.FromArgb(Values[Avalue], Values[Rvalue], Values[Gvalue], Values[Bvalue]));
                    k += 1;
                }
            }
            

            ModifiedBitmap = new Bitmap(bmp);
        }
        

    }
}