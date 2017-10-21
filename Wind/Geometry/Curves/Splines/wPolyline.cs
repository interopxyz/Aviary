using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves
{
    public class wPolyline : wCurve
    {
        public override string GetCurveType { get { return "Polyline"; } }

        public wPolyline()
        {

            IsSingle = false;
        }

        public wPolyline(List<wPoint> PointSet)
        {

            Points = PointSet;
            Indices = Enumerable.Range(0, Points.Count).ToList();

            IsSingle = false;
        }

        public wPolyline(List<wPoint> PointSet, bool IsCurveClosed)
        {

            Points = PointSet;
            Indices = Enumerable.Range(0, Points.Count).ToList();

            IsClosed = IsCurveClosed;
            if (IsClosed) { Indices.Add(0); }

            IsSingle = false;
        }
        
    }
}