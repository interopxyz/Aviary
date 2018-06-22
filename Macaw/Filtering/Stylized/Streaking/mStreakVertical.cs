using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mStreakVertical : mFilter
    {
        VerticalRunLengthSmoothing Effect = new VerticalRunLengthSmoothing();

        int GapSize = 32;
        bool Borders = false;

        public mStreakVertical(int GapSizeInteger, bool borders)
        {

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;

            GapSize = GapSizeInteger;
            Borders = borders;

            Effect = new VerticalRunLengthSmoothing(GapSize);
            Effect.ProcessGapsWithImageBorders = Borders;

            filter = Effect;
        }

    }
}

