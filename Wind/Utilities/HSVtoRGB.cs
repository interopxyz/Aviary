using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wind.Utilities
{
    public class HSVtoRGB
    {
        public double R = 0;
        public double G = 0;
        public double B = 0;
        
        public HSVtoRGB(double H, double S, double V)
        {
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double r, g, b;

            if (V <= 0)
            { r = g = b = 0; }
            else if (S <= 0)
            {
                r = g = b = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        r = V;
                        g = tv;
                        b = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        r = qv;
                        g = V;
                        b = pv;
                        break;
                    case 2:
                        r = pv;
                        g = V;
                        b = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        r = pv;
                        g = qv;
                        b = V;
                        break;
                    case 4:
                        r = tv;
                        g = pv;
                        b = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        r = V;
                        g = pv;
                        b = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        r = V;
                        g = tv;
                        b = pv;
                        break;
                    case -1:
                        r = V;
                        g = pv;
                        b = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        r = g = b = V; // Just pretend its black/white
                        break;
                }
            }
            R = Clamp((int)(r * 255.0));
            G = Clamp((int)(g * 255.0));
            B = Clamp((int)(b * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }
    }
}
