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
    public class mModifyCurvesValues : mModifiers
    {
        CurvesAdjustmentFilter Effect = new CurvesAdjustmentFilter();

        public mModifyCurvesValues(float Value)
        {
            Effect = new CurvesAdjustmentFilter();
            //Effect.Curves = new 
            Effect.Enabled = true;

            Modifiers.Clear();
            Modifiers.Add(Effect);
        }
    }
}
