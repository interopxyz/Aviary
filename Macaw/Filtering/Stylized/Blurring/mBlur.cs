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
    public class mBlur : mFilter
    {
        Blur Effect = new Blur();
        
        public mBlur()
        {

            BitmapType = 1;

            Effect = new Blur();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
