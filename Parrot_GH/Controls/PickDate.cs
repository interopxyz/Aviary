using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Controls;

using MahApps.Metro.Controls;

namespace Parrot_GH.Controls
{
    public class PickDate : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the PickDate class.
        /// </summary>
        public PickDate()
          : base("Pick Date", "Date", "---", "Aviary", "Control")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTimeParameter("DateTime", "D", "The default value of the text box", GH_ParamAccess.item, DateTime.Now);
            pManager[0].Optional = true;
            pManager.AddBooleanParameter("Long", "L", "Long", GH_ParamAccess.item, false);
            pManager[1].Optional = true;
            
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

            var pCtrl = new pPickDate(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPickDate)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            DateTime D = DateTime.Now;
            bool F = false;


            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref F)) return;

            pCtrl.SetProperties(D, F);
            
            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type,1); }
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
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_Date;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{272e8bc3-a5e7-482d-8fa1-e1db15574263}"); }
        }
    }
}