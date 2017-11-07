using System;

using Grasshopper.Kernel;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.FilterColor;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Compiling;
using Macaw.Compiling.Modifiers;

namespace Macaw_GH.Filtering.Adjust
{
    public class Grayscale : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Grayscale class.
        /// </summary>
        public Grayscale()
          : base("Grayscale", "Grayscale", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddNumberParameter("Red", "R", "---", GH_ParamAccess.item, 0.2125);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Green", "G", "---", GH_ParamAccess.item, 0.7154);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Blue", "B", "---", GH_ParamAccess.item, 0.0721);
            pManager[3].Optional = true;
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
            IGH_Goo V = null;
            double R = 0.2125;
            double G = 0.7154;
            double B = 0.0721;

            // Access the input parameters 
            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref R)) return;
            if (!DA.GetData(2, ref G)) return;
            if (!DA.GetData(3, ref B)) return;

            Bitmap A = null;
            if (V != null) { V.CastTo(out A); }
            Bitmap H = new Bitmap(A);

            mFilter Filter = new mFilter();
            mModifiers Modifier = new mModifiers();

            Filter = new mGrayscale(R,G,B);
            Modifier = new mModifyGrayscale();
            H = new Bitmap(new mQuickComposite(A, Modifier).ModifiedBitmap);


            wObject W = new wObject(Filter, "Macaw", Filter.Type);
            wObject U = new wObject(Modifier, "Macaw", Modifier.Type);


            DA.SetData(0, H);
            DA.SetData(1, U);
            DA.SetData(2, W);
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
                return Properties.Resources.Macaw_Filter_Gray;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("62565cd1-45ac-4960-950d-f82901fb88b4"); }
        }
    }
}