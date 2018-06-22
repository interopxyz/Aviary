using Accord.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Compositing
{
    public class mDifferenceThreshold
    {

        ThresholdedDifference Effect = new ThresholdedDifference();

        public Bitmap BitmapOver = null;
        public Bitmap BitmapUnder = null;
        public Bitmap ModifiedBitmap = null;

        int Threshold = 50;

        public mDifferenceThreshold(Bitmap UnderlayBitmap, Bitmap OverlayBitmap, int ThresholdValue)
        {

            Threshold = ThresholdValue;

            BitmapUnder = new mSetFormat(UnderlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, mFilter.BitmapTypes.Rgb24bpp).ModifiedBitmap;

            ModifiedBitmap = UnderlayBitmap;

            Effect = new ThresholdedDifference(Threshold);
            
            Effect.OverlayImage = BitmapOver;

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}