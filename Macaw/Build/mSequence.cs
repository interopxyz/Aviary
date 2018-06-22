using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macaw.Build
{

    public class mSequence : mFilters
    { 

        public mSequence()
        {
        }

        public void ClearSequence()
        {
            Sequence.Clear();
            BitmapType = mFilter.BitmapTypes.None;
        }

        public void AddFilter(mFilters Filters)
        {
            if (Filters.BitmapType < BitmapType) { BitmapType = Filters.BitmapType; }
            foreach(IFilter filter in Filters.Sequence)
            {
                Sequence.Add(filter);
            }
        }

        public void AddFilter(mFilter Filter)
        {
            if (Filter.BitmapType < BitmapType) { BitmapType = Filter.BitmapType; }
            Sequence.Add(Filter.filter);
        }

    }
}
