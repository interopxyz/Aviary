using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;
using Macaw.Utilities.Channels;
using GH_IO.Serialization;
using System.Windows.Forms;
using Macaw.Filtering;

namespace Macaw_GH.Utilities
{
    public class GetPixelValue : GH_Component
    {
        public int ModeIndex = 0;
        public bool unitize = false;
        private string[] modes = { "Color", "Alpha", "Red", "Green", "Blue", "Hue", "Saturation", "Brightness" };

        /// <summary>
        /// Initializes a new instance of the SampleBitmap class.
        /// </summary>
        public GetPixelValue()
          : base("Sample Bitmap", "Sample", "---", "Aviary", "Bitmap Build")
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

            pManager.AddVectorParameter("Point", "P", "A unitized point", GH_ParamAccess.list);
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
            IGH_Goo X = null;
            int M = 0;
            List<Vector3d> V = new List<Vector3d>();

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetDataList(2, V)) return;

            Bitmap A = new Bitmap(10, 10);
            if (X != null) { X.CastTo(out A); }

            if (M != ModeIndex)
            {
                ModeIndex = M;
                UpdateMessage();
            }

            mGetChannel C = null;

            List<double> i = new List<double>();
            List<double> j = new List<double>();

            for (int k = 0; k < V.Count; k++)
            {
                i.Add(V[k].X);
                j.Add(1.0 - V[k].Y);
            }

            switch (M)
            {
                default:
                    C = new mGetRGBColor(A, i, j);
                    break;
                case 1:
                    C = new mGetAlpha(A, i, j, unitize);
                    break;
                case 2:
                    C = new mGetRed(A, i, j, unitize);
                    break;
                case 3:
                    C = new mGetGreen(A, i, j, unitize);
                    break;
                case 4:
                    C = new mGetBlue(A, i, j, unitize);
                    break;
                case 5:
                    C = new mGetHue(A, i, j, unitize);
                    break;
                case 6:
                    C = new mGetSaturation(A, i, j);
                    break;
                case 7:
                    C = new mGetBrightness(A, i, j);
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

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Unitize", unitizeValues, true, unitize);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void unitizeValues(Object sender, EventArgs e)
        {
            unitize = !unitize;

            UpdateMessage();
            ExpireSolution(true);
        }

        /// <summary>
        /// Adds to the default serialization method to save the current child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Write(GH_IWriter writer)
        {
            writer.SetBoolean("Unitize", unitize);

            return base.Write(writer);
        }

        /// <summary>
        /// Adds to the default deserialization method to retrieve the saved child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Read(GH_IReader reader)
        {
            unitize = reader.GetBoolean("Unitize");

            UpdateMessage();
            return base.Read(reader);
        }

        private void UpdateMessage()
        {
            string isUnit = "";
            if (unitize) { isUnit = "*"; }
            Message = modes[ModeIndex] + isUnit;
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
                return Properties.Resources.Macaw_Deconstruct_Sample;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6ce301f0-11c4-4afe-9cd6-946674330039"); }
        }
    }
}