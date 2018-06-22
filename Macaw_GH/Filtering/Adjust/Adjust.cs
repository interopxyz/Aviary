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
using Macaw.Filtering.Stylized;

namespace Macaw_GH.Filtering.Adjust
{
    public class Adjust : GH_Component
    {

        public int ModeIndex = 0;
        private string[] modes = { "Brightness", "Contrast", "Gamma", "Hue", "Saturation", "White Patch", "Gray World", "Histogram" ,"Stretch", "Invert" };

        /// <summary>
        /// Initializes a new instance of the Brightness class.
        /// </summary>
        public Adjust()
          : base("Adjust", "Adjust", "---", "Aviary", "Bitmap Edit")
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
            paramGen.SetPersistentData(new GH_ObjectWrapper(new Bitmap(10, 10)));

            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;

            pManager.AddNumberParameter("Adjust Value", "T", "Suggested Range [0,1]", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue(modes[0], 0);
            param.AddNamedValue(modes[1], 1);
            param.AddNamedValue(modes[2], 2);
            param.AddNamedValue(modes[3], 3);
            param.AddNamedValue(modes[4], 4);
            param.AddNamedValue(modes[5], 5);
            param.AddNamedValue(modes[6], 6);
            param.AddNamedValue(modes[7], 7);
            param.AddNamedValue(modes[8], 8);
            param.AddNamedValue(modes[9], 9);

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
            double V = 0;

            int M = 0;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref V)) return;

            M = M % 10;

            if (M != ModeIndex)
            {
                ModeIndex = M;
                UpdateMessage();
            }

            if(M<5)
            {
                Params.Input[2].NickName = "T";
                Params.Input[2].Name = "Adjust Value";
                Params.Input[2].Description = "Suggested Range [0,1]";
            }
            else
            {
                Params.Input[2].NickName = "-";
                Params.Input[2].Name = "Not Used";
                Params.Input[2].Description = "Not used by this filter";
            }

            Bitmap A = new Bitmap(10, 10);
            if (Z != null) { Z.CastTo(out A); }
            mFilter Filter = new mFilter();

            switch (ModeIndex)
            {
                default:
                    Filter = new mAdjustBrightness((int)(V * 255));
                    break;
                case 1:
                    Filter = new mAdjustContrast((int)(V * 255));
                    break;
                case 2:
                    Filter = new mAdjustGamma(0.1 + (V * 4.9));
                    break;
                case 3:
                    Filter = new mAdjustHue((int)(V * 360));
                    break;
                case 4:
                    Filter = new mAdjustSaturation((float)(-1 + V * 2));
                    break;
                case 5:
                    Filter = new mAdjustWhitePatch();
                    break;
                case 6:
                    Filter = new mAdjustGrayWorld();
                    break;
                case 7:
                    Filter = new mAdjustHistogramEqualization();
                    break;
                case 8:
                    Filter = new mAdjustContrastStretch();
                    break;
                case 9:
                    Filter = new mAdjustInvert();
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
            get { return new Guid("0d409ada-8d21-4ad5-931b-4feccf88abd3"); }
        }
    }
}