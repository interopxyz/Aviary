﻿using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Types;
using Wind.Containers;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.FilterColor;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Build;

namespace Macaw_GH.Filtering.Adjust
{
    public class FilterHSL : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FilterHSL class.
        /// </summary>
        public FilterHSL()
          : base("Filter HSL", "HSL", "...", "Aviary", "Image Edit")
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

            pManager.AddIntervalParameter("Hue", "H", "---", GH_ParamAccess.item, new Interval(0,360));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Saturation", "S", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Luminance", "L", "---", GH_ParamAccess.item, new Interval(0.0, 1.0));
            pManager[3].Optional = true;
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
            Interval H = new Interval(0,360);
            Interval S = new Interval(0, 1.0);
            Interval L = new Interval(0, 1.0);

            // Access the input parameters 
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(1, ref H)) return;
            if (!DA.GetData(2, ref S)) return;
            if (!DA.GetData(3, ref L)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mFilter Filter = new mFilter();

            Filter = new mFilterHSL(new wDomain(H.T0, H.T1), new wDomain(S.T0, S.T1), new wDomain(L.T0, L.T1));

            B = new mApply(A, Filter).ModifiedBitmap;


            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Channels_HSL;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e298e10a-857c-4f73-a49d-98b6eaaa84bb"); }
        }
    }
}