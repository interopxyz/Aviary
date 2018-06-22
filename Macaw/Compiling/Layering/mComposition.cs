using Macaw.Compiling.Modifiers;
using Macaw.Utilities;
using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Caching;
using SoundInTheory.DynamicImage.Filters;
using System.Collections.Generic;
using System.Drawing;
using Wind.Presets;
using Wind.Types;

namespace Macaw.Compiling
{
    public class mComposition
    {
        public string Type = "Composition";
        Composition Composite = new Composition();
        public List<mModifier> Modifiers = new List<mModifier>();
        public Bitmap CompositionBitmap = null;

        public mComposition()
        {
            Composite.Layers.Clear();
        }

        public mComposition(List<mLayer> Layers)
        {
            Composite.Layers.Clear();
            foreach(mLayer layer in Layers )
            {
                layer.ApplyBitmap();
                layer.CompositionLayer.Filters.Clear();
                layer.ApplyFilters();
                layer.ApplyStandardFilters();
                Composite.Layers.Add(layer.CompositionLayer);
            }
        }

        public void BuildComposition()
        {
            Composite.ImageFormat = DynamicImageFormat.Png;
            Composite.Fill.BackgroundColor = new mImageColor(wColors.Transparent).ToDynamicColor();
            CompositionBitmap = new Bitmap(new mConvert(Composite.GenerateImage().Image).SourceToBitmap());
        }

        public void ApplyFilters()
        {
            foreach (mModifier FilterObject in Modifiers)
            {
                Composite.Filters.Add(FilterObject.filter);
            }
        }

    }
}
