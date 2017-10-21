using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;
using Wind.Types;

using Parrot.Containers;
using Parrot.Displays;
using System.Windows.Forms;

namespace Parrot_GH.Displays
{
    public class Label : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();
        wGraphic Graphic = new wGraphic();
        int FontMode;
        bool IsCentered = false;
        int CenterMode = 0;

        /// <summary>
        /// Initializes a new instance of the Label class.
        /// </summary>
        public Label()
          : base("Label", "Label", "---", "Aviary", "Display")
        {
            Graphic.FontObject = new wFont("Swis721 Lt BT", 24, new wColor().DarkGray(), CenterMode, false, false, false, false);
            FontMode = 0;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Text", "T", "The Text", GH_ParamAccess.item, "");
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "WPF Control Element", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string ID = this.Attributes.InstanceGuid.ToString();
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)), false).Text;
            int C = this.RunCount;

            wObject WindObject = new wObject();
            pElement Element = new pElement();
            bool Active = Elements.ContainsKey(C);

            var pCtrl = new pLabel(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pLabel)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            string Text = "";

            if (!DA.GetData(0, ref Text)) return;
            
            pCtrl.SetProperties(Text);
            pCtrl.Graphics = Graphic;
            pCtrl.SetFont();
            
            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }

            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            WindObject.Graphics = Graphic;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);
        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Center", ModeCenter, true, IsCentered);
            
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Bold", ModeBold, true, (FontMode == 0));
            Menu_AppendItem(menu, "Title", ModeTitle, true, (FontMode == 1));
            Menu_AppendItem(menu, "Subtitle", ModeSubtitle, true, (FontMode == 2));
            Menu_AppendItem(menu, "Text", ModeText, true, (FontMode == 3));
            Menu_AppendItem(menu, "Subtext", ModeSubtext, true, (FontMode == 4));

        }

        private void ModeCenter(Object sender, EventArgs e)
        {
            IsCentered = !IsCentered;
            if (IsCentered) { CenterMode = 1; } else { CenterMode = 0; }
            Graphic.FontObject.Justify = CenterMode;
            this.ExpireSolution(true);
        }

        private void ModeBold(Object sender, EventArgs e)
        {
            Graphic.FontObject = new wFont("Swis721 Lt BT", 18, new wColor().DarkGray(), CenterMode, true, false, false, false);
            FontMode = 0;
            this.ExpireSolution(true);
        }

        private void ModeTitle(Object sender, EventArgs e)
        {
            Graphic.FontObject = new wFont("Swis721 Lt BT", 24, new wColor().DarkGray(), CenterMode, false, false, false, false);
            FontMode = 1;
            this.ExpireSolution(true);
        }

        private void ModeSubtitle(Object sender, EventArgs e)
        {
            Graphic.FontObject = new wFont("Swis721 Lt BT", 16, new wColor().DarkGray(), CenterMode, false, false, false, false);
            FontMode = 2;
            this.ExpireSolution(true);
        }

        private void ModeText(Object sender, EventArgs e)
        {
            Graphic.FontObject = new wFont("Swis721 Lt BT", 10, new wColor().DarkGray(), CenterMode, false, false, false, false);
            FontMode = 3;
            this.ExpireSolution(true);
        }

        private void ModeSubtext(Object sender, EventArgs e)
        {
            Graphic.FontObject = new wFont("Swis721 Lt BT", 8, new wColor().DarkGray(), CenterMode, false, false, false, false);
            FontMode = 4;
            this.ExpireSolution(true);
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.septenary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Label_LG;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{ccba1711-e265-43e5-9f09-dfaff5085545}"); }
        }
    }
}