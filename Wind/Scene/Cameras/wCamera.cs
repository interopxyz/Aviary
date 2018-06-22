using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Vectors;

namespace Wind.Scene.Cameras
{

    public class wCamera
    {
        public string Name = "Default";

        public double Pivot = Math.PI / 4;
        public double Tilt = Math.PI / 4;
        public double Distance = 100;

        public wPoint Target = new wPoint(0, 0, 0);
        public wPoint Location = new wPoint(100, 100, 100);

        public wVector Direction = new wVector(-1, -1, -1);
        public wVector Up = new wVector(0, 0, 1);

        public double LensLength = 0;

        public bool IsDefault = true;

        public wCamera()
        {

        }

        public wCamera(double PivotAngle, double TiltAngle, double CameraDistance)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = CameraDistance;

            SetLocation();
            SetDirection();
            SetUp();
        }

        public wCamera(double PivotAngle, double TiltAngle, double CameraDistance, double lensLength)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = CameraDistance;

            LensLength = lensLength;

            SetLocation();
            SetDirection();
            SetUp();
        }

        public wCamera(wPoint CenterPoint, double PivotAngle, double TiltAngle, double CameraDistance, double lensLength)
        {
            Target = CenterPoint;

            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = CameraDistance;

            LensLength = lensLength;

            SetLocation();
            SetDirection();
            SetUp();
        }

        public wCamera(string CameraName, wPoint CenterPoint, double PivotAngle, double TiltAngle, double CameraDistance, double lensLength)
        {
            Name = CameraName;

            Target = CenterPoint;

            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = CameraDistance;

            LensLength = lensLength;

            SetLocation();
            SetDirection();
            SetUp();
        }

        public void SetOrientation(double PivotAngle, double TiltAngle, double CameraDistance)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = CameraDistance;

            SetLocation();
            SetDirection();
            SetUp();
        }

        public void SetOrientation(wPoint CenterPoint, double PivotAngle, double TiltAngle, double CameraDistance)
        {
            Target = CenterPoint;

            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = CameraDistance;

            SetLocation();
            SetDirection();
            SetUp();
        }

        public void SetLocation(wPoint CameraLocation)
        {
            Location = CameraLocation;
            SetAngle();
        }

        public void SetTarget(wPoint CameraTarget)
        {
            Target = CameraTarget;
            SetAngle();
        }

        private void SetAngle()
        {
            double X = Location.X - Target.X;
            double Y = Location.Y - Target.Y;
            double Z = Location.Z - Target.Z;

            Distance = Math.Sqrt(Math.Pow(X,2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            Pivot = Math.Acos(Z / Distance);
            Tilt = Math.Atan(Y / X);

            SetDirection();
            SetUp();
        }

        private void SetOrientation()
        {
            double X = Target.X + Math.Cos(Pivot) * Math.Sin(Tilt) * Distance;
            double Y = Target.Y + Math.Sin(Pivot) * Math.Sin(Tilt) * Distance;
            double Z = Target.Z + Math.Cos(Tilt) * Distance;

            Location = new wPoint(X, Y, Z);
        }

        private void SetLocation()
        {
            double X = Target.X + Math.Cos(Pivot) * Math.Sin(Tilt) * Distance;
            double Y = Target.Y + Math.Sin(Pivot) * Math.Sin(Tilt) * Distance;
            double Z = Target.Z + Math.Cos(Tilt) * Distance;

            Location = new wPoint(X, Y, Z);
        }

        private void SetDirection()
        {
            double X = (Target.X - Location.X);
            double Y = (Target.Y - Location.Y);
            double Z = (Target.Z - Location.Z);

            Direction = new wVector(X, Y, Z);
        }

        private void SetUp()
        {

            double X = Math.Cos(Pivot) * Math.Sin(Tilt + Math.PI / 2) * Distance;
            double Y = Math.Sin(Pivot) * Math.Sin(Tilt + Math.PI / 2) * Distance;
            double Z = Math.Cos(Tilt + Math.PI / 2) * Distance;
            Up = new wVector(X, Y, Z);

        }

        public void SetLength(double CameraDistance)
        {
            Distance = CameraDistance;

            SetLocation();
            SetDirection();
            SetUp();

        }

    }
}
