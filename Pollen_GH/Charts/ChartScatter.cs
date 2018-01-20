using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using Wind.Utilities;
using Parrot.Containers;
using Pollen.Charts;
using Grasshopper.Kernel.Types;
using Pollen.Collections;
using Wind.Presets;

namespace Pollen_GH.Charts
{
    public class ChartScatter : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the RadialChart class.
        /// </summary>
        public ChartScatter()
          : base("Scatter Chart", "Scatter Chart", "---", "Aviary", "Charting & Data")
        {

        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Geometry", "G", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer param1 = (Param_Integer)Params.Input[1];
            param1.AddNamedValue("Scatter", 0);
            param1.AddNamedValue("Bubble", 1);

            Param_Integer param2 = (Param_Integer)Params.Input[2];
            param2.AddNamedValue("Circle", 0);
            param2.AddNamedValue("Square", 1);
            param2.AddNamedValue("Diamond", 2);
            param2.AddNamedValue("Triangle", 3);
            param2.AddNamedValue("Cross", 4);

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
            int M = 1;
            int S = 0;

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref S)) return;

            wObject W = new wObject();
            D.CastTo(out W);

            DataSetCollection DC = (DataSetCollection)W.Element;

            if (DC.TotalCustomFill == 0) { DC.SetDefaultPallet(wGradients.GradientTypes.Metro, false, DC.Sets.Count > 1); }
            if (DC.TotalCustomFont == 0) { DC.SetDefaultFonts(new wFonts(wFonts.FontTypes.ChartPoint).Font); }
            if (DC.TotalCustomMarker == 0){ DC.SetDefaultMarkers(wGradients.GradientTypes.Metro, wMarker.MarkerType.Circle, false, DC.Sets.Count > 1); }
            if (DC.TotalCustomStroke == 0) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.Transparent); }

            List<pCartesianSeries> PointSeriesList = new List<pCartesianSeries>();

            List<pCartesianSeries.SeriesChartType> Modes = new List<pCartesianSeries.SeriesChartType>{ pCartesianSeries.SeriesChartType.Scatter, pCartesianSeries.SeriesChartType.Bubble };

            for (int i = 0; i < DC.Sets.Count; i++)
            {
                pCartesianSeries pSeriesSet = new pCartesianSeries(Convert.ToString(name + i));
                pSeriesSet.SetScatterSeries(DC.Sets[i], Modes[M],S+1);
                pSeriesSet.SetSeriesProperties();
                PointSeriesList.Add(pSeriesSet);
            }

            pControl.SetProperties(DC);
            pControl.SetSeries(PointSeriesList);
            pControl.SetAxisAppearance();

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
                return Properties.Resources.Pollen_Scatter;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{8da8ac70-f448-469e-bf00-c9f0bca14758}"); }
        }
    }
}