using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Wind.Containers;
using Macaw.Filtering;

namespace Macaw_GH.Build
{
    public class Iterate : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Iterate class.
        /// </summary>
        public Iterate()
          : base("Iterate Filter", "Iterate", "---", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filters", "F", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Loops", "L", "---", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;
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

            IGH_Goo X = null;
            IGH_Goo Y = null;
            int L = 1;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref Y)) return;
            if (!DA.GetData(2, ref L)) return;
            
            Bitmap A = null;
            wObject Z = new wObject();
            mFilter F = new mFilter();

            if (X != null) { X.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            if (Y != null) { Y.CastTo(out Z); }
            F = (mFilter)Z.Element;

            B = new mIterate(B, F, L).ModifiedBitmap;
            
            DA.SetData(0, B);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.senary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Apply_Iterate;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6a8a3e9b-35aa-4385-8eab-805f69cedc9f"); }
        }
    }
}