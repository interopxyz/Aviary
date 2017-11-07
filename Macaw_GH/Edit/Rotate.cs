using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Editing.Rotation;

namespace Macaw_GH.Edit
{
    public class Rotate : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Rotate class.
        /// </summary>
        public Rotate()
          : base("Rotate", "Rotate", "---", "Aviary", "Bitmap Edit")
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
            pManager.AddIntegerParameter("Angle", "A", "...", GH_ParamAccess.item, 90);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Fit", "F", "...", GH_ParamAccess.item, true);
            pManager[3].Optional = true;
            pManager.AddColourParameter("Color", "C", "...", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Bicubic", 0);
            param.AddNamedValue("Bilinear", 1);
            param.AddNamedValue("Neighbor", 2);
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
            int R = 90;
            bool F = true;
            Color X = Color.Black;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref R)) return;
            if (!DA.GetData(3, ref F)) return;
            if (!DA.GetData(4, ref X)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mRotateBicubic(R,F,X);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
                case 1:

                    Filter = new mRotateBilinear(R, F, X);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
                case 2:

                    Filter = new mRotateNearistNeighbor(R, F, X);
                    B = new mApply(A, Filter).ModifiedBitmap;
                    break;
            }


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
                return Properties.Resources.Macaw_Edit_Rotate;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("17ece7c9-ecc5-44ba-99e9-cfd842097376"); }
        }
    }
}