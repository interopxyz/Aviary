using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Macaw.Compiling.Modifiers
{
    public class mImageColor
    {
        wColor SourceColor = new wColor();
        public mImageColor(wColor InputColor)
        {
            SourceColor = InputColor;
        }

        public SoundInTheory.DynamicImage.Color ToDynamicColor()
        {
            SoundInTheory.DynamicImage.Color NewColor = new SoundInTheory.DynamicImage.Color();
            NewColor.A = (byte)SourceColor.A;
            NewColor.R = (byte)SourceColor.R;
            NewColor.G = (byte)SourceColor.G;
            NewColor.B = (byte)SourceColor.B;

            return NewColor;
        }

    }
}
