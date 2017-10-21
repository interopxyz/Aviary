using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mSmoothConservative : mFilter
    {
        ConservativeSmoothing Effect = new ConservativeSmoothing();

        public mSmoothConservative()
        {

            BitmapType = 1;
            
            Effect = new ConservativeSmoothing();
            
            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
