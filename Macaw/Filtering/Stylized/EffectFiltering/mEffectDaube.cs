using AForge.Imaging.Filters;
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

        int Radius = 5;

        public mEffectDaube(int EffectRadius)
        {

            BitmapType = 1;

            Radius = EffectRadius;

            Effect = new OilPainting();
            Effect.BrushSize = Radius;

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
