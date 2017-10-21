using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Edging
{
    public class mEdgeHomogenity : mFilter
    {
        HomogenityEdgeDetector Effect = new HomogenityEdgeDetector();
        

        public mEdgeHomogenity()
        {

            BitmapType = 0;

            Effect = new HomogenityEdgeDetector();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}