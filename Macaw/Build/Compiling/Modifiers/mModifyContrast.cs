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
    public class mModifyContrast : mModifiers
    {
        ContrastAdjustmentFilter Effect = new ContrastAdjustmentFilter();

        public mModifyContrast( int Value)
        {
            Effect = new ContrastAdjustmentFilter();
            Effect.Level = Value;
            Effect.Enabled = true;

            Modifiers.Clear();
            Modifiers.Add(Effect);
        }
    }
}
