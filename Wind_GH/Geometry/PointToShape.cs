using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Geometry.Vectors;
using Wind.Geometry.Curves;
using Wind.Geometry.Curves.Primitives;

namespace Wind_GH.Geometry
{
    public class PointToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the PointToShape class.
        /// </summary>
        public PointToShape()
          : base("Point To Shape", "Pshp", "---", "Aviary", "Shape")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "P", "---", GH_ParamAccess.item);
            pManager.AddNumberParameter("Radius", "R", "---", GH_ParamAccess.item,1);
            pManager[0].Optional = true;
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

            Point3d P = new Point3d(0,0,0);
            double R = 1.0;
            if (!DA.GetData(0, ref P)) return;
            if (!DA.GetData(1, ref R)) return;

            wPoint O = new wPoint(P.X, P.Y, P.Z);
            wCurve Crv = new wCircle(O, R);

            wShape Shape = new wShape(Crv);
            wShapeCollection Shapes = new wShapeCollection(Shape);

            wPlane Pln = new wPlane().XYPlane();
            Pln.Origin = O;

            Shapes.Boundary = new wRectangle(Pln, R, R);
            Shapes.Type = Crv.GetCurveType;

            Shapes.Graphics = new wGraphic().BlackFill();
            Shapes.Effects = new wEffects();
            

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
                return Properties.Resources.Wind_Shape_Point;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{cbd8b520-c4a6-405e-9b22-3a3bf6898a54}"); }
        }
    }
}