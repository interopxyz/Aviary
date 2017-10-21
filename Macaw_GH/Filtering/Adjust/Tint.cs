using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Compiling;
using Macaw.Compiling.Modifiers;
using Wind.Types;
using Macaw.Build;
using Wind.Containers;

namespace Macaw_GH.Filtering.Adjust
{
    public class Tint : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Tint class.
        /// </summary>
        public Tint()
          : base("Tint", "Tint", "---", "Aviary", "Image Edit")
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
            
            pManager.AddColourParameter("Color", "C", "...", GH_ParamAccess.item, Color.Blue);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Value", "V", "...", GH_ParamAccess.item, 30);
            pManager[2].Optional = true;
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
            Color C = Color.Red;
            int V = 30;
            Point3d P = new Point3d(1, 1, 0);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref C)) return;
            if (!DA.GetData(2, ref V)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);
            
            mModifiers Modifier = new mModifiers();
            
            Modifier = new mModifyColorTint(new wColor(C.A, C.R, C.G, C.B), V);
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
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Adjust_Tint;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7e8b92dd-4b8a-45f5-8ce2-6cc461d5a1c8"); }
        }
    }
}