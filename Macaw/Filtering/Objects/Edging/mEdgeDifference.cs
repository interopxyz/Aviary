using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Edging
{
    public class mEdgeDifference : mFilter
    {
        DifferenceEdgeDetector Effect = new DifferenceEdgeDetector();
        

        public mEdgeDifference()
        {

            BitmapType = 0;

            Effect = new DifferenceEdgeDetector();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}