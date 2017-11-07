using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;
using System.Windows.Forms;
using Wind.Containers;
using Macaw.Textures;
using Macaw.Compositing;

namespace Macaw_GH.Composite
{
    public class CompositeTexture : GH_Component
    {
        int MatchType = 0;
        int MatchSizing = 0;

        /// <summary>
        /// Initializes a new instance of the CompositeTexture class.
        /// </summary>
        public CompositeTexture()
          : base("Texture Morph", "Texture Morph", "...", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Overlay Bitmap", "O", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Underlay Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Texture", "T", "---", GH_ParamAccess.item);
            
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
            IGH_Goo Z = null;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref Y)) return;
            if (!DA.GetData(2, ref Z)) return;

            Bitmap A = null;
            Bitmap B = null;
            wObject W = new wObject();
            mTexture T = new mTexture();

            if (X != null) { X.CastTo(out A); }
            if (Y != null) { Y.CastTo(out B); }
            Bitmap C = new Bitmap(A);

            if (Z != null) { Z.CastTo(out W); }
            T = (mTexture)W.Element;


            if ((A.Width != B.Width) || (A.Height != B.Height))
            {
                mMatchBitmaps BitmapPair = new mMatchBitmaps(A, B, MatchType, MatchSizing);

                A = new Bitmap(BitmapPair.BottomImage);
                B = new Bitmap(BitmapPair.TopImage);
            }


            C = new mCompositeTextureMorph(A, B, T).ModifiedBitmap;

            DA.SetData(0, C);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Origin", ModeOrigin, true, (MatchType == 0));
            Menu_AppendItem(menu, "Source", ModeTarget, true, (MatchType == 1));

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Crop", SizeCrop, true, (MatchSizing == 0));
            Menu_AppendItem(menu, "Fit", SizeFit, true, (MatchSizing == 1));
            Menu_AppendItem(menu, "Stretch", SizeStretch, true, (MatchSizing == 2));
        }

        private void ModeOrigin(Object sender, EventArgs e)
        {
            MatchType = 0;
            this.ExpireSolution(true);
        }

        private void ModeTarget(Object sender, EventArgs e)
        {
            MatchType = 1;
            this.ExpireSolution(true);
        }

        private void SizeCrop(Object sender, EventArgs e)
        {
            MatchSizing = 0;
            this.ExpireSolution(true);
        }

        private void SizeFit(Object sender, EventArgs e)
        {
            MatchSizing = 1;
            this.ExpireSolution(true);
        }

        private void SizeStretch(Object sender, EventArgs e)
        {
            MatchSizing = 2;
            this.ExpireSolution(true);
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
                return Properties.Resources.Macaw_Texture_Morph;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("925d3fb6-cecc-44f0-9f74-e3f2f80b909f"); }
        }
    }
}