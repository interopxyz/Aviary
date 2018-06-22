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
    public class mModifyContrast : mModifier
    {
        ContrastAdjustmentFilter Effect = new ContrastAdjustmentFilter();

        public mModifyContrast( int Value)
        {
            Effect = new ContrastAdjustmentFilter();
            Effect.Level = Value;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
