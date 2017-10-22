using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;
using System.Windows.Media;

namespace Macaw.Compiling.Modifiers
{
    public class mModifyResize : mModifiers
    {
        ResizeFilter Effect = new ResizeFilter();

        public mModifyResize(int Mode, int Type, int Width, int Height)
        {
            Effect = new ResizeFilter();
            Effect.Mode = (ResizeMode)Mode;
            Effect.BitmapScalingMode = (BitmapScalingMode)Type;
            Effect.Width = Unit.Pixel(Width);
            Effect.Height = Unit.Pixel(Height);

            Effect.Enabled = true;

            Modifiers.Clear();
            Modifiers.Add(Effect);
        }
    }
}