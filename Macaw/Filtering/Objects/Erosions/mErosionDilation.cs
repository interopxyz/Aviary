using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionDilation : mFilter
    {
        BinaryDilatation3x3 Effect = new BinaryDilatation3x3();
        

        public mErosionDilation(int X, int Y)
        {

            BitmapType = 0;
            
            Effect = new BinaryDilatation3x3();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}