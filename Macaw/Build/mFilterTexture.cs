using AForge.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Textures;

namespace Macaw.Build
{
    public class mFilterTexture : mFilter
    {
        public TexturedFilter Effect = null;

        mTexture MaskTexture = new mTexture();

        public mFilterTexture(mTexture SourceTexture, mFilter Filter)
        {

            BitmapType = Filter.BitmapType;

            MaskTexture = SourceTexture;

            Effect = new TexturedFilter(MaskTexture.Texture, Filter.Sequence[0]);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
