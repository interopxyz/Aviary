using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves;
using Wind.Geometry.Vectors;

namespace Hoopoe.Geometry.Curves
{
    public class hPolyline : hCurve
    {
        List<wPoint> Points = new List<wPoint>();

        public hPolyline()
        {

        }

        public hPolyline(List<wPoint> ControlPoints)
        {
            Points = ControlPoints;
        }

        public hPolyline(wPolyline WindGeometry)
        {
            Points = WindGeometry.Points;
        }

        public override void BuildCurve()
        {
            Curve.Clear();
            Curve.Append("M ");

            foreach(wPoint Pt in Points)
            {
                Curve.Append(Pt.X + "," + Pt.Y + " ");
            }
        }

    }
}
