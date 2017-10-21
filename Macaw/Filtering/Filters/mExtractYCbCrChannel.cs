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
    public class mExtractYCbCrChannel : mFilter
    {
        YCbCrExtractChannel Effect = new YCbCrExtractChannel();

        public mExtractYCbCrChannel(short Channel)
        {


            BitmapType = 1;

            Effect = new YCbCrExtractChannel(Channel);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}