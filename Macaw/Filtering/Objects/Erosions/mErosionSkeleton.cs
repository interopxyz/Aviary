using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionSkeleton : mFilter
    {
        SimpleSkeletonization Effect = new SimpleSkeletonization();
        

        public mErosionSkeleton()
        {

            BitmapType = 0;

            Effect = new SimpleSkeletonization();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}