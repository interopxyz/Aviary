using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Macaw.ProceduralNoise;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Macaw_GH.Procedural
{
    public class Noise : GH_Component
    {
        private int mIndex = 0;
        private string[] mModes = { "Value", "Perlin", "Simplex", "White Noise", "Cubic" };

        private int iIndex = 0;
        private string[] iModes = { "Linear", "Hermite", "Quintic" };

        private int fIndex = 3;
        private string[] fModes = { "FBM", "Billow", "Rigid", "None" };

        private bool pA = false;
        private bool pB = false;

        /// <summary>
        /// Initializes a new instance of the Noise class.
        /// </summary>
        public Noise()
          : base("Procedural Noise", "Noise", "---", "Aviary", "Bitmap Build")
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

            pManager.AddIntegerParameter("Fractal Octaves", "fO", "---", GH_ParamAccess.item, 5);
            pManager[5].Optional = true;
            pManager.AddNumberParameter("Fractal Lacunarity", "fL", "---", GH_ParamAccess.item, 2.0);
            pManager[6].Optional = true;
            pManager.AddNumberParameter("Fractal Gain", "fG", "---", GH_ParamAccess.item, 0.5);
            pManager[7].Optional = true;

            pManager.AddNumberParameter("Perturb Amplitude", "pA", "---", GH_ParamAccess.item, 10.0);
            pManager[8].Optional = true;
            pManager.AddNumberParameter("Fractal Gain", "pF", "---", GH_ParamAccess.item, 0.01);
            pManager[9].Optional = true;

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

            int fO = 5;
            double fL = 2;
            double fG = 0.5;

            double pAmp = 0;
            double pFrq = 0;

            // Access the input parameters 
            if (!DA.GetData(0, ref W)) return;
            if (!DA.GetData(1, ref H)) return;
            if (!DA.GetData(2, ref Z)) return;
            if (!DA.GetData(3, ref S)) return;
            if (!DA.GetData(4, ref F)) return;
            if (!DA.GetData(5, ref fO)) return;
            if (!DA.GetData(6, ref fL)) return;
            if (!DA.GetData(7, ref fG)) return;

            if (!DA.GetData(8, ref pAmp)) return;
            if (!DA.GetData(9, ref pFrq)) return;

            mFastNoise noise = new mFastNoise(S);

            noise.SetSize(W, H, Z);
            noise.SetNoiseParameters( (mFastNoise.NoiseModes)mIndex,F, (mFastNoise.InterpolationModes) iIndex);

            noise.SetPerturbance(pAmp, pFrq);

            if (fIndex < 3)
            {
                noise.SetFractal((mFastNoise.FractalModes)fIndex, fO, fL, fG);
            }
            

            noise.BuildBitmap();


            DA.SetData(0, noise.OutputBitmap);
            DA.SetDataList(1, noise.Values);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, mModes[0], mModeA, true, mIndex == 0);
            Menu_AppendItem(menu, mModes[1], mModeB, true, mIndex == 1);
            Menu_AppendItem(menu, mModes[2], mModeC, true, mIndex == 2);
            Menu_AppendItem(menu, mModes[3], mModeD, true, mIndex == 3);
            Menu_AppendItem(menu, mModes[4], mModeE, true, mIndex == 4);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, iModes[0], iModeA, true, iIndex == 0);
            Menu_AppendItem(menu, iModes[1], iModeB, true, iIndex == 1);
            Menu_AppendItem(menu, iModes[2], iModeC, true, iIndex == 2);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, fModes[3], fModeD, true, fIndex == 3);
            Menu_AppendItem(menu, fModes[0], fModeA, true, fIndex == 0);
            Menu_AppendItem(menu, fModes[1], fModeB, true, fIndex == 1);
            Menu_AppendItem(menu, fModes[2], fModeC, true, fIndex == 2);

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

        private void mModeD(Object sender, EventArgs e)
        {
            mIndex = 3;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void mModeE(Object sender, EventArgs e)
        {
            mIndex = 4;

            UpdateMessage();
            ExpireSolution(true);
        }

        //++++++++++++++++++++++++++++++++

        private void iModeA(Object sender, EventArgs e)
        {
            iIndex = 0;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void iModeB(Object sender, EventArgs e)
        {
            iIndex = 1;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void iModeC(Object sender, EventArgs e)
        {
            iIndex = 2;

            UpdateMessage();
            ExpireSolution(true);
        }

        //++++++++++++++++++++++++++++++++

        private void fModeA(Object sender, EventArgs e)
        {
            fIndex = 0;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void fModeB(Object sender, EventArgs e)
        {
            fIndex = 1;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void fModeC(Object sender, EventArgs e)
        {
            fIndex = 2;

            UpdateMessage();
            ExpireSolution(true);
        }

        private void fModeD(Object sender, EventArgs e)
        {
            fIndex = 3;

            UpdateMessage();
            ExpireSolution(true);
        }

        //++++++++++++++++++++++++++++++++

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

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// Adds to the default serialization method to save the current child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("mIndex", mIndex);
            writer.SetInt32("iIndex", iIndex);
            writer.SetInt32("fIndex", fIndex);
            writer.SetBoolean("PerturbA", pA);
            writer.SetBoolean("PerturbB", pB);

            return base.Write(writer);
        }

        /// <summary>
        /// Adds to the default deserialization method to retrieve the saved child status so it persists on copy/paste and save/reopen.
        /// </summary>
        public override bool Read(GH_IReader reader)
        {
            mIndex = reader.GetInt32("mIndex");
            iIndex = reader.GetInt32("iIndex");
            fIndex = reader.GetInt32("fIndex");
            pA = reader.GetBoolean("PerturbA");
            pB = reader.GetBoolean("PerturbB");

            UpdateMessage();
            return base.Read(reader);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void UpdateMessage()
        {
            Message = mModes[mIndex];
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
            get { return new Guid("6ffa6a25-44bd-4899-89b5-73585224d3bf"); }
        }
    }
}