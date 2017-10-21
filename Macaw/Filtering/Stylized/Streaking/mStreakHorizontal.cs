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
    public class mStreakHorizontal : mFilter
    {
        HorizontalRunLengthSmoothing Effect = new HorizontalRunLengthSmoothing();

        int GapSize = 32;

        public mStreakHorizontal(int GapSizeInteger)
        {

            BitmapType = 0;

            GapSize = GapSizeInteger;

            Effect = new HorizontalRunLengthSmoothing(GapSize);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

