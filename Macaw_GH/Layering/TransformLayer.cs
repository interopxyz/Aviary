using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Macaw.Compiling;
using System.Drawing;

namespace Macaw_GH.Layering
{
    public class TransformLayer : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the TransformLayer class.
        /// </summary>
        public TransformLayer()
          : base("TransformLayer", "Xform", "---", "Aviary", "Bitmap Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Layer", "L", "---", GH_ParamAccess.item);
            pManager.AddIntegerParameter("X Translation", "X", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Y Translation", "Y", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Angle", "A", "[0-360]", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
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
            int MoveX = 0;
            int MoveY = 0;
            int Angle = 0;

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref MoveX)) return;
            if (!DA.GetData(2, ref MoveY)) return;
            if (!DA.GetData(3, ref Angle)) return;

            wObject Z = new wObject();
            if (X != null) { X.CastTo(out Z); }
            mLayer L = new mLayer((mLayer)Z.Element);
            
            L.SetPosition(MoveX, MoveY);
            L.SetRotation(Angle);
            
            wObject W = new wObject(L, "Macaw", L.Type);


            DA.SetData(0, W);
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
            get { return new Guid("fe78eec2-63d6-4780-82a3-148cb762eef9"); }
        }
    }
}