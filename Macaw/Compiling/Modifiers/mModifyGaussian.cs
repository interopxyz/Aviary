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
    public class mModifyGaussian : mModifier
    {
        GaussianBlurFilter Effect = new GaussianBlurFilter();

        public mModifyGaussian(int Value)
        {
            Effect = new GaussianBlurFilter();
            Effect.Radius= Value;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
