using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Macaw.Build
{

    public class mSequence : mFilter
    {
        public List<int> BitmapTypes = new List<int>();

        public mSequence()
        {
        }

        public void ClearSequence()
        {
            Sequence.Clear();
            BitmapTypes.Clear();
        }

        public void AddFilter(mFilter Filter)
        {
            BitmapTypes.Add(Filter.BitmapType);
            Sequence.Add(Filter.Sequence[0]);
        }

        public void SetBitmapType()
        {
            int[] ArrBitmapTypes = BitmapTypes.ToArray();

            Array.Sort(ArrBitmapTypes);
            BitmapType = ArrBitmapTypes[0];
        }

    }
}
