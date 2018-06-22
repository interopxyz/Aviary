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
    public class mModifyColorKey : mModifier
    {
        ColorKeyFilter Effect = new ColorKeyFilter();

        public mModifyColorKey(wColor KeyColor, byte Tolerance)
        {
            Effect = new ColorKeyFilter();
            Effect.Color = new mImageColor(KeyColor).ToDynamicColor();
            Effect.ColorTolerance = Tolerance;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
