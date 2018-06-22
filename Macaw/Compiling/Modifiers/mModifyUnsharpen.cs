using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Compiling.Modifiers
{
    public class mModifyUnsharpen : mModifier
    {
        UnsharpMaskFilter Effect = new UnsharpMaskFilter();

        public mModifyUnsharpen(int Amount, int Radius)
        {
            Effect = new UnsharpMaskFilter();
            Effect.Amount = Amount;
            Effect.Radius = Radius;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}