using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Geometry.Curves;
using Wind.Containers;
using Wind.Geometry.Vectors;
using Hoopoe.Drawing;
using Hoopoe.Geometry.Primitives;
using Hoopoe.Assembly;

namespace Parrot_GH.Drawings
{
    public class SVGtester : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SVGtester class.
        /// </summary>
        public SVGtester()
          : base("Test SVG", "testSVG", "---", "Aviary", "2D Drawing")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Shapes", "S", "---", GH_ParamAccess.list);
            pManager.AddRectangleParameter("Boundary", "B", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 0, 0));
            pManager[1].Optional = true;
            pManager.AddRectangleParameter("Frame", "F", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 600, 600));
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Clear Drawing", "C", "---", GH_ParamAccess.item, true);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("SVG", "SVG", "SVG", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {


            //Set Unique Control Properties

            List<IGH_Goo> Shps = new List<IGH_Goo>();
            Rectangle3d B = new Rectangle3d(Plane.WorldXY, 0, 0);
            Rectangle3d F = new Rectangle3d(Plane.WorldXY, 600, 600);
            bool D = true;

            if (!DA.GetDataList(0, Shps)) return;
            if (!DA.GetData(1, ref B)) return;
            if (!DA.GetData(2, ref F)) return;
            if (!DA.GetData(3, ref D)) return;

            List<wShapeCollection> Shapes = new List<wShapeCollection>();

            wObject Wx = new wObject();
            wShapeCollection Sx = new wShapeCollection();

            Shps[0].CastTo(out Wx);
            Sx = (wShapeCollection)Wx.Element;

            BoundingBox Box = new BoundingBox(Sx.Boundary.CornerPoints[0].X, Sx.Boundary.CornerPoints[0].Y, 0, Sx.Boundary.CornerPoints[2].X, Sx.Boundary.CornerPoints[2].Y, 0);

            foreach (IGH_Goo Obj in Shps)
            {
                wObject W = new wObject();
                wShapeCollection S = new wShapeCollection();

                Obj.CastTo(out W);
                S = (wShapeCollection)W.Element;
                Shapes.Add(S);

                wPoint PtA = S.Boundary.CornerPoints[0];
                wPoint PtB = S.Boundary.CornerPoints[2];

                Box.Union(new Point3d(PtA.X, PtA.Y, PtA.Z));
                Box.Union(new Point3d(PtB.X, PtB.Y, PtB.Z));
            }

            if ((B.Width == 0.0) & (B.Height == 0.0))
            {
                Plane pln = Plane.WorldXY;
                pln.Origin = Box.Center;
                B = new Rectangle3d(pln,
                    new Interval(-Box.Diagonal.X / 2.0, Box.Diagonal.X / 2.0),
                    new Interval(-Box.Diagonal.Y / 2.0, Box.Diagonal.Y / 2.0));
            }

            wPlane PlnB = new wPlane(
                new wPoint(B.Center.X, B.Center.Y, B.Center.Z),
                new wVector(B.Plane.XAxis.X, B.Plane.XAxis.Y, B.Plane.XAxis.Z),
                new wVector(B.Plane.YAxis.X, B.Plane.YAxis.Y, B.Plane.YAxis.Z));
            wPlane PlnF = new wPlane(
                new wPoint(F.Center.X, F.Center.Y, F.Center.Z),
                new wVector(F.Plane.XAxis.X, F.Plane.XAxis.Y, F.Plane.XAxis.Z),
                new wVector(F.Plane.YAxis.X, F.Plane.YAxis.Y, F.Plane.YAxis.Z));

            CompiledSVG SVGobject = new CompiledSVG();

            SVGobject.SetSize(600, 600);

            foreach (wShapeCollection S in Shapes)
            {
                switch (S.Type)
                {
                    case "PolyCurveGroup":
                        break;
                    case "PolylineGroup":
                        break;
                    default:
                        SVGobject.AddShape(S);
                        break;
                }
            }

            SVGobject.Build();

            DA.SetData(0, SVGobject);


        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Hoopoe_CompileSVG;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("26b01e08-ee61-47fe-b107-8484fd8d5703"); }
        }
    }
}