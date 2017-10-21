using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves.Primitives
{
    public class wRectangle : wCurve
    {
        public override string GetCurveType { get { return "Rectangle"; } }

        public double Width = 2;
        public double Height = 2;
        public double Rotation = 0;
        public wPoint Center = new wPoint();
        public wPlane Plane = new wPlane();
        public wPoint[] CornerPoints = { new wPoint(-1, -1), new wPoint(1, -1), new wPoint(1, 1), new wPoint(-1, 1) };

        public wRectangle()
        {
            IsClosed = true;
        }

        public wRectangle(wPlane CenterPlane, double RectWidth, double RectHeight)
        {
            Plane = CenterPlane;
            Center = Plane.Origin;

            Width = RectWidth;
            Height = RectHeight;

            CornerPoints[0] = new wPoint(Center.X - Width / 2, Center.Y - Height / 2, Center.Z);
            CornerPoints[1] = new wPoint(Center.X + Width / 2, Center.Y - Height / 2, Center.Z);
            CornerPoints[2] = new wPoint(Center.X + Width / 2, Center.Y + Height / 2, Center.Z);
            CornerPoints[3] = new wPoint(Center.X + Width / 2, Center.Y + Height / 2, Center.Z);

            IsClosed = true;
        }



    }
}
