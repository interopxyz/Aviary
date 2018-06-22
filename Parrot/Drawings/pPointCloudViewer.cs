using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using System.Windows.Controls;

using Parrot.Controls;

using Wind.Containers;
using Wind.Geometry.Meshes;
using Wind.Scene;

using HelixToolkit;
using HelixToolkit.Wpf;
using System.Windows;
using Wind.Types;
using Wind.Geometry.Vectors;
using System.Collections;
using Wind.Scene.Cameras;

namespace Parrot.Drawings
{
    public class pPointCloudViewer : pControl
    {

        public Grid Element;

        public HelixViewport3D ViewPort = new HelixViewport3D();

        public OrthographicCamera Ortho = new OrthographicCamera();
        public PerspectiveCamera Perspective = new PerspectiveCamera();

        public double SceneDiagonal = 0;

        public wCameraStandard Cam = new wCameraStandard();

        public pPointCloudViewer(string InstanceName)
        {
            Element = new Grid();
            Element.Name = InstanceName;
            Type = "PointCloudViewer";

            ViewPort.Width = 600;
            ViewPort.Height = 600;

            Element.Width = 600;
            Element.Height = 600;
        }

        public void SetProperties()
        {
            ViewPort.Name = "HViewPort";
            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.RowDefinitions.Add(new RowDefinition());

            Grid.SetColumn(ViewPort, 0);
            Grid.SetRow(ViewPort, 0);

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Children.Add(ViewPort);

        }

        public void ClearScene()
        {
            ViewPort.Children.Clear();
        }

        //CAMERA

        public void SetCamera(wCameraStandard WindCamera, double Extents)
        {

            Cam = WindCamera;
            if (Cam.IsDefault)
            {
                Ortho = new OrthographicCamera(Cam.Location.ToPoint3D(), Cam.Direction.ToVector3D(), Cam.Up.ToVector3D(), Extents);
                ViewPort.Orthographic = true;
                ViewPort.Camera = Ortho;
                ViewPort.Camera.NearPlaneDistance = -Extents;
            }
            else
            {
                if (Cam.LensLength <= 0)
                {
                    Ortho = new OrthographicCamera(Cam.Location.ToPoint3D(), Cam.Direction.ToVector3D(), Cam.Up.ToVector3D(), Cam.Distance);
                    ViewPort.Orthographic = true;
                    ViewPort.Camera = Ortho;
                    ViewPort.Camera.NearPlaneDistance = -Extents;
                }
                else
                {
                    Perspective = new PerspectiveCamera(Cam.Location.ToPoint3D(), Cam.Direction.ToVector3D(), Cam.Up.ToVector3D(), Cam.LensLength);
                    ViewPort.Orthographic = false;
                    ViewPort.Camera = Perspective;
                }
            }
            SceneDiagonal = Extents;
        }

        public void AddPoint(wPoint WindPoint, wColor WindColor, double Radius)
        {
            PointsVisual3D VisPoint = new PointsVisual3D();
            VisPoint.Size = Radius;
            VisPoint.Color = WindColor.ToMediaColor();
            
            VisPoint.Points.Add(WindPoint.ToPoint3D());
            
            ViewPort.Children.Add(VisPoint);
        }

        public void ZoomExtents(double TransitionTime)
        {
            ViewPort.ZoomExtents(TransitionTime);
            ViewPort.Camera.NearPlaneDistance = -SceneDiagonal;
        }

        public void SetNavigation(bool HasNavigation)
        {
            ViewPort.IsPanEnabled = HasNavigation;
            ViewPort.IsRotationEnabled = HasNavigation;
            ViewPort.IsZoomEnabled = HasNavigation;
        }

        public void SetGizmo(bool HasGizmo)
        {
            ViewPort.ShowViewCube = HasGizmo;
            ViewPort.ViewCubeOpacity = 0.25;
        }

        public void SetCoordinateSystem(bool HasCoordinateSystem)
        {
            ViewPort.ShowCoordinateSystem = HasCoordinateSystem;
        }

        public override void SetFill()
        {
            Element.Background = Graphics.WpfFill;
        }

    }
}
