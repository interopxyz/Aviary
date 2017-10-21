using System.Drawing;
using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Utilities
{
    public class mFitStretch : mFilter
    {

        ResizeBicubic Effect = null;

        public mFitStretch(Bitmap SourceBitmap, Bitmap TargetBitmap)
        {

            BitmapType = 2;

            Effect = new ResizeBicubic(TargetBitmap.Width, TargetBitmap.Height);


            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
