using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments
{
    public class mNormalizeHistogram : mFilters
    {
        HistogramEqualization Effect = new HistogramEqualization();
        
        public mNormalizeHistogram()
        {
            

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new HistogramEqualization();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}