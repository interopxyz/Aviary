using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;

using Wind.Types;
using Wind.Containers;

using Parrot.Containers;

using Pollen.Collections;
using Wind.Geometry.Curves;
using Parrot.Controls;
using System.Windows.Media;
using Grasshopper.Kernel.Parameters;
using System.Windows.Forms;
using Grasshopper.GUI.Base;

namespace Wind_GH.Formatting
{
    public class FillPattern : GH_Component
    {
        int PatternModeStatus = 0;
        double PatternWeight = 0.25;
        NumericUpDown DS = new NumericUpDown();


        /// <summary>
        /// Initializes a new instance of the FillPattern class.
        /// </summary>
        public FillPattern()
          : base("Pattern", "Pattern", "---" ,"Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Pattern", "P", "Pattern", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Scale", "S", "Scale", GH_ParamAccess.item,1);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Fore", "F", "---", GH_ParamAccess.item, new wColor().LightGray().ToDrawingColor());
            pManager[3].Optional = true;
            pManager.AddColourParameter("Back", "B", "---", GH_ParamAccess.item, new wColor().VeryLightGray().ToDrawingColor());
            pManager[4].Optional = true;

            Param_Integer param = (Param_Integer)pManager[1];

            param.AddNamedValue("Solid", 0);


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
            double Scale = 1;
            System.Drawing.Color Background = new wColor().VeryLightGray().ToDrawingColor();
            System.Drawing.Color ForeGround = new wColor().LightGray().ToDrawingColor();

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref Shps)) return;
            if (!DA.GetData(2, ref Scale)) return;
            if (!DA.GetData(3, ref ForeGround)) return;
            if (!DA.GetData(4, ref Background)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.Background = new wColor(Background);
            G.Foreground = new wColor(ForeGround);

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
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics.Background = new wColor(Background);
                            tDataPt.Graphics.Foreground = new wColor(ForeGround);
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics.Background = new wColor(Background);
                            tDataSet.Graphics.Foreground = new wColor(ForeGround);
                            W.Element = tDataSet;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;
                    Shapes.Graphics.Background = new wColor(Background);
                    Shapes.Graphics.Foreground = new wColor(ForeGround);

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);

        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            DS.Minimum = 0.00M;
            DS.Maximum = 1.00M;
            DS.Value = (Decimal)PatternWeight;
            DS.DecimalPlaces = 2;
            DS.Increment = 0.01M;

            DS.UpDownAlign = LeftRightAlignment.Left;

            Menu_AppendCustomItem(menu, DS);
            
            DS.ValueChanged -= (o, e) => { UpdateWidthValue(); };
            DS.ValueChanged += (o, e) => { UpdateWidthValue(); };

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Solid", SolidMode, true, (PatternModeStatus == 0));
            Menu_AppendItem(menu, "Custom", CustomMode, true, (PatternModeStatus == 1));
            Menu_AppendItem(menu, "Point", PointMode, true, (PatternModeStatus == 2));
            Menu_AppendItem(menu, "Line", LineMode, true, (PatternModeStatus == 3));
            Menu_AppendItem(menu, "Grid", GridMode, true, (PatternModeStatus == 4));
        }

        private void UpdateWidthValue()
        {
            PatternWeight = (double)DS.Value;
            this.ExpireSolution(true);
        }

        private void SolidMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 0;
            this.ExpireSolution(true);
        }

        private void CustomMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 1;
            this.ExpireSolution(true);
        }

        private void PointMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 2;
            this.ExpireSolution(true);
        }

        private void LineMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 3;
            this.ExpireSolution(true);
        }

        private void GridMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 4;
            this.ExpireSolution(true);
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
                return Properties.Resources.Wind_Pattern;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{0477d5d6-986d-4b19-bc73-c219e6b1d268}"); }
        }
    }
}