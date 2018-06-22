using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mPadding : mFilter
    {

        CanvasCrop Effect = null;

        public mPadding(int L, int R, int T, int B, int W, int H, System.Drawing.Color FillColor)
        {
            BitmapType = mFilter.BitmapTypes.Rgb24bpp;

            Effect = new CanvasCrop(new System.Drawing.Rectangle(L,B,W-R,H-T));
            Effect.FillColorRGB = FillColor;

            filter = Effect;
        }

    }
}
