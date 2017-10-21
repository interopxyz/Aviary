using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using System.Drawing;
using Wind.Containers;
using Macaw.Build;
using Macaw.Filtering;
using Macaw.Editing.Resizing;

namespace Macaw_GH.Edit
{
    public class Resize : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Resize class.
        /// </summary>
        public Resize()
          : base("Resize", "Resize", "...", "Aviary", "Image Edit")
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

            pManager.AddIntegerParameter("Mode", "M", "...", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Width", "W", "...", GH_ParamAccess.item, 800);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "...", GH_ParamAccess.item, 600);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Bicubic", 0);
            param.AddNamedValue("Bilinear", 1);
            param.AddNamedValue("Neighbor", 2);
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
            IGH_Goo Z = null;
            int M = 0;
            int X = 800;
            int Y = 600;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref X)) return;
            if (!DA.GetData(3, ref Y)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mResizeBicubic( X, Y);
                    break;
                case 1:

                    Filter = new mResizeBilinear(X, Y);
                    break;
                case 2:

                    Filter = new mResizeNearistNeighbor( X, Y);
                    break;
            }

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
                return Properties.Resources.Macaw_Edit_Resize;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("bbd9d754-7414-4824-a88a-746ae49ed3a5"); }
        }
    }
}