using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.FilterColor;

namespace Macaw_GH.Composite
{
    public class SwapChannels : GH_Component
    {
        int Am = 0;
        int Rm = 1;
        int Gm = 2;
        int Bm = 3;
        private string[] modes = { "Alpha", "Red", "Green", "Blue", "Hue", "Saturation", "Luminance" };

        /// <summary>
        /// Initializes a new instance of the SwapChannels class.
        /// </summary>
        public SwapChannels()
          : base("Swap Channels", "Swap", "---", "Aviary", "Bitmap Edit")
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
            paramGen.SetPersistentData(new Bitmap(10, 10));

            pManager.AddIntegerParameter("Alpha", "Ac", "...", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Red", "Rc", "...", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Green", "Gc", "...", GH_ParamAccess.item, 2);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Blue", "Bc", "...", GH_ParamAccess.item, 3);
            pManager[4].Optional = true;

            Param_Integer paramA = (Param_Integer)Params.Input[1];
            paramA.AddNamedValue(modes[0], 0);
            paramA.AddNamedValue(modes[1], 1);
            paramA.AddNamedValue(modes[2], 2);
            paramA.AddNamedValue(modes[3], 3);
            paramA.AddNamedValue(modes[4], 4);
            paramA.AddNamedValue(modes[5], 5);
            paramA.AddNamedValue(modes[6], 6);

            Param_Integer paramR = (Param_Integer)Params.Input[2];
            paramR.AddNamedValue(modes[0], 0);
            paramR.AddNamedValue(modes[1], 1);
            paramR.AddNamedValue(modes[2], 2);
            paramR.AddNamedValue(modes[3], 3);
            paramR.AddNamedValue(modes[4], 4);
            paramR.AddNamedValue(modes[5], 5);
            paramR.AddNamedValue(modes[6], 6);

            Param_Integer paramG = (Param_Integer)Params.Input[3];
            paramG.AddNamedValue(modes[0], 0);
            paramG.AddNamedValue(modes[1], 1);
            paramG.AddNamedValue(modes[2], 2);
            paramG.AddNamedValue(modes[3], 3);
            paramG.AddNamedValue(modes[4], 4);
            paramG.AddNamedValue(modes[5], 5);
            paramG.AddNamedValue(modes[6], 6);

            Param_Integer paramB = (Param_Integer)Params.Input[4];
            paramB.AddNamedValue(modes[0], 0);
            paramB.AddNamedValue(modes[1], 1);
            paramB.AddNamedValue(modes[2], 2);
            paramB.AddNamedValue(modes[3], 3);
            paramB.AddNamedValue(modes[4], 4);
            paramB.AddNamedValue(modes[5], 5);
            paramB.AddNamedValue(modes[6], 6);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo Z = null;
            int A = 0;
            int R = 1;
            int G = 2;
            int B = 3;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref A)) return;
            if (!DA.GetData(2, ref R)) return;
            if (!DA.GetData(3, ref G)) return;
            if (!DA.GetData(4, ref B)) return;

            if ((A != Am) || (R != Rm) || (G != Gm) || (B != Bm))
            {
                Am = A;
                Rm = R;
                Gm = G;
                Bm = B;

                UpdateMessage();
            }

            Bitmap X = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out X); }

            mSwapARGB Swap = new mSwapARGB(X,A,R,G,B);
            
            DA.SetData(0, new Bitmap(Swap.ModifiedBitmap));
        }

        private void UpdateMessage()
        {
            string name = "";
            if (Am != 0) { name += ("Alpha = " + modes[Am] + Environment.NewLine); }
            if (Rm != 1) { name += ("Red = " + modes[Rm] + Environment.NewLine); }
            if (Gm != 2) { name += ("Green = " + modes[Gm] + Environment.NewLine); }
            if (Bm != 3) { name += ("Blue = " + modes[Bm]); }

            Message = name;
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
                return Properties.Resources.Macaw_Channels_Swap;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("74f01ca6-40d9-448f-8b7e-8b934b597f1b"); }
        }
    }
}