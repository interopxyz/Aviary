using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Erosions
{
    public class mErosionClosing : mFilter
    {
        Closing Effect = new Closing();
        

        public mErosionClosing()
        {

            BitmapType = 0;
            
            Effect = new Closing();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}