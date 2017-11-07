using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;

namespace Macaw_GH.Filtering.Extract
{
    public class DeconstructHSL : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructHSL class.
        /// </summary>
        public DeconstructHSL()
          : base("HSL Values", "HSLs", "---", "Aviary", "Bitmap Build")
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
            pManager.AddIntegerParameter("Hue", "H", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Saturation", "S", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Luminance", "L", "---", GH_ParamAccess.list);
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

            mGetHSL GP = new mGetHSL(A);

            DA.SetDataList(0, GP.H);
            DA.SetDataList(1, GP.S);
            DA.SetDataList(2, GP.L);
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
                return Properties.Resources.Macaw_Deconstruct_HSL;
            }
        }
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f0a9258e-f154-4ff0-a123-fe8160a599f0"); }
        }
    }
}