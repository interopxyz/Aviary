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
    public class PanelPlace : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the PlacementPanel class.
        /// </summary>
        public PanelPlace()
          : base("Placement Panel", "Place", "---", "Aviary", "Dashboard Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "---", GH_ParamAccess.list);
            pManager.AddIntervalParameter("Size", "S", "---", GH_ParamAccess.item, new Interval(600,600));
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Horizontal Location", "X", "---", GH_ParamAccess.list, new List<int>());
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Vertical Location", "Y", "---", GH_ParamAccess.list, new List<int>());
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Parrot WPF Layout Element", GH_ParamAccess.item);
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

            var pCtrl = new pPanelPlace(name);
            

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPanelPlace)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }

            //Set Unique Control Properties
            List<IGH_Goo> E = new List<IGH_Goo>();
            Interval S = new Interval(600, 600);
            List < int> Xv = new List<int>();
            List < int> Yv = new List<int>();

            if (!DA.GetDataList(0, E)) return;
            if (!DA.GetData(1, ref S)) return;
            if (!DA.GetDataList(2,  Xv)) return;
            if (!DA.GetDataList(3,  Yv)) return;

            pCtrl.SetProperties((int)S.T0,(int)S.T1);
            pCtrl.ClearChildren();

            int k = Xv.Count;
            for (int i = k;i<E.Count;i++)
            {
                Xv.Add(Xv[Xv.Count - 1]);
            }

            k = Yv.Count;
            for (int i = k; i < E.Count; i++)
            {
                Yv.Add(Yv[Yv.Count - 1]);
            }

            for (int i = 0; i < E.Count; i++)
            {
                wObject W;
                pElement Elem;
                E[i].CastTo(out W);
                Elem = (pElement)W.Element;

                pCtrl.AddElement(Elem, Xv[i], Yv[i]);
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
                return Properties.Resources.Parrot_Panel_Placement1;
            }
        }


        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4f290c39-cb83-47d3-8222-7c67479643be"); }
        }
    }
}