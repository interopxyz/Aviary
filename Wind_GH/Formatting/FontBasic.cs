using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;

using Wind.Types;
using Wind.Containers;

using Parrot.Containers;

using Pollen.Collections;
using Wind.Geometry.Curves;
using Parrot.Controls;
using System.Windows.Forms;
using GH_IO.Serialization;
using Wind.Utilities;
using Parrot.Displays;
using Pollen.Charts;

namespace Wind_GH.Formatting
{
    public class FontBasic : GH_Component
    {
        int hJustify = 0;
        int vJustify = 0;
        bool IsBold = false;
        bool IsItalic = false;
        bool IsUnder = false;

        /// <summary>
        /// Initializes a new instance of the FontBasic class.
        /// </summary>
        public FontBasic()
          : base("Font", "Font", "---", "Aviary", "2D Format")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "---", GH_ParamAccess.item, "Arial");
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Size", "S", "---", GH_ParamAccess.item, 8);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Color", "C", "---", GH_ParamAccess.item, System.Drawing.Color.Black);
            pManager[3].Optional = true;
            
            Param_GenericObject paramGen = (Param_GenericObject)Params.Input[0];
            paramGen.PersistentData.Append(new GH_ObjectWrapper(new pSpacer(new GUIDtoAlpha(Convert.ToString(this.Attributes.InstanceGuid.ToString() + Convert.ToString(this.RunCount)), false).Text)));
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Font", "F", "Font Object", GH_ParamAccess.item);
            pManager.AddGenericParameter("Graphics", "G", "Graphics Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Goo Element = null;
            string N = "Arial";
            double S = 8;
            System.Drawing.Color X = System.Drawing.Color.Black;
            int J = vJustify*3+hJustify;
            bool B = IsBold;
            bool I = IsItalic;
            bool U = IsUnder;

            if (!DA.GetData(0, ref Element)) return;
            if (!DA.GetData(1, ref N)) return;
            if (!DA.GetData(2, ref S)) return;
            if (!DA.GetData(3, ref X)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            wFont F = new wFont(N, S, new wColor(X), (wFontBase.Justification)J, B, I, U, false);
            G.FontObject = F;
            G.CustomFonts += 1;

            W.Graphics = G;

            switch (W.Type)
            {
                case "Parrot":
                    pElement E = (pElement)W.Element;
                    pControl C = (pControl)E.ParrotControl;

                    C.Graphics = G;
                    C.SetFont();
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

                            tDataSet.TotalCustomTitles += 1;

                            W.Element = tDataSet;

                            break;
                        case "Chart":
                        case "Table":
                            pElement pE = (pElement)W.Element;
                            pChart pC = pE.PollenControl;
                            pC.Graphics = G;

                            pC.SetFont();

                            pE.PollenControl = pC;
                            W.Element = pE;
                            break;
                    }
                    break;
                case "Hoopoe":
                    wShapeCollection Shapes = (wShapeCollection)W.Element;

                    Shapes.Fonts = F;

                    W.Element = Shapes;
                    break;
            }

            DA.SetData(0, W);
            DA.SetData(1, G.FontObject);
            DA.SetData(2, G);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);
            
            Menu_AppendItem(menu, "Left", SetLeft, true, (hJustify == 0));
            Menu_AppendItem(menu, "Center", SetCenter, true, (hJustify == 1));
            Menu_AppendItem(menu, "Right", SetRight, true, (hJustify == 2));

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Top", SetTop, true, (vJustify == 0));
            Menu_AppendItem(menu, "Middle", SetMiddle, true, (vJustify == 1));
            Menu_AppendItem(menu, "Bottom", SetBottom, true, (vJustify == 2));

            Menu_AppendSeparator(menu);
            Menu_AppendItem(menu, "Bold", modeBold, true, IsBold);
            Menu_AppendItem(menu, "Italic", modeItalic, true, IsItalic);
            Menu_AppendItem(menu, "Underline", modeUnder, true, IsUnder);

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Vertical", hJustify);
            writer.SetInt32("Horizontal", vJustify);
            writer.SetBoolean("Bold", IsBold);
            writer.SetBoolean("Italic", IsItalic);
            writer.SetBoolean("Under", IsUnder);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            hJustify = reader.GetInt32("Vertical");
            vJustify = reader.GetInt32("Horizontal");
            IsBold = reader.GetBoolean("Bold");
            IsItalic = reader.GetBoolean("Italic");
            IsUnder = reader.GetBoolean("Under");
            
            return base.Read(reader);
        }

        private void SetLeft(Object sender, EventArgs e)
        {
            hJustify = 0;
            
            this.ExpireSolution(true);
        }

        private void SetCenter(Object sender, EventArgs e)
        {
            hJustify = 1;

            this.ExpireSolution(true);
        }

        private void SetRight(Object sender, EventArgs e)
        {
            hJustify = 2;

            this.ExpireSolution(true);
        }

        private void SetTop(Object sender, EventArgs e)
        {
            vJustify = 0;

            this.ExpireSolution(true);
        }

        private void SetMiddle(Object sender, EventArgs e)
        {
            vJustify = 1;

            this.ExpireSolution(true);
        }

        private void SetBottom(Object sender, EventArgs e)
        {
            vJustify = 2;

            this.ExpireSolution(true);
        }

        private void modeBold(Object sender, EventArgs e)
        {
            IsBold = !IsBold;

            this.ExpireSolution(true);
        }

        private void modeItalic(Object sender, EventArgs e)
        {
            IsItalic = !IsItalic;

            this.ExpireSolution(true);
        }

        private void modeUnder(Object sender, EventArgs e)
        {
            IsUnder = !IsUnder;

            this.ExpireSolution(true);
        }



        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Wind_Font;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("9b3b5f44-bdeb-4491-9ea9-5770bd40e81b"); }
        }
    }
}