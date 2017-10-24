using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Objects.Erosions;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Macaw.Filtering.Stylized;
using Macaw.Filtering.Objects.Figures;
using Rhino.Geometry;
using Wind.Types;

namespace Macaw_GH.Filtering.Figure
{
    public class Figure : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Erosion class.
        /// </summary>
        public Figure()
          : base("Figure", "Figure", "...", "Aviary", "Image Edit")
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
            pManager.AddIntervalParameter("Width", "W", "---", GH_ParamAccess.item, new Interval(0,20));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Height", "H", "---", GH_ParamAccess.item, new Interval(0, 20));
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Simple", 0);
            param.AddNamedValue("Dilation", 1);
            param.AddNamedValue("Skeleton", 2);
            param.AddNamedValue("Fill", 3);
            param.AddNamedValue("Opening", 4);
            param.AddNamedValue("Closing", 5);
            param.AddNamedValue("Hat Top", 6);
            param.AddNamedValue("Hat Bottom", 7);
            param.AddNamedValue("Streak Vertical", 8);
            param.AddNamedValue("Streak Horizontal", 9);
            param.AddNamedValue("Unique", 10);
            param.AddNamedValue("Filter", 11);
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
            Interval X = new Interval(0, 20);
            Interval Y = new Interval(0, 20);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref X)) return;
            if (!DA.GetData(3, ref Y)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mErosionSimple();
                    break;
                case 1:
                    Filter = new mErosionDilation((int)X.T1,(int)Y.T1);
                    break;
                case 2:
                    Filter = new mErosionSkeleton();
                    break;
                case 3:
                    Filter = new mErosionFill((int)X.T1, (int)Y.T1);
                    break;
                case 4:
                    Filter = new mErosionOpening((int)X.T1, (int)Y.T1);
                    break;
                case 5:
                    Filter = new mErosionClosing();
                    break;
                case 6:
                    Filter = new mErosionHatTop();
                    break;
                case 7:
                    Filter = new mErosionHatBottom();
                    break;
                case 8:
                    Filter = new mStreakHorizontal((int)Y.T1);
                    break;
                case 9:
                    Filter = new mStreakVertical((int)X.T1);
                    break;
                case 10:
                    Filter = new mFigureUnique(new wDomain(X.T0,X.T1), new wDomain(Y.T0, Y.T1));
                    break;
                case 11:
                    Filter = new mFigureFilter(new wDomain(X.T0, X.T1), new wDomain(Y.T0, Y.T1));
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
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Filter_Extract;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7b0080e9-9aaa-4e81-afe5-894ac119f142"); }
        }
    }
}