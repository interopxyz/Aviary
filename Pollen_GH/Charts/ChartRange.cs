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
using Grasshopper.Kernel.Types;
using Pollen.Collections;

namespace Pollen_GH.Charts
{
    public class ChartRange : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the RadialChart class.
        /// </summary>
        public ChartRange()
          : base("Range Chart", "Range Chart", "---", "Aviary", "Chart")
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
            param.AddNamedValue("Range", 0);
            param.AddNamedValue("Column Range", 1);
            param.AddNamedValue("Bar Range", 2);
            param.AddNamedValue("Bubble", 3);

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

            for (int i = 0; i < DC.Sets.Count; i++)
            {
                pPointSeries pSeriesSet = new pPointSeries(Convert.ToString(name + i));
                pSeriesSet.SetProperties(DC.Sets[i]);
                pSeriesSet.SetRangeChartType(M);
                pSeriesSet.SetChartLabels(DC.LeaderPostion, DC.HasLeader);
                pSeriesSet.SetNumericData(1);
                PointSeriesList.Add(pSeriesSet);
            }

            pControl.SetProperties(DC);
            pControl.SetSeries(PointSeriesList);
            pControl.SetAxisAppearance();
            pControl.SetThreeDView();

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pControl.Element, pControl, pControl.Type); }
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);

        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Pollen_Range1;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{157d9beb-43f8-414d-a22b-f1d246f09852}"); }
        }
    }
}