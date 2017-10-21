using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Macaw.Build;
using Macaw.Filtering;

namespace Macaw_GH.Build
{
    public class Sequence : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Sequence class.
        /// </summary>
        public Sequence()
          : base("Sequence Filters", "Sequence", "---", "Aviary", "Image Edit")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Filters", "F", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Filters", "F", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables

            List<IGH_Goo> X = new List<IGH_Goo>();
            
            // Access the input parameters 
            if (!DA.GetDataList(0, X)) return;

            mFilter Filter = new mFilter();
            mSequence Sequence = new mSequence();

            Sequence.ClearSequence();

            for (int i = 0; i < X.Count; i++)
            {
                wObject Y = new wObject();
                mFilter F = new mFilter();
                X[i].CastTo(out Y);
                F = (mFilter)Y.Element;

                Sequence.AddFilter(F);
            }

            Sequence.SetBitmapType();

            Filter = Sequence;

            wObject W = new wObject(Filter, "Macaw", Filter.Type);


            DA.SetData(0, W);
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
                return Properties.Resources.Macaw_Apply_Sequence;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("23fcfca5-5286-4ade-92ef-2a5ae9535f2c"); }
        }
    }
}