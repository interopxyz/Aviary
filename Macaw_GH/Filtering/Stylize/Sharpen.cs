using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Stylized;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Macaw.Compiling;
using Macaw.Compiling.Modifiers;

namespace Macaw_GH.Filtering.Stylize
{
    public class Sharpen : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Sharpen class.
        /// </summary>
        public Sharpen()
          : base("Sharpen", "Sharpen", "---", "Aviary", "Bitmap Edit")
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
            pManager.AddNumberParameter("Sigma", "S", "---", GH_ParamAccess.item, 1.4);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Size", "R", "---", GH_ParamAccess.item, 10);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Simple", 0);
            param.AddNamedValue("Gaussian", 1);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Modifier", "M", "---", GH_ParamAccess.item);
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
            double S = 4;
            int R = 10;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref S)) return;
            if (!DA.GetData(3, ref R)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();
            mModifiers Modifier = new mModifiers();

            switch (M)
            {
                case 0:
                    Filter = new mSharpenSimple();
                    break;
                case 1:
                    Filter = new mSharpenGaussian(S, R);
                    break;
            }
            Modifier = new mModifyUnsharpen((int)S, R);
            B = new mApply(A, Filter).ModifiedBitmap;

            wObject W = new wObject(Filter, "Macaw", Filter.Type);
            wObject V = new wObject(Modifier, "Macaw", Modifier.Type);


            DA.SetData(0, B);
            DA.SetData(1, W);
            DA.SetData(2, V);
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
                return Properties.Resources.Macaw_Filter_Sharpen;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3e57c0f4-8106-4ff9-94c2-a91e8dec3a8d"); }
        }
    }
}