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
    public class mModifyVignette : mModifier
    {
        VignetteFilter Effect = new VignetteFilter();

        public mModifyVignette( )
        {
            Effect = new VignetteFilter();
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
