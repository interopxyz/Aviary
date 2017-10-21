using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mDitherSierra : mFilter
    {
        SierraDithering Effect = new SierraDithering();
        
        public mDitherSierra()
        {

            BitmapType = 0;

            Effect = new SierraDithering();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

