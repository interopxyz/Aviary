using AForge.Imaging;
using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Filtering.Filters
{
    public class mExtractARGBChannel : mFilter
    {
        ExtractChannel Effect = new ExtractChannel();
        
        public mExtractARGBChannel(short Channel)
        {
            

            BitmapType = 1;

            Effect = new ExtractChannel(Channel);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}