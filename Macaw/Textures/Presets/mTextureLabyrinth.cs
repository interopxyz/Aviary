using Accord.Imaging.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Textures.Presets
{
    public class mTextureLabyrinth : mTexture
    {

        LabyrinthTexture Pattern = new LabyrinthTexture();
        
        public mTextureLabyrinth()
        {

            Pattern = new LabyrinthTexture();
            
            Texture = Pattern;
        }

    }
}
