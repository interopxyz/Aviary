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

namespace Macaw.Compiling
{
    public class mLayerImage : mLayer
    {

        ImageLayer CurrentLayer = new ImageLayer();
        ImageImageSource LayerImage = new ImageImageSource();
        ImageImageSource MaskImage = new ImageImageSource();
        OpacityAdjustmentFilter Opacity = new OpacityAdjustmentFilter();
        ClippingMaskFilter Mask = new ClippingMaskFilter();

        public mLayerImage()
        {
            
        }

        public mLayerImage(Bitmap BitmapLayerImage, int BlendType, int X, int Y, byte T)
        {
            Mask.Enabled  = false;

            CurrentLayer = new ImageLayer();
            LayerImage.Image = new mConvert(BitmapLayerImage).BitmapToWritableBitmap();
            
            CurrentLayer.Source = LayerImage;
            CurrentLayer.BlendMode = (BlendMode)BlendType;

            Opacity.Opacity = T;
            
            CurrentLayer.X = X;
            CurrentLayer.Y = Y;

            CompositionLayer = CurrentLayer;
        }

        public override void ApplyStandardFilters()
        {
            CurrentLayer.Filters.Add(Opacity);
            CurrentLayer.Filters.Add(Mask);
        }

        public override void SetMask(Bitmap MaskBitmap)
        {
            Mask.Enabled = true;
            MaskImage.Image = new mConvert(MaskBitmap).BitmapToWritableBitmap();
            Mask.MaskImage = MaskImage;
        }

        public override void ApplyFilters()
        {
            foreach (Filter FilterObject in Modifiers.Modifiers)
            {
                CompositionLayer.Filters.Add(FilterObject);
            }
        }

    }
}
