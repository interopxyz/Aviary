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
    public class mModifyResize : mModifier
    {
        ResizeFilter Effect = new ResizeFilter();
        public enum ResizingMode { Width, Height, Fill, Uniform, UniformFill};
        public enum ScalingMode {Unspecified, LowQuality, HighQuality, NearestNeighbor};
        public ResizingMode ResizingType = ResizingMode.Fill;
        public ScalingMode ScalingType = ScalingMode.Unspecified;

        public mModifyResize(ResizingMode Mode, ScalingMode Type, int Width, int Height)
        {
            ResizingType = Mode;
            ScalingType = Type;

            Effect = new ResizeFilter();
            Effect.Mode = (ResizeMode)(int)ResizingType;
            Effect.BitmapScalingMode = (BitmapScalingMode)(int)ScalingType;
            Effect.Width = Unit.Pixel(Width);
            Effect.Height = Unit.Pixel(Height);

            Effect.Enabled = true;

            filter = Effect;
        }
    }
}