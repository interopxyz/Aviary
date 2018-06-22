using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Filters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Extract
{
    public class ExtractChannel : GH_Component
    {

        public int ModeIndex = 0;
        private string[] modes = { "Blue", "Green", "Red", "Alpha", "Y", "Cr", "Cb" };

        /// <summary>
        /// Initializes a new instance of the Extract class.
        /// </summary>
        public ExtractChannel()
          : base("Get Channel", "Channel", "...", "Aviary", "Bitmap Edit")
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
            paramGen.SetPersistentData(new Bitmap(10, 10));

            pManager.AddIntegerParameter("Mode", "M", "...", GH_ParamAccess.item, 2);

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue(modes[3], 3);
            param.AddNamedValue(modes[2], 2);
            param.AddNamedValue(modes[1], 1);
            param.AddNamedValue(modes[0], 0);
            param.AddNamedValue(modes[5], 5);
            param.AddNamedValue(modes[6], 6);
            param.AddNamedValue(modes[4], 4);
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

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }
            mFilter Filter = new mFilter();


            if (M != ModeIndex)
            {
                ModeIndex = M;
                UpdateMessage();
            }

            if (M<4)
            {
                Filter = new mExtractARGBChannel((short)M);
            }
            else
            {
                Filter = new mExtractYCbCrChannel((short)(M-4));
            }

            Bitmap B = new mApply(A, Filter).ModifiedBitmap;
            wObject W = new wObject(Filter, "Macaw", Filter.Type);

            DA.SetData(0, B);
            DA.SetData(1, W);
        }

        private void UpdateMessage()
        {
            Message = modes[ModeIndex];
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
                return Properties.Resources.Macaw_Channels_ARGB_Channel;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5697b499-c754-42da-a46a-b875299e8f2a"); }
        }
    }
}