using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Textures;
using Macaw.Textures.Presets;

namespace Macaw_GH.Texture
{
    public class TexturePresets : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the TexturePresets class.
        /// </summary>
        public TexturePresets()
          : base("Texture Presets", "Textures", "...", "Aviary", "Image Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[0].Optional = true;
            pManager.AddNumberParameter("X", "X", "---", GH_ParamAccess.item, 5.0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Y", "Y", "---", GH_ParamAccess.item, 10.0);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue("Clouds", 0);
            param.AddNamedValue("Wood", 1);
            param.AddNamedValue("Marble", 2);
            param.AddNamedValue("Textile", 3);
            param.AddNamedValue("Labyrinth", 4);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Texture", "T", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables

            int M = 0;
            double X = 5.0;
            double Y = 10.0;

            // Access the input parameters 
            if (!DA.GetData(0, ref M)) return;
            if (!DA.GetData(1, ref X)) return;
            if (!DA.GetData(2, ref Y)) return;

            mTexture Texture = new mTexture();

            switch (M)
            {
                case 0:
                    Texture = new mTextureClouds();
                    break;
                case 1:
                    Texture = new mTextureWood(X);
                    break;
                case 2:
                    Texture = new mTextureMarble(X,Y);
                    break;
                case 3:
                    Texture = new mTextureTextile();
                    break;
                case 4:
                    Texture = new mTextureLabyrinth();
                    break;
            }


            wObject W = new wObject(Texture, "Macaw", Texture.Type);


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
                return Properties.Resources.Macaw_Texture_Texture;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ac748797-c503-4ce7-a668-99e03e406f15"); }
        }
    }
}