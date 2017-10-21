using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Wind.Types;
using Macaw.Filtering;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Filtering.Objects.Figures;
using Macaw.Build;

namespace Macaw_GH.Filtering.Object
{
    public class Blob : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Blob class.
        /// </summary>
        public Blob()
          : base("Blob", "Blob", "---", "Aviary", "Macaw")
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
            pManager.AddIntervalParameter("Width", "W", "---", GH_ParamAccess.item, new Interval(50,1000));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Height", "H", "---", GH_ParamAccess.item, new Interval(50, 1000));
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Unique", 0);
            param.AddNamedValue("Filter", 1);
            param.AddNamedValue("Corners", 2);
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
            Interval U = new Interval(50, 1000);
            Interval V = new Interval(50, 1000);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref U)) return;
            if (!DA.GetData(3, ref V)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            wDomain X = new wDomain(U.T0,U.T1);
            wDomain Y = new wDomain(V.T0, V.T1);

            switch (M)
            {
                case 0:
                    Filter = new mFigureUnique(X, Y);
                    break;
                case 1:
                    Filter = new mFigureFilter(X, Y);
                    break;
                case 2:
                   Filter = new mFigureCorners(Color.Red);
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
            get { return GH_Exposure.secondary; }
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
            get { return new Guid("5c63c966-679f-4bef-a918-67502d25044a"); }
        }
    }
}