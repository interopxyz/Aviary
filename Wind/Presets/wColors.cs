using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Types;

namespace Wind.Presets
{
    public class wColors
    {

        //Colors
        public wColor Transparent()
        {
            return new wColor(0, 0, 0, 0);
        }

        public wColor White()
        {
            return new wColor(255, 255, 255, 255);
        }

        public wColor Black()
        {
            return new wColor(255, 0, 0, 0);
        }

        public wColor Red()
        {
            return new wColor(255, 255, 0, 0);
        }

        public wColor Green()
        {
            return new wColor(255, 0, 255, 0);
        }

        public wColor Blue()
        {
            return new wColor(255, 0, 0, 255);
        }

        public wColor Cyan()
        {
            return new wColor(255, 0, 255, 255);
        }

        public wColor Magenta()
        {
            return new wColor(255, 255, 0, 255);
        }

        public wColor Yellow()
        {
            return new wColor(255, 255, 255, 0);
        }



        //Transparency Grayscales
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


        //Solid Grayscales
        public wColor OffWhite()
        {
            return new wColor(255, 250, 250, 250);
        }

        public wColor VeryLightGray()
        {
            return new wColor(255, 240, 240, 240);
        }

        public wColor LightGray()
        {
            return new wColor(255, 211, 211, 211);
        }

        public wColor WashedGray()
        {
            return new wColor(255, 170, 170, 170);
        }

        public wColor Gray()
        {
            return new wColor(255, 128, 128, 128);
        }
        
        public wColor DarkGray()
        {
            return new wColor(255, 64, 64, 64);
        }

        public wColor VeryDarkGray()
        {
            return new wColor(255, 32, 32, 32);
        }
    }
}
