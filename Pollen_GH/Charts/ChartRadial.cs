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
    public class ChartRadial : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ChartRadial class.
        /// </summary>
        public ChartRadial()
          : base("Radial Chart", "Radial Chart", "---", "Aviary", "Charting & Data")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Data", "D", "---", GH_ParamAccess.item);
            pManager.AddNumberParameter("Radius", "R", "---", GH_ParamAccess.item, 50);
            pManager[1].Optional = true;
            
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

            var pControl = new pRadialChart(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                if (Elements[C] != null)
                {
                    WindObject = Elements[C];
                    Element = (pElement)WindObject.Element;
                    pControl = (pRadialChart)Element.PollenControl;
                }
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            IGH_Goo D = null;
            double R = 0;

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref R)) return;

            wObject W = new wObject();
            D.CastTo(out W);
            
            DataSetCollection DC = (DataSetCollection)W.Element;

            if (DC.TotalCustomFill == 0) { DC.SetDefaultPallet(Wind.Presets.wGradients.Metro, false, false); }
            if (DC.TotalCustomStroke == 0) { DC.SetDefaultStrokes(wStrokes.StrokeTypes.OffWhiteSolid); }
            if (DC.TotalCustomFont == 0) { DC.SetDefaultFonts(wFonts.ChartPoint); } 
            if (DC.TotalCustomMarker == 0) { DC.SetDefaultMarkers(wGradients.Metro, wMarker.MarkerType.Circle, false, DC.Sets.Count > 1); }
            if (DC.TotalCustomLabel == 0) { DC.SetDefaultLabels(new wLabel(wLabel.LabelPosition.Center, wLabel.LabelAlignment.Center, new wGraphic(wColors.Transparent))); }

            if (DC.TotalCustomTitles == 0) { DC.Graphics.FontObject = wFonts.AxisLabel; }

            List<pRadialSeries> RadialSeriesList = new List<pRadialSeries>();

            pControl.SetProperties(DC, R);
            pControl.SetPollenSeries(name);
            pControl.ForceRefresh();

            /*
            for (int i = 0; i < DC.Sets.Count; i++)
            {
                pRadialSeries pSeriesSet = new pRadialSeries(Convert.ToString(name + i));
                pSeriesSet.SetRadialSeries(DC.Sets[i]);
                RadialSeriesList.Add(pSeriesSet);
            }
            */

            //pControl.SetSeries(RadialSeriesList);
            //pControl.SetAxisAppearance();

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
                return Properties.Resources.Pollen_Pie1;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("14c7f71c-0aba-4b8f-8053-a71b59afdfda"); }
        }
    }
}