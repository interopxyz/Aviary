using AForge.Imaging.Filters;
using Macaw.Textures;
using Macaw.Utilities;
using System.Drawing;

namespace Macaw.Compositing
{
    public class mCompositeTextureMorph
    {

        TexturedMerge Effect = null;

        mTexture MaskTexture = new mTexture();

        public Bitmap BitmapOver = null;
        public Bitmap BitmapUnder = null;
        public Bitmap ModifiedBitmap = null;

        public mCompositeTextureMorph(Bitmap UnderlayBitmap, Bitmap OverlayBitmap, mTexture SourceTexture)
        {
            MaskTexture = SourceTexture;
            BitmapUnder = new mSetFormat(UnderlayBitmap, 2).ModifiedBitmap;
            BitmapOver = new mSetFormat(OverlayBitmap, 2).ModifiedBitmap;

            ModifiedBitmap = new Bitmap(BitmapUnder);

            Effect = new TexturedMerge(MaskTexture.Texture);

            Effect.OverlayImage = BitmapOver;

            ModifiedBitmap = Effect.Apply(BitmapUnder);
        }

    }
}
