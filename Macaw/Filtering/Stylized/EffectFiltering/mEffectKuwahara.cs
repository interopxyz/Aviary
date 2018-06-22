using Accord.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mEffectKuwahara: mFilter
    {

        Kuwahara Effect = new Kuwahara();

        int KernalSize = 5;

        public mEffectKuwahara(int kernalSize)
        {

            BitmapType = mFilter.BitmapTypes.GrayscaleBT709;

            KernalSize = kernalSize;

            Effect = new Kuwahara();
            Effect.Size = KernalSize;

            filter = Effect;
        }

    }
}
