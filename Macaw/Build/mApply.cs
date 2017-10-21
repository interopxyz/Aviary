using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.ColorReduction;

using Macaw.Utilities;
using Macaw.Filtering;

namespace Macaw.Build
{
    public class mApply
    {

        public Bitmap ModifiedBitmap = null;

        public mApply(Bitmap SourceBitmap, mFilter Filter)
        {

            SourceBitmap = new mSetFormat(SourceBitmap, Filter.BitmapType).ModifiedBitmap;

            ModifiedBitmap = SourceBitmap;
            
            ModifiedBitmap = Filter.Sequence.Apply(SourceBitmap);
        }
        

    }
}
