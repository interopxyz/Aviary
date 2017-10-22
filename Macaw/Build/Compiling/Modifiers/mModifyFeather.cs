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
    public class mModifyFeather : mModifiers
    {
        FeatherFilter Effect = new FeatherFilter();

        public mModifyFeather( int Value)
        {
            Effect = new FeatherFilter();
            Effect.Radius = Value;
            Effect.Shape = FeatherShape.Rectangle;
            Effect.Enabled = true;

            Modifiers.Clear();
            Modifiers.Add(Effect);
        }
    }
}
