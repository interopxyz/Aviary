using Macaw.Compiling.Modifiers;
using Macaw.Utilities;
using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Caching;
using SoundInTheory.DynamicImage.Filters;
using System.Collections.Generic;
using System.Drawing;
using Wind.Types;

namespace Macaw.Compiling
{
    public class mComposition
    {
        public string Type = "Composition";

        Composition Composite = new Composition();

        public mModifiers Filters = new mModifiers();


        public Bitmap CompositionBitmap = null;

        public mComposition()
        {
            Composite.Layers.Clear();
        }

        public mComposition(List<mLayer> Layers,int W, int H)
        {
            Composite.Layers.Clear();
            foreach(mLayer layer in Layers )
            {
                layer.CompositionLayer.Filters.Clear();
                layer.ApplyStandardFilters();
                layer.ApplyFilters();
                Composite.Layers.Add(layer.CompositionLayer);
            }
        }

        public void BuildComposition()
        {
            Composite.ImageFormat = DynamicImageFormat.Png;
            Composite.Fill.BackgroundColor = new mImageColor(new wColor().Transparent()).ToDynamicColor();
               
            CompositionBitmap = new Bitmap(new mConvert(Composite.GenerateImage().Image).SourceToBitmap());

        }

        public void ApplyFilters()
        {
            foreach (Filter FilterObject in Filters.Modifiers)
            {
                Composite.Filters.Add(FilterObject);
            }
        }

    }
}
