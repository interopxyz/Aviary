using System;

using Grasshopper.Kernel;
using Wind.Types;
using Wind.Containers;
using Macaw.Filtering.Adjustments.FilterColor;
using Macaw.Filtering;
using System.Drawing;
using Macaw.Build;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

namespace Macaw_GH.Filtering.Adjust
{
    public class Replace : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the EuclideanFilter class.
        /// </summary>
        public Replace()
          : base("Replace Color", "Replace", "...", "Aviary", "Image Edit")
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

            pManager.AddColourParameter("Source", "S", "...", GH_ParamAccess.item, System.Drawing.Color.Red);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Target", "T", "...", GH_ParamAccess.item, System.Drawing.Color.Blue);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Radius", "R", "...", GH_ParamAccess.item, 100);
            pManager[3].Optional = true;
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
            System.Drawing.Color S = System.Drawing.Color.Red;
            System.Drawing.Color T = System.Drawing.Color.Blue;
            double R = 100;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref S)) return;
            if (!DA.GetData(2, ref T)) return;
            if (!DA.GetData(3, ref R)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            Filter = new mFilterEuclideanColor(new wColor(S.R, S.G, S.B), new wColor(T.R, T.G, T.B), (short)R);

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
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Filter_Replace;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2b272732-26bd-464d-9c1b-177386dc8125"); }
        }
    }
}