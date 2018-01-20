using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;
using Wind.Types;

using Parrot.Containers;

using Pollen.Charts;
using Grasshopper.Kernel.Parameters;
using Pollen.Collections;
using Grasshopper.Kernel.Types;
using System.Windows.Forms;
using GH_IO.Serialization;
using Wind.Presets;

namespace Pollen_GH.Charts
{
    public class ChartPoint : GH_Component
    {
        public int ModeLighting = 0;
        public int ModeStack = 0;

        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the RadialChart class.
        /// </summary>
        public ChartPoint()
          : base("3D Chart", "3D Chart", "---", "Aviary", "Charting & Data")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Pivot", "P", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Tilt", "T", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Lens Length", "L", "---", GH_ParamAccess.item, 0);
            pManager[4].Optional = true;

            Param_Integer param1 = (Param_Integer)Params.Input[1];
            param1.AddNamedValue("Point", 0);
            param1.AddNamedValue("Bar", 1);
            param1.AddNamedValue("Column", 2);
            param1.AddNamedValue("Line", 3);
            param1.AddNamedValue("StepLine", 4);
            param1.AddNamedValue("Spline", 5);
            param1.AddNamedValue("Area", 6);
            

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

            var pControl = new pPointChart(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                if (Elements[C] != null)
                {
                    WindObject = Elements[C];
                    Element = (pElement)WindObject.Element;
                    pControl = (pPointChart)Element.PollenControl;
                }
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            IGH_Goo D = null;
            int M = 0;
            int P = 0;
            int T = 0;
            int L = 0;

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref P)) return;
            if (!DA.GetData(3, ref T)) return;
            if (!DA.GetData(4, ref L)) return;

            wObject W = new wObject();
            D.CastTo(out W);

            bool LineMode = ((M > 2) & (M < 6));
            bool TagMode = ((M == 1) || (M == 2));

            DataSetCollection DC = (DataSetCollection)W.Element;

            if (DC.TotalCustomFill == 0) { DC.SetDefaultPallet(Wind.Presets.wGradients.GradientTypes.Metro, false, DC.Sets.Count > 1); }
            if (DC.TotalCustomStroke == 0) { if (LineMode) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.LineChart, wGradients.GradientTypes.Metro,false, DC.Sets.Count > 1); } else { DC.SetDefaultStrokes(wStrokes.StrokeTypes.OffWhiteSolid); } }
            if (DC.TotalCustomMarker == 0) { if (M == 0) { DC.SetDefaultMarkers(wGradients.GradientTypes.Metro, wMarker.MarkerType.Circle, false, DC.Sets.Count > 1); } else { DC.SetDefaultMarkers(wGradients.GradientTypes.Transparent, wMarker.MarkerType.None, false, DC.Sets.Count > 1); } }
            if (DC.TotalCustomFont == 0) { DC.SetDefaultFonts(new wFonts(wFonts.FontTypes.ChartPointDark).Font); }

            if (DC.TotalCustomTitles == 0) { DC.Graphics.FontObject = new wFonts(wFonts.FontTypes.AxisLabel).Font; }

            List<pPointSeries> PointSeriesList = new List<pPointSeries>();

            pControl.SetProperties(DC);

            for (int i = 0; i < DC.Sets.Count; i++)
            {
                pPointSeries pSeriesSet = new pPointSeries(DC.Sets[i].Title);
                pSeriesSet.SetProperties(DC.Sets[i]);
                pSeriesSet.SetNumberChartType(M, ModeStack, LineMode);
                pSeriesSet.SetChartLabels(DC.Label);
                pSeriesSet.SetNumericData(0);
                PointSeriesList.Add(pSeriesSet);
            }

            pControl.View = new Pollen.Utilities.p3D(P, T, L, (Pollen.Utilities.p3D.LightingMode)ModeLighting);

            pControl.SetSeries(PointSeriesList);
            pControl.SetThreeDView();
            pControl.SetAxisAppearance();
            pControl.SetAxisScale();

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pControl.Element, pControl, pControl.Type); }
            WindObject = new wObject(Element, "Pollen", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);

        }


        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Adjoin", ModeAdjacent, true, (ModeStack == 0));
            Menu_AppendItem(menu, "Stack", ModeStacked, true, (ModeStack == 1));
            Menu_AppendItem(menu, "Stretch", ModeStretched, true, (ModeStack == 2));

            Menu_AppendSeparator(menu);
            

            Menu_AppendItem(menu, "None", ModeNone, true, (ModeLighting == 0));
            Menu_AppendItem(menu, "Simple", ModeSimple, true, (ModeLighting == 1));
            Menu_AppendItem(menu, "Realistic", ModeReal, true, (ModeLighting == 2));

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Mode", ModeLighting);
            writer.SetInt32("Stack", ModeStack);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            ModeLighting = reader.GetInt32("Mode");
            ModeStack = reader.GetInt32("Stack");

            return base.Read(reader);
        }

        private void ModeNone(Object sender, EventArgs e)
        {
            ModeLighting = 0;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeSimple(Object sender, EventArgs e)
        {
            ModeLighting = 1;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeReal(Object sender, EventArgs e)
        {
            ModeLighting = 2;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void ModeAdjacent(Object sender, EventArgs e)
        {
            ModeStack = 0;
            
            this.ExpireSolution(true);
        }

        private void ModeStacked(Object sender, EventArgs e)
        {
            ModeStack = 1;
            
            this.ExpireSolution(true);
        }

        private void ModeStretched(Object sender, EventArgs e)
        {
            ModeStack = 2;
            
            this.ExpireSolution(true);
        }

        private void UpdateMessage()
        {
            string[] msg = { "None", "Simple", "Realistic" };
            Message = msg[ModeLighting];
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
                return Properties.Resources.Pollen_Winform;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{66135512-5c58-406c-b4c3-8a90c6972eba}"); }
        }
    }
}