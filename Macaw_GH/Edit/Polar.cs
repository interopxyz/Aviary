using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Wind.Containers;
using Wind.Types;
using Grasshopper.Kernel.Parameters;
using Macaw.Build;
using Macaw.Filtering;
using Macaw.Editing.Morphs;

namespace Macaw_GH.Edit
{
    public class Polar : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Polar class.
        /// </summary>
        public Polar()
          : base("Polar", "Polar", "...", "Aviary", "Image Edit")
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

            pManager.AddIntegerParameter("Mode", "M", "...", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Offset", "O", "...", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Depth", "D", "...", GH_ParamAccess.item, 1);
            pManager[3].Optional = true;
            pManager.AddIntervalParameter("Size", "S", "...", GH_ParamAccess.item, new Interval(800, 600));
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("ToPolar",0);
            param.AddNamedValue("ToRectangular", 1);
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
            int M = 0;
            IGH_Goo X = null;
            double R = 0;
            double D = 1;
            Interval S = new Interval(800, 600);

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref R)) return;
            if (!DA.GetData(3, ref D)) return;
            if (!DA.GetData(4, ref S)) return;

            Bitmap A = null;
            if (X != null) { X.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            wDomain Y = new wDomain(S.T0,S.T1);

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mPolarToPolar(R,D,Y);
                    break;
                case 1:
                    Filter = new mPolarToRect(R, D, Y);
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
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Edit_Polar;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("76d43e49-3fa2-4575-a985-5f8c8658c46e"); }
        }
    }
}