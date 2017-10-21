using System.Drawing;
using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Utilities
{
    public class mFitScale : mFilter
    {

        ResizeBicubic EffectA = null;
        Crop EffectB = null;

        public mFitScale(Bitmap SourceBitmap, Bitmap TargetBitmap)
        {

            BitmapType = 2;

            double Xratio = (((double)TargetBitmap.Width) / ((double)SourceBitmap.Width));
            double Yratio = (((double)TargetBitmap.Height) / ((double)SourceBitmap.Height));

            double Scale = 1;
            if (Xratio > Yratio) { Scale = Xratio; }else { Scale = Yratio; }

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
