using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Macaw.Compiling;
using System.Drawing;

namespace Macaw_GH.Compose
{
    public class Mask : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Mask class.
        /// </summary>
        public Mask()
          : base("Layer Mask", "Mask", "---", "Aviary", "Image Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Image", "B", "---", GH_ParamAccess.item);
            pManager.AddIntervalParameter("Position", "P", "---", GH_ParamAccess.item, new Interval(0, 0));
            pManager[2].Optional = true;
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
            IGH_Goo Y = null;
            Interval P = new Interval(0, 0);

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref Y)) return;
            if (!DA.GetData(2, ref P)) return;

            Bitmap A = null;
            wObject B = new wObject();

            if (X != null) { X.CastTo(out B); }
            if (Y != null) { Y.CastTo(out A); }

            mLayer L = (mLayer)B.Element;

            L.SetMask(A);
            
            wObject W = new wObject(L, "Macaw", L.Type);


            DA.SetData(0, W);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Build_Mask;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5b2e88a6-394d-4cf8-bebe-6caaea2cbdc1"); }
        }
    }
}