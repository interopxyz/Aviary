using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Graphics.Stroke
{
    public class hStrokeCorner : hGraphic
    {
        public enum StrokeCorner { bevel, miter, round, inherit };
        public StrokeCorner PathCorner = StrokeCorner.round;

        public hStrokeCorner()
        {
        }

        public hStrokeCorner(StrokeCorner PathCornerJoin)
        {
            PathCorner = PathCornerJoin;
            Value = "stroke-linejoin=\"" + PathCorner.ToString() + "\"";
        }

    }
}
