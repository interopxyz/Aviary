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
    public class mModifyBrightness : mModifiers
    {
        BrightnessAdjustmentFilter Effect = new BrightnessAdjustmentFilter();

        public mModifyBrightness(int Value)
        {
            Effect = new BrightnessAdjustmentFilter();
            Effect.Level = Value;
            Effect.Enabled = true;

            Modifiers.Clear();
            Modifiers.Add(Effect);
        }
    }
}
