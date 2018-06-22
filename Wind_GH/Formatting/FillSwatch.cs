
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
using GH_IO.Serialization;
using Grasshopper.Kernel.Parameters;
using Wind.Utilities;
using Parrot.Displays;
using Pollen.Charts;

namespace Wind_GH.Formatting
{
    public class FillSwatch : GH_Component
    {
        public int TilingMode = 4;

        /// <summary>
        /// Initializes a new instance of the FillSwatch class.
        /// </summary>
        public FillSwatch()
          : base("Swatch", "Swatch", "---", "Aviary", "2D Format")
        {
            this.UpdateMessage();
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
            
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new pSpacer(new GUIDtoAlpha(Convert.ToString(this.Attributes.InstanceGuid.ToString() + Convert.ToString(this.RunCount)), false).Text)));
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

            wFillSwatch Swatch = new wFillSwatch(S, Scale, TilingMode, S.X, S.Y, S.Width, S.Height);

            G.FillType = wGraphic.FillTypes.Pattern;
            G.WpfPattern = Swatch.DwgBrush;
            G.WpfFill = Swatch.DwgBrush;
            G.CustomFills +=1;

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
                            tDataPt.Graphics = G;

                            tDataPt.Graphics.WpfFill = G.WpfFill;
                            tDataPt.Graphics.WpfPattern = G.WpfPattern;

                            W.Element = tDataPt;
                            break;
                        case "DataSet":
                            DataSetCollection tDataSet = (DataSetCollection)W.Element;
                            tDataSet.Graphics = G;

                            tDataSet.Graphics.WpfFill = G.WpfFill;
                            tDataSet.Graphics.WpfPattern = G.WpfPattern;

                            W.Element = tDataSet;
                            break;
                        case "Chart":
                        case "Table":

                            pElement pE = (pElement)W.Element;
                            pChart pC = pE.PollenControl;
                            pC.Graphics = G;

                            pC.Graphics.WpfFill = G.WpfFill;
                            pC.Graphics.WpfPattern = G.WpfPattern;

                            pC.SetPatternFill();

                            pE.PollenControl = pC;
                            W.Element = pE;
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

            Menu_AppendItem(menu, "Tile", SetTile, true, (TilingMode == 4));
            Menu_AppendItem(menu, "FlipX", SetFlipX, true, (TilingMode == 1));
            Menu_AppendItem(menu, "FlipY", SetFlipY, true, (TilingMode == 2));
            Menu_AppendItem(menu, "FlipXY", SetFlipXY, true, (TilingMode == 3));
        }
        
        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("TileMode", TilingMode);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            TilingMode = reader.GetInt32("TileMode");

            this.UpdateMessage();

            return base.Read(reader);
        }

        private void SetTile(Object sender, EventArgs e)
        {
            TilingMode = 4;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void SetFlipX(Object sender, EventArgs e)
        {
            TilingMode = 1;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void SetFlipY(Object sender, EventArgs e)
        {
            TilingMode = 2;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void SetFlipXY(Object sender, EventArgs e)
        {
            TilingMode = 3;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void UpdateMessage()
        {
            string[] arrMessage = { "0", "Flip X", "Flip Y", "Flip XY", "Tile" };
            Message = arrMessage[TilingMode];
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