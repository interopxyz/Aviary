using Accord.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Utilities;
using System.Drawing;


namespace Macaw.Compositing
{
    public class mCompositeSubtract
    {

        Subtract Effect = new Subtract();

        public Bitmap BitmapOver = null;
        public Bitmap BitmapUnder = null;
        public Bitmap ModifiedBitmap = null;
        

        public mCompositeSubtract(Bitmap UnderlayBitmap, Bitmap OverlayBitmap)
        {

            BitmapUnder = new mSetFormat(UnderlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;

            ModifiedBitmap = UnderlayBitmap;

            Effect = new Subtract(BitmapOver);

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}