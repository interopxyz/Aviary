using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.SVG.Graphics.Stroke
{
    public class hStrokePattern : hGraphic
    {
        public hStrokePattern()
        {
        }

        public hStrokePattern(double[] Pattern)
        {

            Value = "stroke-dasharray=\"" +String.Join(" ", Pattern) + "\"";
        }

    }
}
