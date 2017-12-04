using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Graphics.Stroke
{
    public class hStrokeWeight : hGraphic
    {
        public hStrokeWeight()
        {
            Value = "stroke-width=\"1\"";
        }

        public hStrokeWeight(double Weight)
        {
            Value = "stroke-width=\"" + Weight + "\"";
        }

    }
}
