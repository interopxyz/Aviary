using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Layouts;


namespace Parrot_GH.Layouts
{
    public class StackPanel : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the StackPanel class.
        /// </summary>
        public StackPanel()
          : base("StackPanel", "Stack", "---", "Aviary", "Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "---", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Horizontal", "H", "---", GH_ParamAccess.item,true);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "---", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string ID = this.Attributes.InstanceGuid.ToString();
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)), true).Text;
            int C = this.RunCount;

            wObject WindObject = new wObject();
            pElement Element = new pElement();
            bool Active = Elements.ContainsKey(C);

            var pCtrl = new pPanelStack(name);

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPanelStack)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties
            List<IGH_Goo> E = new List<IGH_Goo>();
            bool H = true;

            if (!DA.GetDataList(0, E)) return;
            if (!DA.GetData(1, ref H)) return;

            pCtrl.SetProperties(H);

            for (int i = 0; i < E.Count; i++)
            {
                wObject W;
                pElement Elem;
                E[i].CastTo(out W);
                Elem = (pElement)W.Element;

                pCtrl.AddElement(Elem);
            }

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);
            
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
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
                return Properties.Resources.Parrot_Panel_W;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{8db644be-88ba-4712-ac11-1823c9e8b0ab}"); }
        }
    }
}