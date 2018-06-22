using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Vectors;

namespace Hoopoe.SVG.Geometry.Primitives
{
    public class hEllipse : hCurve
    {
        double CenterX = 0;
        double CenterY = 0;
        double RadiusX = 1;
        double RadiusY = 1;
        double Angle = 0;

        public hEllipse()
        {

        }

        public hEllipse(wPoint Center)
        {
            CenterX = Center.X;
            CenterY = Center.Y;

        }

        public hEllipse(wPoint Center, double UniformRadius)
        {
            CenterX = Center.X;
            CenterY = Center.Y;
            RadiusX = UniformRadius;
            RadiusY = UniformRadius;
        }

        public hEllipse(wPoint Center, double UniformRadius, double RotationAngle
            )
        {
            CenterX = Center.X;
            CenterY = Center.Y;
            RadiusX = UniformRadius;
            RadiusY = UniformRadius;
            Angle = RotationAngle;
        }

        public hEllipse(wEllipse WindGeometry)
        {
            CenterX = WindGeometry.Center.X;
            CenterY = WindGeometry.Center.Y;
            RadiusX = WindGeometry.RadiusX;
            RadiusY = WindGeometry.RadiusY;
            Angle = WindGeometry.Rotation;
        }

        public override void BuildSVGCurve()
        {
            Curve.Clear();
            Curve.Append("M " + (CenterX - RadiusX) + " " + CenterY + " " + Environment.NewLine);
            Curve.Append("a " + RadiusX + " " + RadiusY + " 0 1 0 " + RadiusX * 2 + " 0 " + Environment.NewLine);
            Curve.Append("a " + RadiusX + " " + RadiusY + " 0 1 0 -" + RadiusX * 2 + " 0 ");

        }
    }
}
