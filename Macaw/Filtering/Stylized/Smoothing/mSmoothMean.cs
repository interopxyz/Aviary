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
    public class mSmoothMean : mFilter
    {
        public int Divisor = 0;
        public mSmoothMean(int divisor)
        {

            Divisor = divisor;

            BitmapType = BitmapTypes.None;

            Mean Effect = new Mean();

            Effect.Divisor = Divisor;

            filter = Effect;
        }

    }
}
