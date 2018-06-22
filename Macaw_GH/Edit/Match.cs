using System;

using Grasshopper.Kernel;

using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;

namespace Macaw_GH.Edit
{
    public class Match : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Match class.
        /// </summary>
        public Match()
          : base("Match", "Match", "---", "Aviary", "Bitmap Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Source Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.SetPersistentData(new Bitmap(10, 10));

            pManager.AddGenericParameter("Target Bitmap", "T", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;

            Param_GenericObject paramGenA = (Param_GenericObject)Params.Input[1];
            paramGenA.SetPersistentData(new Bitmap(10, 10));

            pManager.AddIntegerParameter("Mode", "M", "...", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer paramA = (Param_Integer)Params.Input[2];
            paramA.AddNamedValue("Crop", 0);
            paramA.AddNamedValue("Fit", 1);
            paramA.AddNamedValue("Stretch", 2);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Top Bitmap", "Bt", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bottom Bitmap", "Bb", "---", GH_ParamAccess.item);
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
            int M = 0;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref Y)) return;
            if (!DA.GetData(2, ref M)) return;

            Bitmap A = new Bitmap(10, 10);
            if (X != null) { X.CastTo(out A); }

            Bitmap B = new Bitmap(10, 10);
            if (Y != null) { Y.CastTo(out B); }

            mMatchBitmaps f = new mMatchBitmaps(A, B, 0, M);



            DA.SetData(0, f.TopImage);
            DA.SetData(0, f.BottomImage);
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
                return Properties.Resources.Macaw_Edit_Match;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7d2f5870-e289-4f59-99b6-329999f7b606"); }
        }
    }
}