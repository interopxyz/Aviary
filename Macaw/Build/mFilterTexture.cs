using Accord.Imaging.Filters;
using Macaw.Filtering;
using Macaw.Textures;

namespace Macaw.Build
{
    public class mFilterTexture : mFilters
    {
        public TexturedFilter Effect = null;

        mTexture MaskTexture = new mTexture();

        public mFilterTexture(mTexture SourceTexture, mFilters Filter)
        {

            BitmapType = Filter.BitmapType;

            MaskTexture = SourceTexture;

            Effect = new TexturedFilter(MaskTexture.Texture, Filter.Sequence[0]);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
