﻿using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;
using Wind.Containers;
using Macaw.Filtering;

namespace Macaw_GH.Build
{
    public class Apply : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Apply class.
        /// </summary>
        public Apply()
          : base("Apply Filter", "Filter", "---", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
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

            IGH_Goo X = null;
            IGH_Goo Y = null;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref Y)) return;

            Bitmap A = null;
            wObject Z = new wObject();
            mFilter F = new mFilter();

            if (X != null) { X.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            if (Y != null) { Y.CastTo(out Z); }
            F = (mFilter)Z.Element;

            B = new mApply(B, F).ModifiedBitmap;

            DA.SetData(0, B);

        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.senary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Apply_Apply;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d1ee1b60-7ed5-48b9-b9ed-04099b4b9ec7"); }
        }
    }
}