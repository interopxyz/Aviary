using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Geometry.Curves;
using Wind.Geometry.Vectors;
using Wind.Geometry.Curves.Primitives;
using Wind.Containers;

namespace Wind_GH.Geometry
{
    public class MeshToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MeshToShape class.
        /// </summary>
        public MeshToShape()
          : base("MeshToShape", "Mshp", "---", "Aviary", "2D Drawing")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "---", GH_ParamAccess.item);
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
            Mesh M = new Mesh();
            if (!DA.GetData(0, ref M)) return;

            Polyline[] P = M.GetNakedEdges();
            List<wShape> Shape = new List<wShape>();

            foreach (Polyline Pline in P)
            {
                List<wPoint> Pts = new List<wPoint>();
                for(int i = 0; i < Pline.Count;i++)
                {
                    Pts.Add(new wPoint(Pline[i].X, Pline[i].Y, Pline[i].Z));
                }

                wCurve Crv = new wPolyline(Pts,true);
                
                Shape.Add( new wShape(Crv));
           }

            wShapeCollection Shapes = new wShapeCollection(Shape);

            BoundingBox B = M.GetBoundingBox(true);
            wPoint O = new wPoint(B.Center.X, B.Center.Y, B.Center.Z);
            wPlane Pln = new wPlane().XYPlane();
            Pln.Origin = O;
            Shapes.Boundary = new wRectangle(Pln, B.Diagonal.X, B.Diagonal.Y);
            Shapes.Type = "PolylineGroup";

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
                return Properties.Resources.Wind_Shape_Mesh;
            }
        }
        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{34384823-0ab5-444a-a9a1-33b183d88591}"); }
        }
    }
}