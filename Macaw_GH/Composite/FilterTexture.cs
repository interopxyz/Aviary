using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Macaw.Build;
using Wind.Containers;
using Macaw.Textures;
using Macaw.Filtering;

namespace Macaw_GH.Composite
{
    public class FilterTexture : GH_Component
    {

        /// <summary>
        /// Initializes a new instance of the FilterTexture class.
        /// </summary>
        public FilterTexture()
          : base("Filter Texture", "Filter Txtr", "...", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Texture", "T", "---", GH_ParamAccess.item);
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

            mTexture T = new mTexture();
            wObject Z = new wObject();
            wObject U = new wObject();
            mFilter F = new mFilter();


            if (X != null) { X.CastTo(out Z); }
            T = (mTexture)Z.Element;
            if (Y != null) { Y.CastTo(out U); }
            F = (mFilter)U.Element;



            mFilter Filter = new mFilter();

            Filter = new mFilterTexture(T, F);


            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Texture_Filter;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0b0c5f15-0aa1-4843-adf9-36ac2008153e"); }
        }
    }
}