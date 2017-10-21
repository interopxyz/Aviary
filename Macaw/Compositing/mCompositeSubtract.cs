using AForge.Imaging.Filters;
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

            BitmapUnder = new mSetFormat(UnderlayBitmap, 2).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, 2).ModifiedBitmap;

            ModifiedBitmap = UnderlayBitmap;

            Effect = new Subtract(BitmapOver);

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}