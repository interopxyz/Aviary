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
    public class Expander : GH_Component
    { 
        //Stores the instance of each run of the control
    public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

    /// <summary>
    /// Initializes a new instance of the Expander class.
    /// </summary>
    public Expander()
          : base("Expander", "Expand", "---", "Aviary", "Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "---", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "Name", GH_ParamAccess.item, "");
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Closed", "C", "Closed", GH_ParamAccess.item, true);
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

            var pCtrl = new pExpand(name);

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pExpand)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            IGH_Goo element = null;
            string N = "";
            bool B = false;

            if (!DA.GetData(0, ref element)) return;
            if (!DA.GetData(1, ref N)) return;
            if (!DA.GetData(2, ref B)) return;

            wObject W;
            pElement E;
            element.CastTo(out W);
            E = (pElement)W.Element;

            pCtrl.SetProperties(N, B, E);
            
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
                return Properties.Resources.Parrot_Expand;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{d9961a79-86f3-4049-b6c3-6ac06c1a9993}"); }
        }
    }
}