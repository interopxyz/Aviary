using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media.Media3D;


namespace Wind.Geometry.Vectors
{
    public class wPoint
    {
        public string Type = "Point";

        public double X = 0;
        public double Y = 0;
        public double Z = 0;

        public wPoint()
        {
        }

        public wPoint(double Xvalue, double Yvalue)
        {
            X = Xvalue;
            Y = Yvalue;
        }

        public wPoint(double Xvalue, double Yvalue, double Zvalue)
        {
            X = Xvalue;
            Y = Yvalue;
            Z = Zvalue;
        }

        public wPoint(wVector WindVector)
        {
            X = WindVector.X;
            Y = WindVector.Y;
            Z = WindVector.Z;
        }

        public wVector ToVector()
        {
            return new wVector(X, Y, Z);
        }

        public Point3D ToPoint3D()
        {
            return new Point3D(X, Y, Z);
        }

        public System.Drawing.Point ToDrawingPoint()
        {
            return new System.Drawing.Point((int)X, (int)Y);
        }

        public System.Windows.Point ToWindowsPoint()
        {
            return new System.Windows.Point((int)X, (int)Y);
        }
    }
}
