using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;
using Wind.Types;
using Wind.Scene;

using Parrot.Containers;
using Parrot.Displays;
using Parrot.Drawings;
using Rhino.DocObjects;
using Grasshopper.Kernel.Types;

using Wind.Geometry.Vectors;
using Wind.Geometry.Meshes;
using System.Windows.Forms;
using System.Drawing;

namespace Parrot_GH.Drawings
{
  public class ViewPointCloud : GH_Component
    {
        public wCamera InitialCamera = new wCamera(135, 54.736, 1.0, 0, true, true);

        int CameraMode = 0;
        bool HasNavigation = true;
        bool HasGizmo = false;
        bool HasCoordinates = false;
        bool ZoomExtents = false;

        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ViewPointCloud class.
        /// </summary>
        public ViewPointCloud()
      : base("ViewPointCloud", "Point Cloud", "---", "Aviary", "3D Scene")
        {
        }
        

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    {
            pManager.AddPointParameter("Points", "P", "---", GH_ParamAccess.list,new Point3d());
            pManager[0].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.list, System.Drawing.Color.Black);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Radius", "R", "---", GH_ParamAccess.list, 1);
            pManager[2].Optional = true;
            pManager.AddGenericParameter("Camera", "C", "---", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "WPF Control Element", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            string ID = this.Attributes.InstanceGuid.ToString();
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)), false).Text;
            int C = this.RunCount;

            wObject WindObject = new wObject();
            pElement Element = new pElement();
            bool Active = Elements.ContainsKey(C);

            var pCtrl = new pPointCloudViewer(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPointCloudViewer)Element.ParrotControl;
            }
            else
            {
                pCtrl.SetProperties();
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            List<Point3d> P = new List<Point3d>();
            List<Color> X = new List<Color>();
            List<double> R = new List<double>();
            IGH_Goo U = null;
            

            if (Params.Input[3].VolatileDataCount < 1)
            {
                Params.Input[3].ClearData();
                Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);
            }

            if (!DA.GetDataList(0, P)) return;
            if (!DA.GetDataList(1, X)) return;
            if (!DA.GetDataList(2, R)) return;
            if (!DA.GetData(3, ref U)) return;

            wCamera V = new wCamera();
            U.CastTo(out V);
            

            for(int i = (X.Count-1);i<P.Count;i++)
            {
                X.Add(X[X.Count - 1]);
            }

            for (int i = (R.Count - 1); i < P.Count; i++)
            {
                R.Add(R[R.Count - 1]);
            }

            pCtrl.ClearScene();

            for (int i = 0; i < P.Count; i++)
            {
                pCtrl.AddPoint(new wPoint(P[i].X, P[i].Y, P[i].Z), new wColor(X[i]), R[i]);
            }

            BoundingBox Bbox = new BoundingBox(P);
            
            pCtrl.SetCamera(V, Bbox.Diagonal.Length);
            
            pCtrl.SetNavigation(HasNavigation);
            pCtrl.SetGizmo(HasGizmo);
            pCtrl.SetCoordinateSystem(HasCoordinates);

            if (ZoomExtents)
            {
                pCtrl.ZoomExtents(1.0);
                ZoomExtents = false;
            }

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }

            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Navigation", InteractionMode, true, HasNavigation);
            Menu_AppendItem(menu, "Gizmo", GizmoMode, true, HasGizmo);
            Menu_AppendItem(menu, "Coordinate System", CoordinateMode, true, HasCoordinates);

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Zoom Extents", RunZoomExtents, true, false);

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "NW Isometric", CameraModeNW, true, (CameraMode == 0));
            Menu_AppendItem(menu, "NE Isometric", CameraModeNE, true, (CameraMode == 1));
            Menu_AppendItem(menu, "SW Isometric", CameraModeSW, true, (CameraMode == 2));
            Menu_AppendItem(menu, "SE Isometric", CameraModeSE, true, (CameraMode == 3));

            Menu_AppendItem(menu, "Top   ", CameraModeT, true, (CameraMode == 4));
            Menu_AppendItem(menu, "Bottom", CameraModeU, true, (CameraMode == 5));
            Menu_AppendItem(menu, "Left  ", CameraModeL, true, (CameraMode == 6));
            Menu_AppendItem(menu, "Right ", CameraModeR, true, (CameraMode == 7));
            Menu_AppendItem(menu, "Front ", CameraModeF, true, (CameraMode == 8));
            Menu_AppendItem(menu, "Back  ", CameraModeB, true, (CameraMode == 9));
        }

        //NAVIGATION
        private void RunZoomExtents(Object sender, EventArgs e)
        {
            ZoomExtents = true;
            this.ExpireSolution(true);
        }
        
        private void InteractionMode(Object sender, EventArgs e)
        {
            HasNavigation = !HasNavigation;
            this.ExpireSolution(true);
        }

        private void GizmoMode(Object sender, EventArgs e)
        {
            HasGizmo = !HasGizmo;
            this.ExpireSolution(true);
        }

        private void CoordinateMode(Object sender, EventArgs e)
        {
            HasCoordinates = !HasCoordinates;
            this.ExpireSolution(true);
        }


        //CAMERA PRESETS
        private void CameraModeNW(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(135, 54.736, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 0;
            this.ExpireSolution(true);
        }

        private void CameraModeNE(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(45, 54.736, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 1;
            this.ExpireSolution(true);
        }

        private void CameraModeSW(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(235, 54.736, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 2;
            this.ExpireSolution(true);
        }

        private void CameraModeSE(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(315, 54.736, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 3;
            this.ExpireSolution(true);
        }

        private void CameraModeT(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(0, 0, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 4;
            this.ExpireSolution(true);
        }

        private void CameraModeU(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(0, 180, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 5;
            this.ExpireSolution(true);
        }

        private void CameraModeL(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(180, 90, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 6;
            this.ExpireSolution(true);
        }

        private void CameraModeR(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(0, 90, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 7;
            this.ExpireSolution(true);
        }

        private void CameraModeF(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(270, 90, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 8;
            this.ExpireSolution(true);
        }

        private void CameraModeB(Object sender, EventArgs e)
        {
            InitialCamera = new wCamera(90, 90, 1.0, 0, true);

            Params.Input[3].ClearData();
            Params.Input[3].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            CameraMode = 9;
            this.ExpireSolution(true);
        }


        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
    {
      get
            {
                return Properties.Resources.Parrot_PointViewer;
            }
    }

    /// <summary>
    /// Gets the unique ID for this component. Do not change this ID after release.
    /// </summary>
    public override Guid ComponentGuid
    {
      get { return new Guid("b772658d-05c6-408c-ae82-6e65e06406c1"); }
    }
  }
}