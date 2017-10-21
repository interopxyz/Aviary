using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.AdjustColor;
using Wind.Containers;
using Wind.Types;

namespace Macaw_GH.Filtering.Adjust
{
    public class Curves : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Curves class.
        /// </summary>
        public Curves()
          : base("Curves", "Curves", "...", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntervalParameter("Red In", "RI", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[0].Optional = true;
            pManager.AddIntervalParameter("Red Out", "RO", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[1].Optional = true;

            pManager.AddIntervalParameter("Green In", "GI", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Green Out", "GO", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[3].Optional = true;

            pManager.AddIntervalParameter("Blue In", "BI", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[4].Optional = true;
            pManager.AddIntervalParameter("Blue Out", "BO", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[5].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables

            Interval Ra = new Interval(0, 255);
            Interval Ga = new Interval(0, 255);
            Interval Ba = new Interval(0, 255);

            Interval Rb = new Interval(0, 255);
            Interval Gb = new Interval(0, 255);
            Interval Bb = new Interval(0, 255);

            // Access the input parameters 
            if (!DA.GetData(0, ref Ra)) return;
            if (!DA.GetData(1, ref Rb)) return;
            if (!DA.GetData(2, ref Ga)) return;
            if (!DA.GetData(3, ref Gb)) return;
            if (!DA.GetData(4, ref Ba)) return;
            if (!DA.GetData(5, ref Bb)) return;

            mFilter Filter = new mFilter();

            Filter = new mAdjustLevels(new wDomain(Ra.T0, Ra.T1), new wDomain(Ga.T0, Ga.T1), new wDomain(Ba.T0, Ba.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Gb.T0, Gb.T1), new wDomain(Bb.T0, Bb.T1));


            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Adjust_Curves;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("06ab0843-d516-4bf7-a23c-0367bf44948f"); }
        }
    }
}