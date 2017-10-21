using AForge.Imaging.Filters;
using Macaw.Utilities;
using System.Drawing;


namespace Macaw.Compositing
{
    public class mCompositeMove
    {

        MoveTowards Effect = new MoveTowards();

        public Bitmap BitmapOver = null;
        public Bitmap BitmapUnder = null;
        public Bitmap ModifiedBitmap = null;

        int Step; 

        public mCompositeMove(Bitmap UnderlayBitmap, Bitmap OverlayBitmap, int StepSize)
        {

            Step = StepSize;

            BitmapUnder = new mSetFormat(UnderlayBitmap, 2).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, 2).ModifiedBitmap;

            ModifiedBitmap = BitmapUnder;

            Effect = new MoveTowards(BitmapOver,Step);

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}