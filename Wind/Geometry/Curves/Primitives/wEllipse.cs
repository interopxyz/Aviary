using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves.Primitives
{
    public class wEllipse : wCurve
    {
        public override string GetCurveType { get { return "Ellipse"; } }

        public wPoint Center = new wPoint();
        public wPlane Plane = new wPlane().XYPlane();
        public double RadiusX = 1;
        public double RadiusY = 1;
        public double Rotation = 0;

        public wEllipse()
        {
            IsClosed = true;
        }

        public wEllipse(wPoint CenterPoint, double XRadius, double YRadius)
        {
            IsClosed = true;

            Center = CenterPoint;
            Plane.SetOrigin(Center);
            RadiusX = XRadius;
            RadiusY = YRadius;
        }

        public wEllipse(wPlane BasePlane, double XRadius, double YRadius)
        {
            IsClosed = true;

            Plane = BasePlane;
            Center = Plane.Origin;
            RadiusX = XRadius;
            RadiusY = YRadius;
        }

        public wEllipse(wPoint CenterPoint, double XRadius, double YRadius, double Rotate)
        {
            IsClosed = true;

            Center = CenterPoint;
            Plane.SetOrigin(Center);
            RadiusX = XRadius;
            RadiusY = YRadius;
            Rotation = Rotate;
        }

        public wEllipse(wPlane BasePlane, double XRadius, double YRadius, double Rotate)
        {
            IsClosed = true;

            Plane = BasePlane;
            Center = Plane.Origin;
            RadiusX = XRadius;
            RadiusY = YRadius;
            Rotation = Rotate;
        }
    }
}
