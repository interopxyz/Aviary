using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mPaintBayer : mFilters
    {
        BayerFilterOptimized Effect = new BayerFilterOptimized();

        public mPaintBayer()
        {

            BitmapType = 0;

            Effect = new BayerFilterOptimized();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

