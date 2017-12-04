using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Vectors;

namespace Hoopoe.Geometry.Primitives
{
    public class hCircle : hCurve
    {
        double CenterX = 0;
        double CenterY = 0;
        double Radius = 1;

        public hCircle()
        {

        }

        public hCircle(wPoint Center)
        {
            CenterX = Center.X;
            CenterY = Center.Y;

        }

        public hCircle(wPoint Center, double CircleRadius)
        {
            CenterX = Center.X;
            CenterY = Center.Y;
            Radius = CircleRadius;
        }

        public hCircle(wCircle WindGeometry)
        {
            CenterX = WindGeometry.Center.X;
            CenterY = WindGeometry.Center.Y;
            Radius = WindGeometry.Radius;
        }

        public override void BuildCurve()
        {
            Curve.Clear();
            Curve.Append("M " + (CenterX - Radius) + " " + CenterY + " " + Environment.NewLine);
            Curve.Append("a " + Radius + " " + Radius + " 0 1 0 " + Radius * 2 + " 0" + Environment.NewLine);
            Curve.Append("a " + Radius + " " + Radius + " 0 1 0 -" + Radius * 2 + " 0");

        }
    }
}
