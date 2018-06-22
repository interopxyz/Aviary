using System.Drawing;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Utilities
{
    public class mFitCrop : mFilters
    {

        ResizeBicubic EffectA = null;
        Crop EffectB = null;

        public mFitCrop(Bitmap SourceBitmap, Bitmap TargetBitmap)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            double Xratio = (((double)TargetBitmap.Width) / ((double)SourceBitmap.Width));
            double Yratio = (((double)TargetBitmap.Height) / ((double)SourceBitmap.Height));

            double Scale = 1;
            if (Xratio > Yratio) { Scale = Xratio; } else { Scale = Yratio; }

            if (Scale < 1) { Scale = 1; }

            int X = (int)(((double)SourceBitmap.Width) * Scale);
            int Y = (int)(((double)SourceBitmap.Height) * Scale);

            EffectA = new ResizeBicubic(X, Y);

            int XC = (int)(((double)(X - TargetBitmap.Width)) / 2.0);
            int XY = (int)(((double)(Y - TargetBitmap.Height)) / 2.0);


            EffectB = new Crop(new Rectangle(XC, XY, TargetBitmap.Width, TargetBitmap.Height));

            Sequence.Clear();
            Sequence.Add(EffectA);
            Sequence.Add(EffectB);

        }

    }
}
