using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Macaw.Filtering;

namespace Macaw.Filtering.Stylized
{
    public class mEffectDaube : mFilter
    {

        OilPainting Effect = new OilPainting();

        int BrushSize = 5;

        public mEffectDaube(int brushSize)
        {

            BitmapType = mFilter.BitmapTypes.None;

            BrushSize = brushSize;

            Effect = new OilPainting();
            Effect.BrushSize = BrushSize;

            filter = Effect;
        }

    }
}
