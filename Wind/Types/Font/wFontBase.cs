using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Presets;

namespace Wind.Types
{
    public abstract class wFontBase
    {
        public string Title = "Custom";
        public string Name = "Arial";
        public double Size = 8;
        public enum Justification { TopLeft, TopCenter, TopRight, MiddleLeft, MiddleCenter, MiddleRight, BottomLeft, BottomCenter, BottomRight };
        public Justification Justify = Justification.TopLeft;

        public bool IsBold = false;
        public bool IsItalic = false;
        public bool IsUnderlined = false;
        public bool IsStrikethrough = false;
        public double Angle = 0;

        public wColor FontColor = wColors.Black;

        public wFontBase()
        {

        }

    }
}
