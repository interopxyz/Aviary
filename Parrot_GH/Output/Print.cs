using System;

using Grasshopper.Kernel;

using System.Drawing;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel.Attributes;
using System.Windows.Forms;
using Grasshopper.Kernel.Types;
using Wind.Containers;
using Parrot.Containers;
using Parrot.Utilities;

namespace Parrot_GH.Output
{
    public class Print : GH_Component
    {
        public bool toggle { get; set; }

        /// <summary>
        /// Initializes a new instance of the Print class.
        /// </summary>
        public Print()
          : base("Print", "Print", "---", "Aviary", "Dashboard Utility")
        {
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
            pManager.AddGenericParameter("Elements", "E", "Elements", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //Set Unique Control Properties
            IGH_Goo E = null;

            if (!DA.GetData(0, ref E)) return;
            
            wObject W;
            pElement Elem;
            E.CastTo(out W);
            Elem = (pElement)W.Element;

            if (toggle) {
                pPrint PrintElement = new pPrint(Elem.Container);
                toggle = false;
            }
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
                Print comp = Owner as Print;

                if (channel == GH_CanvasChannel.Objects)
                {
                    GH_Capsule button = GH_Capsule.CreateTextCapsule(ButtonBounds, ButtonBounds, comp.toggle ? GH_Palette.Grey : GH_Palette.Black, "Invert", 2, 0);
                    button.Render(graphics, Selected, Owner.Locked, false);
                    button.Dispose();
                }
            }

            public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
            {
                Print comp = Owner as Print;
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
                Print comp = Owner as Print;
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

            /// <summary>
            /// Set Exposure level for the component.
            /// </summary>
            public override GH_Exposure Exposure
        {
            get { return GH_Exposure.senary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Print;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{c7851a38-048d-4956-ab19-a250cc418812}"); }
        }
    }
}