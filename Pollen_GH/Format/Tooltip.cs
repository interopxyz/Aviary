using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;

namespace Pollen_GH.Format
{
    public class Tooltip : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Tooltip class.
        /// </summary>
        public Tooltip()
          : base("Tooltip", "Tooltip","---", "Aviary", "Charting & Data")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("DataSet", "D", "---", GH_ParamAccess.item);
            pManager.AddTextParameter("Text", "T", "---", GH_ParamAccess.item, "");
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Mode", "M", "---", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
            pManager.AddGenericParameter("Graphic", "G", "---", GH_ParamAccess.item);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[2];
            param.AddNamedValue("None", 0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Tooltip", "T", "Tooltip", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
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
                return Properties.Resources.Pollen_ToolTip;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("91c58635-346a-4f07-bad5-bb4d0e25c3b3"); }
        }
    }
}