using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media.Media3D;

namespace Wind.Geometry.Vectors
{
    public class wVector
    {
        public string Type = "Vector";

        public double X = 1;
        public double Y = 0;
        public double Z = 0;
        public double Amplitude = 1;

        public wVector()
        {

        }

        public wVector(double Xvalue, double Yvalue)
        {
            X = Xvalue;
            Y = Yvalue;
        }

        public wVector(double Xvalue, double Yvalue, double Zvalue)
        {
            X = Xvalue;
            Y = Yvalue;
            Z = Zvalue;
        }

        public wVector(wPoint WindPoint)
        {
            X = WindPoint.X;
            Y = WindPoint.Y;
            Z = WindPoint.Z;
        }

        public wVector(wPoint StartPoint, wPoint EndPoint)
        {
            X = EndPoint.X - StartPoint.X;
            Y = EndPoint.Y - StartPoint.Y;
            Z = EndPoint.Z - StartPoint.Z;
        }

        public wPoint ToPoint()
        {
            return new wPoint(X, Y, Z);
        }

        public Vector3D ToVector3D()
        {
            return new Vector3D(X, Y, Z);
        }

        public wVector Xaxis(bool unitize = false)
        {
            if (unitize)
            {
                return new wVector(1, 0, 0);
            }
            else
            {
                return new wVector(X, 0, 0);
            }
        }

        public wVector Yaxis(bool unitize = false)
        {
            if (unitize)
            {
                return new wVector(0, 1, 0);
            }
            else
            {
                return new wVector(0, Y, 0);
            }
        }

        public wVector AddVector(wVector VectorA, wVector VectorB)
        {
            return new wVector(VectorA.X + VectorB.X, VectorA.Y + VectorB.Y, VectorA.Z + VectorB.Z);
        }

        public wVector AddVector(wPoint PointA, wPoint PointB)
        {
            return new wVector(PointA.X + PointB.X, PointA.Y + PointB.Y, PointA.Z + PointB.Z);
        }

        public wVector SubtractVector(wVector VectorA, wVector VectorB)
        {
            return new wVector(VectorA.X - VectorB.X, VectorA.Y - VectorB.Y, VectorA.Z - VectorB.Z);
        }

        public wVector SubtractVector(wPoint PointA, wPoint PointB)
        {
            return new wVector(PointA.X - PointB.X, PointA.Y - PointB.Y, PointA.Z - PointB.Z);
        }

        public wVector Zaxis(bool unitize = false)
        {
            if (unitize)
            {
                return new wVector(0, 0, 1);
            }
            else
            {
                return new wVector(0, 0, Z);
            }
        }
    }
}
