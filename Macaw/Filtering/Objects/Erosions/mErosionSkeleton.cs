using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;
using Wind.Types;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionSkeleton : mFilter
    {
        SimpleSkeletonization Effect = new SimpleSkeletonization();

        int Background = 0;
        int Foreground = 255;

        public mErosionSkeleton(int background, int foreground)
        {
            Background = background;
            Foreground = foreground;

            BitmapType = mFilter.BitmapTypes.None;

            Effect = new SimpleSkeletonization();
            Effect.Background = (byte)Background;
            Effect.Foreground = (byte)Foreground;

            filter = Effect;
        }

    }
}