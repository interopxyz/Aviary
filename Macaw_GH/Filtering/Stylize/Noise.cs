using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using Wind.Types;
using Macaw.Filtering;
using Macaw.Filtering.Stylized;

namespace Macaw_GH.Filtering.Stylize
{
    public class Noise : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Noise class.
        /// </summary>
        public Noise()
          : base("Noise", "Noise", "---", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[0].Optional = true;
            pManager.AddIntervalParameter("Domain", "D", "---", GH_ParamAccess.item, new Interval(-50,50));
            pManager[1].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue("Additive", 0);
            param.AddNamedValue("Salt & Pepper", 1);
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

            int M = 0;
            Interval D = new Interval(-50,50);

            // Access the input parameters 
            if (!DA.GetData(0, ref M)) return;
            if (!DA.GetData(1, ref D)) return;

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mNoiseAdditive(new wDomain(D.T0,D.T1));
                    break;
                case 1:
                    Filter = new mNoiseSandP(D.T1);
                    break;
            }


            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Filter_Grain;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("279262c5-a7f3-4d2f-bba5-c90ad79b0e43"); }
        }
    }
}