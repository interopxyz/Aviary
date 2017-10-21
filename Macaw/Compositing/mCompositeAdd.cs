using AForge.Imaging.Filters;
using Macaw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Compositing
{
    public class mCompositeAdd
    {

        Add Effect = new Add();

        public Bitmap BitmapOver = null;
        public Bitmap BitmapUnder = null;
        public Bitmap ModifiedBitmap = null;


        public mCompositeAdd(Bitmap UnderlayBitmap, Bitmap OverlayBitmap)
        {

            BitmapUnder = new mSetFormat(UnderlayBitmap, 2).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, 2).ModifiedBitmap;

            ModifiedBitmap = UnderlayBitmap;

            Effect = new Add(BitmapOver);

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}