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
using Wind.Types;
using System.Windows.Forms;
using GH_IO.Serialization;

namespace Parrot_GH.Displays
{
    public class Gradient : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        public bool IsHorizontal = false;
        public bool HasTicks = true;
        public bool IsExtents = false;
        public bool IsLight = false;
        public bool IsFlipped = false;
        public int OrientMode = 0;


        /// <summary>
        /// Initializes a new instance of the Gradient class.
        /// </summary>
        public Gradient()
          : base("Gradient", "Gradient", "---", "Aviary", "Dashboard Display")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Colors", "C", "---", GH_ParamAccess.list);
            pManager.AddNumberParameter("Parameters", "T", "---", GH_ParamAccess.list, 0);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Upper Value", "V", "---", GH_ParamAccess.list, " ");
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Width", "W", "---", GH_ParamAccess.item, 20);
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

            List<System.Drawing.Color> G = new List<System.Drawing.Color>(); ;
            List<double> T = new List<double>();
            List<string> V = new List<string>();
            int W = 20;

            if (!DA.GetDataList(0, G)) return;
            if (!DA.GetDataList(1, T)) return;
            if (!DA.GetDataList(2, V)) return;
            if (!DA.GetData(3, ref W)) return;

            List<wColor> H = new List<wColor>();

            for(int i = 0; i<G.Count;i++)
            {
                H.Add(new wColor(G[i]));
            }

            int k = T.Count;
            for (int i = k;i<G.Count;i++)
            {
                T.Add(1.0);
            }

            k = V.Count;
            for (int i = k; i < G.Count; i++)
            {
                V.Add(" ");
            }

            pCtrl.SetProperties(H, T, V, W, IsHorizontal, OrientMode, IsExtents, HasTicks,IsLight,IsFlipped);

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

            Menu_AppendItem(menu, "Above / Right", OrientModeL, true, (OrientMode == 0));
            Menu_AppendItem(menu, "Below / Left", OrientModeR, true, (OrientMode == 1));
            Menu_AppendItem(menu, "Within", OrientModeIn, true, (OrientMode == 2));

            Menu_AppendSeparator(menu);

            Menu_AppendItem(menu, "Horizontal", ModeDirection, true, IsHorizontal);

            Menu_AppendSeparator(menu);
            
            Menu_AppendItem(menu, "Ticks", ModeTicks, true, HasTicks);
            Menu_AppendItem(menu, "Flip", ModeFlip, true, IsFlipped);
            Menu_AppendItem(menu, "Extents", ModeExtents, true, IsExtents);
            Menu_AppendItem(menu, "Lighten", ModeLight, true, IsLight);

        }

        public override bool Write(GH_IWriter writer)
        {
            writer.SetInt32("Orientation", OrientMode);
            writer.SetBoolean("Horizontal", IsHorizontal);
            writer.SetBoolean("Extents", IsExtents);
            writer.SetBoolean("Ticks", HasTicks);
            writer.SetBoolean("Light", IsLight);
            writer.SetBoolean("Flipped", IsFlipped);

            return base.Write(writer);
        }

        public override bool Read(GH_IReader reader)
        {
            OrientMode = reader.GetInt32("Orientation");
            IsHorizontal = reader.GetBoolean("Horizontal");
            IsExtents = reader.GetBoolean("Extents");
            HasTicks = reader.GetBoolean("Ticks");
            IsLight = reader.GetBoolean("Light");
            IsFlipped = reader.GetBoolean("Flipped");

            this.UpdateMessage();

            return base.Read(reader);
        }

        private void ModeExtents(Object sender, EventArgs e)
        {
            IsExtents = !IsExtents;

            this.ExpireSolution(true);
        }
        

        private void ModeLight(Object sender, EventArgs e)
        {
            IsLight = !IsLight;
            
            this.ExpireSolution(true);
        }

        private void ModeFlip(Object sender, EventArgs e)
        {
            IsFlipped = !IsFlipped;

            this.ExpireSolution(true);
        }

        private void ModeTicks(Object sender, EventArgs e)
        {
            HasTicks = !HasTicks;
            
            this.ExpireSolution(true);
        }

        private void ModeDirection(Object sender, EventArgs e)
        {
            IsHorizontal = !IsHorizontal;

            this.UpdateMessage();
            this.ExpireSolution(true);
        }

        private void OrientModeL(Object sender, EventArgs e)
        {
            OrientMode = 0;

            this.ExpireSolution(true);
        }

        private void OrientModeR(Object sender, EventArgs e)
        {
            OrientMode = 1;

            this.ExpireSolution(true);
        }

        private void OrientModeIn(Object sender, EventArgs e)
        {
            OrientMode = 2;

            this.ExpireSolution(true);
        }
        
        private void UpdateMessage()
        {
            if (IsHorizontal) { Message = "Horizontal"; } else { Message = "Vertical"; }
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