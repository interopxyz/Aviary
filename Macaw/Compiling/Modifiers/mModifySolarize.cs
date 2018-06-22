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
    public class mModifySolarize : mModifier
    {
        SolarizeFilter Effect = new SolarizeFilter();

        public mModifySolarize( )
        {
            Effect = new SolarizeFilter();
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
