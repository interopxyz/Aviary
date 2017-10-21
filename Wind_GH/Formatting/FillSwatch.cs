using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Wind.Geometry.Curves;
using System.Windows.Media;
using Parrot.Containers;
using Parrot.Controls;
using Pollen.Collections;

namespace Wind_GH.Formatting
{
    public class FillSwatch : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FillSwatch class.
        /// </summary>
        public FillSwatch()
          : base("Swatch", "Swatch", "---", "Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddGenericParameter("Shapes", "S", "Shapes", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Scale", "X", "Scale", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "G", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            IGH_Goo Shps = null;
            double Scale = 1.0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref Shps)) return;
            if (!DA.GetData(2, ref Scale)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            wShapeCollection S = new wShapeCollection();
            if (Shps != null) { Shps.CastTo(out S); }

            DrawingBrush DwgBrush = new DrawingBrush();
            DrawingGroup DwgGroup = new DrawingGroup();

            foreach (wShape Shp in S.Shapes)
            {
                GeometryDrawing dwgG = new GeometryDrawing(new SolidColorBrush(Shp.Graphic.Foreground.ToMediaColor()), new Pen(new SolidColorBrush(Shp.Graphic.StrokeColor.ToMediaColor()), Shp.Graphic.StrokeWeight[0]), Shp.GeometrySet);
                dwgG.Pen.StartLineCap = (PenLineCap)Shp.Graphic.StrokeCap;
                dwgG.Pen.EndLineCap = (PenLineCap)Shp.Graphic.StrokeCap;
                DwgGroup.Children.Add(dwgG);
            }

            DwgGroup.ClipGeometry = new RectangleGeometry(new System.Windows.Rect(0, 0, 300, 300));

            DwgBrush.Drawing = DwgGroup;
            DwgBrush.Viewbox = new System.Windows.Rect(0, 0, 1, 1);
            DwgBrush.Viewport = new System.Windows.Rect(0, 0, Scale, Scale);
            DwgBrush.TileMode = TileMode.Tile;

            G.WpfPattern = DwgBrush;

            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;

                            C.SetPatternFill();
                            break;

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            //tDataPt.Graphics.Background = new wColor(Background);
                            //tDataPt.Graphics.Foreground = new wColor(ForeGround);
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            //tDataSet.Graphics.Background = new wColor(Background);
                            //tDataSet.Graphics.Foreground = new wColor(ForeGround);
                            W.Element = tDataSet;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;
                    //Shapes.Graphics.Background = new wColor(Background);
                    //Shapes.Graphics.Foreground = new wColor(ForeGround);

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
        }

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
                return Properties.Resources.Wind_Fill_Swatch;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a881708f-3358-496e-8c55-a168b2091714"); }
        }
    }
}