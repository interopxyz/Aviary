using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionOpening : mFilter
    {
        Opening Effect = new Opening();
        

        public mErosionOpening(int X, int Y)
        {

            BitmapType = 0;

            Effect = new Opening();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}