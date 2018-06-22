using System.Drawing;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Utilities
{
    public class mFitStretch : mFilters
    {

        ResizeBicubic Effect = null;

        public mFitStretch(Bitmap SourceBitmap, Bitmap TargetBitmap)
        {

            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Effect = new ResizeBicubic(TargetBitmap.Width, TargetBitmap.Height);


            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
