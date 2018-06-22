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
    public class mApplySequence
    {

        public Bitmap ModifiedBitmap = null;

        public mApplySequence(Bitmap SourceBitmap, mFilters Filter)
        {

            SourceBitmap = new mSetFormat(SourceBitmap, Filter.BitmapType).ModifiedBitmap;

            ModifiedBitmap = SourceBitmap;
            
            ModifiedBitmap = Filter.Sequence.Apply(SourceBitmap);
        }
        

    }
}
