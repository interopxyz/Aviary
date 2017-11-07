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
    public class PanelGrid : GH_Component
    {
        //Stores the instance of each run of the control
        public Dictionary<int, wObject> Elements = new Dictionary<int, wObject>();

        /// <summary>
        /// Initializes a new instance of the GridPanel class.
        /// </summary>
        public PanelGrid()
          : base("GridPanel", "Grid", "---", "Aviary", "Dashboard Layout")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Elements", "E", "Elements", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Column Index", "I", "Indices", GH_ParamAccess.list,0);
            pManager.AddIntegerParameter("Row Index", "J", "Indices", GH_ParamAccess.list,0);
            pManager.AddIntegerParameter("Columns", "C", "Columns", GH_ParamAccess.item, 2);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Rows", "R", "Rows", GH_ParamAccess.item, 2);
            pManager[4].Optional = true;
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

            var pCtrl = new pPanelGrid(name);

            //Check if control already exists
            if (Active)
            {
                WindObject = Elements[C];
                Element = (pElement)WindObject.Element;
                pCtrl = (pPanelGrid)Element.ParrotControl;
            }
            else
            {
                Elements.Add(C, WindObject);
            }
            
            List<IGH_Goo> X = new List<IGH_Goo>();
            List<int> I = new List<int>();
            List<int> J = new List<int>();
            int D = 0;
            int R = 0;

            // Access the input parameters 
            if (!DA.GetDataList(0, X)) return;
            if (!DA.GetDataList(1, I)) return;
            if (!DA.GetDataList(2, J)) return;
            if (!DA.GetData(3, ref D)) return;
            if (!DA.GetData(4, ref R)) return;

            pCtrl.SetProperties();
            pCtrl.SetColumns(D);
            pCtrl.SetRows(R);

            int k = I.Count;
            for (int i = k; i < X.Count; i++)
            {
                I.Add(I[I.Count - 1]);
            }

            k = J.Count;
            for (int i = k; i < X.Count; i++)
            {
                J.Add(J[J.Count - 1]);
            }
            
            for (int i = 0; i < X.Count; i++)
            {
                wObject W;
                pElement Elem;
                X[i].CastTo(out W);
                Elem = (pElement)W.Element;
                pCtrl.AddElement(Elem, I[i], J[i]);
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
                return Properties.Resources.Parrot_Grid_W;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3513e141-0c53-4b35-870e-1851deb19695}"); }
        }
    }
}