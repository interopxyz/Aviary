using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using System.Drawing;
using Macaw.Compiling;
using Macaw.Compiling.Modifiers;
using Wind.Containers;

namespace Macaw_GH.Edit
{
    public class Fit : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Fit class.
        /// </summary>
        public Fit()
          : base("Fit", "Fit", "...", "Aviary", "Bitmap Edit")
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
            pManager.AddIntegerParameter("Type", "T", "...", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Width", "W", "...", GH_ParamAccess.item, 800);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "...", GH_ParamAccess.item, 600);
            pManager[3].Optional = true;

            Param_Integer paramA = (Param_Integer)Params.Input[1];
            paramA.AddNamedValue("To Width", 0);
            paramA.AddNamedValue("To Height", 1);
            paramA.AddNamedValue("Fill", 2);
            paramA.AddNamedValue("Uniform", 3);
            paramA.AddNamedValue("Uniform Fill", 4);

            Param_Integer paramB = (Param_Integer)Params.Input[2];
            paramB.AddNamedValue("Fant", 0);
            paramB.AddNamedValue("High Quality", 1);
            paramB.AddNamedValue("Linear", 2);
            paramB.AddNamedValue("Low Quality", 3);
            paramB.AddNamedValue("NearestNeighbor", 4);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Modifier", "M", "---", GH_ParamAccess.item);
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
            int T = 0;
            int X = 800;
            int Y = 600;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref T)) return;
            if (!DA.GetData(3, ref X)) return;
            if (!DA.GetData(4, ref Y)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mModifiers Modifier = new mModifiers();

            Modifier = new mModifyResize(M,T,X,Y);
            B = new Bitmap(new mQuickComposite(A, Modifier).ModifiedBitmap);


            wObject W = new wObject(Modifier, "Macaw", Modifier.Type);


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
                return Properties.Resources.Macaw_Edit_Fit;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ba4a86b6-106d-4a51-b0ac-c827e533b858"); }
        }
    }
}