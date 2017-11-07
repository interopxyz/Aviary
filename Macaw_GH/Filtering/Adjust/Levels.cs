using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Containers;
using Wind.Types;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.AdjustColor;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Macaw.Filtering.Adjustments;

namespace Macaw_GH.Filtering.Adjust
{
    public class Levels : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Levels class.
        /// </summary>
        public Levels()
          : base("Levels", "Levels", "...", "Aviary", "Bitmap Edit")
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


            pManager.AddIntervalParameter("Red In", "RI", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Red Out", "RO", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[3].Optional = true;

            pManager.AddIntervalParameter("Green In", "GI", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[4].Optional = true;
            pManager.AddIntervalParameter("Green Out", "GO", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[5].Optional = true;

            pManager.AddIntervalParameter("Blue In", "BI", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[6].Optional = true;
            pManager.AddIntervalParameter("Blue Out", "BO", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[7].Optional = true;


            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Normalize", 0);
            param.AddNamedValue("Histogram", 1);
            param.AddNamedValue("Uniform", 2);
            param.AddNamedValue("Custom", 3);

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

            Interval Ra = new Interval(0, 255);
            Interval Ga = new Interval(0, 255);
            Interval Ba = new Interval(0, 255);

            Interval Rb = new Interval(0, 255);
            Interval Gb = new Interval(0, 255);
            Interval Bb = new Interval(0, 255);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref Ra)) return;
            if (!DA.GetData(3, ref Rb)) return;
            if (!DA.GetData(4, ref Ga)) return;
            if (!DA.GetData(5, ref Gb)) return;
            if (!DA.GetData(6, ref Ba)) return;
            if (!DA.GetData(7, ref Bb)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();


            switch (M)
            {
                case 0:
                    Filter = new mNormalizeExtents();
                    break;
                case 1:
                    Filter = new mNormalizeHistogram();
                    break;
                case 2:
                    Filter = new mAdjustLevelsGray(new wDomain(Ra.T0, Ra.T1), new wDomain(Rb.T0, Rb.T1));
                    break;
                case 3:
                    Filter = new mAdjustLevels(new wDomain(Ra.T0, Ra.T1), new wDomain(Ga.T0, Ga.T1), new wDomain(Ba.T0, Ba.T1), new wDomain(Rb.T0, Rb.T1), new wDomain(Gb.T0, Gb.T1), new wDomain(Bb.T0, Bb.T1));
                    break;
            }


            B = new mApply(A, Filter).ModifiedBitmap;

            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, B);
            DA.SetData(1, W);
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
                return Properties.Resources.Macaw_Filter_Levels;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("aca04898-d1c1-4e32-82e2-afff8c7cdaa3"); }
        }
    }
}