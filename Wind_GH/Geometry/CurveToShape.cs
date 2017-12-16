using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Geometry.Curves;
using System.Linq;
using Wind.Geometry.Curves.Primitives;
using Wind.Geometry.Vectors;
using Wind.Containers;
using Wind.Geometry.Curves.Splines;

namespace Wind_GH.Geometry
{
    public class CurveToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CurveToShape class.
        /// </summary>
        public CurveToShape()
          : base("CurveToShape", "Cshp", "---", "Aviary", "2D Drawing")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "C", "Curve", GH_ParamAccess.item);
            pManager.AddNumberParameter("Distance Tolerance", "D", "---", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Kink Tolerance", "K", "---", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
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

            Curve C = new Circle(new Point3d(0,0,0),1).ToNurbsCurve();
            double D = 0;
            double K = 0;
            if (!DA.GetData(0, ref C)) return;
            if (!DA.GetData(1, ref D)) return;
            if (!DA.GetData(2, ref K)) return;

            wCurve Crv = new wCircle(new wPoint(), 1);

            Curve[] Segments = C.DuplicateSegments();

            // Check if is pline
            if (Segments.Count()>1)
            {
                Polyline P = new Polyline();
                if (C.TryGetPolyline(out P))
                {
                    List<wPoint> Pts = new List<wPoint>();
                    for (int i = 0; i < P.Count; i++)
                    {
                        Pts.Add(new wPoint(P[i].X, P[i].Y, P[i].Z));
                    }

                    Crv = new wPolyline(Pts, P.IsClosed);
                }
                else
                {
                    Crv = new RhCrvToWindCrv().ToPiecewiseBezier(C,D,K);
                }
            }
            else
            {
                Crv = new RhCrvToWindCrv(C).WindCurve;
            }

            BoundingBox B = C.GetBoundingBox(true);
            wPoint O = new wPoint(B.Center.X, B.Center.Y, B.Center.Z);

            wShape Shape = new wShape(Crv);
            wShapeCollection Shapes = new wShapeCollection(Shape);
            
            wPlane Pln = new wPlane().XYPlane();
            Pln.Origin = O;

            Shapes.Boundary = new wRectangle(Pln, B.Diagonal.X, B.Diagonal.Y);
            Shapes.Type = Crv.GetCurveType;

            if (C.IsClosed) { Shapes.Graphics = new wGraphic().BlackFill(); } else { Shapes.Graphics = new wGraphic().BlackOutline(); }
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
                return Properties.Resources.Wind_Shape_Curve_04;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{c0ca2123-14d9-4aaf-90a8-133b63f68470}"); }
        }
    }
}