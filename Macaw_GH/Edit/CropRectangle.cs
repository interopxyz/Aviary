using System;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Macaw_GH.Edit
{
    public class CropRectangle : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CropRectangle class.
        /// </summary>
        public CropRectangle()
          : base("Crop Rectangle", "Crop R", "---", "Aviary", "Bitmap Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddRectangleParameter("Region", "R", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY,800,600));
            pManager[1].Optional = true;
            pManager.AddColourParameter("Background", "B", "---", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Edit_CropRect;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("75014e8a-dfe1-41fa-bc09-0d9cd9b6e881"); }
        }
    }
}