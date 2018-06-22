using Accord.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Build
{
    public class mFilterMask : mFilters
    {
        public MaskedFilter Effect = null;

        Bitmap MaskBitmap = null;

        public mFilterMask(Bitmap SourceBitmap, mFilters Filter)
        {

            BitmapType = 0;

            MaskBitmap = new Bitmap(SourceBitmap);

            MaskBitmap = new mSetFormat(MaskBitmap, mFilter.BitmapTypes.GrayScale16bpp).ModifiedBitmap;
            
            Effect = new MaskedFilter(Filter.Sequence[0], MaskBitmap);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
