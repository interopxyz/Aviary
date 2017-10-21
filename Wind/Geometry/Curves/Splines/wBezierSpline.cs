using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves.Splines
{
    public class wBezierSpline: wCurve
    {
        public override string GetCurveType { get { return "BezierSpline"; } }

        public List<wBezierSpans> Spans = new List<wBezierSpans>();
        public List<wCubicBezier> Segments = new List<wCubicBezier>();

        public wBezierSpline()
        {
        }

        public wBezierSpline(wPoint A, wPoint B, wPoint C, wPoint D)
        {
            Spans.Add(new wBezierSpans(0, 1, 2, 3));
            Points.AddRange(new List<wPoint>() { A,B,C,D});
            Segments.Add(new wCubicBezier(A, B, C, D));
        }

        public wBezierSpline(List<wPoint> ControlPoints, bool CloseCurve)
        {
            Points = ControlPoints;

            for(int i = 0; i < (ControlPoints.Count / 4); i++ )
            {
                Spans.Add(new wBezierSpans(i * 3, i * 3 + 1, i * 3 + 2, i * 3 + 3));
                Segments.Add(new wCubicBezier(Points[i * 3], Points[i * 3 + 1], Points[i * 3 + 2], Points[i * 3 + 3]));
            }

            IsClosed = CloseCurve;
        }

        public void AddSpan(wPoint A, wPoint B, wPoint C, wPoint D)
        {
            int i = Points.Count - 1;
            Points.AddRange(new List<wPoint>() { B, C, D });
            Spans.Add(new wBezierSpans(i, i + 1, i + 2, i + 3));
            Segments.Add(new wCubicBezier(A, B, C, D ));
        }

    }
}
