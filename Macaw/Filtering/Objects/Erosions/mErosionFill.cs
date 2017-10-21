using AForge.Imaging.Filters;
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

        public mErosionFill(int MaxHoleWidth, int MaxHoleHeight)
        {

            MaxWidth = MaxHoleWidth;
            MaxHeight = MaxHoleHeight;

            BitmapType = 0;

            Effect = new FillHoles();

            Effect.MaxHoleWidth = MaxWidth;
            Effect.MaxHoleHeight = MaxHeight;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}