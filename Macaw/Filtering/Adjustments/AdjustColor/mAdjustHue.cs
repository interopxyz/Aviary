using AForge.Imaging.Filters;
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
        HueModifier Effect = new HueModifier();

        int AdjustValue = 50;

        public mAdjustHue(int Value)
        {

            AdjustValue = Value;

            BitmapType = 1;

            Effect = new HueModifier(AdjustValue);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
