using System;

using Grasshopper.Kernel;
using Wind.Containers;
using Grasshopper.Kernel.Parameters;
using Macaw.Filtering;
using Macaw.Filtering.Adjustments;

namespace Macaw_GH.Filtering.Adjust
{
    public class Normalize : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Normalize class.
        /// </summary>
        public Normalize()
          : base("Normalize", "Normal", "---", "Aviary", "Macaw")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[0].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue("Extent", 0);
            param.AddNamedValue("Histogram", 1);
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

            // Access the input parameters 
            if (!DA.GetData(0, ref M)) return;

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mNormalizeExtents();
                    break;
                case 1:
                    Filter = new mNormalizeHistogram();
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
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Filter_Normalize;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("abb0bd2e-d401-4ed2-900b-1751b394b4c7"); }
        }
    }
}