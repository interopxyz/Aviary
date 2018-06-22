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
    public class ChartLine : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ChartLine class.
        /// </summary>
        public ChartLine()
          : base("Line Chart", "Line Chart", "---", "Aviary", "Charting & Data")
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
            param.AddNamedValue("Line", 1);
            param.AddNamedValue("StepLine", 2);
            param.AddNamedValue("Spline", 3);
            param.AddNamedValue("Area", 4);
            param.AddNamedValue("Area Stack", 5);
            param.AddNamedValue("Area Stretch", 6);
            param.AddNamedValue("Spline Area", 7);
            param.AddNamedValue("Spline Area Stack", 8);
            param.AddNamedValue("Spline Area Stretch", 9);
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

            if (DC.TotalCustomFill == 0) { if (M > 3) { DC.SetDefaultPallet(wGradients.Metro, false, DC.Sets.Count > 1); } else { DC.SetDefaultPallet(wGradients.SolidTransparent, false, DC.Sets.Count > 1); } }
            if (DC.TotalCustomStroke == 0) { if ((M > 3) || (M == 0)) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.Transparent); } else { DC.SetDefaultStrokes(wStrokes.StrokeTypes.LineChart, wGradients.Metro, false, true); } }
            if (DC.TotalCustomMarker == 0) { if (M == 0) { DC.SetDefaultMarkers(wGradients.Metro, wMarker.MarkerType.Circle, false, DC.Sets.Count > 1); } else { DC.SetDefaultMarkers(wGradients.SolidTransparent, wMarker.MarkerType.None, false, DC.Sets.Count > 1); } }
            if (DC.TotalCustomFont == 0) { if ((M == 6) || (M == 9)) { DC.SetDefaultFonts(wFonts.ChartPoint); } else { DC.SetDefaultFonts(wFonts.ChartPointDark); } }
            if (DC.TotalCustomLabel == 0) { if ((M == 0) || (M == 3)) { DC.SetDefaultLabels(new wLabel(wLabel.LabelPosition.Center, wLabel.LabelAlignment.Center, new wGraphic(wColors.Transparent))); } else { DC.SetDefaultLabels(new wLabel(wLabel.LabelPosition.Top, wLabel.LabelAlignment.Perp, new wGraphic(wColors.Transparent))); } }
            
            List<pCartesianSeries> PointSeriesList = new List<pCartesianSeries>();

            pControl.SetProperties(DC);
            switch (M)
            {
                case 0://Point
                    pControl.SetPointChart();
                    break;
                case 1://Line
                    pControl.SetLineChart(0);
                    break;
                case 2://StepLine
                    pControl.SetStepLineChart();
                    break;
                case 3://Spline
                    pControl.SetLineChart(3);
                    break;
                case 4:
                    pControl.SetAreaChart(0);
                    break;
                case 5:
                    pControl.SetStackAreaChart(false,0);
                    break;
                case 6:
                    pControl.SetStackAreaChart(true,0);
                    break;
                case 7:
                    pControl.SetAreaChart(3);
                    break;
                case 8:
                    pControl.SetStackAreaChart(false, 3);
                    break;
                case 9:
                    pControl.SetStackAreaChart(true, 3);
                    break;
            }
            pControl.ForceRefresh();
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
                return Properties.Resources.Pollen_Line;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f4b9ff91-6bd0-4d5a-bdf6-0771cabb8795"); }
        }
    }
}