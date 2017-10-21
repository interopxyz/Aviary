using AForge.Imaging.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Textures.Presets
{
    public class mTextureTextile : mTexture
    {

        TextileTexture Pattern = new TextileTexture();
        
        public mTextureTextile()
        {
            

            Pattern = new TextileTexture();
            
            Texture = Pattern;
        }

    }
}
