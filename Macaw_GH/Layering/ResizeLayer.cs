using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Macaw.Compiling;
using Grasshopper.Kernel.Parameters;

namespace Macaw_GH.Layering
{
    public class ResizeLayer : GH_Component
    {

        public int ModeIndex = 2;
        private string[] modes = { "Width", "Height", "Fill", "Uniform", "UniformFill" };

        public int TypeIndex = 0;
        private string[] types = { "Unspecified", "LowQuality", "HighQuality", "NearestNeighbor" };

        /// <summary>
        /// Initializes a new instance of the ResizeLayer class.
        /// </summary>
        public ResizeLayer()
          : base("Resize Layer", "Resize", "---", "Aviary", "Bitmap Build")
        {
            UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Fitting Mode", "M", "---", GH_ParamAccess.item, 2);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Scaling Mode", "S", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "---", GH_ParamAccess.item, 0);
            pManager[4].Optional = true;

            Param_Integer paramA = (Param_Integer)Params.Input[1];
            paramA.AddNamedValue(modes[0], 0);
            paramA.AddNamedValue(modes[1], 1);
            paramA.AddNamedValue(modes[2], 2);
            paramA.AddNamedValue(modes[3], 3);
            paramA.AddNamedValue(modes[4], 4);

            Param_Integer paramB = (Param_Integer)Params.Input[2];
            paramB.AddNamedValue(types[0], 0);
            paramB.AddNamedValue(types[1], 1);
            paramB.AddNamedValue(types[2], 2);
            paramB.AddNamedValue(types[3], 3);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo X = null;
            int Xsize = 0;
            int Ysize = 0;
            int F = 0;
            int S = 0;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref F)) return;
            if (!DA.GetData(2, ref S)) return;
            if (!DA.GetData(3, ref Xsize)) return;
            if (!DA.GetData(4, ref Ysize)) return;

            if (F != ModeIndex)
            {
                ModeIndex = F;
                UpdateMessage();
            }

            if (S != TypeIndex)
            {
                TypeIndex = S;
                UpdateMessage();
            }

            wObject Z = new wObject();
            if (X != null) { X.CastTo(out Z); }
            mLayer L = new mLayer((mLayer)Z.Element);

            L.SetSizing(F, S, Xsize, Ysize);

            wObject W = new wObject(L, "Macaw", L.Type);


            DA.SetData(0, W);
        }

        private void UpdateMessage()
        {
            Message = modes[ModeIndex] + " | "+types[TypeIndex];
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0581155f-4e39-4f15-90cd-3cd55c13de59"); }
        }
    }
}