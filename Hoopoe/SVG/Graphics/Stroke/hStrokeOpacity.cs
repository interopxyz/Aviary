using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.SVG.Graphics.Stroke
{
    public class hStrokeOpacity : hGraphic
    {
        public hStrokeOpacity()
        {
        }

        public hStrokeOpacity(int Alpha)
        {
            Value = "stroke-opacity = \"" + Alpha / 255.0 + "255\"";
        }

    }
}
