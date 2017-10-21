using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Build;

namespace Macaw_GH.Composite
{
    public class FilterMask : GH_Component
    {

        /// <summary>
        /// Initializes a new instance of the FilterMask class.
        /// </summary>
        public FilterMask()
          : base("Filter Mask", "Filter Mask", "...", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
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

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref Y)) return;

            Bitmap A = null;
            wObject Z = new wObject();
            mFilter F = new mFilter();

            if (X != null) { X.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            if (Y != null) { Y.CastTo(out Z); }
            F = (mFilter)Z.Element;

            
            mFilter Filter = new mFilter();

            Filter = new mFilterMask(B, F);


            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Filter_Mask;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f7946af5-6813-42ef-ac1d-a9590339791d"); }
        }
    }
}