using Hoopoe.Geometry.Curves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoopoe.Geometry.Compound
{
    class hCompoundCurve : hCurve
    {

        public hCompoundCurve()
        {
            Curve.Clear();
        }
        
        public void AddCurve(hPolyline HoopoePolyline)
        {
            Curve.Append(HoopoePolyline.Curve.ToString());
        }

        public void AddCurve(hCubicBezierSpline HoopoeSpline)
        {
            Curve.Append(HoopoeSpline.Curve.ToString());
        }

    }
}
