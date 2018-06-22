using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Wind.Types;

namespace Macaw.Filtering.Adjustments.FilterColor
{
    public class mSwapRGB
    {
        public Bitmap ModifiedBitmap = null;

        public mSwapRGB(Bitmap BaseBitmap, int A, int R, int G, int B)
        {
            ExtractChannel cR = new ExtractChannel(RGB.R);
            ExtractChannel cG = new ExtractChannel(RGB.G);
            ExtractChannel cB = new ExtractChannel(RGB.B);

            ExtractChannel cA = new ExtractChannel(RGB.A);

            YCbCrExtractChannel cL = new YCbCrExtractChannel(YCbCr.YIndex);

            List<Bitmap> maps = new List<Bitmap>();

            Bitmap BmpA = new Bitmap(BaseBitmap.Width, BaseBitmap.Height, PixelFormat.Format16bppGrayScale);
            Bitmap BmpR = new Bitmap(BaseBitmap.Width, BaseBitmap.Height, PixelFormat.Format16bppGrayScale);
            Bitmap BmpG = new Bitmap(BaseBitmap.Width, BaseBitmap.Height, PixelFormat.Format16bppGrayScale);
            Bitmap BmpB = new Bitmap(BaseBitmap.Width, BaseBitmap.Height, PixelFormat.Format16bppGrayScale);
            Bitmap BmpL = new Bitmap(BaseBitmap.Width, BaseBitmap.Height, PixelFormat.Format16bppGrayScale);

            BmpA = cA.Apply(new Bitmap(BaseBitmap));
            BmpR = cR.Apply(new Bitmap(BaseBitmap));
            BmpG = cG.Apply(new Bitmap(BaseBitmap));
            BmpB = cB.Apply(new Bitmap(BaseBitmap));
            BmpL = cL.Apply(new Bitmap(BaseBitmap));

            maps.Add(BmpA);
            maps.Add(BmpR);
            maps.Add(BmpG);
            maps.Add(BmpB);
            maps.Add(BmpL);

            Bitmap bmp = new Bitmap(BaseBitmap.Width, BaseBitmap.Height, PixelFormat.Format32bppArgb);
            bmp.MakeTransparent();

            bmp = new ReplaceChannel(RGB.A, maps[A]).Apply(bmp);
            bmp = new ReplaceChannel(RGB.R, maps[R]).Apply(bmp);
            bmp = new ReplaceChannel(RGB.G, maps[G]).Apply(bmp);
            bmp = new ReplaceChannel(RGB.B, maps[B]).Apply(bmp);

            ModifiedBitmap = new Bitmap(bmp);
        }
        

    }
}