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
    public class mSmoothMean : mFilter
    {
        Mean Effect = new Mean();

        int SmoothSize = 10;

        public mSmoothMean(int SizeValue)
        {

            SmoothSize = SizeValue;

            BitmapType = 1;

            Effect = new Mean();

            Effect.Divisor = SmoothSize;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
