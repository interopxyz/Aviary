using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Stylized;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Stylize
{
    public class Dither : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Dither class.
        /// </summary>
        public Dither()
          : base("Dither", "Dither", "---", "Aviary", "Image Edit")
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
            pManager.AddNumberParameter("Parameter", "P", "...", GH_ParamAccess.item, 50.0);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Bayer", 0);
            param.AddNamedValue("Burkes", 1);
            param.AddNamedValue("Floyd Steinberg", 2);
            param.AddNamedValue("Jarvis Judice Ninke", 3);
            param.AddNamedValue("Ordered", 4);
            param.AddNamedValue("Sierra", 5);
            param.AddNamedValue("Stucki", 6);
            param.AddNamedValue("Carry", 7);
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
            double P = 50.0;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref P)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();
            
            switch (M)
            {
                case 0:
                    Filter = new mDitherBayer();
                    break;
                case 1:
                    Filter = new mDitherBurkes();
                    break;
                case 2:
                    Filter = new mDitherFloydSteinberg();
                    break;
                case 3:
                    Filter = new mDitherJarvisJudiceNinke();
                    break;
                case 4:
                    Filter = new mDitherOrdered();
                    break;
                case 5:
                    Filter = new mDitherSierra();
                    break;
                case 6:
                    Filter = new mDitherStucki();
                    break;
                case 7:
                    Filter = new mDitherThresholdCarry((byte)P);
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
                return Properties.Resources.Macaw_Filter_Dither;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("007c64ed-5cc5-42ce-abe5-4000e6b7e82a"); }
        }
    }
}