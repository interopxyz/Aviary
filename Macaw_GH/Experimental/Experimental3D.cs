using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Compiling;
using Wind.Containers;
using Wind.Geometry.Meshes;
using Macaw.Experimental;

namespace Macaw_GH.Experimental
{
    public class Experimental3D : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Experimental3D class.
        /// </summary>
        public Experimental3D()
          : base("Experimental 3D", "Experimental 3D", "---", "Aviary", "Image Composition")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mesh", "M", "---", GH_ParamAccess.item);
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

            // Access the input parameters 
            if (!DA.GetData(0, ref X)) return;

            wMesh M = new wMesh();
            X.CastTo(out M);

            x3Dviewer LayerObject = new x3Dviewer(M);

            DA.SetData(0, LayerObject.OutputBitmap);
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
                return Properties.Resources.Macaw_Filter_Adjust;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2a01e258-f722-4cb6-90e3-5a8ab098378b"); }
        }
    }
}