using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Wind.Geometry.Vectors;

namespace Wind.Geometry.Curves.Primitives
{
    public class wArc : wCurve
    {
        public override string GetCurveType { get { return "Arc"; } }

        public wPoint Center = new wPoint();
        public wPlane Plane = new wPlane().XYPlane();
        public double Radius = 1;
        public double StartAngle = 0;
        public double EndAngle = 90;
        public double Angle = 90;
        public wPoint StartPoint = new wPoint(1, 0, 0);
        public wPoint EndPoint = new wPoint(0, 1, 0);
        public wPoint MidPoint = new wPoint();
        public bool Clockwise = true;

        public wArc()
        {

        }

        public wArc(wPoint CenterPoint, double RadiusValue, double ArcAngle)
        {
            Center = CenterPoint;
            Plane.SetOrigin(Center);
            Radius = RadiusValue;
            StartAngle = 0;
            EndAngle = ArcAngle;
            Angle = ArcAngle;

            StartPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * StartAngle / 180), Center.Y + Radius * Math.Sin(Math.PI * StartAngle / 180), Center.Z);
            EndPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * EndAngle / 180), Center.Y + Radius * Math.Sin(Math.PI * EndAngle / 180), Center.Z);
        }

        public wArc(wPoint CenterPoint, double RadiusValue, double AngleStart, double AngleEnd)
        {
            Center = CenterPoint;
            Plane.SetOrigin(Center);
            Radius = RadiusValue;
            StartAngle = AngleStart;
            EndAngle = AngleEnd;
            Angle = EndAngle-StartAngle;

            StartPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * StartAngle / 180.0), Center.Y + Radius * Math.Sin(Math.PI * StartAngle / 180.0), Center.Z);
            EndPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * EndAngle / 180.0), Center.Y + Radius * Math.Sin(Math.PI * EndAngle / 180.0), Center.Z);
        }

        public wArc(wPlane BasePlane, double RadiusValue, double Angle)
        {
            Plane = BasePlane;
            Center = Plane.Origin;
            Radius = RadiusValue;
            StartAngle = 0;
            EndAngle = Angle;
            Angle = EndAngle - StartAngle;

            StartPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * StartAngle / 180), Center.Y + Radius * Math.Sin(Math.PI * StartAngle / 180),Center.Z);
            EndPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * EndAngle / 180), Center.Y + Radius * Math.Sin(Math.PI * EndAngle / 180), Center.Z);
        }

        public wArc(wPlane BasePlane, double RadiusValue, double AngleStart, double AngleEnd)
        {
            Plane = BasePlane;
            Center = Plane.Origin;
            Radius = RadiusValue;
            StartAngle = AngleStart;
            EndAngle = AngleEnd;
            Angle = EndAngle - StartAngle;

            StartPoint = new wPoint(Center.X + Radius*Math.Cos(Math.PI * StartAngle / 180), Center.Y + Radius *Math.Sin(Math.PI*StartAngle/180), Center.Z);
            EndPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * EndAngle / 180), Center.Y + Radius * Math.Sin(Math.PI * EndAngle / 180), Center.Z);
        }

        public wArc(wPoint CenterPoint, double RadiusValue, double AngleStart, double AngleEnd, bool Direction)
        {
            Center = CenterPoint;
            Plane.SetOrigin(Center);
            Radius = RadiusValue;
            StartAngle = AngleStart;
            EndAngle = AngleEnd;
            Angle = EndAngle - StartAngle;
            Clockwise = Direction;

            StartPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * StartAngle / 180.0), Center.Y + Radius * Math.Sin(Math.PI * StartAngle / 180.0), Center.Z);
            EndPoint = new wPoint(Center.X + Radius * Math.Cos(Math.PI * EndAngle / 180.0), Center.Y + Radius * Math.Sin(Math.PI * EndAngle / 180.0), Center.Z);
        }
    }
}
