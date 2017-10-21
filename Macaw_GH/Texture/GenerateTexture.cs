using System;

using Grasshopper.Kernel;
using System.Drawing;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Macaw.Textures;

namespace Macaw_GH.Texture
{
    public class GenerateTexture : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GenerateTexture class.
        /// </summary>
        public GenerateTexture()
          : base("Generate Texture", "Generate", "...", "Aviary", "Image Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Texture", "T", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item, 800);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "---", GH_ParamAccess.item, 600);
            pManager[2].Optional = true;
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
            int W = 800;
            int H = 600;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref W)) return;
            if (!DA.GetData(2, ref H)) return;

            wObject Z = new wObject();
            mTexture T = new mTexture();
            if (X != null) { X.CastTo(out Z); }
            T = (mTexture)Z.Element;

            Bitmap B = new Bitmap(new mTextureApply(T,W,H).GeneratedBitmap);


            DA.SetData(0, B);
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
                return Properties.Resources.Macaw_Texture_Apply;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ddf104b4-0bf9-457b-be0f-78478a0ad060"); }
        }
    }
}