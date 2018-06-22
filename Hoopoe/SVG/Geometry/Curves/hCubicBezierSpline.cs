using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves.Splines;
using Wind.Geometry.Vectors;

namespace Hoopoe.SVG.Geometry.Curves
{
    public class hCubicBezierSpline : hCurve
    {
        List<wPoint> Points = new List<wPoint>();

        public hCubicBezierSpline()
        {

        }

        public hCubicBezierSpline(List<wPoint> ControlPoints)
        {
            Points = ControlPoints;
        }

        public hCubicBezierSpline(wBezierSpline WindGeometry)
        {
            Points = WindGeometry.Points;
        }

        public override void BuildSVGCurve()
        {
            Curve.Clear();
            Curve.Append("M " + Points[0].X + " " + Points[0].Y + " ");
            Curve.Append("C " + Points[1].X + " " + Points[1].Y + " ");

            for(int i = 2; i< Points.Count;i++)
            {
                Curve.Append(Points[i].X + " " + Points[i].Y + " ");
            }
        }

    }
}
