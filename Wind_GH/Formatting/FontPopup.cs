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
using Grasshopper.Kernel.Attributes;
using System.Drawing;
using Grasshopper.GUI.Canvas;
using Grasshopper.GUI;

namespace Wind_GH.Formatting
{
    public class FontPopup : GH_Component
    {
        public bool toggle { get; set; }
        public FontDialog GetFont = new FontDialog();

        public string fName = "Arial";
        public double fSize = 8;

        public int hJustify = 0;
        public int vJustify = 0;

        public bool IsBold = false;
        public bool IsItalic = false;
        public bool IsUnder = false;

        Color DrawColor = Color.Black;

        /// <summary>
        /// Initializes a new instance of the FontDialog class.
        /// </summary>
        public FontPopup()
      : base("Font Popup", "Font", "---", "Aviary", "Format")
        {
            GetFont = new FontDialog();
            GetFont.ShowColor = true;
            GetFont.ShowApply = true;
            GetFont.ShowEffects = true;
            GetFont.AllowScriptChange = false;
        }

        public override void CreateAttributes()
        {
            m_attributes = new Attributes_Custom(this);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Object", "O", "Updated Wind Object", GH_ParamAccess.item);
            
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
            int J = vJustify * 3 + hJustify;

            if (!DA.GetData(0, ref Element)) return;

            wObject W = new wObject();
            if (Element != null) { Element.CastTo(out W); }
            wGraphic G = W.Graphics;

            GetFont.Font = new wFont(fName, fSize, new wColor(DrawColor), (wFontBase.Justification)J, IsBold, IsItalic, IsUnder, false).ToDrawingFont().FontObject;
            GetFont.Color = DrawColor;

            if (toggle)
            {

                GetFont.ShowDialog();

                fName = GetFont.Font.Name;
                fSize = GetFont.Font.Size;

                IsBold = GetFont.Font.Bold;
                IsItalic = GetFont.Font.Italic;
                IsUnder = GetFont.Font.Underline;

                DrawColor = GetFont.Color;

                toggle = false;

            }

            wFont F = new wFont(fName, fSize, new wColor(DrawColor), (wFontBase.Justification)J, IsBold, IsItalic, IsUnder, false);
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

                            W.Element = tDataSet;

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
            DA.SetData(1, G);
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

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Vertical", hJustify);
            writer.SetInt32("Horizontal", vJustify);

            writer.SetBoolean("Bold", IsBold);
            writer.SetBoolean("Italic", IsItalic);
            writer.SetBoolean("Under", IsUnder);

            writer.SetDrawingColor("Color", DrawColor);

            writer.SetString("FontName", fName);
            writer.SetDouble("FontSize", fSize);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            hJustify = reader.GetInt32("Vertical");
            vJustify = reader.GetInt32("Horizontal");

            IsBold = reader.GetBoolean("Bold");
            IsItalic = reader.GetBoolean("Italic");
            IsUnder = reader.GetBoolean("Under");

            DrawColor = reader.GetDrawingColor("Color");

            fName = reader.GetString("FontName");
            fSize = reader.GetDouble("FontSize");

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



        public class Attributes_Custom : GH_ComponentAttributes
        {
            public Attributes_Custom(GH_Component owner) : base(owner) { }
            private Rectangle ButtonBounds { get; set; }

            protected override void Layout()
            {
                base.Layout();
                int len = 22;

                Rectangle rec0 = GH_Convert.ToRectangle(Bounds);
                rec0.Height += len;

                Rectangle rec1 = rec0;
                rec1.Y = rec1.Bottom - len;
                rec1.Height = len;
                rec1.Inflate(-2, -2);

                Bounds = rec0;
                ButtonBounds = rec1;
            }

            protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
            {
                base.Render(canvas, graphics, channel);
                FontPopup comp = Owner as FontPopup;

                if (channel == GH_CanvasChannel.Objects)
                {
                    GH_Capsule button = GH_Capsule.CreateTextCapsule(ButtonBounds, ButtonBounds, comp.toggle ? GH_Palette.Grey : GH_Palette.Black, "Set Font", 2, 0);
                    button.Render(graphics, Selected, Owner.Locked, false);
                    button.Dispose();
                }
            }

            public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
            {
                FontPopup comp = Owner as FontPopup;
                if (e.Button == MouseButtons.Left)
                {
                    RectangleF rec = ButtonBounds;
                    if (rec.Contains(e.CanvasLocation))
                    {
                        comp.RecordUndoEvent("Toggled True");
                        comp.toggle = true;

                        comp.ExpireSolution(true);
                        return GH_ObjectResponse.Handled;
                    }
                }
                return base.RespondToMouseDown(sender, e);
            }

            public override GH_ObjectResponse RespondToMouseUp(GH_Canvas sender, GH_CanvasMouseEvent e)
            {
                FontPopup comp = Owner as FontPopup;
                if (e.Button == MouseButtons.Left)
                {
                    RectangleF rec = ButtonBounds;
                    if (rec.Contains(e.CanvasLocation))
                    {
                        comp.RecordUndoEvent("Toggled False");
                        comp.toggle = false;

                        comp.ExpireSolution(true);
                        return GH_ObjectResponse.Handled;
                    }
                }
                return base.RespondToMouseUp(sender, e);
            }

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
                return Properties.Resources.Wind_Font_Simple;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("827dc52f-b739-49d9-b3ab-e72a662bb503"); }
        }
    }
}