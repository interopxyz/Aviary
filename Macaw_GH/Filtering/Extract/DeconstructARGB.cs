using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;

namespace Macaw_GH.Filtering.Extract
{
    public class DeconstructARGB : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructARGB class.
        /// </summary>
        public DeconstructARGB()
          : base("ARGB Values", "ARGBs", "---", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Alpha", "A", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Red", "R", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Green", "G", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Blue", "B", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo Z = null;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }

            mGetARGB GP = new mGetARGB(A);

            DA.SetDataList(0, GP.A);
            DA.SetDataList(1, GP.R);
            DA.SetDataList(2, GP.G);
            DA.SetDataList(3, GP.B);
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
                return Properties.Resources.Macaw_Deconstruct_RGB;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f21b4117-c50a-4df7-a5bc-ba937c1eb9ee"); }
        }
    }
}