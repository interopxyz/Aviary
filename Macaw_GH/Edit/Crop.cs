using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Macaw.Build;
using Wind.Containers;
using System.Drawing;
using Grasshopper.Kernel.Types;
using Macaw.Editing.Resizing;
using Macaw.Filtering;

namespace Macaw_GH.Edit
{
    public class Crop : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Crop class.
        /// </summary>
        public Crop()
          : base("Crop", "Crop", "---", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new Bitmap(100, 100)));

            pManager.AddIntegerParameter("Align X", "X", "---", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Align Y", "Y", "---", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;

            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item, 800);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "---", GH_ParamAccess.item, 600);
            pManager[4].Optional = true;
            pManager.AddColourParameter("Background", "B", "---", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[5].Optional = true;

            Param_Integer paramX = (Param_Integer)Params.Input[1];
            paramX.AddNamedValue("Left", 0);
            paramX.AddNamedValue("Middle", 1);
            paramX.AddNamedValue("Right", 2);

            Param_Integer paramY = (Param_Integer)Params.Input[2];
            paramY.AddNamedValue("Top", 0);
            paramY.AddNamedValue("Center", 1);
            paramY.AddNamedValue("Bottom", 2);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
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
            int AX = 1;
            int AY = 1;
            int CW = 800;
            int CH = 600;
            Color C = Color.Black;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref AX)) return;
            if (!DA.GetData(2, ref AY)) return;
            if (!DA.GetData(3, ref CW)) return;
            if (!DA.GetData(4, ref CH)) return;
            if (!DA.GetData(5, ref C)) return;

            Bitmap A = null;

            if (X != null) { X.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            Filter = new mCropCanvas(AX,AY,CW,CH,A.Width,A.Height,C);
            B = new mApply(A, Filter).ModifiedBitmap;


            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, B);
            DA.SetData(1, W);
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
                return Properties.Resources.Macaw_Edit_Crop;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("8ae47e45-d17f-4c31-8078-e4c1d32f8dda"); }
        }
    }
}