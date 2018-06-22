using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Macaw.Utilities;
using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Layers;
using SoundInTheory.DynamicImage.Sources;
using SoundInTheory.DynamicImage.Fluent;
using SoundInTheory.DynamicImage.Filters;
using Macaw.Compiling.Modifiers;

namespace Macaw.Compiling
{
    public class mLayerImage : mLayer
    {

        public mLayerImage()
        {
            
        }

        public mLayerImage(Bitmap BitmapLayerImage, int BlendType, double T)
        {
            
            LayerImage = BitmapLayerImage;
            
            Blend = (BlendMode)BlendType;

            SetOpacity(T);
        }

    }
}
