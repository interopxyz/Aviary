using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
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
    public class ChartGantt : GH_Component
  {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ChartGantt class.
        /// </summary>
        public ChartGantt()
      : base("Gantt Chart", "Gantt Chart", "---", "Aviary", "Charting & Data")
    {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "---", GH_ParamAccess.item);

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

            if (!DA.GetData(0, ref D)) return;

            wObject W = new wObject();
            D.CastTo(out W);

            DataSetCollection DC = (DataSetCollection)W.Element;

            if (DC.TotalCustomFill == 0) { DC.SetDefaultPallet(wGradients.Metro, false, true); }
            if (DC.TotalCustomStroke == 0) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.Transparent); }
            if (DC.TotalCustomFont == 0) { DC.SetDefaultFonts(wFonts.ChartPointDark); }
            if (DC.TotalCustomMarker == 0) { DC.SetDefaultMarkers(wGradients.SolidTransparent, wMarker.MarkerType.None, false, false); }
            if (DC.TotalCustomLabel == 0) { DC.SetDefaultLabels(new wLabel(wLabel.LabelPosition.Center, wLabel.LabelAlignment.Center, new wGraphic(wColors.Transparent))); }

            List<pCartesianSeries> PointSeriesList = new List<pCartesianSeries>();
            
            pControl.SetProperties(DC);
            pControl.SetGanttChart();
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
                return Properties.Resources.Pollen_Gantt;
            }
        }
        
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
    {
      get { return new Guid("{e5920e15-fe74-4377-bee6-47ddd3f6baa5}"); }
    }
  }
}