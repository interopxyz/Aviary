using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Filtering.Adjustments;
using Macaw.Filtering;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Compiling;
using Macaw.Compiling.Modifiers;
using Wind.Types;
using Macaw.Build;

namespace Macaw_GH.Filtering.Adjust
{
    public class Fill : GH_Component
    {

        public int ModeIndex = 0;
        private string[] modes = { "Color", "Means" };

        /// <summary>
        /// Initializes a new instance of the Fill class.
        /// </summary>
        public Fill()
          : base("Fill", "Fill", "...", "Aviary", "Bitmap Edit")
        {
            UpdateMessage();
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

            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Target", "T", "...", GH_ParamAccess.item, System.Drawing.Color.Red);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Fill", "F", "...", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Tolerance", "P", "...", GH_ParamAccess.item, 10);
            pManager[4].Optional = true;
            pManager.AddPointParameter("Location", "L", "...", GH_ParamAccess.item, new Point3d(1,1,0));
            pManager[5].Optional = true;


            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue(modes[0], 0);
            param.AddNamedValue(modes[1], 1);
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
            Color T = Color.Red;
            Color F = Color.Black;
            int X = 10;
            Point3d P = new Point3d(1, 1, 0);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref T)) return;
            if (!DA.GetData(3, ref F)) return;
            if (!DA.GetData(4, ref X)) return;
            if (!DA.GetData(5, ref P)) return;

            M = M % 10;

            if (M != ModeIndex)
            {
                ModeIndex = M;
                UpdateMessage();
            }

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }
            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mFillColor(T,F, (int)P.X, (int)P.Y);

                    break;
                case 1:
                    Filter = new mFillMean(T,(int)P.X, (int)P.Y);

                    break;
            }
            
            Bitmap B = new mApply(A, Filter).ModifiedBitmap;
            wObject W = new wObject(Filter, "Macaw", Filter.Type);
            
            DA.SetData(0, B);
            DA.SetData(1, W);
        }

        private void UpdateMessage()
        {
            Message = modes[ModeIndex];
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
                return Properties.Resources.Macaw_Analyze_Fill;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("256883b2-0bc4-440d-a51b-8988998a1211"); }
        }
    }
}