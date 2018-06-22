using System;

using System.Windows;

using System.Windows.Controls;

using Rhino.UI.Controls;
using Eto.Forms;
using Eto.Drawing;
using Wind.Geometry.Vectors;

namespace Parrot.Controls
{
    public class pViewport : pControl
    {
        public StackPanel Element = new StackPanel();
        public ViewportControl RhinoViewer = new ViewportControl();

        public bool Status;

        public enum CameraProjection { Parallel, Perspective, TwoPoint };
        public CameraProjection ProjectionMode = CameraProjection.Parallel;

        public pViewport(string InstanceName)
        {
            RhinoViewer = new ViewportControl();
            Element = new StackPanel();
            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.MinWidth = 300;
            Element.MinHeight = 300;

            RhinoViewer.Width = 300;
            RhinoViewer.Height = 300;

            Element.Name = InstanceName;
            Type = "Viewport";

        }

        public void BuildView()
        {

            var nativeControl = RhinoViewer.ToNative(false);
            nativeControl.Width = RhinoViewer.Width;
            nativeControl.Height = RhinoViewer.Height;

            Element.Children.Clear();
            Element.Children.Add(nativeControl);

        }

        public void SetViewPort(Rhino.Display.RhinoViewport SampleViewPort)
        {
            RhinoViewer.Viewport.DisplayMode = SampleViewPort.DisplayMode;
        }

        public void SetProperties(string Text)
        {
            RhinoViewer.Viewport.Name = Text;
        }

        public void SetCameraProjection(CameraProjection SetProjectionMode, double Length)
        {
            ProjectionMode = SetProjectionMode;

            switch (ProjectionMode)
            {
                default:
                    RhinoViewer.Viewport.ChangeToParallelProjection(true);
                    break;
                case CameraProjection.Perspective:
                    RhinoViewer.Viewport.ChangeToPerspectiveProjection(true, 50);
                RhinoViewer.Viewport.Camera35mmLensLength = Length;
                    break;
                case CameraProjection.TwoPoint:
                    RhinoViewer.Viewport.ChangeToTwoPointPerspectiveProjection(50);
                    RhinoViewer.Viewport.Camera35mmLensLength = Length;
                    break;
            }

        }

        public void SetCameraPosition(wPoint LocationPoint, wPoint TargetPoint, wVector UpVector)
        {
            RhinoViewer.Viewport.SetCameraLocation(new Rhino.Geometry.Point3d(LocationPoint.X, LocationPoint.Y, LocationPoint.Z), false);
            RhinoViewer.Viewport.SetCameraTarget(new Rhino.Geometry.Point3d(TargetPoint.X, TargetPoint.Y, TargetPoint.Z), false);
            RhinoViewer.Viewport.CameraUp = new Rhino.Geometry.Vector3d(UpVector.X, UpVector.Y, UpVector.Z);
        }


        public void SetViewShading(Rhino.Display.DisplayModeDescription Shader)
        {

            RhinoViewer.Viewport.DisplayMode = Shader;
        }

        public void SetGridAxis(bool HasGrid, bool HasAxis, bool HasGizmo)
        {
            RhinoViewer.Viewport.ConstructionGridVisible = HasGrid;
            RhinoViewer.Viewport.ConstructionAxesVisible = HasAxis;
            RhinoViewer.Viewport.WorldAxesVisible = HasGizmo;
        }

        public void ZoomExtents()
        {
            RhinoViewer.Viewport.ZoomExtents();
        }

        public void ZoomSelected()
        {
            RhinoViewer.Viewport.ZoomExtentsSelected();
        }

        public override void SetFill()
        {
        }

        public override void SetStroke()
        {
        }

        public override void SetSize()
        {

            if (Graphics.Width < 1)
            {
                RhinoViewer.Width = 300;
            }
            else
            {
                RhinoViewer.Width = (int)Graphics.Width;
            }

            if (Graphics.Height < 1)
            {
                RhinoViewer.Height = 300;
            }
            else
            {
                RhinoViewer.Height = (int)Graphics.Height;
            }
        }

        public void ResetGraphics()
        {
            Element.Margin = new Thickness(0);

        }

        public override void SetMargin()
        {
            Element.Margin = new Thickness(Graphics.Margin[0], Graphics.Margin[1], Graphics.Margin[2], Graphics.Margin[3]);
        }

        public override void SetPadding()
        {
        }

        public override void SetFont()
        {
        }
    }
}
