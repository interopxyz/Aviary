using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Wind.Containers;
using Wind.Utilities;
using Parrot.Containers;
using Parrot.Layouts;
using Grasshopper.Kernel.Types;

namespace Parrot_GH.Layouts
{
    public class FrameScroll : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ScrollPanel class.
        /// </summary>
        public FrameScroll()
          : base("Scroll Panel", "Scroll", "---", "Aviary", "Dashboard Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "---", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Horizontal", "H", "---", GH_ParamAccess.item, true);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Vertical", "V", "---", GH_ParamAccess.item, true);
            pManager[2].Optional = true;
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

            var pCtrl = new pPanelScroll(name);
            
            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPanelScroll)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties
            IGH_Goo E = null;
            bool H = true;
            bool V = true;

            if (!DA.GetData(0, ref E)) return;
            if (!DA.GetData(1, ref H)) return;
            if (!DA.GetData(2, ref V)) return;

            wObject W;
            pElement Elem;
            E.CastTo(out W);
            Elem = (pElement)W.Element;

            pCtrl.SetProperties(H, V, false);

            pCtrl.SetElement(Elem);

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
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Scroll_Panel2;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5be21db9-5d3d-46b6-8672-72fd9a4f8d75"); }
        }
    }
}