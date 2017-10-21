using Macaw.Utilities;
using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Filters;
using SoundInTheory.DynamicImage.Layers;
using SoundInTheory.DynamicImage.Sources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Compiling.Modifiers
{
    public class mQuickComposite
    {
        ImageImageSource image = new ImageImageSource();
        ImageLayer layer = new ImageLayer();
        Composition CompositionObject = new Composition();

        public Bitmap ModifiedBitmap = null;

        public mQuickComposite(Bitmap B, mModifiers M)
        {
             image = new ImageImageSource();
             layer = new ImageLayer();
             CompositionObject = new Composition();

            image.Image = new mConvert(B).BitmapToWritableBitmap();
            layer.Source = image;

            foreach (Filter Fltr in M.Modifiers)
            {
                layer.Filters.Add(Fltr);
            }
            CompositionObject.Layers.Add(layer);

            CompositionObject.ImageFormat = DynamicImageFormat.Png;

            ModifiedBitmap = new Bitmap(new mConvert(CompositionObject.GenerateImage().Image).SourceToBitmap());

        }

        public mQuickComposite(Bitmap B, mModifiers M, wColor BackgroundColor)
        {
            image = new ImageImageSource();
            layer = new ImageLayer();
            CompositionObject = new Composition();
            
            image.Image = new mConvert(B).BitmapToWritableBitmap();
            layer.Source = image;

            foreach (Filter Fltr in M.Modifiers)
            {
                layer.Filters.Add(Fltr);
            }

            Fill fill = new Fill();
            fill.Type = FillType.Solid;
            fill.BackgroundColor = new mImageColor(BackgroundColor).ToDynamicColor();

            CompositionObject.ColorDepth = 16;
            CompositionObject.ImageFormat = DynamicImageFormat.Png;
            CompositionObject.Fill = fill;

            RectangleShapeLayer rect = new RectangleShapeLayer();
            rect.Fill = fill;
            rect.Width = B.Width;
            rect.Height = B.Height;
            //CompositionObject.Layers.Add(rect);

            CompositionObject.Layers.Add(layer);

            GeneratedImage genImage = CompositionObject.GenerateImage();
            genImage.Properties.ColorDepth = 16;
            genImage.Properties.Format = DynamicImageFormat.Png;

            ModifiedBitmap = new mConvert(genImage.Image).SourceToBitmap();

        }
    }
}
