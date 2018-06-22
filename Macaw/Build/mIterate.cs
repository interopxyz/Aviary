using Accord.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Macaw.Build
{
    public class mIterate
    {

        public Bitmap ModifiedBitmap = null;

        public mIterate(Bitmap SourceBitmap, mFilters Filter, int Iterations)
        {
            ModifiedBitmap = (Bitmap)SourceBitmap.Clone();
            ModifiedBitmap = new mSetFormat(ModifiedBitmap, Filter.BitmapType).ModifiedBitmap;
            
            FilterIterator Iterator = new FilterIterator(Filter.Sequence, Iterations);
            
            ModifiedBitmap = Iterator.Apply(ModifiedBitmap);
            ModifiedBitmap = new mSetFormat(ModifiedBitmap, Filter.BitmapType).ModifiedBitmap;
            ModifiedBitmap.SetResolution(SourceBitmap.HorizontalResolution, SourceBitmap.VerticalResolution);
        }

    }
}
