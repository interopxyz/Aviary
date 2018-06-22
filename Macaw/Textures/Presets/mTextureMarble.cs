using Accord.Imaging.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Textures.Presets
{
    public class mTextureMarble : mTexture
    {
        
        MarbleTexture Pattern = new MarbleTexture(10,10);

        double X = 5.0;
            double Y = 10.0;

        public mTextureMarble(double PeriodX, double PeriodY)
        {
            X = PeriodX;
            Y = PeriodY;

            Pattern = new MarbleTexture(X, Y);
            
            Texture = Pattern;
        }

    }
}
