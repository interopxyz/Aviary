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
    public class mModifyBorder : mModifier
    {
        BorderFilter Effect = new BorderFilter();

        public mModifyBorder(wColor BorderColor, int Radius)
        {
            Effect = new BorderFilter();

            Fill fill = new Fill();
            fill.BackgroundColor = new mImageColor(BorderColor).ToDynamicColor();

            Effect.Fill = fill;
            Effect.Width = Radius;
            Effect.Enabled = true;

            filter = Effect;
        }
    }
}
