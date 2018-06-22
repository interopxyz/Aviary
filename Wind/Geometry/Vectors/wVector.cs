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

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double Amplitude { get; private set; }

        #region
        public wVector()
        {
            X = 1;
            Y = 0;
            Z = 0;
            Amplitude = 1;
        }

        public wVector(double Xvalue, double Yvalue)
        {
            X = Xvalue;
            Y = Yvalue;
            Z = 0;
            SetAmplitude();
        }

        public wVector(double Xvalue, double Yvalue, double Zvalue)
        {
            X = Xvalue;
            Y = Yvalue;
            Z = Zvalue;

            SetAmplitude();
        }

        public wVector(wPoint WindPoint)
        {
            X = WindPoint.X;
            Y = WindPoint.Y;
            Z = WindPoint.Z;

            SetAmplitude();
        }

        public wVector(wPoint StartPoint, wPoint EndPoint)
        {
            X = EndPoint.X - StartPoint.X;
            Y = EndPoint.Y - StartPoint.Y;
            Z = EndPoint.Z - StartPoint.Z;

            SetAmplitude();
        }

        #endregion
        
        #region
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

        #endregion


        private void SetAmplitude()
        {
            Amplitude = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }

        public void ScaleVector(double Scale)
        {
            X = X * Scale;
            Y = Y * Scale;
            Z = Z * Scale;

            SetAmplitude();

        }

        private void Unitize()
        {
            X = X / Amplitude;
            Y = Y / Amplitude;
            Z = Z / Amplitude;

            Amplitude = 1;
        }

        public void AddVector(wVector vector)
        {
            X += vector.X;
            Y += vector.Y;
            Z += vector.Z;

            SetAmplitude();
        }

        public void AddVector(wPoint point)
        {
            X += point.X;
            Y += point.Y;
            Z += point.Z;

            SetAmplitude();
        }

        public void SubtractVector(wVector vector)
        {
            X -= vector.X;
            Y -= vector.Y;
            Z -= vector.Z;

            SetAmplitude();
        }

        public void SubtractVector(wPoint point)
        {
            X -= point.X;
            Y -= point.Y;
            Z -= point.Z;

            SetAmplitude();
        }

        public wVector GetCrossProduct(wVector V2)
        {
            wVector ReturnVector = new wVector(Y * V2.Z - Z * V2.Y, (X * V2.Z - Z * V2.X) * (-1), (X * V2.Y - Y * V2.X));
            return ReturnVector;
        }

        public double GetDotProduct(wVector V2)
        {
            return ((X * V2.X) + (Y * V2.Y) + (Z * V2.Z));

        }

        public double GetAngle(wVector V2)
        {

            return (double)Math.Atan2(GetCrossProduct(V2).Z, GetDotProduct(V2));
        }

        public wPoint ToPoint()
        {
            return new wPoint(X, Y, Z);
        }

        public Vector3D ToVector3D()
        {
            return new Vector3D(X, Y, Z);
        }
    }

}
