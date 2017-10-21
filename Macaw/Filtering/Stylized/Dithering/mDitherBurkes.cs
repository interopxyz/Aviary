using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized

{
    public class mDitherBurkes : mFilter
    {
        BurkesDithering Effect = new BurkesDithering();
        
        public mDitherBurkes()
        {

            BitmapType = 0;

            Effect = new BurkesDithering();

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}

