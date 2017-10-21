using Parrot.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using Wind.Containers;
using Wind.Geometry.Meshes;
using Wind.Scene;

using HelixToolkit;
using HelixToolkit.Wpf;
using System.Windows;

namespace Parrot.Drawings
{
    public class pMeshViewer : pControl
    {
        public Grid Element;
        public string Type;
        
        public HelixViewport3D ViewPort = new HelixViewport3D();
        
        public OrthographicCamera Ortho = new OrthographicCamera();
        public PerspectiveCamera Perspective = new PerspectiveCamera();

        public List<MeshGeometry3D> Meshes = new List<MeshGeometry3D>();
        public wCamera Cam = new wCamera();
        
        public List<Material> Materials = new List<Material>();
        public List<Light> Lights = new List<Light>();

        public Rect3D Bound3D = new Rect3D();
        public Point3D Origin = new Point3D();

        public pMeshViewer(string InstanceName)
        {
            Element = new Grid();
            Element.Name = InstanceName;
            Type = "3DView";

            ViewPort.Width = 600;
            ViewPort.Height = 600;

            Element.Width = 600;
            Element.Height = 600;
        }

        public void SetProperties()
        {

            Element.ColumnDefinitions.Add(new ColumnDefinition());
            Element.RowDefinitions.Add(new RowDefinition());

            Grid.SetColumn(ViewPort, 0);
            Grid.SetRow(ViewPort, 0);

            Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            Element.Children.Add(ViewPort);
            
        }

        public void BuildScene()
        {

            ModelVisual3D VisD = new ModelVisual3D();
            Model3DGroup mGroup = new Model3DGroup();

            mGroup = AddMeshes(mGroup);
            mGroup = AddLights(mGroup);

            VisD.Content = mGroup;
            
            ViewPort.Children.Clear();
            ViewPort.Children.Add(VisD);

        }

        //CAMERA
        
        public void SetCamera(wCamera WindCamera)
        {

            Cam = WindCamera;

            Point3D P = Cam.Location.ToPoint3D();
            P = new Point3D(Origin.X + P.X, Origin.Y + P.Y, Origin.Z + P.Z);

            if (Cam.IsPreset)
            {
                double Diagonal = Math.Sqrt(Math.Sqrt(Math.Pow(Bound3D.SizeY,2.0)+ Math.Pow(Bound3D.SizeX, 2.0))+ Math.Pow(Bound3D.SizeZ, 2.0))*1.5;
                Ortho = new OrthographicCamera(P, Cam.Direction.ToVector3D(), Cam.Up.ToVector3D(), Diagonal);
                ViewPort.Orthographic = true;
                ViewPort.Camera = Ortho;
                ViewPort.Camera.NearPlaneDistance = -Diagonal;
            }
            else
            { 
                if (Cam.Length<=0)
                {
                    Ortho = new OrthographicCamera(P, Cam.Direction.ToVector3D(), Cam.Up.ToVector3D(), Cam.Distance); 
                    ViewPort.Orthographic = true;
                    ViewPort.Camera = Ortho;
                    ViewPort.Camera.NearPlaneDistance = double.NegativeInfinity;
                }
                else
                {
                    Perspective = new PerspectiveCamera(P, Cam.Direction.ToVector3D(), Cam.Up.ToVector3D(), Cam.Length);
                    ViewPort.Orthographic = false;
                    ViewPort.Camera = Perspective;
                }
             }
        }

        //LIGHTS

        public void SetLights(List<wLight> LightSet)
        {

            Lights.Clear();

            for(int i = 0; i<LightSet.Count;i++)
            {
                Lights.Add(LightSet[i].GetWPFLight());
            }

        }

        public Model3DGroup AddLights(Model3DGroup Group)
        {

            for(int i = 0;i<Lights.Count;i++)
            { 
            Group.Children.Add(Lights[i]);
            }
            return Group;

        }

        //MESHES & MATERIALS

        public void SetMeshes(List<wMesh> MeshSet)
        {

            Meshes.Clear();
            Materials.Clear();

            for (int i = 0; i < MeshSet.Count; i++)
            {

                Meshes.Add(MeshSet[i].WpfMesh);

                if (i == 0) { Bound3D = MeshSet[i].WpfMesh.Bounds; }else { Bound3D.Union(MeshSet[i].WpfMesh.Bounds); }

                MaterialGroup MatGroup = new MaterialGroup();
                MatGroup.Children.Add(new DiffuseMaterial(new SolidColorBrush(MeshSet[i].Material.DiffuseColor.ToMediaColor())));
                MatGroup.Children.Add(new SpecularMaterial(new SolidColorBrush(MeshSet[i].Material.SpecularColor.ToMediaColor()), 1.0-MeshSet[i].Material.SpecularValue));
                MatGroup.Children.Add(new EmissiveMaterial(new SolidColorBrush(MeshSet[i].Material.EmissiveColor.ToMediaColor())));
                
                Materials.Add(MatGroup);

            }
            Origin = new Point3D(Bound3D.Location.X + Bound3D.SizeX / 2.0, Bound3D.Location.Y + Bound3D.SizeX / 2.0, Bound3D.Location.Z + Bound3D.SizeX / 2.0);
        }

        public Model3DGroup AddMeshes(Model3DGroup Group)
        {

            for (int i = 0; i < Meshes.Count; i++)
            {
                GeometryModel3D mGeo = new GeometryModel3D();

                mGeo.Geometry = Meshes[i];
                mGeo.Material = Materials[i];
                mGeo.BackMaterial = Materials[i];

                Group.Children.Add(mGeo);
            }
            return Group;
        }

        public void ShadeMode(int ShaderMode)
        {
        }

        public void ZoomExtents(double TransitionTime)
        {
            ViewPort.ZoomExtents(TransitionTime);
            ViewPort.Camera.NearPlaneDistance = double.NegativeInfinity;
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

        public void SetEdges()
        {
        }

        public override void SetSolidFill()
        {
            ViewPort.Background = new SolidColorBrush(Graphics.Background.ToMediaColor());
        }

        public override void SetStroke()
        {
            ViewPort.BorderThickness = new Thickness(Graphics.StrokeWeight[0], Graphics.StrokeWeight[1], Graphics.StrokeWeight[2], Graphics.StrokeWeight[3]);
            ViewPort.BorderBrush = new SolidColorBrush(Graphics.StrokeColor.ToMediaColor());
        }

        public override void SetSize()
        {
            if (Graphics.Width < 1)
            {
                Element.Width = double.NaN;
                ViewPort.Width = double.NaN;
            }
            else
            {
                Element.Width = Graphics.Width;
                ViewPort.Width = Graphics.Width;
            }

            if (Graphics.Height < 1)
            {
                Element.Height = double.NaN;
                ViewPort.Height = double.NaN;
            }
            else
            {
                Element.Height = Graphics.Height;
                ViewPort.Height = Graphics.Height;
            }

        }

        public override void SetMargin()
        {
            Element.Margin = new Thickness(Graphics.Margin[0], Graphics.Margin[1], Graphics.Margin[2], Graphics.Margin[3]);
        }

    }
}
