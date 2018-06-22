using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Adjustments.AdjustColor
{
    public class mAdjustHue : mFilter
    {
        public int Hue = 0;
        public mAdjustHue(int hue)
        {

            Hue = hue;

            BitmapType = BitmapTypes.None;

            filter = new HueModifier(Hue);
            
        }

    }
}
