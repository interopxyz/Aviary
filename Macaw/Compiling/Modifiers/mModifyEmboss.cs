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
    public class mModifyEmboss : mModifier
    {
        EmbossFilter Effect = new EmbossFilter();

        public mModifyEmboss( float Value)
        {
            Effect = new EmbossFilter();
            Effect.Amount = Value;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
