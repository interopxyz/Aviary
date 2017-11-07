using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Geometry.Curves;
using Wind.Geometry.Vectors;
using Wind.Geometry.Curves.Primitives;
using Wind.Containers;
using Wind.Geometry.Curves.Splines;
using System.Linq;

namespace Wind_GH.Geometry
{
    public class BrepToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BrepToShape class.
        /// </summary>
        public BrepToShape()
          : base("BrepToShape", "Bshp", "---", "Aviary", "2D Drawing")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBrepParameter("Brep", "B", "Brep", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Shape", "SH", "Shape", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Brep B = new Brep();
            if (!DA.GetData(0, ref B)) return;

            Curve[] C = B.DuplicateNakedEdgeCurves(true, true);
            C = Curve.JoinCurves(C);

            wShapeCollection Shapes = new wShapeCollection();
            
            foreach (Curve Crv in C)
            {
                Shapes.Shapes.Add(new wShape(new RhCrvToWindCrv().ToPiecewiseBezier(Crv)));
            }
            
            BoundingBox X = B.GetBoundingBox(true);
            wPoint O = new wPoint(X.Center.X, X.Center.Y, X.Center.Z);
            wPlane Pln = new wPlane().XYPlane();
            Pln.Origin = O;
            Shapes.Boundary = new wRectangle(Pln, X.Diagonal.X, X.Diagonal.Y);
            Shapes.Type = "PolyCurveGroup";

            Shapes.Graphics = new wGraphic().BlackFill();

            wObject WindObject = new wObject(Shapes, "Hoopoe", Shapes.Type);

            DA.SetData(0, WindObject);

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
                return Properties.Resources.Wind_Shape_Surface_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{69c25257-ac1a-4cf6-8624-f97c6424d20b}"); }
        }
    }
}