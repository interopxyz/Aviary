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
using Wind.Graphics;
using GH_IO.Serialization;

namespace Wind_GH.Formatting
{
    public class FillPattern : GH_Component
    {
        public int PatternModeStatus = 0;
        public int SpacingMode = 2;
        public double PatternWeight = 0.5;
        NumericUpDown DS = new NumericUpDown();


        /// <summary>
        /// Initializes a new instance of the FillPattern class.
        /// </summary>
        public FillPattern()
          : base("Pattern", "Pattern", "---" ,"Aviary", "Format")
        {
            this.UpdateMessage();
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
            param.AddNamedValue("Grid", 0);
            param.AddNamedValue("Diamond", 1);
            param.AddNamedValue("Triangular", 2);
            param.AddNamedValue("Hexagonal", 3);
            param.AddNamedValue("Stagger", 4);
            param.AddNamedValue("Checker", 5);
            param.AddNamedValue("Solid Diamond", 6);
            param.AddNamedValue("Trellis", 7);
            param.AddNamedValue("Dots", 8);


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
            int Pattern = 0;
            double Scale = 1;
            System.Drawing.Color Background = new wColor().VeryLightGray().ToDrawingColor();
            System.Drawing.Color ForeGround = new wColor().LightGray().ToDrawingColor();

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref Pattern)) return;
            if (!DA.GetData(2, ref Scale)) return;
            if (!DA.GetData(3, ref ForeGround)) return;
            if (!DA.GetData(4, ref Background)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.FillType = wGraphic.FillTypes.Pattern;

            G.Background = new wColor(Background);
            G.Foreground = new wColor(ForeGround);
            G.StrokeColor = new wColor(ForeGround);

            G.SetUniformStrokeWeight(PatternWeight);

            wShapeCollection S = new wShapeCollection();

            S.Graphics = G;

            wPattern P = new wPattern(0, 0, 9, 9, S);

            if ((PatternModeStatus < 5) && (PatternModeStatus != 0))
            {
                P.SetStroke(Pattern);
            }
            else
            {
                P.SetStroke(0);
            }

            S = P.SetPattern(PatternModeStatus, Pattern, SpacingMode);

            G = S.Graphics;

            wFillSwatch Swatch = new wFillSwatch(S, Scale, 4, S.X, S.Y, S.Width, S.Height);
            

            G.WpfPattern = Swatch.DwgBrush;
            G.WpfFill = Swatch.DwgBrush;

            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;
                    C.Graphics = G;

                    C.SetFill();

                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics.FillType = wGraphic.FillTypes.Pattern;
                            tDataPt.Graphics.Background = new wColor(Background);
                            tDataPt.Graphics.Foreground = new wColor(ForeGround);
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics.FillType = wGraphic.FillTypes.Pattern;
                            tDataSet.Graphics.Background = new wColor(Background);
                            tDataSet.Graphics.Foreground = new wColor(ForeGround);
                            W.Element = tDataSet;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;
                    Shapes.Graphics.FillType = wGraphic.FillTypes.Pattern;
                    Shapes.Graphics.WpfFill = G.WpfFill;
                    Shapes.Graphics.WpfPattern = G.WpfPattern;
                    
                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, G);

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

            Menu_AppendItem(menu, "Narrow", NarrowMode, true, (SpacingMode == 1));
            Menu_AppendItem(menu, "Regular", RegularMode, true, (SpacingMode == 2));
            Menu_AppendItem(menu, "Wide", WideMode, true, (SpacingMode == 3));

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Grid", GridMode, true, (PatternModeStatus == 0));
            Menu_AppendItem(menu, "Horizontal", HorizontalMode, true, (PatternModeStatus == 1));
            Menu_AppendItem(menu, "Vertical", VerticalMode, true, (PatternModeStatus == 2));
            Menu_AppendItem(menu, "Diagonal Left", DiagLMode, true, (PatternModeStatus == 3));
            Menu_AppendItem(menu, "Diagonal Right", DiagRMode, true, (PatternModeStatus == 4));
            Menu_AppendItem(menu, "Percent", PercentMode, true, (PatternModeStatus == 5));
            Menu_AppendItem(menu, "Pattern", PatternMode, true, (PatternModeStatus == 6));
            Menu_AppendItem(menu, "Architectural", ArchMode, true, (PatternModeStatus == 7));
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Size", SpacingMode);
            writer.SetInt32("Pattern", PatternModeStatus);
            writer.SetDouble("Width", PatternWeight);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            SpacingMode = reader.GetInt32("Size");
            PatternModeStatus = reader.GetInt32("Pattern");
            PatternWeight = reader.GetDouble("Width");

            this.UpdateMessage();
            this.ExpireSolution(true);
            return base.Read(reader);
        }

        private void UpdateWidthValue()
        {
            PatternWeight = (double)DS.Value;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void NarrowMode(Object sender, EventArgs e)
        {
            SpacingMode = 1;
            this.ExpireSolution(true);
        }

        private void RegularMode(Object sender, EventArgs e)
        {
            SpacingMode = 2;
            this.ExpireSolution(true);
        }

        private void WideMode(Object sender, EventArgs e)
        {
            SpacingMode = 3;
            this.ExpireSolution(true);
        }
        
        private void GridMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 0;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("Grid", 0);
            param.AddNamedValue("Diamond", 1);
            param.AddNamedValue("Triangular", 2);
            param.AddNamedValue("Hexagonal", 3);
            param.AddNamedValue("Stagger", 4);
            param.AddNamedValue("Checker", 5);
            param.AddNamedValue("Solid Diamond", 6);
            param.AddNamedValue("Trellis", 7);
            param.AddNamedValue("Dots", 8);

            this.ExpireSolution(true);
        }

        private void HorizontalMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 1;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("Solid", 0);
            param.AddNamedValue("Dots", 1);
            param.AddNamedValue("Dashed", 2);
            param.AddNamedValue("Staggerd", 3);

            this.ExpireSolution(true);
        }

        private void VerticalMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 2;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("Solid", 0);
            param.AddNamedValue("Dots", 1);
            param.AddNamedValue("Dashed", 2);
            param.AddNamedValue("Staggerd", 3);

            this.ExpireSolution(true);
        }
        

        private void DiagLMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 3;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("Solid", 0);
            param.AddNamedValue("Dots", 1);
            param.AddNamedValue("Dashed", 2);
            param.AddNamedValue("Staggerd", 3);

            this.ExpireSolution(true);
        }

        private void DiagRMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 4;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("Solid", 0);
            param.AddNamedValue("Dots", 1);
            param.AddNamedValue("Dashed", 2);
            param.AddNamedValue("Staggerd", 3);

            this.ExpireSolution(true);
        }

        private void PercentMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 5;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("10", 0);
            param.AddNamedValue("20", 1);
            param.AddNamedValue("30", 2);
            param.AddNamedValue("40", 3);
            param.AddNamedValue("50", 4);
            param.AddNamedValue("60", 5);
            param.AddNamedValue("70", 6);
            param.AddNamedValue("80", 7);
            param.AddNamedValue("90", 8);


            this.ExpireSolution(true);
        }

        private void PatternMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 6;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("ZigZag", 0);
            param.AddNamedValue("Confetti", 1);
            param.AddNamedValue("Tile", 2);
            param.AddNamedValue("Bamboo", 3);
            param.AddNamedValue("Cross", 4);
            param.AddNamedValue("Scatter", 5);
            param.AddNamedValue("Star", 6);
            param.AddNamedValue("Pinwheel", 7);
            param.AddNamedValue("Rings", 8);
            param.AddNamedValue("Weave", 9);

            this.ExpireSolution(true);
        }

        private void ArchMode(Object sender, EventArgs e)
        {
            PatternModeStatus = 7;

            Param_Integer param = (Param_Integer)this.Params.Input[1];
            param.ClearNamedValues();
            param.AddNamedValue("Steel", 0);
            param.AddNamedValue("Aluminum", 1);
            param.AddNamedValue("Glass", 2);
            param.AddNamedValue("Concrete", 3);
            param.AddNamedValue("Stone", 4);
            param.AddNamedValue("Tile", 5);
            param.AddNamedValue("Wood", 6);
            param.AddNamedValue("Parquet", 7);
            param.AddNamedValue("Earth", 8);
            param.AddNamedValue("Grass", 9);

            this.ExpireSolution(true);
        }


        private void UpdateMessage()
        {
            Message = PatternWeight.ToString();
        }

        
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
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