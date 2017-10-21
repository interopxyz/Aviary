using AForge.Imaging.Filters;
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

        public mIterate(Bitmap SourceBitmap, mFilter Filter, int Iterations)
        {

            SourceBitmap = new mSetFormat(SourceBitmap, Filter.BitmapType).ModifiedBitmap;

            ModifiedBitmap = SourceBitmap;
            
            FilterIterator Iterator = new FilterIterator(Filter.Sequence, Iterations);

            ModifiedBitmap = Iterator.Apply(SourceBitmap);
        }

    }
}
