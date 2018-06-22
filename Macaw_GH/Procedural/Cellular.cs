using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Macaw_GH.Procedural
{
    public class Cellular : GH_Component
    {
        private int mIndex = 0;
        private string[] modes = { "Euclidean", "Manhattan", "Natural" };

        private int tIndex = 0;
        private string[] types = { "Value", "Lookup", "Distance", "Distance 2", "D2 Addition", "D2 Subtraction", "D2 Multiplication", "D2 Division"};

        private bool pA = false;
        private bool pB = false;

        /// <summary>
        /// Initializes a new instance of the Cellular class.
        /// </summary>
        public Cellular()
          : base("Procedural Cellular", "Cellular", "---", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item, 100);
            pManager[0].Optional = true;
            pManager.AddIntegerParameter("Height", "H", "---", GH_ParamAccess.item, 100);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Depth", "Z", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            pManager.AddIntegerParameter("Seed", "S", "---", GH_ParamAccess.item, 1);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Frequency", "F", "---", GH_ParamAccess.item, 0.1);
            pManager[4].Optional = true;

            pManager.AddIntervalParameter("Indices", "I", "Interval between [0,0] to [0,3]", GH_ParamAccess.item, new Interval(0, 1));
            pManager[5].Optional = true;
            pManager.AddNumberParameter("Jitter", "J", "---", GH_ParamAccess.item, 0.45);
            pManager[6].Optional = true;

            pManager.AddNumberParameter("Perturb Amplitude", "pA", "---", GH_ParamAccess.item, 10.0);
            pManager[7].Optional = true;
            pManager.AddNumberParameter("Fractal Gain", "pF", "---", GH_ParamAccess.item, 0.01);
            pManager[8].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddNumberParameter("Values", "V", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            int W = 100;
            int H = 100;
            int Z = 0;

            int S = 1;
            double F = 0.1;

            Interval I = new Interval(0,1);
            double J = 2;
            double P = 0.5;
            double Pf = 0.5;

            // Access the input parameters 
            if (!DA.GetData(0, ref W)) return;
            if (!DA.GetData(1, ref H)) return;
            if (!DA.GetData(2, ref Z)) return;
            if (!DA.GetData(3, ref S)) return;
            if (!DA.GetData(4, ref F)) return;
            if (!DA.GetData(5, ref I)) return;
            if (!DA.GetData(6, ref J)) return;
            if (!DA.GetData(7, ref P)) return;
            if (!DA.GetData(7, ref Pf)) return;




            //DA.SetData(0, noise.OutputBitmap);
            //DA.SetDataList(1, noise.Values);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, modes[0], mModeA, true, mIndex == 0);
            Menu_AppendItem(menu, modes[1], mModeB, true, mIndex == 1);
            Menu_AppendItem(menu, modes[2], mModeC, true, mIndex == 2);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, types[0], tModeA, true, tIndex == 0);
            Menu_AppendItem(menu, types[1], tModeB, true, tIndex == 1);
            Menu_AppendItem(menu, types[2], tModeC, true, tIndex == 2);
            Menu_AppendItem(menu, types[3], tModeD, true, tIndex == 3);
            Menu_AppendItem(menu, types[4], tModeE, true, tIndex == 4);
            Menu_AppendItem(menu, types[5], tModeF, true, tIndex == 5);
            Menu_AppendItem(menu, types[6], tModeG, true, tIndex == 6);
            Menu_AppendItem(menu, types[7], tModeH, true, tIndex == 7);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Perturb", pModeA, true, pA);
            Menu_AppendItem(menu, "Fractal", pModeB, true, pB);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void mModeA(Object sender, EventArgs e)
        {
            mIndex = 0;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void mModeB(Object sender, EventArgs e)
        {
            mIndex = 1;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void mModeC(Object sender, EventArgs e)
        {
            mIndex = 2;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeA(Object sender, EventArgs e)
        {
            tIndex = 0;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeB(Object sender, EventArgs e)
        {
            tIndex = 1;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeC(Object sender, EventArgs e)
        {
            tIndex = 2;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeD(Object sender, EventArgs e)
        {
            tIndex = 3;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeE(Object sender, EventArgs e)
        {
            tIndex = 4;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeF(Object sender, EventArgs e)
        {
            tIndex = 5;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeG(Object sender, EventArgs e)
        {
            tIndex = 6;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void tModeH(Object sender, EventArgs e)
        {
            tIndex = 7;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void pModeA(Object sender, EventArgs e)
        {
            pA = !pA;
            if (pA) { if (pB) { pB = false; } }

            UpdateMessage();
            ExpireSolution(true);
        }

        private void pModeB(Object sender, EventArgs e)
        {
            pB = !pB;
            if (pB) { if (pA) { pA = false; } }

            UpdateMessage();
            ExpireSolution(true);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Adds to the default serialization method to save the current child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("mIndex", mIndex);
            writer.SetInt32("tIndex", tIndex);

            return base.Write(writer);
        }

        /// <summary>
        /// Adds to the default deserialization method to retrieve the saved child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Read(GH_IReader reader)
        {
            mIndex = reader.GetInt32("mIndex");
            tIndex = reader.GetInt32("tIndex");

            UpdateMessage();
            return base.Read(reader);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void UpdateMessage()
        {

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
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b5789d31-cc3b-411e-8a40-f4daabb4996d"); }
        }
    }
}