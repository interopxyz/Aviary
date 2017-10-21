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
    public class mSharpenSimple : mFilter
    {
        Sharpen Effect = new Sharpen();
        

        public mSharpenSimple()
        {

            BitmapType = 1;
            
            Effect = new Sharpen();
            

            Sequence.Clear();
            Sequence.Add(Effect);

        }

    }
}
