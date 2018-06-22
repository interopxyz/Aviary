using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Utilities;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Drawings;
using Grasshopper.Kernel.Types;
using Wind.Geometry.Curves;
using Wind.Geometry.Vectors;
using Wind.Geometry.Curves.Primitives;
using Hoopoe.SVG.Drawing;
using System.Windows.Forms;
using GH_IO.Serialization;
using System.Text;

namespace Hoopoe_GH.Build
{
    public class ComposeSVG : GH_Component
    {
        Dictionary<string, StringBuilder> SavedPathSets = new Dictionary<string, StringBuilder>();
        BoundingBox SavedBox = new BoundingBox();

        int QualityType = 3;

        /// <summary>
        /// Initializes a new instance of the ComposeSVG class.
        /// </summary>
        public ComposeSVG()
          : base("Compose SVG", "SVG", "---", "Aviary", "2D View")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Shapes", "S", "---", GH_ParamAccess.list);
            pManager.AddRectangleParameter("Crop Rectangle", "C", "---", GH_ParamAccess.item, new Rectangle3d(Plane.WorldXY, 0, 0));
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Frame", "F", "---", GH_ParamAccess.item, new Interval(600, 600));
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Reset Drawing", "R", "---", GH_ParamAccess.item, true);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("SVG Object", "SVG", "SVG", GH_ParamAccess.item);
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

            BoundingBox bBox = new BoundingBox();

            if (D) { SavedBox = new BoundingBox(); } else { bBox = SavedBox; }
            bBox.Union(new BoundingBox(Sx.Boundary.CornerPoints[0].X, Sx.Boundary.CornerPoints[0].Y, 0, Sx.Boundary.CornerPoints[2].X, Sx.Boundary.CornerPoints[2].Y, 0));

            SavedBox = bBox;

            foreach (IGH_Goo Obj in Shps)
            {
                wObject W = new wObject();
                wShapeCollection S = new wShapeCollection();

                Obj.CastTo(out W);
                S = (wShapeCollection)W.Element;
                Shapes.Add(S);

                wPoint PtA = S.Boundary.CornerPoints[0];
                wPoint PtB = S.Boundary.CornerPoints[2];

                bBox.Union(new Point3d(PtA.X, PtA.Y, PtA.Z));
                bBox.Union(new Point3d(PtB.X, PtB.Y, PtB.Z));
            }
            
            if ((B.Width == 0.0) & (B.Height == 0.0))
            {
                Plane pln = Plane.WorldXY;
                pln.Origin = bBox.Center;
                B = new Rectangle3d(pln,
                    new Interval(-bBox.Diagonal.X / 2.0, bBox.Diagonal.X / 2.0),
                    new Interval(-bBox.Diagonal.Y / 2.0, bBox.Diagonal.Y / 2.0));
            }

            wPlane PlnB = new wPlane(
                new wPoint(B.Center.X, B.Center.Y, B.Center.Z),
                new wVector(B.Plane.XAxis.X, B.Plane.XAxis.Y, B.Plane.XAxis.Z),
                new wVector(B.Plane.YAxis.X, B.Plane.YAxis.Y, B.Plane.YAxis.Z));
            wPlane PlnF = new wPlane(
                new wPoint(F.Center.X, F.Center.Y, F.Center.Z),
                new wVector(F.Plane.XAxis.X, F.Plane.XAxis.Y, F.Plane.XAxis.Z),
                new wVector(F.Plane.YAxis.X, F.Plane.YAxis.Y, F.Plane.YAxis.Z));
            
            CompileSVG SVGobject = new CompileSVG();

            double X = F.Width / B.Width;
            double Y = F.Height / B.Height;
            double Z = 1.0;
            if (X < Y) { Z = X; }else { Z = Y; }
            

            SVGobject.SetSize((int)F.Width, (int)F.Height, new wRectangle(PlnB,B.Width,B.Height), Z);
            SVGobject.SetQuality(QualityType);

            if (D){SavedPathSets.Clear();}else{SVGobject.LoadPaths(SavedPathSets);}
            
            int i = 0;
            foreach (wShapeCollection S in Shapes)
            {
                SVGobject.SetShapeType(S,i);
                i += 1;
            }

            SavedPathSets = SVGobject.PathSet;

            if (SVGobject.FrameCount > 0) { SVGobject.SetFrames(); } else { SVGobject.SetGroups(); }
            
            
            SVGobject.Build();

            DA.SetData(0, SVGobject);


        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Auto", QualityAuto, true, (QualityType == 0));
            Menu_AppendItem(menu, "Speed", QualitySpeed, true, (QualityType == 1));
            Menu_AppendItem(menu, "Clarity", QualityClarity, true, (QualityType == 2));
            Menu_AppendItem(menu, "Precision", QualityPrecision, true, (QualityType == 3));

        }
        
        private void QualityAuto(Object sender, EventArgs e)
        {
            QualityType = 0;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void QualitySpeed(Object sender, EventArgs e)
        {
            QualityType = 1;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void QualityClarity(Object sender, EventArgs e)
        {
            QualityType = 2;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void QualityPrecision(Object sender, EventArgs e)
        {
            QualityType = 3;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("QualityType", QualityType);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            QualityType = reader.GetInt32("QualityType");

            return base.Read(reader);
        }

        private void UpdateMessage()
        {
            string[] arrMessage = { "Quality: Auto", "Quality: Speed", "Quality: Clarity", "Quality: Precision" };
            Message = arrMessage[QualityType];
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
                return Properties.Resources.Hoopoe_SVG;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5e6c9d6f-bb0c-4272-a945-a8daae25e38c"); }
        }
    }
}