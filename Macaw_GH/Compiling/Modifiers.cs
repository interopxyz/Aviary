using System;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System.Collections.Generic;
using Wind.Containers;
using Macaw.Compiling;

namespace Macaw_GH.Compose
{
    public class Modifiers : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Filters class.
        /// </summary>
        public Modifiers()
          : base("Layer Modifiers", "Modify", "---", "Aviary", "Image Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Modifiers", "M", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo X = null;
            List<IGH_Goo> Y = new List<IGH_Goo>();

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetDataList(1, Y)) return;

            
            wObject U = new wObject();

            if (X != null) { X.CastTo(out U); }
            mLayer layer = (mLayer)U.Element;

            layer.ClearModifiers();

            foreach (IGH_Goo InputObject in Y)
            {
                wObject Z = new wObject();
                if (InputObject != null) { InputObject.CastTo(out Z); }
                mModifiers Modifer = (mModifiers)Z.Element;

                layer.AddModifiers(Modifer);

            }

            wObject W = new wObject(layer, "Macaw", layer.Type);


            DA.SetData(0, W);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Macaw_Build_Modifiers;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("063c542c-cfa2-466f-b108-1bda03b17cd4"); }
        }
    }
}