using Accord.Imaging.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Textures.Presets
{
    public class mTextureWood : mTexture
    {


        WoodTexture Pattern = new WoodTexture();

        double Rings = 5;

        public mTextureWood(double RingParameter)
        {

            Rings = RingParameter;

            Pattern = new WoodTexture(Rings);

            Texture = Pattern;
        }

    }
}
