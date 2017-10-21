using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;
using AForge.Imaging;

namespace Wind.Utilities
{

    public class AdjustColor
    {
        wColor WindColor = new wColor();
        
        public AdjustColor()
        {

        }

        public AdjustColor(wColor InputColor)
        {
            WindColor = InputColor;
        }

        public wColor SetLuminance(double LuminanceValue)
        {
            HSL hsl = HSL.FromRGB(new RGB((byte)WindColor.R, (byte)WindColor.G, (byte)WindColor.B));
            hsl.Luminance = (float)LuminanceValue;

            RGB rgb = hsl.ToRGB();

            WindColor.R = rgb.Red;
            WindColor.G = rgb.Green;
            WindColor.B = rgb.Blue;

            return WindColor;
        }

    }

}
