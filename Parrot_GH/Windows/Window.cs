using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Types;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Windows;

using Parrot_GH.Utilities;
using System.Windows.Forms;
using System.Windows.Interop;
using GH_IO.Serialization;

namespace Parrot_GH.Windows
{
    public class Window : GH_Component
    {

        int modeStatus = 1;
        pWindow window = new pWindow();
        string ID;
        string name;

        /// <summary>
        /// Initializes a new instance of the Window class.
        /// </summary>
        public Window()
          : base("Window", "Window", "---", "Aviary", "Dashboard Layout")
        {
            this.UpdateMessage();

            ID = this.Attributes.InstanceGuid.ToString();
            name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)),false).Text;
            window = new pWindow(name);
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Elements to Add", GH_ParamAccess.list);
            pManager.AddTextParameter("Title", "T", "...", GH_ParamAccess.item, "");
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Scroll", "S", "---", GH_ParamAccess.item,false);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Launch", "L", "Launch a new Window", GH_ParamAccess.item);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Window", "W", "---", GH_ParamAccess.item);
            pManager.AddGenericParameter("Elements", "E", "---", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool Launch = false;
            string Title = "";
            bool Scroll = false;
            List<IGH_Goo> X = new List<IGH_Goo>();

            if (!DA.GetDataList(0, X)) return;
            if (!DA.GetData(1, ref Title)) return;
            if (!DA.GetData(2, ref Scroll)) return;
            if (!DA.GetData(3, ref Launch)) return;

            if (Launch)
            {
                window.Close();
                window = new pWindow(name);
                window.SetProperties(Title);

                for (int i = 0; i < X.Count; i++)
                {
                    wObject W;
                    pElement E;
                    X[i].CastTo(out W);
                    E = (pElement)W.Element;
                    window.AddElement(E);

                    var cP = Grasshopper.Instances.ActiveCanvas.Document.FindComponent(W.GUID) as Grasshopper.Kernel.IGH_Component;
                    SetComponentWindow S = new SetComponentWindow(cP,W, W.Type, W.Instance);
                }

                window.SetScroll(Scroll);

                WindowInteropHelper H = new WindowInteropHelper(window.Element);

                switch (modeStatus)
                {
                    case 1:
                        window.Element.Topmost = true;
                        break;
                    case 2:
                        H.Owner = Rhino.RhinoApp.MainWindowHandle();
                        break;
                    case 3:
                        H.Owner = Grasshopper.Instances.DocumentEditor.Handle;
                        break;
                    default:
                        window.Element.Topmost = false;
                        break;
                }

                window.Open();

            }

            wObject Object = new wObject(window, "Parrot", window.Type, "Window");

            DA.SetData(0, Object);
            DA.SetDataList(1, X);

        }

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "None", NoneMode, true, (modeStatus == 0));
            Menu_AppendItem(menu, "On Top", TopMode, true, (modeStatus == 1));
            Menu_AppendItem(menu, "Rhino", RhMode, true, (modeStatus == 2));
            Menu_AppendItem(menu, "Grasshopper", GhMode, true, (modeStatus == 3));
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("FilterMode", modeStatus);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            modeStatus = reader.GetInt32("FilterMode");

            this.UpdateMessage();

            return base.Read(reader);
        }
        
        private void NoneMode(Object sender, EventArgs e)
        {
            modeStatus = 0;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void TopMode(Object sender, EventArgs e)
        {
            modeStatus = 1;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void RhMode(Object sender, EventArgs e)
        {
            modeStatus = 2;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void GhMode(Object sender, EventArgs e)
        {
            modeStatus = 3;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void UpdateMessage()
        {
            string[] arrMessage = { "None", "On Top", "Rhino", "Grasshopper" };
            Message = arrMessage[modeStatus];
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
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
                return Properties.Resources.Parrot_Form;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{942e1003-30d6-46e7-99a4-75fce5c95671}"); }
        }
    }
}