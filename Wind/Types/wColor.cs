

using System;

namespace Wind.Types
{
    public class wColor
    {
        public int A = 255;
        public int R = 0;
        public int G = 0;
        public int B = 0;

        public wColor()
        {
        }

        public wColor(wColor WindColor)
        {
            A = WindColor.A;
            R = WindColor.R;
            G = WindColor.G;
            B = WindColor.B;
        }

        public wColor(int Red, int Green, int Blue)
        {
            R = Red;
            G = Green;
            B = Blue;
        }

        public wColor(int Alpha, int Red, int Green, int Blue)
        {
            A = Alpha;
            R = Red;
            G = Green;
            B = Blue;
        }

        public void Flatten()
        {
            A = 255;
        }

        public wColor(System.Windows.Media.Color MediaColor)
        {
            A = MediaColor.A;
            R = MediaColor.R;
            G = MediaColor.G;
            B = MediaColor.B;
        }

        public void Lighten( double T)
        {
            R = (int)Math.Floor(R + (255 - R) * T);
            G = (int)Math.Floor(G + (255 - G) * T);
            B = (int)Math.Floor(B + (255 - B) * T);
        }

        public System.Windows.Media.Color? ToNullableMediaColor()
        {
            return System.Windows.Media.Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B);
        }

        public System.Windows.Media.Color ToMediaColor()
        {
            return System.Windows.Media.Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B);
        }
        
        public wColor(System.Drawing.Color DrawingColor)
        {
            A = DrawingColor.A;
            R = DrawingColor.R;
            G = DrawingColor.G;
            B = DrawingColor.B;
        }
        
        public System.Drawing.Color ToDrawingColor()
        {
            return System.Drawing.Color.FromArgb(A, R, G, B);
        }

        public wColor White()
        {
            return new wColor(255, 255, 255, 255);
        }

        public wColor Black()
        {
            return new wColor(255, 0, 0, 0);
        }

        public wColor Transparent()
        {
            return new wColor(0, 0, 0, 0);
        }

        public wColor VeryLightGrayFilter()
        {
            return new wColor(10, 0, 0, 0);
        }

        public wColor LightGrayFilter()
        {
            return new wColor(50, 0, 0, 0);
        }

        public wColor GrayFilter()
        {
            return new wColor(100, 0, 0, 0);
        }

        public wColor DarkGrayFilter()
        {
            return new wColor(200, 0, 0, 0);
        }

        public wColor VeryLightGray()
        {
            return new wColor(255, 240, 240, 240);
        }

        public wColor LightGray()
        {
            return new wColor(255, 211, 211, 211);
        }

        public wColor Gray()
        {
            return new wColor(255, 128, 128, 128);
        }

        public wColor DarkGray()
        {
            return new wColor(255, 169, 169, 169);
        }
        
    }
}
