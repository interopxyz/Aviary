using AForge.Imaging.Textures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Textures
{
    public class mTextureApply
    {
        public Bitmap GeneratedBitmap = null;

        int Width = 800;
        int Height = 600;

        public mTextureApply(mTexture Texture, int ImageWidth, int ImageHeight)
        {

            Width = ImageWidth;
            Height = ImageHeight;
            
            float[,] Txtr = Texture.Texture.Generate(Width, Height);

            GeneratedBitmap = TextureTools.ToBitmap(Txtr);
        }

    }
}
