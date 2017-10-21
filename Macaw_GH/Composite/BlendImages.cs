using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using System.Windows.Forms;
using Macaw.Utilities;
using Macaw.Compositing;

namespace Macaw_GH.Composite
{
    public class BlendImages : GH_Component
    {
        int MatchType = 0;
        int MatchSizing = 0;

        /// <summary>
        /// Initializes a new instance of the Composite class.
        /// </summary>
        public BlendImages()
          : base("Quick Blend", "Blend", "...", "Aviary", "Image Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Base Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Top Bitmap", "T", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Parameter", "P", "...", GH_ParamAccess.item, 0.5);
            pManager[3].Optional = true;
            
            Param_Integer param = (Param_Integer)Params.Input[2];
            param.AddNamedValue("Add", 0);
            param.AddNamedValue("Subtract", 1);
            param.AddNamedValue("Difference", 2);
            param.AddNamedValue("Intersect", 3);
            param.AddNamedValue("Flat Field", 4);
            param.AddNamedValue("Merge", 5);
            param.AddNamedValue("Morph", 6);
            param.AddNamedValue("Move", 7);
            param.AddNamedValue("Threshold", 8);
            param.AddNamedValue("Euclidean", 9);
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

            int M = 0;
            IGH_Goo X = null;
            IGH_Goo Y = null;
            double P = 0.5;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref Y)) return;
            if (!DA.GetData(2, ref M)) return;
            if (!DA.GetData(3, ref P)) return;

            Bitmap A = null;
            Bitmap B = null;

            if (X != null) { X.CastTo(out A); }
            if (Y != null) { Y.CastTo(out B); }
            Bitmap C = new Bitmap(A);
            

            
            if ((A.Width != B.Width) || (A.Height != B.Height))
            {
                mMatchBitmaps BitmapPair = new mMatchBitmaps(A, B, MatchType, MatchSizing);

                A = new Bitmap(BitmapPair.BottomImage);
                B = new Bitmap(BitmapPair.TopImage);
            }

            switch (M)
            {
                case 0:
                    C = new mCompositeAdd(A,B).ModifiedBitmap;
                    break;
                case 1:
                    C = new mCompositeSubtract(A, B).ModifiedBitmap;
                    break;
                case 2:
                    C = new mCompositeDifference(A, B).ModifiedBitmap;
                    break;
                case 3:
                    C = new mCompositeIntersect(A, B).ModifiedBitmap;
                    break;
                case 4:
                    C = new mCompositeFlatField(A, B).ModifiedBitmap;
                    break;
                case 5:
                    C = new mCompositeMerge(A, B).ModifiedBitmap;
                    break;
                case 6:
                    C = new mCompositeMorph(A, B, P).ModifiedBitmap;
                    break;
                case 7:
                    C = new mCompositeMove(A, B, (int)P).ModifiedBitmap;
                    break;
                case 8:
                    C = new mDifferenceThreshold(A, B, (int)P).ModifiedBitmap;
                    break;
                case 9:
                    C = new mDifferenceEuclidean(A, B, (int)P).ModifiedBitmap;
                    break;
            }
            

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
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Apply_Blend;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e3c18afe-f24e-4297-8043-3cf553a46fd4"); }
        }
    }
}