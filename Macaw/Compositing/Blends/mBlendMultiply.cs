using System.Drawing;

using SoundInTheory.DynamicImage.Layers;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Sources;

using Macaw.Utilities;

namespace Macaw.Compositing.Blends
{
    public class mBlendMultiply
    {
        Composition Comp = new Composition();
        ImageLayer LayerA = new ImageLayer();
        ImageLayer LayerB = new ImageLayer();
        GaussianBlurFilter Effect = new GaussianBlurFilter();
        
        public Bitmap ModifiedBitmap = null;

        public mBlendMultiply(Bitmap SourceBitmap, Bitmap TargetBitmap, double T)
        {
            ImageImageSource ImageA = new ImageImageSource();
            ImageA.Image = new mConvert(SourceBitmap).BitmapToSource();
            LayerA.Source = ImageA;

            Effect.Radius = (float)T;

            ImageImageSource ImageB = new ImageImageSource();
            ImageB.Image = new mConvert(TargetBitmap).BitmapToSource();
            LayerB.Source = ImageB;
            LayerB.BlendMode = BlendMode.Multiply;

            LayerA.Filters.Add(Effect);
            LayerB.Filters.Add(Effect);

            Comp.Layers.Add(LayerB);
            Comp.ImageFormat = DynamicImageFormat.Bmp;

            ModifiedBitmap = new Bitmap(new mConvert(Comp.GenerateImage().Image).SourceToBitmap());

        }

    }
}
