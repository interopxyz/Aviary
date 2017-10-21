using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mDitherBayer : mFilter
    {
        BayerDithering Effect = new BayerDithering();
        
        public mDitherBayer()
        {

            BitmapType = 0;

            Effect = new BayerDithering();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

