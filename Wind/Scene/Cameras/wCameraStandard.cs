using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wind.Geometry.Vectors;

namespace Wind.Scene.Cameras
{
    public class wCameraStandard : wCamera
    {

        public wCameraStandard()
        {
        }

        public wCameraStandard(double PivotAngle, double TiltAngle)
        {
            //SetOrientation(PivotAngle, TiltAngle, 1);
        }

        public wCameraStandard(double PivotAngle, double TiltAngle, double TargetDistance)
        {
            //SetOrientation(PivotAngle, TiltAngle, TargetDistance);
        }

        public wCameraStandard(double PivotAngle, double TiltAngle, double TargetDistance, double Length)
        {
            //SetOrientation(PivotAngle, TiltAngle, TargetDistance);
            //LensLength = Length;
        }

        public wCameraStandard(double PivotAngle, double TiltAngle, double TargetDistance, double Length, bool IsPresetCamera)
        {
            //SetOrientation(PivotAngle, TiltAngle, TargetDistance);
            //LensLength = Length;
            //IsDefault = IsPresetCamera;
        }

        public wCameraStandard(wPoint PositionPoint, wPoint TargetPoint, wVector UpVector, double Length)
        {
            SetLocation(PositionPoint);
            SetTarget(TargetPoint);

            Up = UpVector;
            LensLength = Length;
        }
        

        public wCameraStandard(wPoint PositionPoint, double PivotAngle, double TiltAngle, double TargetDistance, double Length)
        {
            SetOrientation(PositionPoint, PivotAngle, TiltAngle, TargetDistance);
            LensLength = Length;
        }

    }
}
