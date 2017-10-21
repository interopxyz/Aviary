using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Windows;
using Grasshopper.Kernel.Types;

namespace Parrot_GH.Utilities
{
    public class SetTheme : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SetTheme class.
        /// </summary>
        public SetTheme()
          : base("Set Theme", "Theme", "---", "Aviary", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Window", "W", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            IGH_Goo X = null;
            if (!DA.GetData(0, ref X)) return;

            wObject W;
            X.CastTo(out W);
            pWindow E = (pWindow)W.Element;

            

        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("38aa617a-7934-4b53-8819-624da58aff0d"); }
        }
    }
}