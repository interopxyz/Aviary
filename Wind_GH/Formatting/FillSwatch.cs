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
using Wind.Graphics;
using System.Windows.Forms;
using Wind.Types;

namespace Wind_GH.Formatting
{
    public class FillSwatch : GH_Component
    {

        int TileTypeMode = 4;

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

            wFillSwatch Swatch = new wFillSwatch(S, Scale, TileTypeMode, S.Width, S.Height);

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
                    Shapes.Graphics.WpfFill = G.WpfFill;

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
        }

        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Tile", SetTile, true, (TileTypeMode == 4));
            Menu_AppendItem(menu, "FlipX", SetFlipX, true, (TileTypeMode == 1));
            Menu_AppendItem(menu, "FlipY", SetFlipY, true, (TileTypeMode == 2));
            Menu_AppendItem(menu, "FlipXY", SetFlipXY, true, (TileTypeMode == 3));
        }

        private void SetTile(Object sender, EventArgs e)
        {
            TileTypeMode = 4;
            this.ExpireSolution(true);
        }

        private void SetFlipX(Object sender, EventArgs e)
        {
            TileTypeMode = 1;
            this.ExpireSolution(true);
        }

        private void SetFlipY(Object sender, EventArgs e)
        {
            TileTypeMode = 2;
            this.ExpireSolution(true);
        }

        private void SetFlipXY(Object sender, EventArgs e)
        {
            TileTypeMode = 3;
            this.ExpireSolution(true);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Swatches;
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