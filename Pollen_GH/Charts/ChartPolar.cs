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
using GH_IO.Serialization;
using Wind.Presets;

namespace Pollen_GH.Charts
{
    public class PolarChart : GH_Component
    {
        public bool modeStatus = false;

        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the RadialChart class.
        /// </summary>
        public PolarChart()
          : base("Polar Chart", "Polar Chart", "---", "Aviary", "Charting & Data")
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
            param.AddNamedValue("Polar", 0);
            param.AddNamedValue("Radar", 1);

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

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref M)) return;
            
            wObject W = new wObject();
            D.CastTo(out W);

            DataSetCollection DC = (DataSetCollection)W.Element;

            if (DC.TotalCustomFill == 0) { DC.SetDefaultPallet(wGradients.Metro, false, DC.Sets.Count > 1);}
            if (DC.TotalCustomStroke == 0) { if (M == 1) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.Transparent); } else { DC.SetDefaultStrokes(wStrokes.StrokeTypes.LineChart, wGradients.Metro, false, true); } }
            if (DC.TotalCustomMarker == 0) { if (M == 0) { DC.SetDefaultMarkers(wGradients.Metro, wMarker.MarkerType.Circle, false, DC.Sets.Count > 1); } else { DC.SetDefaultMarkers(wGradients.SolidTransparent, wMarker.MarkerType.None, false, DC.Sets.Count > 1); } }
            if (DC.TotalCustomFont == 0) { DC.SetDefaultFonts(wFonts.ChartPointDark); }
            if (DC.TotalCustomLabel == 0) { DC.SetDefaultLabels(new wLabel(wLabel.LabelPosition.Center, wLabel.LabelAlignment.None, new wGraphic(wColors.Transparent))); }

            if (DC.TotalCustomTitles == 0) { DC.Graphics.FontObject = wFonts.AxisLabel; }

            List<pPointSeries> PointSeriesList = new List<pPointSeries>();

            int T = 0;

            double X = DC.Sets.Count;
            double Y = 99.0 / X;

            if(!DC.Axes.Enabled)
            {
                DC.Axes.AxisX.Enabled = true;
                DC.Axes.AxisX.HasLabel = false;
                DC.Axes.AxisY.Enabled = true;
                DC.Axes.AxisY.HasLabel = false;
            }

            pControl.SetProperties(DC);

            for (int i = 0; i < DC.Sets.Count; i++)
            {
                pPointSeries pSeriesSet = new pPointSeries(DC.Sets[i].Title);
                pSeriesSet.SetProperties(DC.Sets[i]);
                pSeriesSet.SetRadialChartType(M);
                pSeriesSet.SetChartLabels(DC.Label);
                pSeriesSet.SetNumericData(4);
                PointSeriesList.Add(pSeriesSet);
            }
            
            pControl.SetSeries(PointSeriesList);
            pControl.SetAxisScale();
            pControl.SetAxisAppearance();
            if (M == 0) { pControl.SetXaxis(new wDomain(0, PointSeriesList[0].DataList.Count - 1)); }

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pControl.Element, pControl, pControl.Type); }
            WindObject = new wObject(Element, "Pollen", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetBoolean("Mode", modeStatus);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            modeStatus = reader.GetBoolean("Mode");

            return base.Read(reader);
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
                return Properties.Resources.Pollen_Polar;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{89948bac-a022-4813-9c95-53f8b0c6b24e}"); }
        }
    }
}