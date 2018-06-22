using Accord.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Utilities;
using System.Drawing;

namespace Macaw.Compositing
{
    public class mCompositeMerge
    {

        Merge Effect = new Merge();

        public Bitmap BitmapOver = null;
        public Bitmap BitmapUnder = null;
        public Bitmap ModifiedBitmap = null;

        public mCompositeMerge(Bitmap UnderlayBitmap, Bitmap OverlayBitmap)
        {

            BitmapUnder = new mSetFormat(UnderlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;

            ModifiedBitmap = BitmapUnder;

            Effect = new Merge(BitmapOver);

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}