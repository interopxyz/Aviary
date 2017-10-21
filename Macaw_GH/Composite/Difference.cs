using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;
using System.Windows.Forms;
using Macaw.Compositing;

namespace Macaw_GH.Composite
{
    public class Difference : GH_Component
    {
        int MatchType = 0;
        int MatchSizing = 0;

        /// <summary>
        /// Initializes a new instance of the Difference class.
        /// </summary>
        public Difference()
          : base("Difference", "Difference", "...", "Aviary", "Macaw")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[0].Optional = true;
            pManager.AddGenericParameter("Overlay Bitmap", "O", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Underlay Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Threshold", "T", "---", GH_ParamAccess.item, 50);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue("Threshold", 0);
            param.AddNamedValue("Euclidean", 1);
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
            int P = 50;

            // Access the input parameters 
            if (!DA.GetData(0, ref M)) return;
            if (!DA.GetData(1, ref X)) return;
            if (!DA.GetData(2, ref Y)) return;
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
                    C = new mDifferenceThreshold(A, B,P).ModifiedBitmap;
                    break;
                case 1:
                    C = new mDifferenceEuclidean(A, B,P).ModifiedBitmap;
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
            get { return GH_Exposure.senary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Composite_Layer;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a637769e-7bf9-4c6c-b5d4-f9b1492b12fc"); }
        }
    }
}