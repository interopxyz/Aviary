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
    public class ChartBar : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ChartBar class.
        /// </summary>
        public ChartBar()
          : base("Bar Chart", "Bar Chart", "---", "Aviary", "Charting & Data")
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
            param.AddNamedValue("Row Adjacent", 0);
            param.AddNamedValue("Row Stack", 1);
            param.AddNamedValue("Row Stretch", 2);
            param.AddNamedValue("Column Adjacent", 3);
            param.AddNamedValue("Column Stack", 4);
            param.AddNamedValue("Column Stretch", 5);
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

            var pControl = new pCartesianChart(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                if (Elements[C] != null)
                {
                    WindObject = Elements[C];
                    Element = (pElement)WindObject.Element;
                    pControl = (pCartesianChart)Element.PollenControl;
                }
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

            if (DC.TotalCustomFill == 0) { DC.SetDefaultPallet(wGradients.GradientTypes.Metro, false, DC.Sets.Count > 1); }
            if (DC.TotalCustomFont == 0) { DC.SetDefaultFonts(new wFonts(wFonts.FontTypes.ChartPoint).Font); }
            if (DC.TotalCustomMarker == 0) { DC.SetDefaultMarkers(wGradients.GradientTypes.Transparent, wMarker.MarkerType.None, false, DC.Sets.Count > 1); }
            if (DC.TotalCustomStroke == 0) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.Transparent); }

            List<pCartesianSeries> PointSeriesList = new List<pCartesianSeries>();

            pCartesianSeries.SeriesChartType[] T ={
                pCartesianSeries.SeriesChartType.BarAdjacent,
                pCartesianSeries.SeriesChartType.BarStack,
                pCartesianSeries.SeriesChartType.BarStack100,
                pCartesianSeries.SeriesChartType.ColumnAdjacent,
                pCartesianSeries.SeriesChartType.ColumnStack,
                pCartesianSeries.SeriesChartType.ColumnStack100};



            for (int i = 0; i < DC.Sets.Count; i++)
            {
                pCartesianSeries pSeriesSet = new pCartesianSeries(Convert.ToString(name + i));
                pSeriesSet.SetBarSeries(DC.Sets[i], T[M]);
                pSeriesSet.SetSeriesProperties();
                PointSeriesList.Add(pSeriesSet);
            }

            pControl.SetProperties(DC);
            if ((M == 0) || (M == 3)) { pControl.SetSeries(PointSeriesList); } else { pControl.SetSequence(PointSeriesList); }

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pControl.Element, pControl, pControl.Type); }
            WindObject = new wObject(Element, "Pollen", Element.Type);
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
            get { return GH_Exposure.quarternary; }
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
            get { return new Guid("067fb8e4-9cc2-404a-b8d5-26f7d6972276"); }
        }
    }
}