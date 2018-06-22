using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Imaging.ColorReduction;

using Macaw.Utilities;
using Macaw.Filtering;

namespace Macaw.Build
{
    public class mApply
    {
        public Bitmap ModifiedBitmap = new Bitmap(10,10);

        public mApply(Bitmap SourceBitmap, mFilter Filter)
        {
            Bitmap TargetBitmap = (Bitmap)SourceBitmap.Clone();
            TargetBitmap = new mSetFormat(TargetBitmap, Filter.BitmapType).ModifiedBitmap;
            TargetBitmap = Filter.filter.Apply(TargetBitmap);

            ModifiedBitmap = (Bitmap)TargetBitmap.Clone();
            ModifiedBitmap.CopyResolutionFrom(SourceBitmap);
            ModifiedBitmap = Accord.Imaging.Image.Clone(ModifiedBitmap, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //ModifiedBitmap.SetResolution(SourceBitmap.HorizontalResolution, SourceBitmap.VerticalResolution);

        }

        public mApply(Bitmap SourceBitmap, mFilter Filter, int Width, int Height)
        {
            Bitmap TargetBitmap = (Bitmap)SourceBitmap.Clone();
            TargetBitmap = new mSetFormat(TargetBitmap, Filter.BitmapType).ModifiedBitmap;
            TargetBitmap = Filter.filter.Apply(TargetBitmap);

            ModifiedBitmap = (Bitmap)TargetBitmap.Clone();
            ModifiedBitmap.SetResolution(Width, Height);
            ModifiedBitmap = Accord.Imaging.Image.Clone(ModifiedBitmap, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }


    }
}
