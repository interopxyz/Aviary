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
    public class mStreakHorizontal : mFilter
    {
        HorizontalRunLengthSmoothing Effect = new HorizontalRunLengthSmoothing();

        int GapSize = 32;
        bool Borders = true;

        public mStreakHorizontal(int GapSizeInteger, bool borders)
        {

            BitmapType = BitmapTypes.GrayscaleBT709;

            GapSize = GapSizeInteger;
            Borders = borders;

            Effect = new HorizontalRunLengthSmoothing(GapSize);
            Effect.ProcessGapsWithImageBorders = Borders;
            filter = Effect;
        }

    }
}

