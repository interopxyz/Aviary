using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Filtering;
using Macaw.Compiling;
using Wind.Containers;
using Macaw.Editing.Resizing;
using Macaw.Filtering.Stylized;
using Macaw.Compiling.Modifiers;
using Wind.Types;

namespace Macaw_GH.Filtering.Stylize
{
    public class Border : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Border class.
        /// </summary>
        public Border()
          : base("Border", "Border", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddIntegerParameter("Radius", "R", "---", GH_ParamAccess.item, 10);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, Color.White);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
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
            int R = 10;
            Color C = Color.Red;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref R)) return;
            if (!DA.GetData(2, ref C)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();
            mModifiers Modifier = new mModifiers();

            Filter = new mPadding(R,R,R,R,A.Width,A.Height,C);
            Modifier = new mModifyBorder(new wColor(C.A,C.R,C.G,C.B),R);
            B = new Bitmap(new mQuickComposite(A, Modifier).ModifiedBitmap);

            
            wObject W = new wObject(Filter, "Macaw", Filter.Type);
            wObject V = new wObject(Modifier, "Macaw", Modifier.Type);


            DA.SetData(0, B);
            DA.SetData(1, W);
            DA.SetData(2, V);

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
                return Properties.Resources.Macaw_Effect_Border;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e3c24d9b-8e80-4f92-8850-2b0e7cf5cef4"); }
        }
    }
}