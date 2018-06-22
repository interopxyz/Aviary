using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.SVG.Graphics.Stroke
{
    class hStrokeCap : hGraphic
    {
        public enum StrokeCap { butt, square, round, inherit };
        public StrokeCap PathCap = StrokeCap.butt;

        public hStrokeCap()
        {
            Value = "stroke-linecap=\"butt\"";
        }

        public hStrokeCap(StrokeCap PathEndCap)
        {
            PathCap = PathEndCap;
            Value = "stroke-linecap=\"" + PathCap.ToString() + "\"";
        }

    }
}
