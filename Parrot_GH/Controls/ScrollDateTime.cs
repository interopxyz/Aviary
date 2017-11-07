using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Controls;

namespace Parrot_GH.Controls
{
    public class ScrollDateTime : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the PickDateTime class.
        /// </summary>
        public ScrollDateTime()
          : base("Scroll Date & Time", "D&T", "---", "Aviary", "Dashboard Control")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTimeParameter("DateTime", "D", "The default value of the text box", GH_ParamAccess.item, DateTime.Now);
            pManager[0].Optional = true;
            pManager.AddIntegerParameter("Format", "F", "Format", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Custom Format", "C", "---", GH_ParamAccess.item, "dddd, MMMM dd, yyyy ( hh:mm:ss tt )");
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Custom", 0);
            param.AddNamedValue("Monday, June 15, 2009 (01:00:00 PM)", 1);
            param.AddNamedValue("06/15/2009, 01:00:00 PM", 2);
            param.AddNamedValue("2009-06-15, 13:00:00", 3);
            param.AddNamedValue("Monday, June 15, 2009", 4);
            param.AddNamedValue("06/15/2009", 5);
            param.AddNamedValue("2009-06-15", 6);
            param.AddNamedValue("1:45 PM", 7);
            param.AddNamedValue("1:45:30 PM", 8);
            param.AddNamedValue("13:45:30", 9);
            
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

            var pCtrl = new pDateTime(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pDateTime)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties
            
            DateTime D = DateTime.Now;
            string F = "dddd, MMMM dd, yyyy ( hh:mm:ss tt )";
            int M = 0;

            if (!DA.GetData(0, ref D)) return;
            if (!DA.GetData(1, ref M)) return;
            if (!DA.GetData(2, ref F)) return;

            pCtrl.SetDate(D, M, F);


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
            get { return GH_Exposure.secondary; }
        }
        
        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Parrot_DateTime;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{430d358a-3897-4f03-91f5-2f8e44f72aea}"); }
        }
    }
}