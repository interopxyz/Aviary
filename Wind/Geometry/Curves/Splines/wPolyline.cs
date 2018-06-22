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

        public wPolyline(wPoint[] PointArray)
        {

            Points = PointArray.ToList();
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

        public bool IsPolylineClosed()
        {

            return ((Points[0].X == Points[Points.Count - 1].X) && (Points[0].Y == Points[Points.Count - 1].Y) && (Points[0].Z == Points[Points.Count - 1].Z));
        }

        public void ClosePolyline()
        {
            if(!IsPolylineClosed())
            {
                Points.Add(new wPoint(Points[0].X, Points[0].Y, Points[0].Z));
                Indices.Add(Indices.Count);
            }

        }

        public void OpenPolyline()
        {
            if (IsPolylineClosed())
            {
                Points.RemoveAt(Points.Count-1);
                Indices.RemoveAt(Indices.Count - 1);
            }

        }


        public void Flip()
        {
            Points.Reverse();
        }
        
    }
}