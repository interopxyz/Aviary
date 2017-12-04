using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Graphics.Stroke
{
    public class hStrokePattern : hGraphic
    {
        public hStrokePattern()
        {
            Value = "stroke-dasharray=\"1 0\"";
        }

        public hStrokePattern(double[] Pattern)
        {

            Value = "stroke-dasharray=\"" +String.Join(" ", Pattern) + "\"";
        }

    }
}
