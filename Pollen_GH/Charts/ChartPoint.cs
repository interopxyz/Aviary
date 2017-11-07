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

namespace Pollen_GH.Charts
{
  public class ChartPoint : GH_Component
    {
        int modeStatus = 0;

        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the RadialChart class.
        /// </summary>
        public ChartPoint()
          : base("Point Chart", "Point Chart", "---", "Aviary", "Charting & Data")
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

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Point", 0);
            param.AddNamedValue("Bar", 1);
            param.AddNamedValue("Column", 2);
            param.AddNamedValue("Line", 3);
            param.AddNamedValue("StepLine", 4);
            param.AddNamedValue("Spline", 5);
            param.AddNamedValue("Area", 6);
            param.AddNamedValue("SplineArea", 7);

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
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pControl = (pPointChart)Element.PollenControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            IGH_Goo D = null;
            int M = 0;

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref M)) return;

            wObject W = new wObject();
            D.CastTo(out W);

            DataSetCollection DC = (DataSetCollection)W.Element;

            List<pPointSeries> PointSeriesList = new List<pPointSeries>();

            pControl.SetProperties(DC);

            for (int i = 0; i<DC.Sets.Count;i++)
            {
                pPointSeries pSeriesSet = new pPointSeries(Convert.ToString(name + i));
                pSeriesSet.SetProperties(DC.Sets[i]);
                pSeriesSet.SetNumberChartType(M, modeStatus);
                pSeriesSet.SetChartLabels(DC.LeaderPostion,DC.HasLeader);
                pSeriesSet.SetNumericData(0);
                PointSeriesList.Add(pSeriesSet);
            }
            
            pControl.SetSeries(PointSeriesList);
            pControl.SetThreeDView();
            pControl.SetAxisAppearance();
            pControl.SetAxisScale(); 

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pControl.Element, pControl, pControl.Type); }
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

            Menu_AppendItem(menu, "None", ModeNone, true, (modeStatus == 0));
            Menu_AppendItem(menu, "Stack", ModeStack, true, (modeStatus == 1));
            Menu_AppendItem(menu, "Stack100", ModeStackFill, true, (modeStatus == 2));

        }


        private void ModeNone(Object sender, EventArgs e)
        {
            modeStatus = 0;
            this.ExpireSolution(true);
        }

        private void ModeStack(Object sender, EventArgs e)
        {
            modeStatus = 1;
            this.ExpireSolution(true);
        }

        private void ModeStackFill(Object sender, EventArgs e)
        {
            modeStatus = 2;
            this.ExpireSolution(true);
        }


        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
    {
      get
            {
                return Properties.Resources.Pollen_Points1;
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