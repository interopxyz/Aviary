using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;

namespace Macaw_GH.Filtering.Extract
{
    public class BitmapProperties : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BitmapProperties class.
        /// </summary>
        public BitmapProperties()
          : base("BitmapProperties", "Properties", "---", "Category", "Subcategory")
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
            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Height", "H", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Depth", "D", "---", GH_ParamAccess.item);
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
            
            DA.SetData(0, A.Width);
            DA.SetData(1, A.Height);
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a6d69ade-19a0-4189-baca-7d2843ba6983"); }
        }
    }
}