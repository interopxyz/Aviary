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
    public class mModifyColorTint : mModifier
    {
        ColorTintFilter Effect = new ColorTintFilter();

        public mModifyColorTint(wColor FillColor, int Value)
        {
            Effect = new ColorTintFilter();
            Effect.Color = new mImageColor(FillColor).ToDynamicColor();
            Effect.Amount = Value;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
