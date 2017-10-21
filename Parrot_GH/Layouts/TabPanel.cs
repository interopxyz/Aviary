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
    public class TabPanel : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the TabPanel class.
        /// </summary>
        public TabPanel()
          : base("TabPanel", "Tabs", "---", "Aviary", "Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Elements", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Index", "I", "Index", GH_ParamAccess.list, 0);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Names", "N", "Names", GH_ParamAccess.list);
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

            var pCtrl = new pPanelTabs(name);

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPanelTabs)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            List<string> T = new List<string>();
            List<IGH_Goo> X = new List<IGH_Goo>();
            List<int> I = new List<int>();

            // Access the input parameters 
            if (!DA.GetDataList(0, X)) return;
            if (!DA.GetDataList(1, I)) return;
            if (!DA.GetDataList(2, T)) return;

            pCtrl.SetProperties();

            for (int i = 0; i < T.Count; i++)
            {
                pCtrl.AddTab(T[i]);
            }

            wObject W;
            pElement E;

            for (int i = 0; i < X.Count; i++)
            {
                X[i].CastTo(out W);
                E = (pElement)W.Element;

                pCtrl.AddElement(I[i], E);

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
                return Properties.Resources.Parrot_Tabs_W;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{a35b3130-27ca-4b7c-938d-a3f73aae5191}"); }
        }
    }
}