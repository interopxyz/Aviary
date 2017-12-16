using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Controls;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Parrot_GH.Controls
{
    public class ScrollNumber : GH_Component
    {

        public bool BoolCycle;

        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the ScrollNumber class.
        /// </summary>
        public ScrollNumber()
          : base("Scroll Number", "Numeric", "Parrot Control Element. Numeric field and graphic spinner which allows for stepping up and down through a numeric domain by a given increment.", "Aviary", "Dashboard Control")
        {
            this.UpdateMessage();
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Value", "V", "---", GH_ParamAccess.item, 0);
            pManager[0].Optional = true;
            pManager.AddIntervalParameter("Domain", "D", "Domain which sets the min and max value.", GH_ParamAccess.item, new Interval(0, 1));
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Interval", "I", "The step interval value.", GH_ParamAccess.item, 0.1);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Parrot WPF Control Element", GH_ParamAccess.item);
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

            var pCtrl = new pScrollNumber(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                if (Elements[C] != null)
                {
                    WindObject = Elements[C];
                    Element = (pElement)WindObject.Element;
                    pCtrl = (pScrollNumber)Element.ParrotControl;
                }
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            double V = 0.0;
            Interval D = new Interval(0, 1);
            double I = 0.1;

            if (!DA.GetData(0, ref V)) return;
            if (!DA.GetData(1, ref D)) return;
            if (!DA.GetData(2, ref I)) return;

            pCtrl.SetProperties(V, D.T0, D.T1, I);


            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);

        }
        
        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Cycle", SetCycle, true, BoolCycle);
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetBoolean("Cycle", BoolCycle);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            BoolCycle = reader.GetBoolean("Cycle");

            this.UpdateMessage();
            return base.Read(reader);
        }

        private void SetCycle(Object sender, EventArgs e)
        {
            BoolCycle = !BoolCycle;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void UpdateMessage()
        {
            if (BoolCycle) { Message = "Cycle"; } else { Message = "Limit"; } 
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
                return Properties.Resources.Parrot_NumericScoller;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{e622729d-043d-46ff-8381-a66170c28982}"); }
        }
    }
}