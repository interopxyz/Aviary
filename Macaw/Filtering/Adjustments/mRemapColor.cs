using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments
{
    class mRemapColor : mFilter
    {
        ColorRemapping Effect = new ColorRemapping();

        int AdjustValue = 50;

        public mRemapColor(int Value)
        {

            AdjustValue = Value;

            BitmapType = 1;

            //Effect = new ColorRemapping(AdjustValue);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}