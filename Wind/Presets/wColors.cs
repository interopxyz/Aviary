using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Presets
{
    public static class wColors
    {
        //Primary Color Set
        public static wColor Red =new wColor(255, 255, 0, 0);
        public static wColor Green =new wColor(255, 0, 255, 0);
        public static wColor Blue =new wColor(255, 0, 0, 255);
        public static wColor Cyan = new wColor(255, 0, 255, 255);
        public static wColor Magenta=new wColor(255, 255, 0, 255);
        public static wColor Yellow =new wColor(255, 255, 255, 0);
        public static wColor Black = new wColor(255, 0, 0, 0);
        public static wColor White = new wColor(255, 255, 255, 255);
        public static wColor Transparent = new wColor(0, 0, 0, 0);

        //Transparency Grayscales
        public static wColor VeryLightGrayFilter =new wColor(10, 0, 0, 0);
        public static wColor LightGrayFilter = new wColor(50, 0, 0, 0);
        public static wColor GrayFilter =new wColor(100, 0, 0, 0);
        public static wColor DarkGrayFilter =new wColor(200, 0, 0, 0);

        //Solid Grayscales
        public static wColor OffWhite =new wColor(255, 250, 250, 250);
        public static wColor VeryLightGray =new wColor(255, 240, 240, 240);
        public static wColor LightGray =new wColor(255, 211, 211, 211);
        public static wColor WashedGray =new wColor(255, 170, 170, 170);
        public static wColor Gray =new wColor(255, 128, 128, 128);
        public static wColor Charcoal = new wColor(255, 80, 80, 80);
        public static wColor DarkGray =new wColor(255, 64, 64, 64);
        public static wColor VeryDarkGray =new wColor(255, 32, 32, 32);
        
    }
}
