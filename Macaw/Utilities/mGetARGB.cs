using System.Collections.Generic;
using System.Drawing;

namespace Macaw.Utilities
{
    public class mGetARGB
    {

        public List<int> A = new List<int>();
        public List<int> R = new List<int>();
        public List<int> G = new List<int>();
        public List<int> B = new List<int>();

        public mGetARGB(Bitmap BaseBitmap)
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
                    
                }
            }
        }


    }
}