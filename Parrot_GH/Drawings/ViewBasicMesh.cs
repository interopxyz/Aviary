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
using Grasshopper.Kernel.Parameters;

namespace Parrot_GH.Drawings
{
    public class ViewBasicMesh : GH_Component
    {
        public List<wLight> InitialLights = new List<wLight>();

        public wCamera InitialCamera = new wCamera();

        int LightMode = 0;
        int CameraMode = 0;
        bool HasNavigation = true;
        bool HasGizmo = false;
        bool HasCoordinates = false;
        bool ZoomExtents = false;

        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ViewMesh3D class.
        /// </summary>
        public ViewBasicMesh()
          : base("View Basic Mesh", "Basic Mesh", "---", "Aviary", "3D Scene")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            InitialLights = SoftLights();
            InitialCamera = CameraNW();

            pManager.AddGenericParameter("Mesh", "M", "---", GH_ParamAccess.list);

            pManager.AddGenericParameter("Lights", "L", "---", GH_ParamAccess.list);
            UpdateLightInput(1);

            pManager.AddGenericParameter("Camera", "C", "---", GH_ParamAccess.item);
            UpdateCameraInput(2);

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

            var pCtrl = new pMeshViewer(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pMeshViewer)Element.ParrotControl;
            }
            else
            {
                pCtrl.SetProperties();
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            List<IGH_Goo> X = new List<IGH_Goo>();
            List<IGH_Goo> Y = new List<IGH_Goo>();
            IGH_Goo U = null;

            if (Params.Input[1].VolatileDataCount < 1) { UpdateLightInput(1); }
            if (Params.Input[2].VolatileDataCount < 1) { UpdateCameraInput(2); }

            if (!DA.GetDataList(0, X)) return;
            if (!DA.GetDataList(1, Y)) return;
            if (!DA.GetData(2, ref U)) return;

            wCamera V = new wCamera();
            U.CastTo(out V);
            
            List<wMesh> Meshes = new List<wMesh>();
            foreach (IGH_Goo Obj in X)
            {
                wMesh M = new wMesh();
                Obj.CastTo(out M);
                Meshes.Add(M);
            }

            pCtrl.SetMeshes(Meshes);

            List<wLight> Lights = new List<wLight>();
            foreach (IGH_Goo Obj in Y)
            {
                wLight L = new wLight();
                Obj.CastTo(out L);
                Lights.Add(L);
            }
            
            pCtrl.SetLights(Lights);
            
            pCtrl.BuildScene();

            pCtrl.SetCamera(V);

            pCtrl.SetNavigation(HasNavigation);
            pCtrl.SetGizmo(HasGizmo);
            pCtrl.SetCoordinateSystem(HasCoordinates);

            //if (V.IsPreset) { pCtrl.ZoomExtents(1.0); }

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


        //ADD MENUS TO COMPONENT RIGHT CLICK
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

            Menu_AppendItem(menu, "Soft", LightModeSoft, true, (LightMode == 0));
            Menu_AppendItem(menu, "Rainbow", LightModeRainbowLight, true, (LightMode == 1));
            Menu_AppendItem(menu, "Hot & Cold", LightModeHotCold, true, (LightMode == 2));

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


        //LIGHT INPUTS
        private void UpdateLightInput(int index)
        {
            Params.Input[index].ClearData();
            Params.Input[index].AddVolatileDataList(new Grasshopper.Kernel.Data.GH_Path(0), InitialLights);

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[index];
            paramGen.PersistentData.ClearData();
            foreach (wLight light in InitialLights)
            {
                paramGen.PersistentData.Append(new GH_ObjectWrapper(light));
            }
        }

        //LIGHT PRESETS
        private List<wLight> SoftLights()
        {
            List<wLight> lights = new List<wLight>
            {
            new wAmbientLight(new wColor(100, 100, 100)),
            new wDirectionalLight(new wVector(new wPoint(0, 1, 0.1)), new wColor(28, 28, 28)),
            new wDirectionalLight(new wVector(new wPoint(-1, 0, 0.1)), new wColor(45, 45, 45)),
            new wDirectionalLight(new wVector(new wPoint(0, -1, -0.1)), new wColor(45, 45, 45)),
            new wDirectionalLight(new wVector(new wPoint(1, 0, -0.1)), new wColor(28, 28, 28))
            };
            return lights;
        }

        private List<wLight> RainbowLights()
        {
            List<wLight> lights = new List<wLight>
            {
            new wAmbientLight(new wColor(70, 70, 70)),
            new wDirectionalLight(new wVector(new wPoint( 0.783924, 0.569554, 0.247125 )), new wColor( 34, 2, 7 )),
            new wDirectionalLight(new wVector(new wPoint( 0.25031, 0.770375, -0.586402 )), new wColor( 40, 0, 22 )),
            new wDirectionalLight(new wVector(new wPoint( -0.246346, 0.758176, -0.603724 )), new wColor( 38, 0, 44 )),
            new wDirectionalLight(new wVector(new wPoint( -0.790485, 0.574321, 0.212811 )), new wColor( 7, 14, 33 )),
            new wDirectionalLight(new wVector(new wPoint( -0.70772, 0.0, 0.706493 )), new wColor( 0, 32, 39 )),
            new wDirectionalLight(new wVector(new wPoint( -0.776514, -0.564171, 0.280602 )), new wColor( 3, 37, 18 )),
            new wDirectionalLight(new wVector(new wPoint( -0.2544, -0.782964, -0.567669 )), new wColor( 16, 39, 1 )),
            new wDirectionalLight(new wVector(new wPoint( 0.242543, -0.746469, -0.619643 )), new wColor( 32, 34, 2 )),
            new wDirectionalLight(new wVector(new wPoint( 0.796131, -0.578423, 0.177767 )), new wColor( 36, 22, 1 )),
            new wDirectionalLight(new wVector(new wPoint( 0.709554, 0.0, 0.704651 )), new wColor( 38, 9, 0 ))
            };
            return lights;
        }

        private List<wLight> HotColdLights()
        {
            List<wLight> lights = new List<wLight>
            {
            new wAmbientLight(new wColor(0, 0, 0)),
            new wDirectionalLight(new wVector(new wPoint(0,0,-1)), new wColor(0, 0, 0)),
            new wDirectionalLight(new wVector(new wPoint(1,1,1)), new wColor(95, 75, 57)),
            new wDirectionalLight(new wVector(new wPoint(1,-1,-1)), new wColor(57, 72, 95)),
            new wDirectionalLight(new wVector(new wPoint(-1,1,1)), new wColor(95, 75, 57)),
            new wDirectionalLight(new wVector(new wPoint(-1,-1,-1)), new wColor(57, 72, 95))
            };
            return lights;
        }

        //SET LIGHT MODES
        private void LightModeSoft(Object sender, EventArgs e)
        {
            InitialLights = SoftLights();
            UpdateLightInput(1);

            LightMode = 0;
            this.ExpireSolution(true);
        }
        
        private void LightModeRainbowLight(Object sender, EventArgs e)
        {
            InitialLights = RainbowLights();
            UpdateLightInput(1);

            LightMode = 1;
            this.ExpireSolution(true);
        }
        
        private void LightModeHotCold(Object sender, EventArgs e)
        {
            InitialLights = HotColdLights();
            UpdateLightInput(1);

            LightMode = 2;
            this.ExpireSolution(true);
        }

        //CAMERA INPUTS
        
        private void UpdateCameraInput(int index)
        {
            Params.Input[index].ClearData();
            Params.Input[index].AddVolatileData(new Grasshopper.Kernel.Data.GH_Path(0), 0, InitialCamera);

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[index];
            paramGen.PersistentData.ClearData();
            paramGen.PersistentData.Append(new GH_ObjectWrapper(InitialCamera));

        }

        //CAMERA PRESETS
            private wCamera CameraNW()
        { 
            return new wCamera(135, 54.736, 1.0, 0, true);
        }
        private wCamera CameraNE()
        {
            return new wCamera(45, 54.736, 1.0, 0, true);
        }
        private wCamera CameraSW()
        {
            return new wCamera(235, 54.736, 1.0, 0, true);
        }
        private wCamera CameraSE()
        {
            return new wCamera(315, 54.736, 1.0, 0, true);
        }
        private wCamera CameraTop()
        {
            return new wCamera(0, 0, 1.0, 0, true);
        }
        private wCamera CameraBottom()
        {
            return new wCamera(0, 180, 1.0, 0, true);
        }
        private wCamera CameraLeft()
        {
            return new wCamera(180, 90, 1.0, 0, true);
        }
        private wCamera CameraRight()
        {
            return new wCamera(0, 90, 1.0, 0, true);
        }
        private wCamera CameraFront()
        {
            return new wCamera(270, 90, 1.0, 0, true);
        }
        private wCamera CameraBack()
        {
            return new wCamera(90, 90, 1.0, 0, true);
        }

        //CAMERA PRESETS
        private void CameraModeNW(Object sender, EventArgs e)
        {
            InitialCamera = CameraNW();
            UpdateCameraInput(2);

            CameraMode = 0;
            this.ExpireSolution(true);
        }

        private void CameraModeNE(Object sender, EventArgs e)
        {
            InitialCamera = CameraNE();
            UpdateCameraInput(2);

            CameraMode = 1;
            this.ExpireSolution(true);
        }

        private void CameraModeSW(Object sender, EventArgs e)
        {
            InitialCamera = CameraSW();
            UpdateCameraInput(2);

            CameraMode = 2;
            this.ExpireSolution(true);
        }

        private void CameraModeSE(Object sender, EventArgs e)
        {
            InitialCamera = CameraSE();
            UpdateCameraInput(2);

            CameraMode = 3;
            this.ExpireSolution(true);
        }

        private void CameraModeT(Object sender, EventArgs e)
        {
            InitialCamera = CameraTop();
            UpdateCameraInput(2);

            CameraMode = 4;
            this.ExpireSolution(true);
        }

        private void CameraModeU(Object sender, EventArgs e)
        {
            InitialCamera = CameraBottom();
            UpdateCameraInput(2);

            CameraMode = 5;
            this.ExpireSolution(true);
        }

        private void CameraModeL(Object sender, EventArgs e)
        {
            InitialCamera = CameraLeft();
            UpdateCameraInput(2);

            CameraMode = 6;
            this.ExpireSolution(true);
        }

        private void CameraModeR(Object sender, EventArgs e)
        {
            InitialCamera = CameraRight();
            UpdateCameraInput(2);

            CameraMode = 7;
            this.ExpireSolution(true);
        }

        private void CameraModeF(Object sender, EventArgs e)
        {
            InitialCamera = CameraFront();
            UpdateCameraInput(2);

            CameraMode = 8;
            this.ExpireSolution(true);
        }

        private void CameraModeB(Object sender, EventArgs e)
        {
            InitialCamera = CameraBack();
            UpdateCameraInput(2);

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
                return Properties.Resources.Parrot_ViewMeshBasic;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3d39481b-d6c3-4343-888f-43608b96608d}"); }
        }
    }
}