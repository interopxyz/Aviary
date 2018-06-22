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
    public class mModifyRotation : mModifier
    {
        RotationFilter Effect = new RotationFilter();

        public mModifyRotation(int angle)
        {
            Effect = new RotationFilter();
            Effect.Angle = angle;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
