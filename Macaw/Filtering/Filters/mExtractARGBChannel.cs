using Accord.Imaging;
using Accord.Imaging.Filters;
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
        
        public mExtractARGBChannel(short Channel)
        {
            
            BitmapType = mFilter.BitmapTypes.None;

            filter = new ExtractChannel(Channel);
            
        }

    }
}