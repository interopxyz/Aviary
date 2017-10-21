using AForge.Imaging.Filters;
using Macaw.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Filtering.Stylized
{
    public class mEffectPixelate : mFilter
    {

        Pixellate Effect = new Pixellate();

        int PixelHeight = 5;
        int PixelWidth = 5;

        public mEffectPixelate(int PixelWidthValue, int PixelHeightValue)
        {

            BitmapType = 2;

            PixelWidth = PixelWidthValue;
            PixelHeight = PixelHeightValue;

            Effect = new Pixellate(PixelWidth, PixelHeight);

            Sequence.Clear();
            Sequence.Add(Effect);
        }

    }
}
