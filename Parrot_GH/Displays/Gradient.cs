using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Displays;
using Grasshopper.Kernel.Types;

namespace Parrot_GH.Displays
{
    public class Gradient : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the Gradient class.
        /// </summary>
        public Gradient()
          : base("Gradient", "Gradient", "---", "Aviary", "Display")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Gradient", "G", "Value", GH_ParamAccess.item);
            pManager.AddTextParameter("Min Value", "0", "--", GH_ParamAccess.item, "Min");
            pManager[1].Optional = true;
            pManager.AddTextParameter("Max Value", "1", "---", GH_ParamAccess.item, "Max");
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Horizontal", "H", "---", GH_ParamAccess.item, true);
            pManager[3].Optional = true;
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

            var pCtrl = new pGradient(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pGradient)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            IGH_Goo X = null;
            string L = "Min";
            string H = "Max";
            bool D = true;

            if (!DA.GetData(0, ref X)) return;
            if (!DA.GetData(1, ref L)) return;
            if (!DA.GetData(2, ref H)) return;
            if (!DA.GetData(3, ref D)) return;

            wGraphic G = new wGraphic();
            X.CastTo(out G);

            pCtrl.SetProperties(L, H, G, D);

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
            get { return GH_Exposure.septenary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Gradient;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{0b13f040-ded1-4f27-b29c-07ff392e7633}"); }
        }
    }
}