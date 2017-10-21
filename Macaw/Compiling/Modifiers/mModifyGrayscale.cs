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
    public class mModifyGrayscale : mModifiers
    {
        GrayscaleFilter Effect = new GrayscaleFilter();

        public mModifyGrayscale()
        {
            Effect = new GrayscaleFilter();
            Effect.Enabled = true;

            Modifiers.Clear();
            Modifiers.Add(Effect);
        }
    }
}
