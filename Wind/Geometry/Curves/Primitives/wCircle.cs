using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves.Primitives
{
    public class wCircle : wCurve
    {
        public override string GetCurveType { get { return "Circle"; } }

        public wPoint Center = new wPoint();
        public wPlane Plane = new wPlane().XYPlane();
        public double Radius = 1;

        public wCircle()
        {
            IsClosed = true;
        }

        public wCircle(wPoint CenterPoint, double RadiusValue)
        {
            IsClosed = true;

            Center = CenterPoint;
            Plane.SetOrigin(Center);
            Radius = RadiusValue;
        }

        public wCircle(wPlane BasePlane, double RadiusValue)
        {
            IsClosed = true;

            Plane = BasePlane;
            Center = Plane.Origin;
            Radius = RadiusValue;
        }

    }
}
