using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments
{
    public class mNormalizeExtents : mFilters
    {
        ContrastStretch Effect = new ContrastStretch();
        
        public mNormalizeExtents()
        {
            

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new ContrastStretch();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}