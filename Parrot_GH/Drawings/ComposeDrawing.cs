using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;
using Parrot.Containers;
using Parrot.Drawings;
using Grasshopper.Kernel.Types;
using Wind.Geometry.Curves;
using Wind.Geometry.Vectors;
using Wind.Geometry.Curves.Primitives;

namespace Parrot_GH.Drawings
{
    public class ComposeDrawing : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ComposeDrawing class.
        /// </summary>
        public ComposeDrawing()
          : base("Compose Drawing", "Drawing", "---", "Aviary", "2D Drawing")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Shapes", "S", "---", GH_ParamAccess.list);
            pManager.AddRectangleParameter("Crop Rectangle", "C", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 0, 0));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Frame", "F", "---", GH_ParamAccess.item, new Interval(600,600));
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Reset Drawing", "R", "---", GH_ParamAccess.item, true);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "WPF Control Element", GH_ParamAccess.item);
            pManager.AddGenericParameter("Geometry Group", "G", "Geometry Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bitmap", "B", "Raster Image of Drawing", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            string ID = this.Attributes.InstanceGuid.ToString();
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)), false).Text;
            int C = this.RunCount;

            wObject WindObject = new wObject();
            pElement Element = new pElement();
            bool Active = Elements.ContainsKey(C);

            var pCtrl = new pDrawing(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pDrawing)Element.ParrotControl;
            }
            else
            {
                pCtrl.SetProperties();
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            List<IGH_Goo> Shps = new List<IGH_Goo>();
            Rectangle3d B = new Rectangle3d(Plane.WorldXY, 0, 0);
            Interval Fx = new Interval(600, 600);
            bool D = true;

            if (!DA.GetDataList(0, Shps)) return;
            if (!DA.GetData(1, ref B)) return;
            if (!DA.GetData(2, ref Fx)) return;
            if (!DA.GetData(3, ref D)) return;

            List<wShapeCollection> Shapes = new List<wShapeCollection>();

            Rectangle3d F = new Rectangle3d(Plane.WorldXY, Fx.T0, Fx.T1);

            wObject Wx = new wObject();
            wShapeCollection Sx = new wShapeCollection();

            Shps[0].CastTo(out Wx);
            Sx = (wShapeCollection)Wx.Element;

            BoundingBox Box = new BoundingBox(Sx.Boundary.CornerPoints[0].X, Sx.Boundary.CornerPoints[0].Y,0, Sx.Boundary.CornerPoints[2].X, Sx.Boundary.CornerPoints[2].Y,0);

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

            pCtrl.SetCanvasSize(new wRectangle(PlnF, F.Width, F.Height), new wRectangle(PlnB, B.Width, B.Height));

            pCtrl.Graphics.Width = F.Width;
            pCtrl.Graphics.Height = F.Height;

            pCtrl.group.Width = (int)F.Width;
            pCtrl.group.Height = (int)F.Height;

            pCtrl.SetSize();

            pCtrl.SetScale();
            if (D) { pCtrl.ClearDrawing(); }

            foreach (wShapeCollection S in Shapes)
            {
                switch (S.Type)
                {
                    case "PolyCurveGroup":
                        pCtrl.AddPolySpline(S);
                        break;
                    case "PolylineGroup":
                        pCtrl.AddPolyFigure(S);
                        break;
                    default:
                        pCtrl.AddShape(S);
                        break;
                }
            }

            pCtrl.SetCanvas();

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);
            DA.SetData(1, pCtrl.group);
            DA.SetData(2, pCtrl.GetBitmap());
            
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
                return Properties.Resources.Parrot_Drawing;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{e3412071-98e3-4c3b-aa54-7dfea9358139}"); }
        }
    }
}