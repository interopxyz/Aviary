using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;
using System.Drawing;
using Macaw.Analysis;
using Wind.Types;

namespace Macaw_GH.Filtering.Analyze
{
    public class Blobs : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Blobs class.
        /// </summary>
        public Blobs()
          : base("Figure Bounds", "Bounds", "---", "Aviary", "Image Build")
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

            pManager.AddIntervalParameter("Width", "W", "---", GH_ParamAccess.item, new Interval(0, 20));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Height", "H", "---", GH_ParamAccess.item, new Interval(0, 20));
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Boundaries", "R", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables
            IGH_Goo V = null;
            Interval X = new Interval(0, 20);
            Interval Y = new Interval(0, 20);

            // Access the input parameters 
            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref X)) return;
            if (!DA.GetData(2, ref Y)) return;

            Bitmap A = null;
            if (V != null) { V.CastTo(out A); }
            Bitmap B = new Bitmap(A);

            mAnalyzeBlobs Figures = new mAnalyzeBlobs(A,new wDomain(X.T0,X.T1),new wDomain(Y.T0,Y.T1));

            List<Rectangle3d> P = new List<Rectangle3d>();

            foreach (Rectangle Z in Figures.ExtractBoundaries())
            {
                P.Add(new Rectangle3d(new Plane(new Point3d(Z.X, B.Height - Z.Y,0),Vector3d.XAxis,-Vector3d.YAxis),Z.Width,Z.Height));
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
                return Properties.Resources.Macaw_Object_Boundary;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("08c115ad-4b67-43dc-be25-bc82f421fe36"); }
        }
    }
}