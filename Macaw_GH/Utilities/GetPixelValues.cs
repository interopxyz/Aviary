using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities.Channels;
using Grasshopper.Kernel.Parameters;

namespace Macaw_GH.Filtering.Extract
{
    public class GetPixelValues : GH_Component
    {
        public int ModeIndex = 0;
        private string[] modes = { "Color", "Alpha", "Red", "Green", "Blue", "Hue", "Saturation", "Brightness" };

        /// <summary>
        /// Initializes a new instance of the DeconstructARGB class.
        /// </summary>
        public GetPixelValues()
          : base("Get Values", "Values", "---", "Aviary", "Bitmap Build")
        {
            UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue(modes[0], 0);
            param.AddNamedValue(modes[1], 1);
            param.AddNamedValue(modes[2], 2);
            param.AddNamedValue(modes[3], 3);
            param.AddNamedValue(modes[4], 4);
            param.AddNamedValue(modes[5], 5);
            param.AddNamedValue(modes[6], 6);
            param.AddNamedValue(modes[7], 7);


        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Values", "V", "---", GH_ParamAccess.list);
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
            
            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;

            if (M != ModeIndex)
            {
                ModeIndex = M;
                UpdateMessage();
            }

            Bitmap A = new Bitmap(10,10);
            if (Z != null) { Z.CastTo(out A); }

            mGetChannel C = null;
            switch(M)
            {
                default:
                    C = new mGetRGBColor(A);
                    break;
                case 1:
                    C = new mGetAlpha(A);
                    break;
                case 2:
                    C = new mGetRed(A);
                    break;
                case 3:
                    C = new mGetGreen(A);
                    break;
                case 4:
                    C = new mGetBlue(A);
                    break;
                case 5:
                    C = new mGetHue(A);
                    break;
                case 6:
                    C = new mGetSaturation(A);
                    break;
                case 7:
                    C = new mGetBrightness(A);
                    break;
            }

            if (M == 0)
            {
                DA.SetDataList(0, C.Colors);
            }
            else
            {
                DA.SetDataList(0, C.Values);
            }
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
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Deconstruct_RGB;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f21b4117-c50a-4df7-a5bc-ba937c1eb9ee"); }
        }
    }
}