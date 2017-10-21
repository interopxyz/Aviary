using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Displays;

namespace Parrot_GH.Displays
{
    public class Legend : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the Legend class.
        /// </summary>
        public Legend()
          : base("Legend", "Legend", "---", "Aviary", "Display")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Value", "V", "Value", GH_ParamAccess.list,"");
            pManager.AddColourParameter("Colors", "C", "Colors", GH_ParamAccess.list, System.Drawing.Color.Black);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Horizontal", "H", "---", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Size", "S", "Size", GH_ParamAccess.item, 0);
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

            var pCtrl = new pLegend(name);
            if (Elements.ContainsKey(C)) { Active = true; }

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pLegend)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties

            List<string> V = new List<string>();
            List<System.Drawing.Color> E = new List<System.Drawing.Color>();
            int X = 0;
            bool D = false;
            E.Add(System.Drawing.Color.Black);

            if (!DA.GetDataList(0, V)) return;
            if (!DA.GetDataList(1, E)) return;
            if (!DA.GetData(2, ref D)) return;
            if (!DA.GetData(3, ref X)) return;

            if (E.Count < 1){E.Add(System.Drawing.Color.Black);}

            int A = E.Count;
            int B = V.Count ;

            for (int i = A; i< V.Count; i++)
            {
                E.Add(E[A-1]);
            }

            pCtrl.SetDirection(X, D);
            pCtrl.SetItems(V, E);

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
                return Properties.Resources.Parrot_Legend;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{435ff529-1a96-49fb-9b62-d9f16d869d78}"); }
        }
    }
}