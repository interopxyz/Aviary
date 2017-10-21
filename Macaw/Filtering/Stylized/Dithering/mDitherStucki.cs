using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherStucki : mFilter
    {
        StuckiDithering Effect = new StuckiDithering();
        
        public mDitherStucki()
        {

            BitmapType = 0;

            Effect = new StuckiDithering();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

