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
    public class mSharpenHighBoost : mFilter
    {

        public HighBoost Effect = new HighBoost();

        public int Boost = 10;

        public mSharpenHighBoost(int boost)
        {

            Boost = boost;

            BitmapType = mFilter.BitmapTypes.None;

            Effect.Boost = Boost;

            filter = Effect;

        }

    }
}
