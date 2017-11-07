﻿using System;

using Grasshopper.Kernel;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using Macaw.Filtering.Stylized;
using Macaw.Filtering;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Stylize
{
    public class Smooth : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Smooth class.
        /// </summary>
        public Smooth()
          : base("Smooth", "Smooth", "---", "Aviary", "Bitmap Edit")
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
            pManager.AddIntegerParameter("Size", "S", "---", GH_ParamAccess.item, 7);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Space", "X", "---", GH_ParamAccess.item, 10.0);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Factor", "F", "---", GH_ParamAccess.item, 60.0);
            pManager[4].Optional = true;
            pManager.AddNumberParameter("Power", "P", "---", GH_ParamAccess.item, 60.0);
            pManager[5].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Adaptive", 0);
            param.AddNamedValue("Bilateral", 1);
            param.AddNamedValue("Conservative", 2);
            param.AddNamedValue("Mean", 3);
            param.AddNamedValue("Median", 4);
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
            int M = 0;
            int S = 7;
            double X = 10.0;
            double F = 60.0;
            double P = 60.0;

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref S)) return;
            if (!DA.GetData(3, ref X)) return;
            if (!DA.GetData(4, ref F)) return;
            if (!DA.GetData(5, ref P)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mSmoothAdaptive(F);
                    break;
                case 1:
                    Filter = new mSmoothBilateral(X,F,P,S);
                    break;
                case 2:
                    Filter = new mSmoothConservative();
                    break;
                case 3:
                    Filter = new mSmoothMean(S);
                    break;
                case 4:
                    Filter = new mSmoothMedian(S);
                    break;
            }

            B = new mApply(A, Filter).ModifiedBitmap;

            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Filter_Smooth;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4c0b4282-34ed-4050-bff2-531fcab7d219"); }
        }
    }
}