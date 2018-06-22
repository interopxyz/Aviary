using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mSmoothMedian : mFilter
    {
        public int EffectSize = 1;
        public mSmoothMedian(int effectSize)
        {

            EffectSize = effectSize;

            BitmapType = BitmapTypes.None;

            Median Effect = new Median();

            Effect.Size = EffectSize;

            filter = Effect;
        }

    }
}
