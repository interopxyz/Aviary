using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wind.Geometry.Vectors;
using Wind.Types;

namespace Wind.Geometry.Shapes
{
    public class wText
    {
        public string Type = "Text";

        public string Text = null;
        public wFont Font = new wFont();

        public wText()
        {
        }

        public wText(string StartText)
        {
            Text = StartText;
        }

        public wText(string StartText, wFont TextFont)
        {
            Text = StartText;
            Font = TextFont;
        }

    }
}
