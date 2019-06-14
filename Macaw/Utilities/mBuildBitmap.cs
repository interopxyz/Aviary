using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Macaw.Utilities
{
    public class mBuildBitmap
    {
        public Bitmap OutputBitmap = null;

        public mBuildBitmap()
        {

        }

        public mBuildBitmap(int W,int H, List<int> A, List<int> R, List<int> G, List<int> B)
        {
            Bitmap bmp = new Bitmap(W,H,PixelFormat.Format32bppArgb);
            bmp = new mConvert(new mConvert(bmp).BitmapToSource()).SourceToBitmap();

            int k = 0;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {
                    bmp.SetPixel(j, H-i-1, Color.FromArgb(A[k], R[k], G[k], B[k]));
                    k += 1;
                }
            }
            
            OutputBitmap = bmp;
        }

        public mBuildBitmap(int W, int H, List<int> R, List<int> G, List<int> B)
        {
            Bitmap bmp = new Bitmap(W, H, PixelFormat.Format32bppArgb);
            bmp = new mConvert(new mConvert(bmp).BitmapToSource()).SourceToBitmap();

            int k = 0;
            for (int i = 0; i < H; i++)
            {
                for (int j = 0; j < W; j++)
                {

                    bmp.SetPixel(j, H-i-1, Color.FromArgb( R[k], G[k], B[k]));
                    k += 1;
                }
            }


            OutputBitmap = bmp;
        }

    }
}