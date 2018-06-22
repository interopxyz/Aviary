using System;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Wind.Containers;
using Macaw.Compiling;
using System.Collections.Generic;

namespace Macaw_GH.Compose
{
    public class Composition : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Composition class.
        /// </summary>
        public Composition()
          : base("Stack Layers", "Stack", "---", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layers", "L", "---", GH_ParamAccess.list);

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
            List<IGH_Goo> X = new List<IGH_Goo>();

            // Access the input parameters 
            if (!DA.GetDataList(0, X)) return;

            List<mLayer> Layers = new List<mLayer>();
            foreach(IGH_Goo InputObject in X)
            {
                wObject Z = new wObject();
                if (InputObject != null) { InputObject.CastTo(out Z); }
                mLayer LayerObject = new mLayer((mLayer)Z.Element);

                Layers.Add(LayerObject);
            }

            Bitmap C = null;

            mComposition CompositionObject = new mComposition(Layers);

            CompositionObject.BuildComposition();

            C = CompositionObject.CompositionBitmap;

            //wObject W = new wObject(CompositionObject, "Macaw", CompositionObject.Type);


            DA.SetData(0, C);
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
                return Properties.Resources.Macaw_Build_Compose;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3236fc56-bbf3-42b7-80c2-e928874973e4"); }
        }
    }
}