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
    public class SliderRange : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();
        public bool boolDirection;
        public bool boolLabel;
        public bool boolTick;

        /// <summary>
        /// Initializes a new instance of the ScrollRange class.
        /// </summary>
        public SliderRange()
          : base("Range Slider", "Slide R", "Parrot Control Element. A graphic slider which sets a low and high value within a numeric domain.", "Aviary", "Control")
        {
            boolDirection = true;
            boolLabel = true;
            boolTick = false;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntervalParameter("Selection", "S", "Domain which sets the initial min and max selected values within the given domain.", GH_ParamAccess.item, new Interval(25, 75));
            pManager[0].Optional = true;
            pManager.AddIntervalParameter("Domain", "D", "Domain which sets the min and max value", GH_ParamAccess.item, new Interval(0, 100));
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Interval", "I", "The step interval value", GH_ParamAccess.item, 0.1);
            pManager[2].Optional = true;
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

            var pCtrl = new pRangeSlider(name);
            pCtrl.SetProperties();
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pRangeSlider)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            Interval Domain = new Interval(0, 100);
            Interval Selection = new Interval(25, 75);
            double I = 1.0;

            if (!DA.GetData(0, ref Selection)) return;
            if (!DA.GetData(1, ref Domain)) return;
            if (!DA.GetData(2, ref I)) return;

            pCtrl.SetValue(Domain.T0, Domain.T1, Selection.T0, Selection.T1, I, boolDirection, boolLabel, boolTick);

            //Set Parrot Element and Wind Object properties
            if (!Active) { Element = new pElement(pCtrl.Element, pCtrl, pCtrl.Type); }
            WindObject = new wObject(Element, "Parrot", Element.Type);
            WindObject.GUID = this.InstanceGuid;
            WindObject.Instance = C;

            Elements[this.RunCount] = WindObject;

            DA.SetData(0, WindObject);

        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);
            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Horizontal", Direction, true, boolDirection);
            Menu_AppendItem(menu, "Label", Label, true, boolLabel);
            Menu_AppendItem(menu, "Tick", Tick, true, boolTick);
        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetBoolean("Horizontal", boolDirection);
            writer.SetBoolean("Label", boolLabel);
            writer.SetBoolean("Tick", boolTick);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            boolDirection = reader.GetBoolean("Horizontal");
            boolLabel = reader.GetBoolean("Label");
            boolTick = reader.GetBoolean("Tick");

            return base.Read(reader);
        }

        private void Direction(Object sender, EventArgs e)
        {
            boolDirection = !boolDirection;
            this.ExpireSolution(true);
        }

        private void Label(Object sender, EventArgs e)
        {
            boolLabel = !boolLabel;
            this.ExpireSolution(true);
        }

        private void Tick(Object sender, EventArgs e)
        {
            boolTick = !boolTick;
            this.ExpireSolution(true);
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
                return Properties.Resources.Parrot_RangeSlider;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{44ce65dc-c4df-4b1f-9f41-9e932a582c33}"); }
        }
    }
}