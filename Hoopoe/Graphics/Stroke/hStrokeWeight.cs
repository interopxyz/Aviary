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
        }

        public hStrokeWeight(double Weight)
        {
            Value = "stroke-width=\"" + Weight + "\"";
        }

    }
}
