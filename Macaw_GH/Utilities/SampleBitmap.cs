using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;

namespace Macaw_GH.Utilities
{
    public class SampleBitmap : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SampleBitmap class.
        /// </summary>
        public SampleBitmap()
          : base("Sample Bitmap", "Sample", "---", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);

            pManager.AddVectorParameter("Point", "P", "A unitized point", GH_ParamAccess.list);
            

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Alpha", "A", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Red", "R", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Green", "G", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Blue", "B", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Hue", "H", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Saturation", "S", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Luminance", "L", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            // Declare variables
            IGH_Goo X = null;
            List<Vector3d> P = new List<Vector3d>();

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetDataList(1, P)) return;

            Bitmap Z = null;
            if (X != null) { X.CastTo(out Z); }

            List<Color> C = new List<Color>();

            List<int> A = new List<int>();
            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            List<double> H = new List<double>();
            List<double> S = new List<double>();
            List<double> L = new List<double>();

            mSampleBitmap bmp = new mSampleBitmap(Z);

            foreach (Vector3d V in P)
            {
                Color clr = bmp.Sample(V.X, V.Y);

                C.Add(clr);
                A.Add(clr.A);
                R.Add(clr.R);
                G.Add(clr.G);
                B.Add(clr.B);

                H.Add(clr.GetHue());
                S.Add(clr.GetSaturation());
                L.Add(clr.GetBrightness());
            }

            DA.SetDataList(0, C);

            DA.SetDataList(1, A);
            DA.SetDataList(2, R);
            DA.SetDataList(3, G);
            DA.SetDataList(4, B);

            DA.SetDataList(5, H);
            DA.SetDataList(6, S);
            DA.SetDataList(7, L);

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