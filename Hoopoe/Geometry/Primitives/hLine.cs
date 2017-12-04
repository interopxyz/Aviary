using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Vectors;

namespace Hoopoe.Geometry.Primitives
{
    public class hLine : hCurve
    {
        double StartX = 0;
        double StartY = 0;
        double EndX = 1;
        double EndY = 1;

        public hLine()
        {

        }

        public hLine(wLine WindGeometry)
        {
            StartX = WindGeometry.Start.X;
            StartY = WindGeometry.Start.Y;
            EndX = WindGeometry.End.X;
            EndY = WindGeometry.End.Y;
        }

        public hLine(wPoint Start, wPoint End)
        {
            StartX = Start.X;
            StartY = Start.Y;
            EndX = End.X;
            EndY = End.Y;

        }

        public hLine(double startX, double startY, double endX, double endY)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
        }

        public override void BuildCurve()
        {
            Curve.Clear();
            Curve.Append("M " + StartX + "," + StartY + " " + EndX + "," + EndY + " ");

        }


    }
}
