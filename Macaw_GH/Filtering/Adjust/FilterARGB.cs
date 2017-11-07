using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Wind.Types;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.FilterColor;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Adjust
{
    public class Filter : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Filter class.
        /// </summary>
        public Filter()
          : base("Filter ARGB", "ARGB", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Red", "R", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Green", "G", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Blue", "B", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[4].Optional = true;


            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Channel", 0);
            param.AddNamedValue("Color", 1);
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
            int M = 0;
            Interval Rc = new Interval(0,255);
            Interval Gc = new Interval(100, 255);
            Interval Bc = new Interval(100, 255);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref Rc)) return;
            if (!DA.GetData(3, ref Gc)) return;
            if (!DA.GetData(4, ref Bc)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mFilterARGBChannel(new wDomain(Rc.T0,Rc.T1), new wDomain(Gc.T0, Gc.T1), new wDomain(Bc.T0, Bc.T1));
                    break;
                case 1:
                    Filter = new mFilterARGBColor(new wDomain(Rc.T0, Rc.T1), new wDomain(Gc.T0, Gc.T1), new wDomain(Bc.T0, Bc.T1));
                    break;
            }

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
                return Properties.Resources.Macaw_Channels_ARGB;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ad802f15-70c0-4836-84a9-7c68f7dd98c9"); }
        }
    }
}