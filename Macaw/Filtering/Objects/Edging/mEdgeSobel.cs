using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Objects.Edging
{
    public class mEdgeSobel : mFilter
    {
        SobelEdgeDetector Effect = new SobelEdgeDetector();
        

        public mEdgeSobel()
        {

            BitmapType = 0;

            Effect = new SobelEdgeDetector();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}