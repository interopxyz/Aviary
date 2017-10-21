using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Types;
using Wind.Containers;
using Macaw.Filtering.Adjustments.FilterColor;
using Macaw.Filtering;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Adjust
{
    public class FilterHSLLinear : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FilterLinearHSL class.
        /// </summary>
        public FilterHSLLinear()
          : base("Filter Linear HSL", "HSL Line", "...", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new Bitmap(100, 100)));

            pManager.AddIntervalParameter("Saturation In", "SI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Saturation Out", "SO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Luminance", "LI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Luminance", "LO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[4].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo Z = null;
            Interval SA = new Interval(0.0, 1.0);
            Interval SB = new Interval(0.0, 1.0);
            Interval LA = new Interval(0.0, 1.0);
            Interval LB = new Interval(0.0, 1.0);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref SA)) return;
            if (!DA.GetData(2, ref SB)) return;
            if (!DA.GetData(3, ref LA)) return;
            if (!DA.GetData(4, ref LB)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            Filter = new mFilterHSLLinear(new wDomain(SA.T0, SA.T1), new wDomain(SB.T0, SB.T1), new wDomain(LA.T0, LA.T1), new wDomain(LB.T0, LB.T1));

            B = new mApply(A, Filter).ModifiedBitmap;


            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Channels_HSL;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0904a726-732c-44ca-84ae-a09d76c948a8"); }
        }
    }
}