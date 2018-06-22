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
        private int ModeIndex = 0;
        private string[] modes = { "Bayer", "Ordered", "Burkes", "Floyd Steinberg", "Jarvis Judice Ninke", "Sierra", "Stucki", "Carry" };
        private int[] V = { 0, 0, 32, 16, 48, 32, 42, 50 };
        /// <summary>
        /// Initializes a new instance of the Dither class.
        /// </summary>
        public Dither()
              : base("Dither", "Dither", "---", "Aviary", "Bitmap Edit")
        {
            UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.SetPersistentData(new GH_ObjectWrapper(new Bitmap(10, 10)));

            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Threshold", "T", "...", GH_ParamAccess.item, -1);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue(modes[0], 0);
            param.AddNamedValue(modes[1], 1);
            param.AddNamedValue(modes[2], 2);
            param.AddNamedValue(modes[3], 3);
            param.AddNamedValue(modes[4], 4);
            param.AddNamedValue(modes[5], 5);
            param.AddNamedValue(modes[6], 6);
            param.AddNamedValue(modes[7], 7);
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
            double P = -1;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref P)) return;

            if (M != ModeIndex)
            {
                ModeIndex = M;
                UpdateMessage();
                if (M > 1)
                {
                    SetParameter(2, "Threshold", "T", ""+V[M]+"");
                }
                else
                {
                    SetParameter(2, "Not Used", "-", "Not used by this filter");
                }
            }

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }

            mFilter Filter = new mFilter();

            switch (ModeIndex)
            {
                case 0:
                    Filter = new mDitherBayer();
                    break;
                case 1:
                    Filter = new mDitherOrdered();
                    break;
                case 2:
                    if (P < 0) { P = 32; }
                    Filter = new mDitherBurkes((byte)P);
                    break;
                case 3:
                    if (P < 0) { P = 16; }
                    Filter = new mDitherFloydSteinberg((byte)P);
                    break;
                case 4:
                    if (P < 0) { P = 48; }
                    Filter = new mDitherJarvisJudiceNinke((byte)P);
                    break;
                case 5:
                    if (P < 0) { P = 32; }
                    Filter = new mDitherSierra((byte)P);
                    break;
                case 6:
                    if (P < 0) { P = 42; }
                    Filter = new mDitherStucki((byte)P);
                    break;
                case 7:
                    if (P < 0) { P = 50; }
                    Filter = new mDitherThresholdCarry((byte)P);
                    break;
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

        private void SetParameter(int index, string Name, string NickName, string Description)
        {
            Param_Number param = (Param_Number)Params.Input[index];
            param.Name = Name;
            param.NickName = NickName;
            param.Description = Description;
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