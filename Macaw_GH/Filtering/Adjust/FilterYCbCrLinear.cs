using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Types;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.FilterColor;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Adjust
{
    public class FilterYCbCrLinear : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FilterYCbCrLinear class.
        /// </summary>
        public FilterYCbCrLinear()
          : base("Filter Linear YCbCr", "YCbCr Line", "...", "Aviary", "Bitmap Edit")
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

            pManager.AddIntervalParameter("Luminance In", "YI", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Luminance Out", "YO", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Chrominance Blue In", "BI", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Chrominance Blue Out", "BO", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));
            pManager[4].Optional = true;
            pManager.AddIntervalParameter("Chrominance Red In", "RI", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));
            pManager[5].Optional = true;
            pManager.AddIntervalParameter("Chrominance Red Out", "RO", "---", GH_ParamAccess.item, new Interval(-0.5, 0.5));
            pManager[6].Optional = true;

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
            Interval YI = new Interval(0.0, 1.0);
            Interval YO = new Interval(0.0, 1.0);
            Interval BI = new Interval(-0.5, 0.5);
            Interval BO = new Interval(-0.5, 0.5);
            Interval RI = new Interval(-0.5, 0.5);
            Interval RO = new Interval(-0.5, 0.5);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref YI)) return;
            if (!DA.GetData(2, ref YO)) return;
            if (!DA.GetData(3, ref BI)) return;
            if (!DA.GetData(4, ref BO)) return;
            if (!DA.GetData(5, ref RI)) return;
            if (!DA.GetData(6, ref RO)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            Filter = new mFilterYCbCrLinear(new wDomain(YI.T0, YI.T1), new wDomain(BI.T0, BI.T1), new wDomain(RI.T0, RI.T1), new wDomain(YO.T0, YO.T1), new wDomain(BO.T0, BO.T1), new wDomain(RO.T0, RO.T1));

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
                return Properties.Resources.Macaw_Channels_YCbCr;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("19c3392b-db8e-463e-8af2-19eebf475a0f"); }
        }
    }
}