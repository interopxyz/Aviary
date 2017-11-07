using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Utilities;

namespace Macaw_GH.Build
{
    public class BuildBitmap : GH_Component
    {

        /// <summary>
        /// Initializes a new instance of the BuildBitmap class.
        /// </summary>
        public BuildBitmap()
          : base("Build Bitmap", "Build", "---", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            List<int> Astart = new List<int>();
            Astart.Add(255);

            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Height", "H", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Alpha", "A", "---", GH_ParamAccess.list, Astart);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Red", "R", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Green", "G", "---", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Blue", "B", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            int W = 10;
            int H = 10;
            List<int> A = new List<int>();
            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            // Access the input parameters 
            if (!DA.GetData(0, ref W)) return;
            if (!DA.GetData(1, ref H)) return;
            if (!DA.GetDataList(2, A)) return;
            if (!DA.GetDataList(3, R)) return;
            if (!DA.GetDataList(4, G)) return;
            if (!DA.GetDataList(5, B)) return;

            mBuildBitmap bmp = new mBuildBitmap();

            if (A.Count<2)
            { 
                bmp = new mBuildBitmap(W, H, R, G, B);
            }
            else
            {
                bmp = new mBuildBitmap(W, H, A, R, G, B);
            }

            DA.SetData(0, bmp.OutputBitmap);
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
                return Properties.Resources.Macaw_Build_Build;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c83a6593-250e-43c1-ad85-7ca6d2184549"); }
        }
    }
}