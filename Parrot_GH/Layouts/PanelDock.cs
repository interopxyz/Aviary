using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.Kernel.Parameters;

using Wind.Containers;
using Wind.Utilities;

using Parrot.Containers;
using Parrot.Layouts;
using Grasshopper.Kernel.Types;

namespace Parrot_GH.Layouts
{
    public class PanelDock : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the DockPanel class.
        /// </summary>
        public PanelDock()
          : base("DockPanel", "Dock", "---", "Aviary", "Dashboard Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Elements", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Dock Direction", "D", "Dock Direction", GH_ParamAccess.list, 0);
            pManager[1].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[1];
            param.AddNamedValue("Top", 0);
            param.AddNamedValue("Bottom", 1);
            param.AddNamedValue("Left", 2);
            param.AddNamedValue("Right", 3);
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
            string name = new GUIDtoAlpha(Convert.ToString(ID + Convert.ToString(this.RunCount)), false).Text;
            int C = this.RunCount;

            wObject WindObject = new wObject();
            pElement Element = new pElement();
            bool Active = Elements.ContainsKey(C);

            var pCtrl = new pPanelDock(name);

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPanelDock)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties
            List<IGH_Goo> E = new List<IGH_Goo>();
            List<int> D = new List<int>();

            if (!DA.GetDataList(0, E)) return;
            if (!DA.GetDataList(1, D)) return;

            pCtrl.SetProperties();

            wObject W;
            pElement Elem;

            for (int i = D.Count;i<E.Count;i++)
            {
                D.Add(D[D.Count - 1]);
            }

            for (int i = 0; i < E.Count; i++)
            {
                E[i].CastTo(out W);
                Elem = (pElement)W.Element;

                pCtrl.AddElements(Elem,D[i]);
            }

            E[E.Count - 1].CastTo(out W);
            Elem = (pElement)W.Element;
            pCtrl.LastElement(Elem);

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
                return Properties.Resources.Parrot_Dock_W;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3d36d811-c70c-4b2a-8834-e7ccedc64feb}"); }
        }
    }
}