using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves
{
    public class wQuadraticBezier : wCurve
    {
        public override string GetCurveType { get { return "QuadraticBezier"; } }

        public wPoint StartPoint = new wPoint(0, 0, 0);
        public wPoint EndPoint = new wPoint(1, 1, 0);
        public wPoint StartControlPoint = new wPoint(1, 0, 0);
        public wPoint EndControlPoint = new wPoint(0, 1, 0);

        public wQuadraticBezier()
        {

            Points.AddRange(new List<wPoint>(){ StartPoint, StartControlPoint, EndControlPoint, EndPoint });
            Indices.AddRange(new List<int>() { 0, 1, 2, 3});
        }

        public wQuadraticBezier(wPoint PointStart, wPoint ControlPointStart, wPoint ControlPointEnd, wPoint PointEnd)
        {

            StartPoint = PointStart;
            EndPoint = PointEnd;
            StartControlPoint = ControlPointStart;
            EndControlPoint = ControlPointEnd;

            Points.AddRange(new List<wPoint>() { StartPoint, StartControlPoint, EndControlPoint, EndPoint });
            Indices.AddRange(new List<int>() { 0, 1, 2, 3 });
        }

    }
}
