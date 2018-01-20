using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;

using Wind.Containers;
using Wind.Types;

using Parrot.Containers;
using Pollen.Collections;
using Wind.Geometry.Curves;
using Parrot.Controls;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Wind_GH.Formatting
{
    public class Stroke : GH_Component
    {
        int CapMode = 0;
        int CornerMode = 0;
        int PatternMode = 0;

        /// <summary>
        /// Initializes a new instance of the Stroke class.
        /// </summary>
        public Stroke()
      : base("Stroke", "Stroke", "---", "Aviary", "Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Wind Objects", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Weight", "W", "---", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Pattern", "P", "---", GH_ParamAccess.list, 0);
            pManager[3].Optional = true;

            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(null));

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
            System.Drawing.Color B = System.Drawing.Color.Black;
            double T = 1;
            List<double> P = new List<double>();

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref B)) return;
            if (!DA.GetData(2, ref T)) return;
            if (!DA.GetDataList(3, P)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.StrokeColor = new wColor(B);
            G.SetUniformStrokeWeight(T);

            G.StrokeCap = (wGraphic.StrokeCaps)CapMode;
            G.StrokeCorner = (wGraphic.StrokeCorners)CornerMode;

            switch (PatternMode)
            {
                case 0:
                    if ((P.Count == 1) && (P[0] == 0)) { P = new List<double> { 1, 0 }; }
                    break;
                case 1:
                    P = new List<double> { 2, 3 };
                    break;
                case 2:
                    P = new List<double> { 5 };
                    break;
                case 3:
                    P = new List<double> { 15, 10 };
                    break;
                case 4:
                    P = new List<double> { 0.5, 2 };
                    break;
                case 5:
                    P = new List<double> { 30, 5, 10, 5, };
                    break;
            }


            List<double> SP = new List<double>();

            foreach (double PV in P)
            {
                SP.Add(PV / T);
            }

            G.StrokePattern = SP.ToArray();
            G.CustomStrokes += 1;

            W.Graphics = G;



            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;

                    C.Graphics = G;
                    C.SetStroke();
                    break;
                case "Pollen":
                    switch (W.SubType)
                    {
                        case "DataPoint":
                            DataPt tDataPt = (DataPt)W.Element;
                            tDataPt.Graphics = G;

                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics = G;

                            W.Element = tDataSet;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;
                    Shapes.Graphics.StrokeColor = G.StrokeColor;
                    Shapes.Graphics.StrokeWeight = G.StrokeWeight;


                    Shapes.Graphics.StrokePattern = G.StrokePattern;

                    Shapes.Graphics.StrokeCap = G.StrokeCap;
                    Shapes.Graphics.StrokeCorner = G.StrokeCorner;

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

            Menu_AppendItem(menu, "Flat", CapFlatMode, true, (CapMode == 0));
            Menu_AppendItem(menu, "Square", CapSquareMode, true, (CapMode == 1));
            Menu_AppendItem(menu, "Round", CapRoundMode, true, (CapMode == 2));
            Menu_AppendItem(menu, "Triangle", CapTriangleMode, true, (CapMode == 3));

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Bevel", CornerBevelMode, true, (CornerMode == 0));
            Menu_AppendItem(menu, "Mitre", CornerMitreMode, true, (CornerMode == 1));
            Menu_AppendItem(menu, "Round", CornerRoundMode, true, (CornerMode == 2));

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Custom", PatternCustomMode, true, (PatternMode == 0));
            Menu_AppendItem(menu, "Dot", PatternDotMode, true, (PatternMode == 1));
            Menu_AppendItem(menu, "Hidden", PatternHiddenMode, true, (PatternMode == 2));
            Menu_AppendItem(menu, "Dash", PatternDashMode, true, (PatternMode == 3));
            Menu_AppendItem(menu, "Hash", PatternHashMode, true, (PatternMode == 4));
            Menu_AppendItem(menu, "Center", PatternCenterMode, true, (PatternMode == 5));

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Cap", CapMode);
            writer.SetInt32("Corner", CornerMode);
            writer.SetInt32("Pattern", PatternMode);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            CapMode = reader.GetInt32("Cap");
            CornerMode = reader.GetInt32("Corner");
            PatternMode = reader.GetInt32("Pattern");

            return base.Read(reader);
        }

        private void PatternCustomMode(Object sender, EventArgs e)
        {
            PatternMode = 0;
            this.ExpireSolution(true);
        }

        private void PatternDotMode(Object sender, EventArgs e)
        {
            PatternMode = 1;
            this.ExpireSolution(true);
        }

        private void PatternHiddenMode(Object sender, EventArgs e)
        {
            PatternMode = 2;
            this.ExpireSolution(true);
        }

        private void PatternDashMode(Object sender, EventArgs e)
        {
            PatternMode = 3;
            this.ExpireSolution(true);
        }

        private void PatternHashMode(Object sender, EventArgs e)
        {
            PatternMode = 4;
            this.ExpireSolution(true);
        }

        private void PatternCenterMode(Object sender, EventArgs e)
        {
            PatternMode = 5;
            this.ExpireSolution(true);
        }


        private void CornerBevelMode(Object sender, EventArgs e)
        {
            CornerMode = 0;
            this.ExpireSolution(true);
        }

        private void CornerMitreMode(Object sender, EventArgs e)
        {
            CornerMode = 1;
            this.ExpireSolution(true);
        }

        private void CornerRoundMode(Object sender, EventArgs e)
        {
            CornerMode = 2;
            this.ExpireSolution(true);
        }

        private void CapFlatMode(Object sender, EventArgs e)
        {
            CapMode = 0;
            this.ExpireSolution(true);
        }

        private void CapSquareMode(Object sender, EventArgs e)
        {
            CapMode = 1;
            this.ExpireSolution(true);
        }

        private void CapRoundMode(Object sender, EventArgs e)
        {
            CapMode = 2;
            this.ExpireSolution(true);
        }

        private void CapTriangleMode(Object sender, EventArgs e)
        {
            CapMode = 3;
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
                return Properties.Resources.Parrot_Stroke;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{9f8fc9d1-0f9d-4d30-bd12-9cc5aab36b00}"); }
        }
    }
}