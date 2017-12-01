using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves.Primitives
{
    public class wLine : wCurve
    {
        public override string GetCurveType { get { return "Line"; } }

        public wPoint Start = new wPoint(0, 0, 0);
        public wPoint End = new wPoint(1, 0, 0);
        public double Length = 1;
        public wVector Direction = new wVector().Xaxis();

        public wLine()
        {
            Points.AddRange(new List<wPoint>() { Start, End });
            Indices.AddRange(new List<int>() { 0, 1 });
        }

        public wLine(double StartX, double StartY, double EndX, double EndY)
        {
            Start = new wPoint(StartX,StartY);
            End = new wPoint(EndX,EndY);
            Points.AddRange(new List<wPoint>() { Start, End });
            Indices.AddRange(new List<int>() { 0, 1 });

            Direction = new wVector(Start, End);
        }

        public wLine(wPoint StartPoint, wPoint EndPoint)
        {
            Start = StartPoint;
            End = EndPoint;
            Points.AddRange(new List<wPoint>() { Start, End });
            Indices.AddRange(new List<int>() { 0, 1 });

            Direction = new wVector(Start, End);
        }

        public wLine(wPoint StartPoint, wVector Direction, double Length)
        {
            Points.AddRange(new List<wPoint>() { Start, End });
            Indices.AddRange(new List<int>() {0,1 });
        }
    }
}
