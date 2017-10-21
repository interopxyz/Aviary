using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoundInTheory.DynamicImage;
using System.Drawing;
using SoundInTheory.DynamicImage.Filters;

namespace Macaw.Compiling
{
    public class mLayer
    {

        public string Type = "Layer";

        public Layer CompositionLayer = null;
        public mModifiers Modifiers = new mModifiers();

        public mLayer()
        {

        }

        public virtual void ClearModifiers()
        {
            Modifiers.Modifiers.Clear();
        }

        public virtual void AddModifiers(mModifiers Modifiers)
        {

            foreach (Filter Modifier in Modifiers.Modifiers)
            {
                Modifiers.Modifiers.Add(Modifier);
            }
        }

        public virtual void ApplyFilters()
        {

        }

        public virtual void ApplyStandardFilters()
        {

        }

        public virtual void SetMask(Bitmap MaskBitmap)
        {

        }
    }
}
