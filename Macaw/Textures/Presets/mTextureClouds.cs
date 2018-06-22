using Accord.Imaging.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Textures.Presets
{
    public class mTextureClouds : mTexture
    {

        CloudsTexture Pattern = new CloudsTexture();

        public mTextureClouds()
        {

            Pattern = new CloudsTexture();
            
            Texture = Pattern;
        }

    }
}
