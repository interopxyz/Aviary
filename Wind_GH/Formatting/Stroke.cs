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

namespace Wind_GH.Formatting
{
  public class Stroke : GH_Component
  {
        int CapMode = 0;

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
            pManager.AddIntegerParameter("Pattern", "P", "---", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[3];
            param.AddNamedValue("Solid", 0);
            param.AddNamedValue("Dashed", 1);
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
            int P = 0;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref B)) return;
            if (!DA.GetData(2, ref T)) return;
            if (!DA.GetData(3, ref P)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            G.StrokeColor = new wColor(B);
            G.StrokeWeight[0] = T;
            G.StrokeWeight[1] = T;
            G.StrokeWeight[2] = T;
            G.StrokeWeight[3] = T;

            G.StrokeCap = (wGraphic.StrokeCaps)CapMode;

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
                            tDataPt.Graphics.StrokeColor = new wColor(B);
                            tDataPt.Graphics.StrokeWeight[0] = T;
                            tDataPt.Graphics.StrokeWeight[1] = T;
                            tDataPt.Graphics.StrokeWeight[2] = T;
                            tDataPt.Graphics.StrokeWeight[3] = T;
                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics.StrokeColor = new wColor(B);
                            tDataSet.Graphics.StrokeWeight[0] = T;
                            tDataSet.Graphics.StrokeWeight[1] = T;
                            tDataSet.Graphics.StrokeWeight[2] = T;
                            tDataSet.Graphics.StrokeWeight[3] = T;
                            W.Element = tDataSet;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;
                    Shapes.Graphics.StrokeColor = new wColor(B);
                    Shapes.Graphics.StrokeWeight[0] = T;
                    Shapes.Graphics.StrokeWeight[1] = T;
                    Shapes.Graphics.StrokeWeight[2] = T;
                    Shapes.Graphics.StrokeWeight[3] = T;

                    W.Element = Shapes;
                    break;

            }

            DA.SetData(0, W);
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

            Menu_AppendTextItem(menu, "Corners", null, null, false);
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
            get { return GH_Exposure.primary; }
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