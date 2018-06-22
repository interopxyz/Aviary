using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Grasshopper.Kernel.Types;
using Wind.Geometry.Vectors;
using Wind.Geometry.Curves;
using Wind.Geometry.Curves.Primitives;
using Wind.Containers;

namespace Hoopoe_GH.Wind.Geometry
{
    public class BitmapToShape : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BItmapToShape class.
        /// </summary>
        public BitmapToShape()
          : base("Bitmap to Shape", "BMshp", "---", "Aviary", "2D View")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Bitmap", "B", "---", GH_ParamAccess.item);
            pManager.AddRectangleParameter("Frame", "F", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY,150,150));
            pManager[1].Optional = true;
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
            IGH_Goo Z = null;
            Rectangle3d R = new Rectangle3d(Plane.WorldXY, 150, 150);
            if (!DA.GetData(0, ref Z)) return;
            if (!DA.GetData(0, ref R)) return;

            Bitmap A = null;
            if (Z != null) { Z.CastTo(out A); }

            // Check if is pline
            Curve C = R.ToNurbsCurve();

            BoundingBox B = C.GetBoundingBox(true);
            wPoint O = new wPoint(B.Center.X, B.Center.Y, B.Center.Z);

            wShape Shape = new wShape(new wRectangle());
            wShapeCollection Shapes = new wShapeCollection(Shape);

            wPlane Pln = new wPlane().XYPlane();
            Pln.Origin = O;

            Shapes.Boundary = new wRectangle(Pln, B.Diagonal.X, B.Diagonal.Y);
            //Shapes.Type = Crv.GetCurveType;

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
                return Properties.Resources.Wind_Shape_Bitmap;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("808a05da-6d7b-493d-ae49-9e5dd0f4bb29"); }
        }
    }
}