using AForge.Imaging.Filters;
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

            BitmapUnder = new mSetFormat(UnderlayBitmap, 2).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, 2).ModifiedBitmap;

            ModifiedBitmap = BitmapUnder;

            Effect = new Merge(BitmapOver);

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}