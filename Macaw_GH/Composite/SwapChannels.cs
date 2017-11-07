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
        /// <summary>
        /// Initializes a new instance of the SwapChannels class.
        /// </summary>
        public SwapChannels()
          : base("Swap Channels", "Swap", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddIntegerParameter("Alpha", "Ac", "...", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Red", "Rc", "...", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Green", "Gc", "...", GH_ParamAccess.item, 2);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Blue", "Bc", "...", GH_ParamAccess.item, 3);
            pManager[4].Optional = true;

            Param_Integer paramA = (Param_Integer)Params.Input[1];
            paramA.AddNamedValue("Alpha", 0);
            paramA.AddNamedValue("Red", 1);
            paramA.AddNamedValue("Green", 2);
            paramA.AddNamedValue("Blue", 3);
            paramA.AddNamedValue("Hue", 4);
            paramA.AddNamedValue("Saturation", 5);
            paramA.AddNamedValue("Luminance", 6);

            Param_Integer paramR = (Param_Integer)Params.Input[2];
            paramR.AddNamedValue("Alpha", 0);
            paramR.AddNamedValue("Red", 1);
            paramR.AddNamedValue("Green", 2);
            paramR.AddNamedValue("Blue", 3);
            paramR.AddNamedValue("Hue", 4);
            paramR.AddNamedValue("Saturation", 5);
            paramR.AddNamedValue("Luminance", 6);

            Param_Integer paramG = (Param_Integer)Params.Input[3];
            paramG.AddNamedValue("Alpha", 0);
            paramG.AddNamedValue("Red", 1);
            paramG.AddNamedValue("Green", 2);
            paramG.AddNamedValue("Blue", 3);
            paramG.AddNamedValue("Hue", 4);
            paramG.AddNamedValue("Saturation", 5);
            paramG.AddNamedValue("Luminance", 6);

            Param_Integer paramB = (Param_Integer)Params.Input[4];
            paramB.AddNamedValue("Alpha", 0);
            paramB.AddNamedValue("Red", 1);
            paramB.AddNamedValue("Green", 2);
            paramB.AddNamedValue("Blue", 3);
            paramB.AddNamedValue("Hue", 4);
            paramB.AddNamedValue("Saturation", 5);
            paramB.AddNamedValue("Luminance", 6);

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

            Bitmap X = null;
            if (Z != null) { Z.CastTo(out X); }
            Bitmap Y = new Bitmap(X);

            mSwapARGB Swap = new mSwapARGB(X,A,R,G,B);
            
            DA.SetData(0, new Bitmap(Swap.ModifiedBitmap));
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