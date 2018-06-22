using Accord.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Utilities;
using System.Drawing;


namespace Macaw.Compositing
{
    public class mCompositeMorph
    {

        Morph Effect = new Morph();

        public Bitmap BitmapOver = null;
        public Bitmap BitmapUnder = null;
        public Bitmap ModifiedBitmap = null;

        public mCompositeMorph(Bitmap UnderlayBitmap, Bitmap OverlayBitmap, double Parameter)
        {

            BitmapUnder = new mSetFormat(UnderlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;
            BitmapOver = new  mSetFormat(OverlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;

            ModifiedBitmap = BitmapUnder;

            Effect = new Morph(BitmapOver);
            Effect.SourcePercent = Parameter;

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}