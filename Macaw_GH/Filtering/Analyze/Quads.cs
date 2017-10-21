using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Analysis;
using Wind.Geometry.Vectors;

namespace Macaw_GH.Filtering.Analyze
{
    public class Quads : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Quads class.
        /// </summary>
        public Quads()
          : base("Figure Quads", "Quads", "---", "Aviary", "Image Build")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager[0].Optional = true;
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new Bitmap(100, 100)));

            pManager.AddIntegerParameter("Difference", "D", "---", GH_ParamAccess.item, 25);
            pManager[1].Optional = true;

            pManager.AddIntegerParameter("Geometry", "G", "---", GH_ParamAccess.item, 18);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Points", "P", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo V = null;
            int D = 25;
            int G = 18;
            Color Y = Color.Red;

            // Access the input parameters 
            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref D)) return;
            if (!DA.GetData(2, ref G)) return;
            if (!DA.GetData(3, ref Y)) return;

            Bitmap A = null;
            if (V != null) { V.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mAnalyzeQuads Corners = new mAnalyzeQuads(A);
            
            List<Point3d> P = new List<Point3d>();

            foreach (wPoint X in Corners.Points)
            {
                P.Add(new Point3d(X.X, X.Y, X.Z));
            }
            
            DA.SetDataList(0, P);
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
            get { return new Guid("0c053377-efe8-4eb5-a054-9fdb7b75c31a"); }
        }
    }
}