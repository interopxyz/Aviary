using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionFill : mFilter
    {
        FillHoles Effect = new FillHoles();

        int MaxWidth = 20;
        int MaxHeight = 20;
        bool Couple = false;

        public mErosionFill(int maxWidth, int maxHeight, bool couple)
        {

            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            Couple = couple;

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;

            Effect = new FillHoles();

            Effect.MaxHoleWidth = MaxWidth;
            Effect.MaxHoleHeight = MaxHeight;
            Effect.CoupledSizeFiltering = couple;

            filter = Effect;
        }

    }
}