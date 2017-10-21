using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Wind.Containers;
using Macaw.Filtering.Stylized;
using Macaw.Filtering;

namespace Macaw_GH.Filtering.Stylize
{
    public class Streak : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Streak class.
        /// </summary>
        public Streak()
          : base("Streak", "Streak", "---", "Aviary", "Macaw")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 0);
            pManager[0].Optional = true;
            pManager.AddIntegerParameter("Gap", "G", "---", GH_ParamAccess.item, 32);
            pManager[1].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[0];
            param.AddNamedValue("Horizontal", 0);
            param.AddNamedValue("Vertical", 1);
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
            int D = 32;

            // Access the input parameters 
            if (!DA.GetData(0, ref M)) return;
            if (!DA.GetData(1, ref D)) return;

            mFilter Filter = new mFilter();

            switch (M)
            {
                case 0:
                    Filter = new mStreakHorizontal(D);
                    break;
                case 1:
                    Filter = new mStreakVertical(D);
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
                return Properties.Resources.Macaw_Filter_Streaks;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("83f84481-f45d-4952-9d53-333d52056354"); }
        }
    }
}