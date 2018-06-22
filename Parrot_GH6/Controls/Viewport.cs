using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Utilities;
using Wind.Containers;
using Parrot.Containers;
using Parrot_GH6.Controls;
using Parrot.Controls;
using System.Windows.Forms;
using Grasshopper.Kernel.Parameters;
using GH_IO.Serialization;
using System.Linq;
using Wind.Scene;
using Wind.Scene.Cameras;
using Grasshopper.Kernel.Types;

namespace Parrot_GH6.Controls
{
    public class Viewport : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        public bool HasGrid = false;
        public bool HasAxis = false;
        public bool HasGizmo = true;

        public bool ZoomExtent = false;
        public bool ZoomSelect = false;

        public int ProjectionMode = 0;

        /// <summary>
        /// Initializes a new instance of the Viewport class.
        /// </summary>
        public Viewport()
          : base("View Port", "ViewPort","---","Aviary", "V6")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Viewport Name", "N", "---", GH_ParamAccess.item, "GH_HACK");
            pManager[0].Optional = true;

            pManager.AddIntegerParameter("Display", "D", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;

            pManager.AddIntegerParameter("Projection", "P", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            pManager.AddGenericParameter("Camera", "C", "---", GH_ParamAccess.item);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)pManager[2];
            param.AddNamedValue("Parallel", 0);
            param.AddNamedValue("Perspective", 1);
            param.AddNamedValue("Two Point", 2);

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[3];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new wCameraStandard()));

            SetDisplayMode();
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Parrot WPF Control Element", GH_ParamAccess.item);
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

            var pCtrl = new pViewport(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                if (Elements[C] != null)
                {
                    WindObject = Elements[C];
                    Element = (pElement)WindObject.Element;
                    pCtrl = (pViewport)Element.ParrotControl;
                }
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties
            string Text = "GH_HACK";
            int DisplayIndex = 0;
            int ProjectionIndex = 0;
            wCamera Cam = new wCameraStandard();

            if (!DA.GetData(0, ref Text)) return;
            if (!DA.GetData(1, ref DisplayIndex)) return;
            if (!DA.GetData(2, ref ProjectionIndex)) return;
            if (!DA.GetData(3, ref Cam)) return;

            pCtrl.SetProperties(Text);

            if (ZoomExtent)
            {
                pCtrl.ZoomExtents();
                ZoomExtent = false;
            }

            if (ZoomSelect)
            {
                pCtrl.ZoomSelected();
                ZoomSelect = false;
            }

            pCtrl.SetGridAxis(HasGrid, HasAxis, HasGizmo);
            pCtrl.SetCameraProjection( (pViewport.CameraProjection)ProjectionIndex, Cam.LensLength);
            pCtrl.SetCameraPosition(Cam.Location, Cam.Target, Cam.Up);
            //pCtrl.SetViewPort(Rhino.RhinoDoc.ActiveDoc.Views.ToList()[0].ActiveViewport);
           pCtrl.SetViewShading(Rhino.Display.DisplayModeDescription.GetDisplayModes()[0]);

            pCtrl.BuildView();
            

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }

            Element.Control = pCtrl;
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);

        }

        private void SetDisplayMode()
        {
            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            int i = 0;

            foreach (Rhino.Display.DisplayModeDescription DM in Rhino.Display.DisplayModeDescription.GetDisplayModes())
            {
                param.AddNamedValue(DM.EnglishName, i);
                i += 1;
            }
            
        }

        //ADD MENUS TO COMPONENT RIGHT CLICK
        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Grid",  GridMode , true, HasGrid );
            Menu_AppendItem(menu, "Axis",  AxisMode , true, HasAxis );
            Menu_AppendItem(menu, "Gizmo", GizmoMode, true, HasGizmo);
            
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Zoom Extents", RunZoomExtents, true, false);
            Menu_AppendItem(menu, "Zoom Selected", RunZoomSelected, true, false);

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Refresh Display Modes", DisplayMode, true, false);

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetBoolean("Grid", HasGrid);
            writer.SetBoolean("Axis", HasAxis);
            writer.SetBoolean("Gizmo", HasGizmo);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            HasGrid = reader.GetBoolean("Grid");
            HasAxis = reader.GetBoolean("Axis");
            HasGizmo = reader.GetBoolean("Gizmo");
            
            this.ExpireSolution(true);
            return base.Read(reader);
        }

        private void DisplayMode(Object sender, EventArgs e)
        {
            SetDisplayMode();
            this.ExpireSolution(true);
        }

        //HELPERS
        private void GridMode(Object sender, EventArgs e)
        {
            HasGrid = !HasGrid;
            this.ExpireSolution(true);
        }

        private void AxisMode(Object sender, EventArgs e)
        {
            HasAxis = !HasAxis;
            this.ExpireSolution(true);
        }

        private void GizmoMode(Object sender, EventArgs e)
        {
            HasGizmo = !HasGizmo;
            this.ExpireSolution(true);
        }

        //ZOOMS
        private void RunZoomExtents(Object sender, EventArgs e)
        {
            ZoomExtent = true;
            this.ExpireSolution(true);
        }

        private void RunZoomSelected(Object sender, EventArgs e)
        {
            ZoomSelect = true;
            this.ExpireSolution(true);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
                //return Properties.Resources.Parrot_Button;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1c42c2d3-8921-487d-9ead-fcfe95da4b2a"); }
        }
    }
}