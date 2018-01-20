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
        string Closed = " ";

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
            if (WindGeometry.IsClosed) { Closed = "z "; } 
        }

        public override void BuildSVGCurve()
        {
            Curve.Clear();
            Curve.Append("M ");

            foreach(wPoint Pt in Points)
            {
                Curve.Append(Pt.X + "," + Pt.Y + " ");
            }
            Curve.Append(Closed);
        }

        
    }
}
