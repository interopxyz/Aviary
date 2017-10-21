using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherFloydSteinberg : mFilter
    {
        FloydSteinbergDithering Effect = new FloydSteinbergDithering();
        
        public mDitherFloydSteinberg()
        {

            BitmapType = 0;

            Effect = new FloydSteinbergDithering();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

