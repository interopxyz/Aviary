using System;

using Grasshopper.Kernel;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using Macaw.Filtering.Adjustments.AdjustColor;
using Macaw.Filtering;
using Macaw.Compiling;
using Macaw.Compiling.Modifiers;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Adjust
{
    public class Adjust : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Brightness class.
        /// </summary>
        public Adjust()
          : base("Adjust", "Adjust", "---", "Aviary", "Image Edit")
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

            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Adjust Value", "V", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("*Invert", 0);
            param.AddNamedValue("*Brightness", 1);
            param.AddNamedValue("*Contrast", 2);
            param.AddNamedValue("Gamma", 3);
            param.AddNamedValue("Hue", 4);
            param.AddNamedValue("Saturation", 5);
            param.AddNamedValue("Channel Shift", 6);
            param.AddNamedValue("Threshold", 7);

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
            int M = 0;
            double V = 0;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref V)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();
            mModifiers Modifier = new mModifiers();

            switch (M)
            {
                case 0:
                    Filter = new mAdjustInvert();
                    Modifier = new mModifyInvert();
                    break;
                case 1:
                    Filter = new mAdjustBrightness((int)V);
                    Modifier = new mModifyBrightness((int)V);
                    break;
                case 2:
                    Filter = new mAdjustContrast((int)V);
                    Modifier = new mModifyContrast((int)V);
                    break;
                case 3:
                    Filter = new mAdjustGamma(V);
                    break;
                case 4:
                    Filter = new mAdjustHue((int)V);
                    break;
                case 5:
                    Filter = new mAdjustSaturation((float)V);
                    break;
                case 6:
                    Filter = new mAdjustShift((int)V);
                    break;
                case 7:
                    Filter = new mAdjustBradley((float)V);
                    break;
            }

            B = new mApply(A, Filter).ModifiedBitmap;

            wObject W = new wObject(Filter, "Macaw", Filter.Type);
            wObject U = new wObject(Modifier, "Macaw", Modifier.Type);


            DA.SetData(0, B);
            DA.SetData(1, W);
            DA.SetData(2, U);
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
                return Properties.Resources.Macaw_Filter_Brightness;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("238c3cdb-ddfa-43cd-80c4-004a387635f2"); }
        }
    }
}