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
using Macaw.Build;

namespace Macaw_GH.Filtering.Adjust
{
    public class Grayscale : GH_Component
    {
        private int ModeIndex = 0;
        private string[] modes = { "Custom", "BT709", "RMY", "Y" };

        /// <summary>
        /// Initializes a new instance of the Grayscale class.
        /// </summary>
        public Grayscale()
          : base("Grayscale", "Grayscale", "---", "Aviary", "Bitmap Edit")
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

            pManager.AddNumberParameter("Red", "R", "---", GH_ParamAccess.item, 0.2125);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Green", "G", "---", GH_ParamAccess.item, 0.7154);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Blue", "B", "---", GH_ParamAccess.item, 0.0721);
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue(modes[0], 0);
            param.AddNamedValue(modes[1], 1);
            param.AddNamedValue(modes[2], 2);
            param.AddNamedValue(modes[3], 3);

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
            IGH_Goo X = null;
            int M = 0;
            double R = 0.2125;
            double G = 0.7154;
            double B = 0.0721;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref M)) return;

            if (M != ModeIndex)
            {
                ModeIndex = M;
                UpdateMessage();
                if (M == 0)
                {
                    SetParameter(2, "Red", "R", "---");
                    SetParameter(3, "Green", "G", "---");
                    SetParameter(4, "Blue", "B", "---");
                }
                else
                {
                    SetParameter(2, "Not Used", "-", "Not used by this filter");
                    SetParameter(3, "Not Used", "-", "Not used by this filter");
                    SetParameter(4, "Not Used", "-", "Not used by this filter");
                }
            }

            if (!DA.GetData(2, ref R)) return;
            if (!DA.GetData(3, ref G)) return;
            if (!DA.GetData(4, ref B)) return;

            Bitmap A = new Bitmap(10, 10);
            if (X != null) { X.CastTo(out A); }

            mFilter Filter = new mFilter();

            Filter = new mGrayscale(R, G, B, (mGrayscale.GrayscaleModes)M);

            Bitmap C = new mApply(A, Filter).ModifiedBitmap;
            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, C);
            DA.SetData(1, W);
        }

        private void UpdateMessage()
        {
            Message = modes[ModeIndex];
        }

        private void SetParameter(int index, string Name, string NickName, string Description)
        {
            Param_Number param = (Param_Number)Params.Input[index];
            param.Name = Name;
            param.NickName = NickName;
            param.Description = Description;
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