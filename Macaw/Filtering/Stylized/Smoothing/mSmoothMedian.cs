using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mSmoothMedian : mFilter
    {
        Median Effect = new Median();

        int SmoothSize = 10;

        public mSmoothMedian(int SizeValue)
        {

            SmoothSize = SizeValue;

            BitmapType = 1;

            Effect = new Median();

            Effect.Size = SmoothSize;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
