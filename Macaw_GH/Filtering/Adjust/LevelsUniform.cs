using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Containers;
using Wind.Types;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments.AdjustColor;

namespace Macaw_GH.Filtering.Adjust
{
    public class LevelsUniform : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the LevelsUniform class.
        /// </summary>
        public LevelsUniform()
          : base("Levels Uniform", "Level", "...", "Aviary", "Macaw")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntervalParameter("Gray In", "I", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[0].Optional = true;
            pManager.AddIntervalParameter("Gray Out", "O", "---", GH_ParamAccess.item, new Interval(0, 255));
            pManager[1].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Filter", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables

            Interval Ga = new Interval(0, 255);
            Interval Gb = new Interval(0, 255);

            // Access the input parameters 
            if (!DA.GetData(0, ref Ga)) return;
            if (!DA.GetData(1, ref Gb)) return;

            mFilter Filter = new mFilter();

            Filter = new mAdjustLevelsGray(new wDomain(Ga.T0, Ga.T1), new wDomain(Gb.T0, Gb.T1));


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
                return Properties.Resources.Macaw_Filter_Levels;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("9d8c5fb0-7a3d-4460-8be4-96d94bb2882f"); }
        }
    }
}