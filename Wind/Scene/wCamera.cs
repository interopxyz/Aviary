using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Vectors;

namespace Wind.Scene
{

    public class wCamera
    {
        public double Pivot = 0;
        public double Tilt = 0;
        public double Distance = 0;
        public double Length = 0;
        public wPoint Target = new wPoint(0,0,0);
        public wPoint Location = new wPoint(1,1,1);
        public wVector Direction = new wVector(-1,-1,-1);
        public wVector Up = new wVector(-1, 1, 1);
        public bool IsPreset = false;
        public bool IsDefault = false;

        public wCamera()
        {
            SetLocation();
            SetDirection();
        }

        public wCamera(double PivotAngle, double TiltAngle)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;

            SetLocation();
            SetDirection();
        }

        public wCamera(double PivotAngle, double TiltAngle, double TargetDistance)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = TargetDistance;

            SetLocation();
            SetDirection();
        }
        
        public wCamera(double PivotAngle, double TiltAngle, double TargetDistance, double LensLength)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = TargetDistance;
            Length = LensLength;

            SetLocation();
            SetDirection();
        }

        public wCamera(double PivotAngle, double TiltAngle, double TargetDistance, double LensLength, bool IsPresetCamera)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = TargetDistance;
            Length = LensLength;
            IsPreset = IsPresetCamera;

            SetLocation();
            SetDirection();
        }

        public wCamera(double PivotAngle, double TiltAngle, double TargetDistance, double LensLength, bool IsPresetCamera, bool IsDefaultCamera)
        {
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = TargetDistance;
            Length = LensLength;
            IsPreset = IsPresetCamera;
            IsDefault = IsDefaultCamera;

            SetLocation();
            SetDirection();
        }

        public wCamera(wPoint Center, double PivotAngle, double TiltAngle, double TargetDistance, double LensLength)
        {
            Target = Center;
            Pivot = PivotAngle;
            Tilt = TiltAngle;
            Distance = TargetDistance;
            Length = LensLength;

            SetLocation();
            SetDirection();
        }

        public void SetLocation()
        {
            double X = (Target.X + Math.Cos((Pivot / 180.0) * Math.PI) * Math.Sin((Tilt / 180.0) * Math.PI) )* Distance;
            double Y = (Target.Y + Math.Sin((Pivot / 180.0) * Math.PI) * Math.Sin((Tilt / 180.0) * Math.PI)) * Distance;
            double Z = (Target.Z + Math.Cos((Tilt / 180.0) * Math.PI)) * Distance;

            Location = new wPoint(X, Y, Z);
        }

        public void SetDirection()
        {
            double X = -(Target.X + Math.Cos((Pivot / 180.0) * Math.PI) * Math.Sin((Tilt / 180.0) * Math.PI)) * Distance;
            double Y = -(Target.Y + Math.Sin((Pivot / 180.0) * Math.PI) * Math.Sin((Tilt / 180.0) * Math.PI)) * Distance;
            double Z = -(Target.Z + Math.Cos((Tilt / 180.0) * Math.PI)) * Distance;

            double UX = -(Math.Cos((Pivot / 180.0) * Math.PI) * Math.Sin(((Tilt+90.0) / 180.0) * Math.PI)) * Distance;
            double UY = -(Math.Sin((Pivot / 180.0) * Math.PI) * Math.Sin(((Tilt + 90.0) / 180.0) * Math.PI)) * Distance;
            double UZ = -(Math.Cos(((Tilt + 90.0) / 180.0) * Math.PI)) * Distance;

            Direction = new wVector(X, Y, Z);
            Up = new wVector(UX, UY, UZ);

        }

    }
}
