using SoundInTheory.DynamicImage;
using SoundInTheory.DynamicImage.Filters;
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
    public class mModifyFlatten
    {
            mLayerImage LayerA = new mLayerImage();
            mLayerImage LayerB = new mLayerImage();

            public Bitmap ModifiedBitmap = null;

        public mModifyFlatten(Bitmap ForeImage, System.Drawing.Color BackColor)
            {

            LayerA = new mLayerImage(ForeImage, 0, 0, 0, 100);


            mQuickComposite Comp = new mQuickComposite(ForeImage, new mModifiers(), new wColor(BackColor));

            ModifiedBitmap = Comp.ModifiedBitmap;

        }
        }
    }